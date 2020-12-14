using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheManOnTheMoon2.Models
{
    public class Transport<T>:HttpPostedFileBase
    {
        #region Variables

        #endregion

        #region Properties
        public T ObjectData { get; set; }
        HttpPostedFileBase ImageData { get; set; }
        #endregion

        #region Constructor

        #endregion

        #region SubClases
       
        #endregion

        #region Functions

        #endregion
    }
}