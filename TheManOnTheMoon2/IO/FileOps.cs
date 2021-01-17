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
using System.Drawing;

namespace TheManOnTheMoon2.IO
{
    public class FileOps
    {
        #region Variables
        readonly List<string> ImageProperties = new List<string> { "Image_Main", "Image_Alt_1","Image_Alt_2" };

        string  ImagesRoot= HttpContext.Current.Server.MapPath("~/Images/");
        #endregion
        public T SaveImages<T>(T obj,List<ImageData> imageFiles,TableType tableType)
        {
            T returnObj = default;

            string imageFolderCategoricalDir = string.Empty;
            string imageFolderUniqueDir = string.Empty;
            string nameBase = string.Empty;

            List<string> savedImageUrls = default;
   
            nameBase = typeof(T).GetProperty("Name").GetValue(obj).ToString();

            switch (tableType.Value)
            { case "Product" :
                    imageFolderCategoricalDir ="Products/";
                    break;
               case "Brand" :
                    imageFolderCategoricalDir ="Brands/";
                    break;
                case "Category" :
                    imageFolderCategoricalDir = "Categories/";
                    break;
                default:
                    obj = default;
                    return obj;
            }

           imageFolderUniqueDir= CreateFolder(imageFolderCategoricalDir,nameBase);
            if(String.IsNullOrEmpty(imageFolderUniqueDir))
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
                    for(int i=0; i<=savedImageUrls.Count-1; i++)
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

        private List<string> SaveImages(List<ImageData> imageBlobs,string imageFolderUniqueDir, string nameBase)
        {
            List<string> savedImageUrls = new List<string>();

            string extension = default;
            string stringByte = default;
            string filename = nameBase+"__" + Guid.NewGuid().ToString();
            string savePath = imageFolderUniqueDir + "/";

            foreach(ImageData image in imageBlobs)
            {
                extension = returnFileExtension(image.MimeType);
                stringByte = ConvertByteArray64ToString(image.Data);

                savePath += filename + extension;

                
                File.WriteAllBytes(ImagesRoot+savePath, Convert.FromBase64String(stringByte));
                savedImageUrls.Add(savePath);
            }


            return savedImageUrls;
        }

        private string CreateFolder(string typeDirectory,string baseName)
        {
            string newDirectory = default;

            if(Directory.Exists(ImagesRoot+typeDirectory + baseName))
            {
                return newDirectory;
            }
            else
            {
                DirectoryInfo di= Directory.CreateDirectory(ImagesRoot+typeDirectory + baseName+"/");
                newDirectory = typeDirectory + baseName;
            }
            return newDirectory;
        }

        private string returnFileExtension(string Mime)
        {
            string extension = default;
            switch(Mime)
            {
                case "image/gif": extension = ".gif";
                    break;
                case "image/jpeg": extension = ".jpg";
                    break;
                case "image/png": extension = ".png";
                    break;
                case "image/webp":
                    extension = ".webp";
                    break;
                default: extension = ".png";
                    break;
            }
            return extension;
        }

        private string ConvertByteArray64ToString(byte[] data)
        {
            string stringByte = default;

            stringByte = Convert.ToBase64String(data);

            return stringByte;
        }
       
    }
}