using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManOnTheMoon.Models
{
    public partial class Product
    {
        #region Constructor

        #endregion

        #region Variables
        private string productImageUrl_1;
        private string productImageUrl_2;
        private string productImageUrl_3;
        private int productImagesTableRecordId;
        #endregion

        #region Properties
            public string ProductImageUrl_1
        {
            get { return this.productImageUrl_1; }
            set { productImageUrl_1 = value; }
        }
        public string ProductImageUrl_2
        {
            get { return this.productImageUrl_2; }
            set { productImageUrl_2 = value; }
        }
        public string ProductImageUrl_3
        {
            get { return this.productImageUrl_3; }
            set { productImageUrl_3 = value; }
        }
        public int ProductImagesTableRecordId
        {
            get { return this.productImagesTableRecordId; }
            set { productImagesTableRecordId = value; }
        }
        #endregion
    }
}