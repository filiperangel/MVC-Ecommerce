using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.ViewModel
{
    public class CartViewModel
    {
        public class CartViewModelCreate : CartViewModel
        {
            public Cart cart = new Cart();

            public static void SaveNew(Cart cart)
            {
                //cart.SaveNew();
            }
        }
    }
}