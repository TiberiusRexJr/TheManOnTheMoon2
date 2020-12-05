﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManOnTheMoon.Models
{
    public partial class Category : IEnumerable
    {

        #region Variables
        List<Category> categories;
        private string _Category_Image_Url;
        #endregion

        #region Constructor

        #endregion

        #region Properties
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Category_Image_Url", DbType = "VarChar(MAX)")]
        public string Category_Image_Url
        {
            get
            {
                return this._Category_Image_Url;
            }
            set
            {
                if ((this._Category_Image_Url != value))
                {
                    this.OnCategory_Image_UrlChanging(value);
                    this.SendPropertyChanging();
                    this._Category_Image_Url = value;
                    this.SendPropertyChanged("Category_Image_Url");
                    this.OnCategory_Image_UrlChanged();
                }
            }
        }
        #endregion

        #region Functions
        partial void OnCategory_Image_UrlChanging(string value);
        partial void OnCategory_Image_UrlChanged();
        #endregion

        #region NestedClasses
        public class CategoryEnumerator : IEnumerator
        {
            List<Category> categoryEnumerationList;

            int position = -1;

            public CategoryEnumerator(List<Category> categories)
            {
                categoryEnumerationList = categories;
               

            }

            public object Current
            {
                get
                {
                    try
                    {
                        return categoryEnumerationList[position];
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
                return (position < categoryEnumerationList.Count);
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

        public CategoryEnumerator GetEnumerator()
        {
            return new CategoryEnumerator(categories);
        }
        #endregion


    }
}