using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryBackend.DTOs;
using LibraryDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend.Controllers
{
    [Route("library/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        // GET: library/suppliers
        [HttpGet]
        public ActionResult<List<SupplierDTO>> Get()
        {
            List<Supplier> dbSuppliers = Context.db.Suppliers.ToList();
            Console.WriteLine($"*** returning {dbSuppliers.Count()} suppliers ***");
            return dbSuppliers.Select(x => DTOConverter.convertSupplierToDTO(x)).ToList();
        }

        // GET: library/suppliers/5
        [HttpGet("{id}")]
        public ActionResult<SupplierDTO> Get(int id)
        {
            return DTOConverter.convertSupplierToDTO(Context.db.Suppliers.Where(x => x.SupplierId == id).First());
        }

        // POST: library/suppliers
        //public string company_name;
        //public string address;
        [HttpPost]
        public ActionResult<SupplierIdDTO> Post([FromBody] SupplierDTO newSupplier)
        {
            Supplier s = new Supplier()
            {
                CompanyName = newSupplier.company_name,
                Address = newSupplier.address
            };
            Context.db.Suppliers.Add(s);
            Context.db.SaveChanges();
            int newId = s.SupplierId;
            Console.WriteLine($"                ++++++++++++++ {newId} ++++++++++++++");
            return new SupplierIdDTO()
            {
                supplier_id = newId,
                message = $"New supplier with ID {newId} has been added."
            };
        }

        // PUT: library/suppliers/5
        [HttpPut("{id}")]
        public ActionResult<SupplierIdDTO> Put(int id, [FromBody] SupplierDTO newSupplier)
        {
            try
            {
                Supplier s = Context.db.Suppliers.Where(x => x.SupplierId == id).First();
                s.CompanyName = newSupplier.company_name;
                s.Address = newSupplier.address;
                Context.db.SaveChanges();
                return new SupplierIdDTO() { supplier_id = s.SupplierId };
            }
            catch (Exception e)
            {
                Console.WriteLine($"#### there was no supplier with id {id}: {e.Message} ####");
                return new SupplierIdDTO() { supplier_id = -1 };
            }
        }

        // DELETE: library/suppliers/5
        [HttpDelete("{id}")]
        public ActionResult<SupplierIdDTO> Delete(int id)
        {
            try
            {
                Supplier s = Context.db.Suppliers.Where(x => x.SupplierId == id).First();
                if (s == null) throw new Exception();
                Context.db.Suppliers.Remove(s);
                Context.db.SaveChanges();
                return new SupplierIdDTO() { supplier_id = s.SupplierId };
            }
            catch (Exception e)
            {
                Console.WriteLine($"#### there was no supplier with id {id}: {e.Message} ####");
                return new SupplierIdDTO() { supplier_id = -1 };
            }
        }
    }
}