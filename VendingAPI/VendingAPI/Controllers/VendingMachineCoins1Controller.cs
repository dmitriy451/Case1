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
    public class VendingMachineCoins1Controller : ApiController
    {
        private VendingMachinesEntities db = new VendingMachinesEntities();

        // GET: api/VendingMachineCoins1
        public IQueryable<VendingMachineCoins> GetVendingMachineCoins()
        {
            return db.VendingMachineCoins;
        }

        // GET: api/VendingMachineCoins1/5
        [ResponseType(typeof(List<Models.ResponseCoins>))]
        public IHttpActionResult GetVendingMachineCoins(int id)
        {
            var responseCoins = db.VendingMachineCoins.ToList().ConvertAll(p => new Models.ResponseCoins(p));
            if (responseCoins == null)
            {
                return NotFound();
            }

            return Ok(responseCoins);
        }

        // PUT: api/VendingMachineCoins1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendingMachineCoins(List<Models.ResponseCoins> responseCoins)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.VendingMachineCoins.First(p => p.Coins.Denomination == 1).Count = responseCoins.First(p => p.Denomination == 1).Count;
            db.VendingMachineCoins.First(p => p.Coins.Denomination == 2).Count = responseCoins.First(p => p.Denomination == 2).Count;
            db.VendingMachineCoins.First(p => p.Coins.Denomination == 5).Count = responseCoins.First(p => p.Denomination == 5).Count;
            db.VendingMachineCoins.First(p => p.Coins.Denomination == 10).Count = responseCoins.First(p => p.Denomination == 10).Count;

            db.VendingMachineCoins.First(p => p.Coins.Denomination == 1).IsActive = responseCoins.First(p => p.Denomination == 1).IsActive;
            db.VendingMachineCoins.First(p => p.Coins.Denomination == 2).IsActive = responseCoins.First(p => p.Denomination == 2).IsActive;
            db.VendingMachineCoins.First(p => p.Coins.Denomination == 5).IsActive = responseCoins.First(p => p.Denomination == 5).IsActive;
            db.VendingMachineCoins.First(p => p.Coins.Denomination == 10).IsActive = responseCoins.First(p => p.Denomination == 10).IsActive;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                    return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("api/addMonetka")]
        // PUT: api/VendingMachineCoins1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendingMachineCoins(int denomination, Coins coin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.VendingMachineCoins.First(p => p.Coins.Denomination == denomination).Count += 1;
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/VendingMachineCoins1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendingMachineCoins(int Change)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int tens_out = Change / 10;
            if (db.VendingMachineCoins.First(p => p.Coins.Denomination == 10).Count < tens_out)
            {
                Change -= db.VendingMachineCoins.First(p => p.Coins.Denomination == 10).Count * 10;
                tens_out = db.VendingMachineCoins.First(p => p.Coins.Denomination == 10).Count;
            }
            else
            {
                Change -= tens_out * 10;
            }

            int fives_out = Change / 5;
            if (db.VendingMachineCoins.First(p => p.Coins.Denomination == 5).Count < fives_out)
            {
                Change -= db.VendingMachineCoins.First(p => p.Coins.Denomination == 5).Count * 5;
                fives_out = db.VendingMachineCoins.First(p => p.Coins.Denomination == 5).Count;
            }
            else
            {
                Change -= fives_out * 5;
            }


            int twos_out = Change / 2;
            if (db.VendingMachineCoins.First(p => p.Coins.Denomination == 2).Count < twos_out)
            {
                Change -= db.VendingMachineCoins.First(p => p.Coins.Denomination == 2).Count * 2;
                twos_out = db.VendingMachineCoins.First(p => p.Coins.Denomination == 2).Count;
            }
            else
            {
                Change -= twos_out * 2;
            }

            int ones_out = Change / 1;
            if (db.VendingMachineCoins.First(p => p.Coins.Denomination == 1).Count < ones_out)
            {
                Change -= db.VendingMachineCoins.First(p => p.Coins.Denomination == 1).Count * 1;
                ones_out = db.VendingMachineCoins.First(p => p.Coins.Denomination == 1).Count;
            }
            else
            {
                Change -= ones_out * 1;
            }

            db.VendingMachineCoins.First(p => p.Coins.Denomination == 1).Count -= ones_out;
            db.VendingMachineCoins.First(p => p.Coins.Denomination == 2).Count -= twos_out;
            db.VendingMachineCoins.First(p => p.Coins.Denomination == 5).Count -= fives_out;
            db.VendingMachineCoins.First(p => p.Coins.Denomination == 10).Count -= tens_out;

            try
                {
                    db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST: api/VendingMachineCoins1
        [ResponseType(typeof(VendingMachineCoins))]
        public IHttpActionResult PostVendingMachineCoins(VendingMachineCoins vendingMachineCoins)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VendingMachineCoins.Add(vendingMachineCoins);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendingMachineCoins.Id }, vendingMachineCoins);
        }

        // DELETE: api/VendingMachineCoins1/5
        [ResponseType(typeof(VendingMachineCoins))]
        public IHttpActionResult DeleteVendingMachineCoins(int id)
        {
            VendingMachineCoins vendingMachineCoins = db.VendingMachineCoins.Find(id);
            if (vendingMachineCoins == null)
            {
                return NotFound();
            }

            db.VendingMachineCoins.Remove(vendingMachineCoins);
            db.SaveChanges();

            return Ok(vendingMachineCoins);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendingMachineCoinsExists(int id)
        {
            return db.VendingMachineCoins.Count(e => e.Id == id) > 0;
        }
    }
}