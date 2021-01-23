using TheManOnTheMoon2.Database;
using TheManOnTheMoon2.Models;
using TheManOnTheMoon2.IO;
using System;
using System.Diagnostics;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Collections;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.IO;


namespace TheManOnTheMoon2.Api
{
    public class AdminController : ApiController
    {
        #region Variables
        DbAdmin db = new DbAdmin();
        FileOps fops = new FileOps();

       private readonly string root = HttpContext.Current.Server.MapPath("~/App_Data");

        private class FormItem
        {
            public FormItem() { }
            public string name { get; set; }
            public byte[] data { get; set; }
            public string fileName { get; set; }
            public string mediaType { get; set; }
            public string value { get { return Encoding.Default.GetString(data); } }
            public bool isAFileUpload { get { return !String.IsNullOrEmpty(fileName); } }
        }

        #endregion

        #region Errorhandlers
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
        #endregion

        #region ResponseClasses
        public class Response<TReturnData> : IHttpActionResult
        {
            #region Properties
            public TReturnData returnData { get; set; }
            public HttpStatusCode status { get; set; }
            public string ReasonPhrase { get; set; }
            #endregion



            #region IImplementation
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var settings = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore,

                    
                };
                var data = JsonConvert.SerializeObject(returnData,settings);
                var response = new HttpResponseMessage()
                {
                    Content = new StringContent(data, Encoding.UTF8, "application/json"),
                    StatusCode = status,
                    ReasonPhrase = ReasonPhrase


                };

                return Task.FromResult(response);
            }
            #endregion
        }
        #endregion

        #region HelperClasses
        public class MultiFormData
        {
            public string Obj { get; set; }
            public string Age { get; set; }
        }
        #endregion

        #region Post

        [HttpPost]
        [Route("api/Admin/PostProduct/{product}")]
        public async Task<Response<Product>> PostProduct()
        {
            Product product = new Product();
            List<ImageData> imageFiles = new List<ImageData>();

            Response<Product> responseMessage = new Response<Product>();

            MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(root);

            await Request.Content.ReadAsMultipartAsync(provider);

            var dataList = provider.FormData.GetValues("ObjectData");

            product = (new JavaScriptSerializer()).Deserialize<Product>(dataList[0]);

            

            if (product == null)
            {
                responseMessage.status = HttpStatusCode.BadRequest;
                responseMessage.returnData = product;
                return responseMessage;
            }

            if (db.ExistByName(product.Name, TableType.Product))
            {
                responseMessage.status = HttpStatusCode.Found;
                responseMessage.returnData = product;
                return responseMessage;
            }

            if (provider.FileData.Count > 0)
            {

                for (int i = 0; i <= provider.FileData.Count - 1; i++)
                {
                    ImageData imageData = new ImageData();
                    var fileupload = provider.FileData[i];
                    var temppath = fileupload.LocalFileName;
                    var bytes = File.ReadAllBytes(temppath);
                    imageData.Data = bytes;
                    imageData.MimeType = provider.FileData[i].Headers.ContentType.ToString();

                    imageFiles.Add(imageData);
                }
                System.Console.WriteLine(imageFiles.Count);

                product = fops.SaveImages(product, imageFiles, TableType.Category);

                if (product == null)
                {
                    responseMessage.status = HttpStatusCode.InternalServerError;
                    responseMessage.returnData = null;
                    return responseMessage;
                }
                else
                {

                    var _ = db.CreateProduct(product);
                    if (_ == null)
                    {
                        responseMessage.returnData = null;
                        responseMessage.status = HttpStatusCode.BadRequest;
                        return responseMessage;
                    }
                    else
                    {
                        responseMessage.returnData = product;
                        responseMessage.status = HttpStatusCode.Created;
                        return responseMessage;
                    }
                }

            }

            var result = db.CreateProduct(product);
            if (result == null)
            {
                responseMessage.returnData = null;
                responseMessage.status = HttpStatusCode.BadRequest;

            }
            else
            {
                responseMessage.returnData = product;
                responseMessage.status = HttpStatusCode.Created;

            }

            return responseMessage;


        }

        [HttpPost]
        [Route("api/Admin/PostCategory/{category}")]
        public async Task<Response<Category>> PostCategory()
        {
            Category category = new Category();

            Response<Category> responseMessage = new Response<Category>();
            List<ImageData> imageFiles = new List<ImageData>();

            MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(root);

            await Request.Content.ReadAsMultipartAsync(provider);
            var dataList = provider.FormData.GetValues("ObjectData");
            category = (new JavaScriptSerializer()).Deserialize<Category>(dataList[0]);
          

            if (category == null)
            {
                responseMessage.status = HttpStatusCode.BadRequest;
                responseMessage.returnData = category;
                return responseMessage;
            }

            if (db.ExistByName(category.Name, TableType.Category))
            {
                responseMessage.status = HttpStatusCode.Found;
                responseMessage.returnData = category;
                return responseMessage;
            }

            if (provider.FileData.Count > 0)
            {

                for (int i = 0; i <= provider.FileData.Count - 1; i++)
                {
                    ImageData imageData = new ImageData();
                    var fileupload = provider.FileData[i];
                    var temppath = fileupload.LocalFileName;
                    var bytes = File.ReadAllBytes(temppath);
                    imageData.Data = bytes;
                    imageData.MimeType = provider.FileData[i].Headers.ContentType.ToString();

                    imageFiles.Add(imageData);
                }
                System.Console.WriteLine(imageFiles.Count);

                category = fops.SaveImages(category, imageFiles, TableType.Category);

                if (category == null)
                {
                    responseMessage.status = HttpStatusCode.InternalServerError;
                    responseMessage.returnData = null;
                    return responseMessage;
                }
                else
                {

                    var _ = db.CreateCategory(category);
                    if (_ == null)
                    {
                        responseMessage.returnData = null;
                        responseMessage.status = HttpStatusCode.BadRequest;
                        return responseMessage;
                    }
                    else
                    {
                        responseMessage.returnData = category;
                        responseMessage.status = HttpStatusCode.Created;
                        return responseMessage;
                    }
                }

            }

            var result = db.CreateCategory(category);
            if (result == null)
            {
                responseMessage.returnData = null;
                responseMessage.status = HttpStatusCode.BadRequest;

            }
            else
            {
                responseMessage.returnData = category;
                responseMessage.status = HttpStatusCode.Created;

            }

            return responseMessage;

        }

        [HttpPost]
        [Route("api/Admin/PostBrand/{objData}")]
        public async Task<Response<Brand>> PostBrand()
        {

        Response<Brand> responseMessage = new Response<Brand>();

            

            Brand brand = default;
            List<ImageData> ImageFiles = new List<ImageData>();

            MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(root);

            await Request.Content.ReadAsMultipartAsync(provider);
            var dataList = provider.FormData.GetValues("ObjectData");
            brand = (new JavaScriptSerializer()).Deserialize<Brand>(dataList[0]);
            
           

            if (brand==null)
            {
                responseMessage.status = HttpStatusCode.BadRequest;
                responseMessage.returnData = brand;
                return responseMessage;
            }

            if(db.ExistByName(brand.Name, TableType.Brand))
            {
                responseMessage.status = HttpStatusCode.Found;
                responseMessage.returnData = brand;
                return responseMessage;
            }
            
            if (provider.FileData.Count>0)
            {

            for (int i = 0; i <= provider.FileData.Count-1; i++)
            {
                ImageData imageData = new ImageData();
                var fileupload = provider.FileData[i];
                var temppath = fileupload.LocalFileName;
                var bytes = File.ReadAllBytes(temppath);
                imageData.Data = bytes;
                imageData.MimeType = provider.FileData[i].Headers.ContentType.ToString();

                ImageFiles.Add(imageData);
            }
            System.Console.WriteLine(ImageFiles.Count);

            brand= fops.SaveImages(brand,ImageFiles, TableType.Brand);

                if (brand == null)
                {
                    responseMessage.status = HttpStatusCode.InternalServerError;
                    responseMessage.returnData = null;
                    return responseMessage;
                }
                else
                {

                    var _ = db.CreateBrand(brand);
                    if (_ == null)
                    {
                        responseMessage.returnData = null;
                        responseMessage.status = HttpStatusCode.BadRequest;
                        return responseMessage;
                    }
                    else
                    {
                        responseMessage.returnData = brand;
                        responseMessage.status = HttpStatusCode.Created;
                        return responseMessage;
                    }
                }

            }
            var result = db.CreateBrand(brand);
            if (result == null)
            {
                responseMessage.returnData = null;
                responseMessage.status = HttpStatusCode.BadRequest;
                
            }
            else
            {
                responseMessage.returnData = brand;
                responseMessage.status = HttpStatusCode.Created;
                
            }








            return responseMessage;
        }

        private async Task<(string obj,List<byte[]> imageData)> GetMultiFormData()
        {
            string obj = default;
            List<byte[]> imageData = default;

            MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(root);

            await Request.Content.ReadAsMultipartAsync(provider);

            var dataList = provider.FormData.GetValues("ObjectData");
            obj= dataList[0];


            for (int i = 0; i <= provider.FileData.Count; i++)
            {
                var fileupload = provider.FileData[i];
                var temppath = fileupload.LocalFileName;
                var bytes = File.ReadAllBytes(temppath);

                imageData.Add(bytes);
            }

            return (obj, imageData);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("api/Admin/PutProduct/{product}")]
        public Response<Product> PutProduct(Product product)
        {
            Response<Product> responseMessage = new Response<Product>();
            var dbResponse = db.UpdateProduct(product);
            try
            {

                if (dbResponse == false)
                {
                    responseMessage.returnData = null;
                    responseMessage.status = HttpStatusCode.InternalServerError;
                    responseMessage.ReasonPhrase = "Update Failed";
                }
                else if (dbResponse == true)
                {
                    responseMessage.returnData = null;
                    responseMessage.status = HttpStatusCode.OK;
                    responseMessage.ReasonPhrase = "Update Succeded";
                }

            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return responseMessage;
        }

        [HttpPut]
        [Route("api/Admin/PutCategory/{category}")]
        public Response<Category> PutCategory(Category category)
        {
            Response<Category> responseMessage = new Response<Category>();
            var dbResponse = db.UpdateCategory(category);
            try
            {

                if (dbResponse == false)
                {
                    responseMessage.returnData = null;
                    responseMessage.status = HttpStatusCode.Conflict;
                    responseMessage.ReasonPhrase = "Update Failed";
                }
                else if (dbResponse == true)
                {
                    responseMessage.returnData = null;
                    responseMessage.status = HttpStatusCode.NoContent;
                    responseMessage.ReasonPhrase = "Update Succeded";
                }

            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return responseMessage;
        }

        [HttpPut]
        [Route("api/Admin/PutBrand/{brand}")]
        public Response<Brand> PutBrand(Brand brand)
        {
            Response<Brand> responseMessage = new Response<Brand>();
            var dbResponse = db.UpdateBrand(brand);
            try
            {

                if (dbResponse == false)
                {
                    responseMessage.returnData = null;
                    responseMessage.status = HttpStatusCode.Conflict;
                    responseMessage.ReasonPhrase = "Update Failed";
                }
                else if (dbResponse == true)
                {
                    responseMessage.returnData = null;
                    responseMessage.status = HttpStatusCode.NoContent;
                    responseMessage.ReasonPhrase = "Update Succeded";
                }

            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return responseMessage;
        }
       



        #endregion

        #region Get
        [HttpGet]
        [Route("api/Admin/GetProductById/{productId}")]
        public Response<Product> GetProductById(int productId)
        {
            Response<Product> responseMessage = new Response<Product>();
            try
            {
                var DbResponse = db.GetproductById(productId);

                DbResponse.Brand1.Products.Clear();
                DbResponse.Category1.Products.Clear();
                if (DbResponse == null)
                {
                    responseMessage.returnData = DbResponse;
                    responseMessage.status = HttpStatusCode.NotFound;
                    responseMessage.ReasonPhrase = "Requested item was Not Found";
                }
                else
                {
                    responseMessage.returnData = DbResponse;
                    responseMessage.status = HttpStatusCode.OK;
                    responseMessage.ReasonPhrase = "Requested item Found";
                }
            }
            catch (Exception e)
            {
                responseMessage.returnData = null;
                responseMessage.status = HttpStatusCode.InternalServerError;
                Errorhead(e);
            }
            return responseMessage;

        }

        [HttpGet]
        [Route("api/Admin/GetAllProducts")]
      
        public Response<List<Product>> GetAllProducts()
        {

            Response<List<Product>> responseMessage = new Response<List<Product>>();
            try
            {
                List<Product> returnList = db.GetAllProducts();
                if(returnList==null)
                {
                    responseMessage.status = HttpStatusCode.NotFound;
                    responseMessage.returnData = null;
                }
                else
                {
                    responseMessage.status = HttpStatusCode.OK;
                    responseMessage.returnData = returnList;
                }
            }
            catch(Exception e)
            {
                responseMessage.status = HttpStatusCode.InternalServerError;
                responseMessage.returnData = null;
                Errorhead(e);
            }
            return responseMessage;
        }

        [HttpGet]
        [Route("api/Admin/GetAllCategories")]
        public Response<List<Category>> GetAllCategories()
        {
   
            Response<List<Category>> responseMessage = new Response<List<Category>>();
            try
            {
                 var _=db.GetAllCategories();

                if(_==null)
                {
                    responseMessage.status = HttpStatusCode.NotFound;
                    responseMessage.returnData = null;
                }
                else
                {
                    responseMessage.status = HttpStatusCode.OK;
                    responseMessage.returnData = _;

                }
            }
            catch(Exception e)
            {
                Errorhead(e);
                responseMessage.status = HttpStatusCode.InternalServerError;
                responseMessage.returnData = null;
            }
            return responseMessage;
        }

        [HttpGet]
        [Route("api/Admin/GetAllBrands")]
        public Response<List<Brand>> GetAllBrands()
        {

            Response<List<Brand>> responseMessage = new Response<List<Brand>>();
            try
            {
                var _ = db.GetAllBrands();

                if (_ == null)
                {
                    responseMessage.status = HttpStatusCode.NotFound;
                    responseMessage.returnData = null;
                }
                else
                {
                    responseMessage.status = HttpStatusCode.OK;
                    responseMessage.returnData = _;

                }
            }
            catch (Exception e)
            {
                Errorhead(e);
                responseMessage.status = HttpStatusCode.InternalServerError;
                responseMessage.returnData = null;
            }
            return responseMessage;
        }

        #endregion

        #region Delete

        [HttpDelete]
        public Response<bool> DeleteProducts( [FromBody] List<Product> products)
        {

            Response<bool> responseMessage = new Response<bool>();

            try
            {
                var databaseResponse = db.DeleteProduct(products);
                if (databaseResponse == false)
                {
                    responseMessage.returnData = false;
                    responseMessage.status = HttpStatusCode.Conflict;
                    responseMessage.ReasonPhrase = "Failed To Delte";
                }
                else if (databaseResponse == true)
                {
                    responseMessage.returnData = true;
                    responseMessage.status = HttpStatusCode.OK;
                    responseMessage.ReasonPhrase = "Product Deleted";
                }
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return responseMessage;
        }

        [HttpDelete]
        [Route("api/Admin/DeleteBrands/brands")]
        public Response<bool> DeleteBrands([FromBody] List<Brand> brands)
        {
            Response<bool> responseMessage = new Response<bool>();
            try
            {
                var databaseResponse = db.DeleteBrand(brands);

                if (databaseResponse == false)
                {
                    responseMessage.returnData = false;
                    responseMessage.status = HttpStatusCode.Conflict;
                    responseMessage.ReasonPhrase = "Failed To Delte";
                }
                else if (databaseResponse == true)
                {
                    responseMessage.returnData = true;
                    responseMessage.status = HttpStatusCode.OK;
                    responseMessage.ReasonPhrase = "Product Deleted";
                }
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return responseMessage;
        }

        [HttpDelete]
        public Response<bool> DeleteCategorys([FromBody] List<Category> categories)
        {
            Response<bool> responseMessage = new Response<bool>();

            try
            {
                var databaseResponse = db.DeleteCategory(categories);

                if (databaseResponse == false)
                {
                    responseMessage.returnData = false;
                    responseMessage.status = HttpStatusCode.Conflict;
                    responseMessage.ReasonPhrase = "Failed To Delte";
                }
                else if (databaseResponse == true)
                {
                    responseMessage.returnData = true;
                    responseMessage.status = HttpStatusCode.OK;
                    responseMessage.ReasonPhrase = "Category Deleted";
                }
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return responseMessage;
        }

      


        #endregion

        #region Search
        [HttpGet]
        [Route("api/Admin/ExistByName/{itemName}/{tableType}")]
        public Response<bool> ExistByName([FromUri] string itemName,[FromUri] string tableType)
        {
            Response<bool> responseMessage = new Response<bool>();

            responseMessage.returnData = false;
            responseMessage.status = HttpStatusCode.NotFound;

            if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(tableType))
            {
                responseMessage.status = HttpStatusCode.BadRequest;

                return responseMessage;
            }

            TableType tabletoSearch = ReturnTableType(tableType);

            if (tabletoSearch == null)
            {
                responseMessage.status = HttpStatusCode.BadRequest;
                return responseMessage;
            }
            else
            {
                try
                {

                    if(db.ExistByName(itemName, tabletoSearch))
                    {
                        responseMessage.returnData = true;
                        responseMessage.status= HttpStatusCode.Found;
                    }
                }
                catch (Exception e)
                {
                    Errorhead(e);
                }

            }
            return responseMessage;
        }
        #endregion

        #region Utilities
        public TableType ReturnTableType(string tableType)
        {
            TableType returnTable = null;

            switch(tableType)
            {
                case "Brand":
                     returnTable= TableType.Brand;
                    break;
                case "Category":
                    returnTable= TableType.Category;
                    break;
                case "Product":
                    returnTable= TableType.Product;
                    break;
                default:
                    returnTable = null;
                    break;
            }
            return returnTable;
        }
        #endregion

    }

}

