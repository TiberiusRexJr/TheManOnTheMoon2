using TheManOnTheMoon2.Database;
using TheManOnTheMoon2.Models;
using TheManOnTheMoon2.IO;
using System;
using System.Diagnostics;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

                var data = JsonSerializer.Serialize(returnData);
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

        #region Post

        [HttpPost]
        [Route("api/Admin/PostProduct/{product}")]
        public Response<Product> PostProduct([FromBody] Product product)
        {
            Response<Product> responseMessage = new Response<Product>();
            try
            {
                var DbResponse = db.CreateProduct(product);

                if (DbResponse == null)
                {
                    responseMessage.returnData = null;
                    responseMessage.ReasonPhrase = "Database Responsed With Null";
                    responseMessage.status = HttpStatusCode.Conflict;
                }
                else
                {
                    responseMessage.returnData = DbResponse;
                    responseMessage.ReasonPhrase = "SuccessFully Inserted";
                    responseMessage.status = HttpStatusCode.Created;
                }
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return responseMessage;
        }

        [HttpPost]
        [Route("api/Admin/PostCategory/{category}")]
        public Response<Category> PostCategory([FromBody] Category category)
        {
            Response<Category> responseMessage = new Response<Category>();
            try
            {
                var DbResponse = db.CreateCategory(category);

                if (DbResponse == null)
                {
                    responseMessage.returnData = null;
                    responseMessage.ReasonPhrase = "Database Responsed With Null";
                    responseMessage.status = HttpStatusCode.Conflict;
                }
                else
                {
                    responseMessage.returnData = DbResponse;
                    responseMessage.ReasonPhrase = "SuccessFully Inserted";
                    responseMessage.status = HttpStatusCode.Created;
                }
            }
            catch (Exception e)
            {
                Errorhead(e);
            }
            return responseMessage;

        }

        //[HttpPost]
        //[Route("api/Admin/PostBrand/{brand}")]
        //public Response<Brand> PostBrand([FromBody] Brand brand)
        //{
        //    Response<Brand> responseMessage = new Response<Brand>();

        //    if (brand == null)
        //    {
        //        responseMessage.status = HttpStatusCode.BadRequest;
        //        responseMessage.returnData = null;
        //    }
        //    else
        //    {
        //        try
        //        {

        //            var DbResponse = db.CreateBrand(brand);

        //            if (DbResponse == null)
        //            {
        //                responseMessage.returnData = null;
        //                responseMessage.status = HttpStatusCode.InternalServerError;
        //            }
        //            else
        //            {
        //                responseMessage.returnData = DbResponse;

        //                responseMessage.status = HttpStatusCode.Created;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Errorhead(e);
        //        }
        //    }

        //    return responseMessage;
        //}

        [HttpPost]
        [Route("api/Admin/PostBrand/{objData}")]
        public async Task<Response<Brand>> PostBrand()
        {
            
            Response<Brand> responseMessage = new Response<Brand>();

            Brand brand = default;
            List<byte[]> ImageFiles = default;
            string root = HttpContext.Current.Server.MapPath("~/App_Data");

        MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(root);

        

            if (Request.Content.IsMimeMultipartContent())
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var dataList = provider.FormData.GetValues("ObjectData");
                brand = (new JavaScriptSerializer()).Deserialize<Brand>(dataList[0]);

                Console.WriteLine(brand.Name);

                var provider2 = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider2);

                for (int i =0; i <= provider.FileData.Count; i++)
                {
                    var filedata = provider.FileData[i].LocalFileName;
                    var data = provider.FileData[i];
                    Console.WriteLine(data);
                    ImageFiles.Add(File.ReadAllBytes(filedata));
                }    
                
                
                Brand returnObj = fops.SaveImages(brand, ImageFiles, TableType.Brand);
                if (returnObj == null)
                {
                    responseMessage.status = HttpStatusCode.InternalServerError;
                    responseMessage.returnData = null;
                }
                else
                {
                    Brand result = db.CreateBrand(returnObj);
                    if (result == null)
                    {
                        responseMessage.status = HttpStatusCode.InternalServerError;
                        responseMessage.returnData = null;
                    }
                    else
                    {
                        responseMessage.status = HttpStatusCode.Created;
                        responseMessage.returnData = result;
                    }

                }

            }
            else
            {

                var _=db.CreateBrand(brand);
                if(_==null)
                {
                    responseMessage.status = HttpStatusCode.InternalServerError;
                    responseMessage.returnData = null;
                }


            }


            return responseMessage;
        }

        #endregion

        #region Put
        [HttpPut]
        public Response<Product> PutProduct(Product product)
        {
            Response<Product> responseMessage = new Response<Product>();
            var dbResponse = db.UpdateProduct(product);
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

        /*?[HttpDelete]
        //public Response<bool> DelteUser([FromBody] List<User>   user) 
        //{ 
        
        //}
        */
        [HttpDelete]


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

            DbAdmin.TableType tabletoSearch = ReturnTableType(tableType);

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
        public DbAdmin.TableType ReturnTableType(string tableType)
        {
            DbAdmin.TableType returnTable = null;

            switch(tableType)
            {
                case "Brand":
                     returnTable= DbAdmin.TableType.Brand;
                    break;
                case "Category":
                    returnTable= DbAdmin.TableType.Category;
                    break;
                case "Product":
                    returnTable= DbAdmin.TableType.Product;
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

