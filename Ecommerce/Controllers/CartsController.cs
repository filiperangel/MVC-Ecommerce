using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace Ecommerce.Controllers
{
    public class CartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Carts
        //public ActionResult Index()
        //{
        //    var carts = db.Carts.Include(c => c.User);
        //    return View(carts.ToList());
        //}

        // GET: Carts/Details/5

        // GET: Carts/Add/1
        [Authorize]
        public ActionResult Add(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cart cart = new Cart();
            cart.Book = db.Books.Find(id);
            cart.Quantity = 1;
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int id, int quantity)
        {
            Cart cart = null;
            int itemNumber = 0;
            if (ModelState.IsValid)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                try
                {
                    Cart cartAux = db.Carts.Where(c => c.UserId == user.Id).OrderByDescending(c => c.ItemNumber).First();
                    itemNumber = cartAux.ItemNumber;
                }
                catch(Exception e)
                {
                }
                cart = new Cart();
                cart.UserId = user.Id;
                cart.ItemNumber = ++itemNumber;
                cart.DateTimeAdded = DateTime.Now;
                cart.Book = db.Books.Find(id);
                cart.Quantity = quantity;
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(cart);
        }

        [Authorize]
        public ActionResult Details()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var cart = db.Carts.Where(c => c.UserId == user.Id).OrderBy(c => c.ItemNumber).ToList();
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        /*public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }*/

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
        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Quantity,DateTimeAdded")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Cart cart = db.Carts.Where(c => c.UserId == user.Id && c.ItemNumber == id).First();
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", cart.UserId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemNumber,Quantity")] Cart cart, int? ItemNumber)
        {
            //TODO item number not returnig
            Cart cart2 = null;
            if (ModelState.IsValid)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                cart2 = db.Carts.Where(c => c.UserId == user.Id && c.ItemNumber == ItemNumber).First();
                cart2.Quantity = cart.Quantity;
                db.Entry(cart2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                cart2 = cart;
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", cart.UserId);
            return View(cart2);
        }

        // GET: Carts/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //int itemNumber = Int32.Parse(id);
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Cart cart = db.Carts.Where(c => c.UserId == user.Id && c.ItemNumber == id).First();
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Cart cart = db.Carts.Where(c => c.UserId == user.Id && c.ItemNumber == id).First();
            db.Carts.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("Details");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public PartialViewResult ShowCart()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user != null)
                ViewBag.NumberofItems = db.Carts.Where(c => c.UserId == user.Id).Sum(x => x.Quantity);
            else
                ViewBag.NumberofItems = 0;
            return PartialView();
        }
    }
}
