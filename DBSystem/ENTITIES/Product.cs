using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DBSystem.ENTITIES
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        //public string QuantityPerUnit { get; set; }

        //private string _QuantityPerUnit;
        //public string QuantityPerUnit
        //{
        //    get
        //    {
        //        return _QuantityPerUnit;
        //    }
        //    set
        //    {
        //        _QuantityPerUnit = string.IsNullOrEmpty(value) ? null : value;
        //    }
        //}
        public decimal? UnitPrice { get; set; }
        //public Int16? UnitsInStock { get; set; }
        //public Int16? UnitsOnOrder { get; set; }
        //public Int16? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        [NotMapped]
        public string ProductandID
        {
            get
            {
                return ProductName + "(" + ProductID + ")";
            }
        }
    }
}
