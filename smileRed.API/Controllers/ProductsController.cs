﻿using System;
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
using smileRed.Domain;

namespace smileRed.API.Controllers
{
    //[Authorize]
    public class ProductsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(string id)
        {
            //Product product = await db.Products.FindAsync(id);              
            Group catego = await db.Groups.Where
                           (pc => pc.Description == id).FirstAsync();

            if (catego == null)
            {
                return NotFound();
            }

            var categoId = catego.CategoryId;

            var listPro = await db.Products.Where(pc => pc.CategoryId == categoId).ToListAsync();
            var responses = new List<Product>();

            foreach (var index in listPro)
            {
                responses.Add(new Product
                {
                    ProductId = index.ProductId,
                    CategoryId = index.CategoryId,
                    Name = index.Name,
                    Description = index.Description,
                    Price = index.Price,
                    VAT = index.VAT,
                    Image = index.Image,
                    Stock = index.Stock,
                    IsActive = index.IsActive,
                    Remarks = index.Remarks,
                });
            }

            return Ok(responses);
        }

        // PUT: api/Products/5
        [Authorize(Users = "filintomeireles@gmail.com, meireles596@hotmail.com")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [Authorize(Users = "filintomeireles@gmail.com, meireles596@hotmail.com")]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [Authorize(Users = "filintomeireles@gmail.com, meireles596@hotmail.com")]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductId == id) > 0;
        }
    }
}