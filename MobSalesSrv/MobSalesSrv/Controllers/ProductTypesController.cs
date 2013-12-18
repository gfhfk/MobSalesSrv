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
    builder.EntitySet<ProductType>("ProductTypes");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ProductTypesController : ODataController
    {
        private MobSalesSrvContext db = new MobSalesSrvContext();

        // GET odata/ProductTypes
        [Queryable]
        public IQueryable<ProductType> GetProductTypes()
        {
            return db.ProductTypes;
        }

        // GET odata/ProductTypes(5)
        [Queryable]
        public SingleResult<ProductType> GetProductType([FromODataUri] int key)
        {
            return SingleResult.Create(db.ProductTypes.Where(producttype => producttype.ProductTypeID == key));
        }

        // PUT odata/ProductTypes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, ProductType producttype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != producttype.ProductTypeID)
            {
                return BadRequest();
            }

            db.Entry(producttype).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(producttype);
        }

        // POST odata/ProductTypes
        public async Task<IHttpActionResult> Post(ProductType producttype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductTypes.Add(producttype);
            await db.SaveChangesAsync();

            return Created(producttype);
        }

        // PATCH odata/ProductTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ProductType> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProductType producttype = await db.ProductTypes.FindAsync(key);
            if (producttype == null)
            {
                return NotFound();
            }

            patch.Patch(producttype);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(producttype);
        }

        // DELETE odata/ProductTypes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            ProductType producttype = await db.ProductTypes.FindAsync(key);
            if (producttype == null)
            {
                return NotFound();
            }

            db.ProductTypes.Remove(producttype);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductTypeExists(int key)
        {
            return db.ProductTypes.Count(e => e.ProductTypeID == key) > 0;
        }
    }
}
