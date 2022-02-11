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
using VendingAPI.Models;

namespace VendingAPI.Controllers
{
    public class ResponseDrinksController : ApiController
    {
        private VendingMachinesEntities db = new VendingMachinesEntities();

        // GET: api/ResponseDrinks
        public IQueryable<ResponseDrinks> GetResponseDrinks()
        {
            return db.ResponseDrinks;
        }

        // GET: api/ResponseDrinks/5
        [ResponseType(typeof(ResponseDrinks))]
        public IHttpActionResult GetResponseDrinks(int id)
        {
            List<Models.ResponseDrinks> drinks = new List<Models.ResponseDrinks>();
            try
            {
                drinks = db.Drinks.ToList().ConvertAll(p => new Models.ResponseDrinks(p));
            }
            catch
            {
                return NotFound();
            }
            if (drinks == null)
            {
                return NotFound();
            }

            return Ok(drinks);
        }

        // PUT: api/ResponseDrinks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResponseDrinks(int id, ResponseDrinks responseDrinks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != responseDrinks.id)
            {
                return BadRequest();
            }

            db.Entry(responseDrinks).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponseDrinksExists(id))
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

        // POST: api/ResponseDrinks
        [ResponseType(typeof(ResponseDrinks))]
        public IHttpActionResult PostResponseDrinks(ResponseDrinks responseDrinks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ResponseDrinks.Add(responseDrinks);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = responseDrinks.id }, responseDrinks);
        }

        // DELETE: api/ResponseDrinks/5
        [ResponseType(typeof(ResponseDrinks))]
        public IHttpActionResult DeleteResponseDrinks(int id)
        {
            ResponseDrinks responseDrinks = db.ResponseDrinks.Find(id);
            if (responseDrinks == null)
            {
                return NotFound();
            }

            db.ResponseDrinks.Remove(responseDrinks);
            db.SaveChanges();

            return Ok(responseDrinks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResponseDrinksExists(int id)
        {
            return db.ResponseDrinks.Count(e => e.id == id) > 0;
        }
    }
}