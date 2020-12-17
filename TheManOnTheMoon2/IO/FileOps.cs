using TheManOnTheMoon2.Database;
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

namespace TheManOnTheMoon2.IO
{
    public class FileOps
    {
        public T SaveImages<T>(T obj,List<MultipartFileData> imageFiles,TableType tableType)
        {
            T returnObj = default;
            string saveToDir = string.Empty;

            switch (tableType.Value)
            { case "Product" :
                    saveToDir = HttpContext.Current.Server.MapPath(null);
                    break;
               case "Brand" :
                    saveToDir = HttpContext.Current.Server.MapPath(null);

                    break;

                case "Category" :
                    saveToDir = HttpContext.Current.Server.MapPath(null);

                    break;

                default: returnObj = default;
                    break;

            }

            return returnObj;
        }
    }
}