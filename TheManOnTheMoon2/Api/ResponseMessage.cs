using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheManOnTheMoon2.Database;
using System.Threading.Tasks;
using System.Threading;

namespace TheManOnTheMoon2.Api
{
    public class ResponseMessage<TReturnData> : IHttpActionResult
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
}