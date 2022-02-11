using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using VendingAPI.DataBase;

namespace VendingAPI.Controllers
{
    public class VendingMachinesController : ApiController
    {
        private VendingMachinesEntities db = new VendingMachinesEntities();

        // GET: api/VendingMachines
        public IQueryable<VendingMachines> GetVendingMachines()
        {
            return db.VendingMachines;
        }

        // GET: api/VendingMachines/5
        [ResponseType(typeof(VendingMachines))]
        public IHttpActionResult GetVendingMachines(int id)
        {
            VendingMachines vendingMachines = db.VendingMachines.First(p => p.Id == id);
            if (vendingMachines == null)
            {
                return NotFound();
            }

            return Ok(vendingMachines);
        }

        // PUT: api/VendingMachines/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendingMachines(int id, VendingMachines vendingMachines)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendingMachines.Id)
            {
                return BadRequest();
            }

            db.Entry(vendingMachines).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendingMachinesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/VendingMachines
        [ResponseType(typeof(VendingMachines))]
        public IHttpActionResult PostVendingMachines(VendingMachines vendingMachines)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VendingMachines.Add(vendingMachines);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendingMachines.Id }, vendingMachines);
        }

        // DELETE: api/VendingMachines/5
        [ResponseType(typeof(VendingMachines))]
        public IHttpActionResult DeleteVendingMachines(int id)
        {
            VendingMachines vendingMachines = db.VendingMachines.Find(id);
            if (vendingMachines == null)
            {
                return NotFound();
            }

            db.VendingMachines.Remove(vendingMachines);
            db.SaveChanges();

            return Ok(vendingMachines);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendingMachinesExists(int id)
        {
            return db.VendingMachines.Count(e => e.Id == id) > 0;
        }
    }
}