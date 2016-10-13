using Ecommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.BusinessControllers
{
    public class CartBusinessCon
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public bool Create(int quantity, int bookId)
        {
            Cart cart = new Cart();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            cart.UserId = user.Id;
            cart.Book = db.Books.Find(bookId);
            cart.Quantity = quantity;
            cart.DateTimeAdded = DateTime.Now;
            return true;
        }
    }
}