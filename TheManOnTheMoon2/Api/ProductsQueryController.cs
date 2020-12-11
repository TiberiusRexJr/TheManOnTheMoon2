using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using TheManOnTheMoon2.Utilities;
using TheManOnTheMoon2.Database;
using TheManOnTheMoon2.Models;

namespace TheManOnTheMoon2.Api
{
    public class ProductsQueryController : ApiController
    {
        #region Variables
        DbQueryProducts db = new DbQueryProducts();
        ErrorHandler errorHandler = new ErrorHandler();
        #endregion

        #region RetriveAll
        public ResponseMessage<List<Product>> GetAllProducts()
        {
            ResponseMessage<List<Product>> responseMessage = new ResponseMessage<List<Product>>();
            try
            {
                var databaseResponse = db.GetAllProducts();
                if(databaseResponse==null)
                {
                    responseMessage.returnData = null;
                    responseMessage.status = HttpStatusCode.Conflict;
                    responseMessage.ReasonPhrase="Error Getting Products";
                }
                else
                {
                    responseMessage.returnData = databaseResponse;
                    responseMessage.status = HttpStatusCode.OK;
                    responseMessage.ReasonPhrase = "Ok";
                }
            }
            catch(Exception e)
            {
                errorHandler.Errorhead(e);
            }
            return responseMessage;
        }
        public ResponseMessage<List<Brand>> GetAllBrands()
        {
            ResponseMessage<List<Brand>> responseMessage = new ResponseMessage<List<Brand>>();
            try
            {
                var databaseResponse = db.GetAllBrands();
                if (databaseResponse == null)
                {
                    responseMessage.returnData = null;
                    responseMessage.status = HttpStatusCode.Conflict;
                    responseMessage.ReasonPhrase = "Error Getting Products";
                }
                else
                {
                    responseMessage.returnData = databaseResponse;
                    responseMessage.status = HttpStatusCode.OK;
                    responseMessage.ReasonPhrase = "Ok";
                }
            }
            catch (Exception e)
            {
                errorHandler.Errorhead(e);
            }
            return responseMessage;
        }
        public ResponseMessage<List<Category>> GetAllCategories()
        {
            ResponseMessage<List<Category>> responseMessage = new ResponseMessage<List<Category>>();
            try
            {
                var databaseResponse = db.GetAllCategories();
                if (databaseResponse == null)
                {
                    responseMessage.returnData = null;
                    responseMessage.status = HttpStatusCode.Conflict;
                    responseMessage.ReasonPhrase = "Error Getting Products";
                }
                else
                {
                    responseMessage.returnData = databaseResponse;
                    responseMessage.status = HttpStatusCode.OK;
                    responseMessage.ReasonPhrase = "Ok";
                }
            }
            catch (Exception e)
            {
                errorHandler.Errorhead(e);
            }
            return responseMessage;
        }
        #endregion

        #region RetrieveByPricePoint
        public ResponseMessage<List<Product>> GetProductsByPriceOrder(int mode)
        {
            ResponseMessage<List<Product>> responseMessage = new ResponseMessage<List<Product>>();
            try
            {
                var databasResponse= db.RetrieveProductsByPriceOrder(mode);
                if(databasResponse==null)
                {
                    responseMessage.returnData = null;
                    responseMessage.status = HttpStatusCode.Conflict;
                    responseMessage.ReasonPhrase = "Error";
                }
                else
                {
                    responseMessage.returnData = databasResponse;
                    responseMessage.status = HttpStatusCode.OK;
                    responseMessage.ReasonPhrase = "Ok";
                }
            }
            catch(Exception e)
            {
                errorHandler.Errorhead(e);
            }

            return responseMessage;

        }

        public ResponseMessage<List<Product>> GetProductsByPriceRang(int mode)
        {
            ResponseMessage<List<Product>> responseMessage = new ResponseMessage<List<Product>>();
            try
            {
                var databasResponse = db.RetrieveProductsByPriceRange(mode);
                if (databasResponse == null)
                {
                    responseMessage.returnData = null;
                    responseMessage.status = HttpStatusCode.Conflict;
                    responseMessage.ReasonPhrase = "Error";
                }
                else
                {
                    responseMessage.returnData = databasResponse;
                    responseMessage.status = HttpStatusCode.OK;
                    responseMessage.ReasonPhrase = "Ok";
                }
            }
            catch (Exception e)
            {
                errorHandler.Errorhead(e);
            }

            return responseMessage;

        }

        #endregion
    }
}
