using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheManOnTheMoon2.Controllers
{
    public class ProductsController : Controller
    {
        // GET: SingleProduct
        public ActionResult AllProducts()
        {
            return View();
        }

        // GET: SingleProduct/Details/5
        public ActionResult SingleProductDetail(int id)
        {
            return View();
        }

        // GET: SingleProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SingleProduct/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SingleProduct/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SingleProduct/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SingleProduct/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SingleProduct/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
