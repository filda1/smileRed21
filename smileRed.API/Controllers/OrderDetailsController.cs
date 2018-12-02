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
using System.Web.Http.Description;
using smileRed.API.Models;
using smileRed.Domain;

namespace smileRed.API.Controllers
{
    [RoutePrefix("api/OrderDetails")]
    public class OrderDetailsController : ApiController
    {
        private DataContext db = new DataContext();
        [Authorize]

        // GET: api/OrderDetails
        public IQueryable<OrderDetails> GetOrderDetails()
        {
            return db.OrderDetails;
        }

        // GET: api/OrderDetails/5
        [ResponseType(typeof(OrderDetails))]
        public async Task<IHttpActionResult> GetOrderDetails(int id)
        {
            //OrderDetails orderDetails = await db.OrderDetails.FindAsync(id);

            var orderDetails = await db.OrderDetails.
               Where(u => u.OrderID == id).
               ToListAsync();

            if (orderDetails == null)
            {
                return NotFound();
            }

            return Ok(orderDetails);
        }

        // http://...../api/OrderDetails/GetAllOrderDetails/21
        [HttpGet]
        [Route("GetAllOrderDetails/{id}")]
        public async Task<IHttpActionResult> GetAllOrderDetails(int id)
        {
            var q = (from o in db.Orders
                     join od in db.OrderDetails on o.OrderID equals od.OrderID
                     join u in db.Users on o.Email equals u.Email
                     join p in db.Products on od.ProductID equals p.ProductId
                     where
                     o.OrderID == id
                     //&& od.ActiveOrderDetails == false
                     //&& o.VisibleOrders == false
                     //&& od.VisibleOrderDetails == false
                     //&& o.ActiveOrders == false
                     select new
                     {
                         o.OrderID,
                         od.OrderDetailsID,
                         p.Name,
                         p.Description,
                         p.Image,
                         p.Price,
                         od.Quantity,
                         o
                         .DateOrder,
                         p.Remarks,
                         u.FirstName,
                         u.LastName,
                         p.VAT,
                         u.Address,
                         //os.OrderStatusName,
                         od.Ingredients,
                     });

            var productorders = new List<ProductsOrders>();
            foreach (var t in q)
            {
                productorders.Add(new ProductsOrders()
                {
                    OrderID = t.OrderID,
                    OrderDetailsID = t.OrderDetailsID,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Name = t.Name,
                    Description = t.Description,
                    Image = t.Image,
                    Price = t.Price,
                    Quantity = t.Quantity,
                    DateOrder = t.DateOrder,
                    VAT = t.VAT,
                    Address = t.Address,
                    //OrderStatusName = t.OrderStatusName,
                    Ingredients = t.Ingredients,
                });
            }

            if (productorders == null)
            {
                return NotFound();
            }

            return Ok(productorders);
        }

        // http://...../api/OrderDetails/SumPriceQuantity/21
        [HttpGet]
        [Route("SumPriceQuantity/{id}")]
        public async Task<IHttpActionResult> SumPriceQuantity(int id)
        {
            var q = (from o in db.Orders
                     join od in db.OrderDetails on o.OrderID equals od.OrderID
                     join u in db.Users on o.Email equals u.Email
                     join p in db.Products on od.ProductID equals p.ProductId
                     where o.ActiveOrders == false
                     //&& od.ActiveOrderDetails == false
                     && o.VisibleOrders == false
                     //&& od.VisibleOrderDetails == false
                     && o.OrderID == id
                     select new
                     {
                         o.OrderID,
                         od.OrderDetailsID,
                         p.Name,
                         p.Description,
                         p.Image,
                         p.Price,
                         od.Quantity,
                         o
                         .DateOrder,
                         p.Remarks,
                         u.FirstName,
                         u.LastName,
                         p.VAT,
                         u.Address,
                         //os.OrderStatusName,
                         od.Ingredients,
                     });

            var productorders = new List<ProductsOrders>();
            foreach (var t in q)
            {
                productorders.Add(new ProductsOrders()
                {
                    OrderID = t.OrderID,
                    OrderDetailsID = t.OrderDetailsID,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Name = t.Name,
                    Description = t.Description,
                    Image = t.Image,
                    Price = t.Price,
                    Quantity = t.Quantity,
                    DateOrder = t.DateOrder,
                    VAT = t.VAT,
                    Address = t.Address,
                    //OrderStatusName = t.OrderStatusName,
                    Ingredients = t.Ingredients,
                });
            }

            decimal SUM_PriceQuantity = productorders.Where(p => p.OrderID == id).Sum(p => p.PriceQuantity);

            string cadena = SUM_PriceQuantity.ToString();

        
            var StringPrice = cadena.Replace('.', ',');

            var cadena2= StringPrice + " " + "€";
            TotalRequest total = new TotalRequest
            {
                PriceQuantity = cadena2,
            };

            if (total == null)
            {
                return NotFound();
            }

            return Ok(total);
        }

        // http://...../api/OrderDetails/SumPriceVATQuantity/21
        [HttpGet]
        [Route("SumPriceVATQuantity/{id}")]
        public async Task<IHttpActionResult> SumPriceVATQuantity(int id)
        {
            var q = (from o in db.Orders
                     join od in db.OrderDetails on o.OrderID equals od.OrderID
                     join u in db.Users on o.Email equals u.Email
                     join p in db.Products on od.ProductID equals p.ProductId
                     where o.ActiveOrders == false
                     //&& od.ActiveOrderDetails == false
                     && o.VisibleOrders == false
                     //&& od.VisibleOrderDetails == false
                     && o.OrderID == id
                     select new
                     {
                         o.OrderID,
                         od.OrderDetailsID,
                         p.Name,
                         p.Description,
                         p.Image,
                         p.Price,
                         od.Quantity,
                         o
                         .DateOrder,
                         p.Remarks,
                         u.FirstName,
                         u.LastName,
                         p.VAT,
                         u.Address,
                         //os.OrderStatusName,
                         od.Ingredients,
                     });

            var productorders = new List<ProductsOrders>();
            foreach (var t in q)
            {
                productorders.Add(new ProductsOrders()
                {
                    OrderID = t.OrderID,
                    OrderDetailsID = t.OrderDetailsID,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Name = t.Name,
                    Description = t.Description,
                    Image = t.Image,
                    Price = t.Price,
                    Quantity = t.Quantity,
                    DateOrder = t.DateOrder,
                    VAT = t.VAT,
                    Address = t.Address,
                    //OrderStatusName = t.OrderStatusName,
                    Ingredients = t.Ingredients,
                });
            }

            decimal SUM_PriceVATQuantity = productorders.Where(p => p.OrderID == id).Sum(p => p.PriceVATQuantity);

            string cadena = SUM_PriceVATQuantity.ToString();


            var StringPrice = cadena.Replace('.', ',');

            var cadena2 = StringPrice + " " + "€";
            TotalRequest total = new TotalRequest
            {
                PriceQuantity = cadena2,
            };

            if (total== null)
            {
                return NotFound();
            }

            return Ok(total);
        }

        // PUT: api/OrderDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrderDetails(int id, OrderDetails orderDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderDetails.OrderDetailsID)
            {
                return BadRequest();
            }

            db.Entry(orderDetails).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(orderDetails);
            //return StatusCode(HttpStatusCode.NoContent);
        }
  
        // POST: api/OrderDetails
        [ResponseType(typeof(OrderDetails))]
        public async Task<IHttpActionResult> PostOrderDetails(OrderDetails orderDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderDetails.Add(orderDetails);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = orderDetails.OrderDetailsID }, orderDetails);
        }

        // DELETE: api/OrderDetails/5
        [ResponseType(typeof(OrderDetails))]
        public async Task<IHttpActionResult> DeleteOrderDetails(int id)
        {
            //OrderDetails orderDetails = await db.OrderDetails.FindAsync(id);
            var orderDetails = await db.OrderDetails.
              Where(u => u.OrderID == id).ToListAsync();

            if (orderDetails == null)
            {
                return NotFound();
            }

            db.OrderDetails.RemoveRange(orderDetails);
            await db.SaveChangesAsync();

            return Ok(orderDetails);
        }

        // http://...../api/OrderDetails/DeleteOrderDetailsID/21
        [HttpDelete]
        [Route("DeleteOrderDetailsID/{id}")]
        public async Task<IHttpActionResult> DeleteOrderDetailsID(int id)
        {
            OrderDetails orderDetails = await db.OrderDetails.FindAsync(id);
         
            if (orderDetails == null)
            {
                return NotFound();
            }

            db.OrderDetails.Remove(orderDetails);
            await db.SaveChangesAsync();

            return Ok(orderDetails);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderDetailsExists(int id)
        {
            return db.OrderDetails.Count(e => e.OrderDetailsID == id) > 0;
        }
    }
}