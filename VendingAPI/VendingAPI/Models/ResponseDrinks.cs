using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingAPI.Models
{
    public class ResponseDrinks
    {
        public ResponseDrinks(DataBase.Drinks drink)
        {
            id = drink.Id;
            try
            {
                Count = drink.VendingMachineDrinks.First().Count;
            }
            catch
            {

            }
            Name = drink.Name;
            Image = drink.Image;
            Cost = drink.Cost;

        }
        public int id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public decimal Cost { get; set; }
    }
}