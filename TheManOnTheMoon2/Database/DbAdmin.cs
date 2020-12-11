using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheManOnTheMoon2.Models;

namespace TheManOnTheMoon2.Database
{
    public class DbAdmin : DbQueryProducts
    {

        #region Variables
        private DataBaseModelsDataContext db = new DataBaseModelsDataContext();

        #endregion

        #region NestedClasses
        public class StringFormat
        {
            #region Constructor
            private StringFormat(string value) { Value = value; }

            #endregion
            #region Variables
            public string Value { get; set; }
            #endregion
            #region Properties
            public static StringFormat ForDatabase { get { return new StringFormat("ForDatabase"); } }
            public static StringFormat ForPresentation { get { return new StringFormat("ForPresentation"); } }

            #endregion
        }
        public class TableType
        {
            #region Constructor
            private TableType(string value) { Value = value; }
            #endregion
            #region Variables
            public string Value { get; set; }
            #endregion

            #region Properties
            public static TableType Brand { get { return new TableType("Brand"); } }
            public static TableType Category { get { return new TableType("Category"); } }
            public static TableType Product { get { return new TableType("Product"); } }

            #endregion
        }
        #endregion

        #region Methods
        //public new void Errorhead(Exception e)
        //{
        //    Console.WriteLine("----------Error----------: ");
        //    Console.WriteLine("Error Message: " + e.Message);
        //    Console.WriteLine("ErrorType: " + e.GetType().ToString());
        //    Console.WriteLine("Exception Instance: " + e.InnerException.ToString());
        //    Console.WriteLine("Error Method: " + e.TargetSite);//Gets Erroneius Method
        //    Console.WriteLine("Error Object/Application: " + e.Source.ToString());
        //    Console.WriteLine("Error StackTrace: " + e.StackTrace);
        //}

        #endregion

        #region Create
        public Product CreateProduct(Product product)
        {
            Product returnProduct = null;
            if (product == null)
            {
                return returnProduct;
            }
            try
            {
                db.Products.InsertOnSubmit(product);
                db.SubmitChanges();
                returnProduct = product;

            }
            catch (Exception e)
            {
                Errorhead(e);


            }
            return returnProduct;
        }
        public Category CreateCategory(Category category)
        {
            Category responseCategory = null;
            if (category == null)
            {
                return responseCategory;
            }
            try
            {
                db.Categories.InsertOnSubmit(category);
                db.SubmitChanges();
                responseCategory = category;
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return responseCategory;
        }
        public Brand CreateBrand(Brand brand)
        {
            Brand responseBrand = null;

            if (brand == null)
            {
                return responseBrand;
            }
            //if (ExistByName(brand.Name, DbAdmin.TableType.Brand))
            //{
            //    return responseBrand;
            //}
            try
            {
                //brand.Name = FormatString(brand.Name, DbAdmin.StringFormat.ForDatabase);
                
                db.Brands.InsertOnSubmit(brand);
                db.SubmitChanges();
                responseBrand = brand;
            }
            catch (Exception e)
            {
                Errorhead(e);

            }

            return responseBrand;
        }
        public Product_Image CreateProductImageRecord(Product_Image product_Images)
        {
            Product_Image product_Image2Response = null;
            if (product_Images == null)
            {
                return product_Image2Response;
            }
            try
            {
                db.Product_Images.InsertOnSubmit(product_Images);
                db.SubmitChanges();
                product_Image2Response = product_Images;
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return product_Image2Response;
        }

        #endregion

        #region Update
        public bool UpdateProduct(Product product)
        {
            bool status = false;

            if (product == null)
            {
                return status;
            }
            try
            {
                Product currentProduct = db.Products.Where(p => p.Id == product.Id).FirstOrDefault();
                currentProduct.Name = product.Name;
                currentProduct.Description = product.Description;
                currentProduct.Upc = product.Upc;
                currentProduct.Brand = product.Brand;
                currentProduct.Length = product.Length;
                currentProduct.Weight = product.Weight;
                currentProduct.Height = product.Height;
                currentProduct.Cost = product.Cost;
                currentProduct.Retail_Price = product.Retail_Price;
                currentProduct.Sale_Price = product.Sale_Price;
                currentProduct.Stock_Quantity = product.Stock_Quantity;
                currentProduct.Category = product.Category;
                currentProduct.Sub_Category = product.Sub_Category;
                currentProduct.On_Sale_Status = product.On_Sale_Status;
                currentProduct.Width = product.Width;
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return status;
        }
        public bool UpdateCategory(Category category)
        {
            bool status = false;
            if (category == null)
            { return status; }
            try
            {

                var oldCategory = db.Categories.Where(c => c.Id == category.Id).FirstOrDefault();
                oldCategory.Name = category.Name;
                db.SubmitChanges();
                status = true;

            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return status;
        }
        public bool UpdateBrand(Brand brand)
        {
            bool status = false;
            if (brand == null)

            { return status; }
            try
            {
                Brand oldBrand = db.Brands.Where(b => b.Id == brand.Id).FirstOrDefault();
                oldBrand.Name = brand.Name;

                db.SubmitChanges();
                status = true;
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return status;
        }
        public bool UpdateProductImages(Product_Image product_Images)
        {
            bool status = false;
            try
            {
                Product_Image currentImageRecord = db.Product_Images.Where(p => p.Id == product_Images.Id).FirstOrDefault();
                currentImageRecord.Product_Id = product_Images.Product_Id;
                currentImageRecord.Product_Image_1 = product_Images.Product_Image_1;
                currentImageRecord.Product_Image_2 = product_Images.Product_Image_2;
                currentImageRecord.Product_Image_3 = product_Images.Product_Image_3;
                db.SubmitChanges();
                status = true;
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return status;
        }

        #endregion

        #region Retrieve
        public Product GetproductById(int id)
        {
            Product product = null;

            if (String.IsNullOrEmpty(id.ToString()))
            { return product; }
            try
            {
                var query = db.Products.ToList();
                product = query.Where(p => p.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return product;
        }
        #endregion

        #region Delete
        public bool DeleteProduct(List<Product> products)
        {
            bool status = false;

            if (products == null||products.Count==0)
                {
                    
                    return status;
                }   
                try
                {
                foreach(Product product in products)
                {
                    Product oldProduct = db.Products.Where(p => p.Id == product.Id).FirstOrDefault();

                    if(oldProduct==null)
                    {
                        return status;
                    }
                    else
                    {
                        db.Products.DeleteOnSubmit(oldProduct);
                        db.SubmitChanges();
                    }

                }    
                status = true;
                }
                catch(Exception e)
                {
                Errorhead(e);
                }
            return status;
            }
            public bool DeleteBrand(List<Brand> brands)
            {
            bool status = false;
            if(brands==null||brands.Count==0)
            { return status; }
            try
            {
                foreach(Brand brand in brands)
                {
                Brand oldBrand = db.Brands.Where(b => b.Id==brand.Id).FirstOrDefault();
                    if(oldBrand==null)
                    {
                        return status;
                    }
                    else
                    {
                    db.Brands.DeleteOnSubmit(oldBrand);
                        db.SubmitChanges();

                    }
                }
                status = true;


            }
            catch(Exception e)
            {
                Errorhead(e);
            }
            return status;
            }
            public bool DeleteCategory(List<Category> categories)
            {
            bool status = false;
            if(categories == null|| categories.Count==0)
            {
                return status;
            }
            try
            {
                foreach(Category category in categories)
                {
                    Category oldCategory= db.Categories.Where(c => c.Id == category.Id).FirstOrDefault();
                    if(oldCategory==null)
                    {
                        return status;
                    }
                    else
                    {
                        db.Categories.DeleteOnSubmit(oldCategory);
                        db.SubmitChanges();
                    }

                }
                status = true;
            }
            catch(Exception e)
            {
                Errorhead(e);
            }
            return status;
            }
            public bool DeleteAllProductImages(Product_Image product_Images)
            {
            bool status = false;
            try
            {
                db.Product_Images.DeleteOnSubmit(product_Images);
                db.SubmitChanges();
                status = true;
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return status;
            }
        #endregion

        #region Search

        // public bool ExistByName(string itemName,DbAdmin.TableType tableType)
        //{
        //    bool statusExist = false;
        //    string formattedSearchItem = string.Empty;


        //    if (string.IsNullOrEmpty(itemName)||tableType==null)
        //    {
        //        return statusExist;
        //    }
        //    else
        //    {
        //        formattedSearchItem = FormatString(itemName, DbAdmin.StringFormat.ForDatabase);
        //    }

        //    switch(tableType.Value)
        //    {
        //        case "Brand":var queryBrand = db.Brands.Where(b => b.Name== formattedSearchItem).FirstOrDefault();
        //            if (queryBrand != null) { statusExist = true; };
        //            break;
        //        case "Category":
        //            var queryCategory = db.Categories.Where(c => c.Name == formattedSearchItem).FirstOrDefault();
        //            if (queryCategory!= null) { statusExist = true; };

        //            break;
        //        case "Product":
        //            var queryProducts = db.Products.Where(p => p.Name == formattedSearchItem).FirstOrDefault();
        //            if (queryProducts!= null) { statusExist = true; };

        //            break;
        //    }
        //    return statusExist;
        //}

        public bool ExistByName(string itemName, DbAdmin.TableType tableType)
        {
            bool statusExist = false;
            string formattedSearchItem = string.Empty;


            if (string.IsNullOrEmpty(itemName) || tableType == null)
            {
                return statusExist;
            }
            else
            {
                formattedSearchItem = FormatString(itemName, DbAdmin.StringFormat.ForDatabase);
            }

            switch (tableType.Value)
            {
                case "Brand":
                    var queryBrand = db.Brands.Where(b => b.Name == formattedSearchItem).FirstOrDefault();
                    if (queryBrand != null) { statusExist = true; };
                    break;
                case "Category":
                    var queryCategory = db.Categories.Where(c => c.Name == formattedSearchItem).FirstOrDefault();
                    if (queryCategory != null) { statusExist = true; };

                    break;
                case "Product":
                    var queryProducts = db.Products.Where(p => p.Name == formattedSearchItem).FirstOrDefault();
                    if (queryProducts != null) { statusExist = true; };

                    break;
            }
            return statusExist;
        }
        #endregion

        #region Utilities
        public string FormatString(string item, DbAdmin.StringFormat mode)
        {
            string returnString = string.Empty;

            char spliter=' ';
            char formater = ' ';

            if (string.IsNullOrEmpty(item))
            {
                return returnString;
            }

            switch (mode.Value)
            {
                case "ForDatabase":
                    spliter = ' ';
                    formater = '_';
                break;
                case "ForPresentation":
                    spliter = '_';
                    formater = ' ';
                    break;
            }

            

            string[] stringItems = item.Split(spliter);

            foreach(string word in stringItems)
            {
                returnString += char.ToUpper(word[0]) + word.Substring(1)+formater;
            }

            return returnString;

        }
        #endregion

        //upWork.com
    }
}