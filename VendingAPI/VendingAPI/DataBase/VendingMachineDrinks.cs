//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VendingAPI.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class VendingMachineDrinks
    {
        public int Id { get; set; }
        public int VendingMachineId { get; set; }
        public int DrinksId { get; set; }
        public int Count { get; set; }
    
        public virtual VendingMachines VendingMachines { get; set; }
        public virtual Drinks Drinks { get; set; }
    }
}
