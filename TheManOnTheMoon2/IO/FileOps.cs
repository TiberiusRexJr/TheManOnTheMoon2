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
using System.Reflection;
using System.IO;

namespace TheManOnTheMoon2.IO
{
    public class FileOps
    {
        #region Variables
        readonly List<string> ImageProperties = new List<string> { "Image_Main", "Image_Alt_1","Image_Alt_2" };
        #endregion
        public T SaveImages<T>(T obj,List<byte[]> imageFiles,TableType tableType)
        {
            T returnObj = default;

            string imageFolderCategoricalDir = string.Empty;
            string imageFolderUniqueDir = string.Empty;
            string nameBase = string.Empty;

            List<string> savedImageUrls = default;
   
            nameBase = typeof(T).GetProperty("Name").GetValue(obj).ToString();

            switch (tableType.Value)
            { case "Product" :
                    imageFolderCategoricalDir = HttpContext.Current.Server.MapPath(null);
                    break;
               case "Brand" :
                    imageFolderCategoricalDir = HttpContext.Current.Server.MapPath(null);
                    break;
                case "Category" :
                    imageFolderCategoricalDir = HttpContext.Current.Server.MapPath(null);
                    break;
                default:
                    obj = default;
                    return obj;
            }

           imageFolderUniqueDir= CreateFolder(imageFolderCategoricalDir,nameBase);
            if(String.IsNullOrEmpty( imageFolderUniqueDir))
            {
                obj = default;
                return obj;
            }
            else
            {
                savedImageUrls= SaveImages(imageFiles, imageFolderUniqueDir,nameBase);

                if(savedImageUrls.Count==0)
                {
                    obj = default;
                    return obj;
                }
                else
                {
                    for(int i=1; i<=savedImageUrls.Count; i++)
                    {
                       TrySetProperty(obj, ImageProperties[i], savedImageUrls[i]);

                    }
                    returnObj= obj;
                }
            }
            


            return returnObj;
        }

        private object TrySetProperty(object obj,string proptery,object value)
        {
            var property = obj.GetType().GetProperty(proptery, BindingFlags.Public | BindingFlags.Instance);

            if(property!=null&&property.CanWrite)
            {
                property.SetValue(obj, value, null);
                return obj;

            }
            else
            {
                obj = default;
                return obj;
            }

            
        }

        private List<string> SaveImages(List<byte[]> imageBlobs,string imageFolderUniqueDir, string nameBase)
        {
            List<string> savedImageUrls = default;

            foreach(byte[] image in imageBlobs)
            {
                string path = imageFolderUniqueDir + "/" + nameBase + Guid.NewGuid().ToString();
                File.WriteAllBytes(path, image);
                savedImageUrls.Add(path);
            }


            return savedImageUrls;
        }

        private string CreateFolder(string baseDirectory,string baseName)
        {
            string newDirectory = default;

            if(Directory.Exists(baseDirectory+"/"+baseName))
            {
                return newDirectory;
            }
            else
            {
                DirectoryInfo di= Directory.CreateDirectory(baseDirectory + "/" + baseName);
                newDirectory = di.FullName;
            }
            return newDirectory;
        }
    }
}