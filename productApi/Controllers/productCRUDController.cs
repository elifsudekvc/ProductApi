using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using productApi.Models;

namespace productApi.Controllers
{
    public class productCRUDController : ApiController
    {

        productApiEntities2 db = new productApiEntities2();
        // GET api/values
        public IHttpActionResult getProduct()
        {
            var result = db.product.ToList();
            return Ok(result);
        }
        [HttpPost]
        public IHttpActionResult insertProduct(product insertProduct)
        {
            db.product.Add(insertProduct);
            db.SaveChanges();
            return Ok();
        }
        public IHttpActionResult GetProductId(int id)
        {
            ProductClass productDetails = null;
            productDetails = db.product.Where(x => x.productId == id).Select(x => new ProductClass()
            {
                productId= x.productId,
                productName= x.productName,
                productDescription= x.productDescription,
                productPrice= x.productPrice,
            }).FirstOrDefault<ProductClass>();
            if(productDetails == null)
            {
                return NotFound();
            }
           
            return Ok(productDetails);
        }

        public IHttpActionResult Put(ProductClass pc)
        {

            var updateProduct = db.product.Where(x => x.productId == pc.productId ).FirstOrDefault<product>();
            if(updateProduct != null)
            {
                updateProduct.productId = pc.productId;
                updateProduct.productName = pc.productName;
                updateProduct.productDescription = pc.productDescription;
                updateProduct.productPrice = pc.productPrice;
                db.SaveChanges();   
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            var productDelete = db.product.Where(x => x.productId == id).FirstOrDefault();
            db.Entry(productDelete).State=System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();   
            return Ok();
        }

    }   
}
