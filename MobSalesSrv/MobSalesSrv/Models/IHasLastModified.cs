using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobSalesSrv.Models
{
    public interface IHasLastModified
    {
        DateTime? LastModified { get; set; }
    }
}