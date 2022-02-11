using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingAPI.Models
{
    public class ResponseCoins
    {
        public ResponseCoins(DataBase.VendingMachineCoins vendingMachineCoins)
        {
            if (vendingMachineCoins!= null)
            {
                VendingMachineId = vendingMachineCoins.Id;
                Denomination = vendingMachineCoins.Coins.Denomination;
                Count = vendingMachineCoins.Count;
                IsActive = vendingMachineCoins.IsActive;
            }

        }
        public int Id { get; set; }
        public int VendingMachineId { get; set; }
        public int Denomination { get; set; }
        public int Count { get; set; }
        public int IsActive { get; set; }
    }
}