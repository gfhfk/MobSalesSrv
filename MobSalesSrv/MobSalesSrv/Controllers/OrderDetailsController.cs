using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using MobSalesSrv.Models;

namespace MobSalesSrv.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using MobSalesSrv.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<OrderDetails>("OrderDetails");
    builder.EntitySet<Order>("Orders"); 
    builder.EntitySet<Product>("Products"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OrderDetailsController : ODataController
    {
        private MobSalesSrvContext db = new MobSalesSrvContext();

        // GET odata/OrderDetails
        [Queryable]
        public IQueryable<OrderDetails> GetOrderDetails()
        {
            return db.OrderDetails;
        }

        // GET odata/OrderDetails(5)
        [Queryable]
        public SingleResult<OrderDetails> GetOrderDetails([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrderDetails.Where(orderdetails => orderdetails.OrderDetailsID == key));
        }

        // PUT odata/OrderDetails(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, OrderDetails orderdetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != orderdetails.OrderDetailsID)
            {
                return BadRequest();
            }

            db.Entry(orderdetails).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(orderdetails);
        }

        // POST odata/OrderDetails
        public async Task<IHttpActionResult> Post(OrderDetails orderdetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderDetails.Add(orderdetails);
            await db.SaveChangesAsync();

            return Created(orderdetails);
        }

        // PATCH odata/OrderDetails(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<OrderDetails> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrderDetails orderdetails = await db.OrderDetails.FindAsync(key);
            if (orderdetails == null)
            {
                return NotFound();
            }

            patch.Patch(orderdetails);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(orderdetails);
        }

        // DELETE odata/OrderDetails(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            OrderDetails orderdetails = await db.OrderDetails.FindAsync(key);
            if (orderdetails == null)
            {
                return NotFound();
            }

            db.OrderDetails.Remove(orderdetails);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/OrderDetails(5)/Order
        [Queryable]
        public SingleResult<Order> GetOrder([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrderDetails.Where(m => m.OrderDetailsID == key).Select(m => m.Order));
        }

        // GET odata/OrderDetails(5)/Product
        [Queryable]
        public SingleResult<Product> GetProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrderDetails.Where(m => m.OrderDetailsID == key).Select(m => m.Product));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderDetailsExists(int key)
        {
            return db.OrderDetails.Count(e => e.OrderDetailsID == key) > 0;
        }
    }
}
