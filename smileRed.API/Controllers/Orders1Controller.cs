using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using smileRed.Domain;

namespace smileRed.API.Controllers
{
    [RoutePrefix("api/Orders1")]
    public class Orders1Controller : ApiController
    {
        private DataContext db = new DataContext();
        [Authorize]

        // GET: api/Orders1
        public IQueryable<Order> GetOrders()
        {
            return db.Orders;
        }

        // GET: api/Orders1/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            //Order order = await db.Orders.FindAsync(id

            var order = await db.Orders.
                 Where(u => u.UserId == id && u.Delete == false).
                 OrderByDescending(u => u.OrderID).
                  FirstOrDefaultAsync();
            //Select(u=>u.OrderID).


            if (order ==null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet]
        [Route("GetOrderStatusID/{id}")]
        public async Task<IHttpActionResult> GetOrderStatusID(int id)
        {
            //Order order = await db.Orders.FindAsync(id

            var order = await db.Orders.
                 Where(u => u.UserId == id && u.Delete == true).
                 OrderByDescending(u => u.OrderID).
                // Select(u => u.OrderStatusID).
                 FirstOrDefaultAsync();
           
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }


        // GET: api/Orders1/GetOrderID/5
        [HttpGet]
        [Route("GetOrderID/{id}")]
        public async Task<IHttpActionResult> GetOrderID(int id)
        {       
             var order = await db.Orders.
                  Where(u => u.UserId == id && u.Delete == false).
                 ToListAsync(); 

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // GET: api/Orders1/GetCountOrderID/5
        [HttpGet]
        [Route("GetCountOrderID/{id}")]
        public async Task<IHttpActionResult> GetCountOrderID(int id)
        {
            var order = await db.Orders.
                 Where(u => u.UserId == id && u.Delete == false).
                CountAsync();

            if (order == 0)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderID)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("GetOrderByEmail")]
        public async Task<IHttpActionResult> GetOrderByEmail(JObject form)
        {
            var email = string.Empty;
            //DateTime? date = null;

            dynamic jsonObject = form;
            try
            {
                email = jsonObject.Email.Value;
                //date = jsonObject.DateOrder.value;
            }
            catch
            {
                return BadRequest("Missing parameter.");
            }

           /* var user = await db.Orders.
                 Where(u => u.Email.ToLower() == email.ToLower()).
                 FirstOrDefaultAsync();*/

            var user = await db.Orders.
                Where(u => u.Email.ToLower() == email.ToLower())
               .OrderByDescending(u => u.OrderID)
               .FirstOrDefaultAsync();


            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/Orders1
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = order.OrderID }, order);
        }

        // DELETE: api/Orders1/5
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> DeleteOrder(int id)
        {
            //Order order = await db.Orders.FindAsync(id);
            var order = await db.Orders.
              Where(u => u.OrderID == id).
              SingleOrDefaultAsync();


            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            await db.SaveChangesAsync();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.OrderID == id) > 0;
        }
    }
}