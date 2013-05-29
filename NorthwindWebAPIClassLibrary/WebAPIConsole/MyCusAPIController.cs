using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Http;
//using Mvc4WebAPIQueryableTestApp1.Models;
using NorthwindWebAPIClassLibrary;

namespace WebAPIConsole
{
    public class MyCusAPIController : ApiController
    {
        private NorthwindEntities db = new NorthwindEntities();

        // GET api/MyCusAPI
        public IQueryable<Customers> GetCustomers()
        {
            //return db.Customers.AsEnumerable();
            return db.Customers.AsQueryable();
        }

        // GET api/MyCusAPI/5
        public Customers GetCustomers(string id)
        {
            Customers customers = db.Customers.Single(c => c.CustomerID == id);
            if (customers == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return customers;
        }

        // PUT api/MyCusAPI/5
        public HttpResponseMessage PutCustomers(string id, Customers customers)
        {
            if (ModelState.IsValid && id == customers.CustomerID)
            {
                db.Customers.Attach(customers);
                db.ObjectStateManager.ChangeObjectState(customers, EntityState.Modified);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, customers);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/MyCusAPI
        public HttpResponseMessage PostCustomers(Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Customers.AddObject(customers);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, customers);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = customers.CustomerID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/MyCusAPI/5
        //[WebGet(UriTemplate = "api/MyCusAPI")]
        public HttpResponseMessage DeleteCustomers(string id)
        {
            Customers customers = db.Customers.Single(c => c.CustomerID == id);
            if (customers == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Customers.DeleteObject(customers);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, customers);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}