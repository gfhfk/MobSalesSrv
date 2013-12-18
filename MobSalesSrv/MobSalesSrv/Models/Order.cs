using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MobSalesSrv.Models
{
    public class Order : IHasLastModified
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int CustomerID { get; set; }
        virtual public Customer Customer { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal TotalSum { get; set; }
        [StringLength(150)]
        public string Comment { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }

        public DateTime? LastModified { get; set; }
    }
}