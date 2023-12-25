using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using productApi.Models;
using System.Net.Http;

namespace productApi.Controllers
{
    public class CRUDController : Controller
    {
        HttpClient hc = new HttpClient();
        // GET: CRUD
        public ActionResult Index()
        {
            IEnumerable<product> productObj = null;

            hc.BaseAddress = new Uri("https://localhost:44313/api/productCRUD");

            var consumeApi = hc.GetAsync("productCRUD");
            consumeApi.Wait();

            var readData = consumeApi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<IList<product>>();
                displayData.Wait();

                productObj = displayData.Result;
            }
            return View(productObj);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(product insertProduct)
        {

            hc.BaseAddress = new Uri("https://localhost:44313/api/productCRUD");
            var insertRecord = hc.PostAsJsonAsync<product>("productCRUD", insertProduct);
            insertRecord.Wait();
            var saveData = insertRecord.Result;
            if (saveData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");

        }

        public ActionResult Details(int id)
        {
            ProductClass productObj = null;
            hc.BaseAddress = new Uri("https://localhost:44313/api/productCRUD");
            var consumeApi = hc.GetAsync("productCRUD?id=" + id.ToString());
            consumeApi.Wait();
            var readData = consumeApi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<ProductClass>();
                displayData.Wait();

                productObj = displayData.Result;
            }
            return View(productObj);
        }

        public ActionResult Edit(int id)
        {
            ProductClass productObj = null;
            hc.BaseAddress = new Uri("https://localhost:44313/api/productCRUD");
            var consumeApi = hc.GetAsync("productCRUD?id=" + id.ToString());
            consumeApi.Wait();

            var readData = consumeApi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<ProductClass>();
                displayData.Wait();

                productObj = displayData.Result;
            }
            return View(productObj);
        }
        [HttpPost]
        public ActionResult Edit(ProductClass pc)
        {
            hc.BaseAddress = new Uri("https://localhost:44313/api/productCRUD");
            var insertRecord = hc.PutAsJsonAsync<ProductClass>("productCRUD", pc);
            insertRecord.Wait();
            var saveData = insertRecord.Result;
            if (saveData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Not Update";
            }
            return View(pc);

        }

        public ActionResult Delete(int id) 
        {
            hc.BaseAddress = new Uri("https://localhost:44313/api/productCRUD");
            var delRecord = hc.DeleteAsync("productCRUD/" + id.ToString());
            delRecord.Wait();

            var displayData=delRecord.Result;
            if (displayData.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");   
        }
    }
}