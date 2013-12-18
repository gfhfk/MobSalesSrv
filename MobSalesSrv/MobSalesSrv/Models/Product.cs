using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobSalesSrv.Models
{
    public class Product : IHasLastModified
    {
        public int ProductID { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    
        public int ProductTypeID { get; set; }
        public virtual ProductType ProductType { get; set; }

        public DateTime? LastModified { get; set; }
    }
}