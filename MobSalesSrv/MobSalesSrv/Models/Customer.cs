using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MobSalesSrv.Models
{
    public class Customer : IHasLastModified
    {
        public int CustomerID { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }
        [StringLength(150)]
        public string Address { get; set; }
        public string Comment { get; set; }
        [ForeignKey("Route")]
        public int RouteID { get; set; }
        virtual public Route Route { get; set; }

        public DateTime? LastModified { get; set; }
    }

}