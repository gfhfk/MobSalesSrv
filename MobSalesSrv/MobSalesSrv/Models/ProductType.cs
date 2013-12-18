using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobSalesSrv.Models
{
    public class ProductType : IHasLastModified
    {
        public int ProductTypeID { get; set; }
        [Required]
        [StringLength(30)]
        public string ProductTypeName { get; set; }
        public DateTime? LastModified { get; set; }

    }
}