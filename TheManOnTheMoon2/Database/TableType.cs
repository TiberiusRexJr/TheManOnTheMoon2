using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheManOnTheMoon2.Database
{
    public class TableType
    {
        #region Constructor
        private TableType(string value) { Value = value; }
        #endregion
        #region Variables
        public string Value { get; set; }
        #endregion

        #region Properties
        public static TableType Brand { get { return new TableType("Brand"); } }
        public static TableType Category { get { return new TableType("Category"); } }
        public static TableType Product { get { return new TableType("Product"); } }

        #endregion
    }
}