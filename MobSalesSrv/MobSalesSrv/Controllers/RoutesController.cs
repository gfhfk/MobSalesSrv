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
    builder.EntitySet<Route>("Routes");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RoutesController : ODataController
    {
        private MobSalesSrvContext db = new MobSalesSrvContext();

        // GET odata/Routes
        [Queryable]
        public IQueryable<Route> GetRoutes()
        {
            return db.Routes;
        }

        // GET odata/Routes(5)
        [Queryable]
        public SingleResult<Route> GetRoute([FromODataUri] int key)
        {
            return SingleResult.Create(db.Routes.Where(route => route.RouteID == key));
        }

        // PUT odata/Routes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Route route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != route.RouteID)
            {
                return BadRequest();
            }

            db.Entry(route).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(route);
        }

        // POST odata/Routes
        public async Task<IHttpActionResult> Post(Route route)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Routes.Add(route);
            await db.SaveChangesAsync();

            return Created(route);
        }

        // PATCH odata/Routes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Route> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Route route = await db.Routes.FindAsync(key);
            if (route == null)
            {
                return NotFound();
            }

            patch.Patch(route);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(route);
        }

        // DELETE odata/Routes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Route route = await db.Routes.FindAsync(key);
            if (route == null)
            {
                return NotFound();
            }

            db.Routes.Remove(route);
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

        private bool RouteExists(int key)
        {
            return db.Routes.Count(e => e.RouteID == key) > 0;
        }
    }
}
