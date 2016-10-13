using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.ViewModel
{
    public class BookViewModel
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public Book book { get; set; }

        public List<Author> SelectedAuthors { get; set; }

        public List<Author> AvailableAuthors { get; set; }
        public List<Genre> SelectedGenres { get; set; }

        public List<Genre> AvailableGenres { get; set; }

        public BookViewModel()
        {
            AvailableAuthors = db.Authors.ToList();
            AvailableGenres = db.Genres.ToList();
        }

        public BookViewModel(int? id)
        {
            book = db.Books.Find(id);

            AvailableAuthors = db.Authors.ToList();

            foreach(Author a in book.Authors)
            {
                if(AvailableAuthors.Exists(e => e.Id == a.Id))
                {
                    AvailableAuthors.Remove(a);
                }
            }

            AvailableGenres = db.Genres.ToList();
            foreach(Genre g in book.Genres)
            {
                if(AvailableGenres.Exists(a => a.Id == g.Id))
                {
                    AvailableGenres.Remove(g);
                }
            }
        }
    }
}