using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.WebSockets;
using TheManOnTheMoon2.Models;

namespace TheManOnTheMoon2.Database
{
    public class DbQueryProducts
    {

        #region Variables

        private DataBaseModelsDataContext db = new DataBaseModelsDataContext();
        #endregion

        #region Constructor
         
        #endregion

        #region Methods
            public void Errorhead(Exception e)
            {
            Console.WriteLine("----------Error----------: ");
            Console.WriteLine("Error Message: " + e.Message);
            Console.WriteLine("ErrorType: " + e.GetType().ToString());
            Console.WriteLine("Exception Instance: " + e.InnerException.ToString());
            Console.WriteLine("Error Method: " + e.TargetSite);//Gets Erroneius Method
            Console.WriteLine("Error Object/Application: " + e.Source.ToString());
            Console.WriteLine("Error StackTrace: " + e.StackTrace);
        }

        //ReTrieve

        #endregion

        //Retrieve (All)
        #region RetrieveAll
        public List<Product> GetAllProducts()
        {
            List<Product> Products = new List<Product>();
            try
            {
                //get all ROWS from PRODUCTS and all ROWS from IMAGES JOIN ON Product_ID
                Products = (from p in db.Products join i in db.Product_Images on p.Id equals i.Product_Id select new Product
                {
                    Id=p.Id,
                    Name=p.Name,
                    Description=p.Description,
                    Upc=p.Upc,
                    Brand=p.Brand,
                    Length=p.Length,
                    Width=p.Width,
                    Weight=p.Weight,
                    Height=p.Height,
                    Cost=p.Cost,
                    Retail_Price=p.Retail_Price,
                    Sale_Price=p.Sale_Price,
                    Stock_Quantity=p.Stock_Quantity,
                    Category=p.Category,
                    Sub_Category=p.Sub_Category,
                    On_Sale_Status=p.On_Sale_Status,

                }) .ToList();
            }
            catch (Exception e)
            {
                this.Errorhead(e);
            }
            return Products;
        }
        public List<Brand> GetAllBrands()
        {
            List<Brand> Brands = new List<Brand>();
            try
            {
                Brands = db.Brands.ToList();
            }
            catch (Exception e)
            {
                this.Errorhead(e);
            }
            return Brands;
        }
        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            try
            {
                categories = db.Categories.ToList();
            }
            catch (Exception e)
            {
                this.Errorhead(e);
            }
            return categories;
        }
        #endregion

        //Retrieve (By prince point)
        #region ByPricePoint 
        public List<Product> RetrieveProductsByPriceOrder(int mode)
        {
            List<Product> products = new List<Product>();

            try
            {
                var queryResult = GetAllProducts();
                switch (mode)
                {

                    case 1:
                        
                        products = queryResult.OrderBy(p => p.Retail_Price).ToList();
                        break;
                    case 2:
                        products = queryResult.OrderByDescending(p => p.Retail_Price).ToList();
                        break;
                }
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return products;
        }
        public List<Product> RetrieveProductsByPriceRange(int mode)
        {
            List<Product> products = new List<Product>();
            var query = GetAllProducts();
            try
            {
                switch (mode)
                {
                    case 1:
                        
                        products = query.Where(p => p.Retail_Price > 0 && p.Retail_Price <= 30).OrderBy(p => p.Retail_Price).ToList();
                        break;
                    case 2:
                        products = query.Where(p => p.Retail_Price > 30 && p.Retail_Price <= 80).OrderBy(p => p.Retail_Price).ToList();
                        ;
                        break;
                    case 3:
                        products = query.Where(p => p.Retail_Price > 130 && p.Retail_Price <= 180).OrderBy(p => p.Retail_Price).ToList();

                        ;
                        break;
                    case 4:
                        products = query.Where(p => p.Retail_Price > 180 && p.Retail_Price <= 230).OrderBy(p => p.Retail_Price).ToList();

                        ;
                        break;
                    case 5:
                        products = query.Where(p => p.Retail_Price > 230).OrderBy(p => p.Retail_Price).ToList();

                        ;
                        break;
                }
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return products;
        }
        #endregion

        //Retrieve (ByCategory)
        #region ByCategory
        public List<Product> RetrieveProductByCategory(string category)
        {
            List<Product> productsByCategory = new List<Product>();
            try
            {
                var queryResult = GetAllProducts();
                productsByCategory = queryResult.Where(p => p.Category == category).OrderBy(p => p.Name).ToList();

            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return productsByCategory;


        }
        #endregion

        #region Unions
        public Tuple<Product,List<Product>> GetProductandReleatedProducts(int primaryProductId)
        {
            Product primaryProduct=null;
            List<Product> relatedProducts=null;

            /*? primaryProduct & relatedProdcuts may stay NULL here. Not sure if my
                reassingments will apply to the primaryProduct and relatedProduct nested in
                the Resultdata.
             */
            var resultData = new Tuple<Product,List<Product>>(primaryProduct,relatedProducts);
            
            primaryProduct = db.Products.Where(p => p.Id == primaryProductId).FirstOrDefault();

            if(primaryProduct==null)
            {
                primaryProduct = null;
                relatedProducts = null;
                return (resultData);
            }
            else
            {

             relatedProducts = db.Products.Where(p => p.Category == primaryProduct.Category && p.Brand == primaryProduct.Brand).OrderBy(p => p.Name).ToList();
                if(relatedProducts==null)
                {
                    relatedProducts = db.Products.OrderBy(p => p.Retail_Price).ToList();
                }
            }


            return resultData;
        }
        #endregion




    }
}