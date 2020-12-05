using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

namespace ManOnTheMoon.Models
{
    public partial class Product: IEnumerable
    {
        #region Constructor

        #endregion

        #region Variables
        private string productImageUrl_1;
        private string productImageUrl_2;
        private string productImageUrl_3;
        private int productImagesTableRecordId;
        public List<Product> productsList;
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

        #region NestedClasses
        public class ProductEnumerator : IEnumerator
        {
            List<Product> productEnumerationList;

            int position = -1;

            public ProductEnumerator(List<Product> products)
            {
                productEnumerationList = products;
 
            }

            public object Current
            {
                get
                {
                    try
                    {
                        return productEnumerationList[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public bool MoveNext()
            {
                position++;
                return (position < productEnumerationList.Count);
            }

            public void Reset()
            {
                position = -1;
            }
        }
        #endregion

        #region Implementations
        // Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public ProductEnumerator GetEnumerator()
        {
            return new ProductEnumerator(productsList);
        }
        #endregion
    }
}