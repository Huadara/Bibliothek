﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryBackend.Controllers;
using LibraryDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LibraryBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            InitDatabase();
        }

        private void InitDatabase()
        {
            Context.db.Lendings.Where(x => (DateTime.Now - x.StartDate).TotalDays > 3 && DateTime.Compare(x.StartDate, x.ActualReturnDate) == 0)
                .ToList()
                .ForEach(x =>
                {
                    var price = Context.db.Books
                    .Single(y => y.BookId == x.StoredBookBookId)
                    .Price;
                    Context.db.Sales.Add(new Sale
                    {
                        CustomerId = x.CustomerId,
                        BookId = (int)x.StoredBookBookId,
                        StoreId = (int)x.StoredBookStoreId,
                        PaidPrice = price
                    });
                    x.ActualReturnDate = DateTime.Now;
                });

            Context.db.SaveChanges();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowMyOrigin"));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();
            //app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors("AllowMyOrigin");
        }
    }
}
