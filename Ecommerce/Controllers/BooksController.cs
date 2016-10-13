using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using System.Data.Entity.Infrastructure;
using System.IO;
using Ecommerce.ViewModel;

namespace Ecommerce.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            BookViewModel book = new BookViewModel();
            return View(book);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,title,publishDate,ISBN,Price,Synopsis")] Book book, string[] selectedAuthors, string[] selectedGenres, HttpPostedFileBase coverImg)
        {
            BookViewModel bookViewModel = new BookViewModel();
            bookViewModel.book = book;
            if(selectedAuthors != null)
            {
                bookViewModel.book.Authors = new List<Author>();
                foreach (var author in selectedAuthors)
                {
                    var authorToAdd = db.Authors.Find(int.Parse(author));
                    bookViewModel.book.Authors.Add(authorToAdd);
                }
            }

            if(selectedGenres != null)
            {
                bookViewModel.book.Genres = new List<Genre>();
                foreach(var genre in selectedGenres)
                {
                    var genreToAdd = db.Genres.Find(int.Parse(genre));
                    bookViewModel.book.Genres.Add(genreToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                if (coverImg != null)
                {
                    Directory.CreateDirectory(Server.MapPath("~/Images/Books/" + bookViewModel.book.Id + "/"));
                    string coverImagePath = "/Images/Books/" + bookViewModel.book.Id + "/" + "cover" + Path.GetExtension(coverImg.FileName);
                    coverImg.SaveAs(Server.MapPath("~" + coverImagePath));
                    bookViewModel.book.CoverImagePath = coverImagePath;
                }
                db.Books.Add(bookViewModel.book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookViewModel);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookViewModel book = new BookViewModel(id);


            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        //public ActionResult Edit([Bind(Include = "Id,title,publishDate,ISBN,Price")] Book book)
        //public ActionResult Edit([Bind(Include = "Id,title,publishDate,ISBN,Price")] Book book, string[] selectedAuthors, string[] selectedGenres)
        public ActionResult Edit(Book book, string[] selectedAuthors, string[] selectedGenres, HttpPostedFileBase coverImg)
        {
            BookViewModel bookViewModel = new BookViewModel(book.Id);
            bookViewModel.book.title = book.title;
            bookViewModel.book.publishDate = book.publishDate;
            bookViewModel.book.ISBN = book.ISBN;
            bookViewModel.book.Price = book.Price;

            if (ModelState.IsValid)
            {
                try
                {
                    if (selectedAuthors == null)
                    {
                        book.Authors = new List<Author>();
                    }
                    else
                    {
                        var selectedOptionsHS = new HashSet<string>(selectedAuthors);
                        var bookAuthors = new HashSet<int>(bookViewModel.book.Authors.Select(a => a.Id));
                        foreach (var author in db.Authors)
                        {
                            if (selectedOptionsHS.Contains(author.Id.ToString()))
                            {
                                if (!bookAuthors.Contains(author.Id))
                                {
                                    book.Authors.Add(author);
                                }
                            }
                            else
                            {
                                if (bookAuthors.Contains(author.Id))
                                {
                                    book.Authors.Remove(author);
                                }
                            }
                        }
                    }

                    if (selectedGenres == null)
                    {
                        book.Genres = new List<Genre>();
                    }
                    else
                    {
                        var selectedOptionsHS = new HashSet<string>(selectedGenres);
                        var bookGenres = new HashSet<int>(bookViewModel.book.Genres.Select(a => a.Id));
                        foreach (var genre in db.Genres)
                        {
                            if (selectedOptionsHS.Contains(genre.Id.ToString()))
                            {
                                if (!bookGenres.Contains(genre.Id))
                                {
                                    book.Genres.Add(genre);
                                }
                            }
                            else
                            {
                                if (bookGenres.Contains(genre.Id))
                                {
                                    book.Genres.Remove(genre);
                                }
                            }
                        }
                    }
                    if (coverImg != null)
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Images/Books/" + bookViewModel.book.Id + "/"));
                        string coverImagePath = "/Images/Books/" + bookViewModel.book.Id + "/" + "cover" + Path.GetExtension(coverImg.FileName);
                        coverImg.SaveAs(Server.MapPath("~" + coverImagePath));
                        bookViewModel.book.CoverImagePath = coverImagePath;
                    }
                    book.CoverImagePath = bookViewModel.book.CoverImagePath;
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }
            return View(bookViewModel);
            //var bookToUpdate = db.Books
            //.Include(b => b.Authors)
            //.Where(i => i.Id == id)
            //.Single();

            /*if (TryUpdateModel(bookToUpdate))
            {
                try
                {
                    if (selectedAuthors == null)
                    {
                        bookToUpdate.Authors = new List<Author>();
                    }
                    else
                    {
                        var selectedOptionsHS = new HashSet<string>(selectedAuthors);
                        var bookAuthors = new HashSet<int>(bookToUpdate.Authors.Select(a => a.Id));
                        foreach (var author in db.Authors)
                        {
                            if (selectedOptionsHS.Contains(author.Id.ToString()))
                            {
                                if (!bookAuthors.Contains(author.Id))
                                {
                                    bookToUpdate.Authors.Add(author);
                                }
                            }
                            else
                            {
                                if (bookAuthors.Contains(author.Id))
                                {
                                    bookToUpdate.Authors.Remove(author);
                                }
                            }
                        }
                    }

                    if(selectedGenres == null)
                    {
                        bookToUpdate.Genres = new List<Genre>();
                    }
                    else
                    {
                        var selectedOptionsHS = new HashSet<string>(selectedGenres);
                        var bookGenres = new HashSet<int>(bookToUpdate.Genres.Select(a => a.Id));
                        foreach (var genre in db.Genres)
                        {
                            if (selectedOptionsHS.Contains(genre.Id.ToString()))
                            {
                                if (!bookGenres.Contains(genre.Id))
                                {
                                    bookToUpdate.Genres.Add(genre);
                                }
                            }
                            else
                            {
                                if (bookGenres.Contains(genre.Id))
                                {
                                    bookToUpdate.Genres.Remove(genre);
                                }
                            }
                        }
                    }

                    db.Entry(bookToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }
            return View(bookToUpdate);*/
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GetSelectedAuthors(int? id)
        {
            var book = db.Books.Find(id);
            var allAuthors = db.Authors;
            var bookAuthors = new HashSet<int>(book.Authors.Select(a => a.Id));
            var viewModelAvailable = new List<Author>();
            var viewModelSelected = new List<Author>();
            foreach (var author in allAuthors)
            {
                if (bookAuthors.Contains(author.Id))
                {
                    viewModelSelected.Add(new Author
                    {
                        Id = author.Id,
                        Name = author.Name
                    });
                }
                else
                {
                    viewModelAvailable.Add(new Author
                    {
                        Id = author.Id,
                        Name = author.Name
                    });
                }
            }

            //ViewBag.SelectedAuthors = new MultiSelectList(viewModelSelected, "Id", "Name", bookAuthors.ToArray());
            ViewBag.AvailableAuthors = new MultiSelectList(viewModelAvailable, "Id", "Name", bookAuthors.ToArray());
        }

        private void GetGenres(int? id)
        {
            var book = db.Books.Find(id);
            var allGenres = db.Genres;
            var bookGenres = new HashSet<int>(book.Genres.Select(a => a.Id));
            var viewModelAvailable = new List<Genre>();
            var viewModelSelected = new List<Genre>();

            foreach (var genre in allGenres)
            {
                if (bookGenres.Contains(genre.Id))
                {
                    viewModelSelected.Add(new Genre
                    {
                        Id = genre.Id,
                        Description = genre.Description
                    });
                }
                else
                {
                    viewModelAvailable.Add(new Genre
                    {
                        Id = genre.Id,
                        Description = genre.Description
                    });
                }
            }

            ViewBag.SelectedGenres = new MultiSelectList(viewModelSelected, "Id", "Description", bookGenres.ToArray());
            ViewBag.AvailableGenres = new MultiSelectList(viewModelAvailable, "Id", "Description", bookGenres.ToArray());
        }

        public PartialViewResult Display()
        {
            List<Book> books = db.Books.ToList();
            string search = this.Request.QueryString["search"];
            string genre = this.Request.QueryString["genre"];
            if(genre != null)
            {
                try
                {
                    int gen = Int32.Parse(genre);
                    books = books.Where(b => b.Genres.Select(g => g.Id).Contains(gen)).ToList();
                }
                catch(Exception e)
                {

                }
            }
            if (search != null)
            {
                books = books.Where(b => b.title.Contains(search)).ToList();
            }
            return PartialView(books);
        }

    }
}
