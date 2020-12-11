using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheManOnTheMoon2.Models;
using TheManOnTheMoon2.Database;

namespace TheManOnTheMoon2.Controllers
{
    public class ProductsController : Controller
    {
        #region NestedClasses
        public class ViewModelProduct
        {
            public Product PrimaryProduct { get; set; }
            public IEnumerable<Product> RelatedProducts { get; set; }

        }
        #endregion

        private DbQueryProducts dbqueryProducts = new DbQueryProducts();
       
        // GET: SingleProduct
        public ActionResult AllProducts()
        {
            return View();
        }

        public ActionResult SingleProductDetail(int id)
        {
            ViewModelMultipleModels returnData = new ViewModelMultipleModels();

            var queryResultData = dbqueryProducts.GetProductandReleatedProducts(id);

            if(queryResultData.Item1==null)
            {
                returnData.PrimaryProduct = null;
            }
            else
            {
                returnData.PrimaryProduct = queryResultData.Item1;
            }
            if(queryResultData.Item2==null)
            {
                returnData.RelatedProducts = null;
            }
            else
            {
                returnData.RelatedProducts = queryResultData.Item2;
            }
            return View(returnData);
        }
        
        /*x* public ActionResult SingleProductDetail(int id)
        //{
        //    dynamic resultModel = new ExpandoObject();

        //    var resultData = dbqueryProducts.GetProductandReleatedProducts(id);

        //    if (resultData.Item1 == null)
        //    {
        //        resultModel.Product = null;
        //    }
        //    else
        //    {
        //        resultModel.Product = resultData.Item1;
        //    }

        //    if(resultData.Item2==null)
        //    {
        //        resultModel.RelatedProducts = null;
        //    }
        //    else
        //    {
        //        resultModel.RelatedProducts = resultData.Item2;

        //    }


        //    return View(resultModel);
        //}
        */

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
