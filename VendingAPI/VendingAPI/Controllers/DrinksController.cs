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
    public class DrinksController : ApiController
    {
        private VendingMachinesEntities db = new VendingMachinesEntities();

        // GET: api/Drinks
        public IQueryable<Drinks> GetDrinks()
        {
            return db.Drinks;
        }

        // GET: api/Drinks/5
        [ResponseType(typeof(List<Models.ResponseDrinks>))]
        public IHttpActionResult GetDrinks(int id)
        {
            /*try
            {*/
                var drinks = db.Drinks.ToList().ConvertAll(p => new Models.ResponseDrinks(p));
            /*}
            catch
            {
                return NotFound();
            }*/
                if (drinks == null)
            {
                return NotFound();
            }

            return Ok(drinks);
        }

        // PUT: api/Drinks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDrinks(Drinks drinks, int Count)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.Entry(drinks).State = EntityState.Modified;
            db.Drinks.Where(p => p.Id == drinks.Id).FirstOrDefault().Cost = drinks.Cost;
            db.Drinks.Where(p => p.Id == drinks.Id).FirstOrDefault().Name = drinks.Name;
            db.Drinks.Where(p => p.Id == drinks.Id).FirstOrDefault().Image = drinks.Image;
            db.Drinks.Where(p => p.Id == drinks.Id).FirstOrDefault().VendingMachineDrinks.First().Count = Count;
            db.SaveChanges();
            try
            {
                
            }
            catch (DbUpdateConcurrencyException)
            {
                    return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Drinks
        [ResponseType(typeof(Drinks))]
        public IHttpActionResult PostDrinks(Drinks drinks, int Count)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newDrink = db.Drinks.Add(drinks);
            db.VendingMachineDrinks.Add(new VendingMachineDrinks() { Count = Count, Drinks = newDrink, DrinksId = drinks.Id, VendingMachineId = 1});

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = drinks.Id }, drinks);
        }

        // DELETE: api/Drinks/5
        [ResponseType(typeof(Drinks))]
        public IHttpActionResult DeleteDrinks(int id)
        {
            Drinks drinks = db.Drinks.Find(id);
            if (drinks == null)
            {
                return NotFound();
            }
            db.VendingMachineDrinks.RemoveRange(db.VendingMachineDrinks.Where(p => p.DrinksId == id));
            db.Drinks.Remove(drinks);
            db.SaveChanges();

            return Ok(drinks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DrinksExists(int id)
        {
            return db.Drinks.Count(e => e.Id == id) > 0;
        }
    }
}