using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobSalesSrv.Models
{
    public class Route : IHasLastModified
    {
        public int RouteID { get; set; }
        [Required]
        [StringLength(30)]
        public string RouteName { get; set; }


        public DateTime? LastModified { get; set; }
    }
}