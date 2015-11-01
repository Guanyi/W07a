using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using W07a.Models;

namespace W07a.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using W07a.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Person>("Person");
    config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PersonController : ODataController
    {
        private FlintstonesEntities db = new FlintstonesEntities();

        // GET: odata/Person
        [EnableQuery]
        //[Authorize]
        public IQueryable<Person> GetPerson()
        {
            return db.People;
        }

        // GET: odata/Person(5)
        [EnableQuery]
        //[Authorize]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(db.People.Where(person => person.PersonId == key));
        }

        // PUT: odata/Person(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Person> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person person = db.People.Find(key);
            if (person == null)
            {
                return NotFound();
            }

            patch.Put(person);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(person);
        }

        // POST: odata/Person
        public IHttpActionResult Post(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(person);
            db.SaveChanges();

            return Created(person);
        }

        // PATCH: odata/Person(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Person> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person person = db.People.Find(key);
            if (person == null)
            {
                return NotFound();
            }

            patch.Patch(person);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(person);
        }

        // DELETE: odata/Person(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Person person = db.People.Find(key);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            db.SaveChanges();

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

        private bool PersonExists(int key)
        {
            return db.People.Count(e => e.PersonId == key) > 0;
        }
    }
}
