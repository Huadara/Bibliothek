namespace LibaryDb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LibaryDb.LibaryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LibaryDb.LibaryContext context)
        {
            new DatabaseFiller().FillDatabase();
        }
    }
}
