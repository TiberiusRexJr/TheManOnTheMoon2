﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheManOnTheMoon2.Models;
namespace TheManOnTheMoon2.Controllers
{
    public class ViewModelMultipleModels
    {
        public Product PrimaryProduct { get; set; }
        public IEnumerable<Product> RelatedProducts { get; set; }
    }
}