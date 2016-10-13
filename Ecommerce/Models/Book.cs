using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [DisplayName("Title")]
        public string title { get; set; }
        [Required(ErrorMessage = "Publish Date is required.")]
        [DisplayName("Publish Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime publishDate { get; set; }
        [Required(ErrorMessage = "ISBN is required.")]
        [Index("IX_UniqueISBN", IsUnique = true)]
        public int ISBN { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Invalid price.")]
        public double Price { get; set; }
        [DisplayName("Synopsis")]
        public string Synopsis { get; set; }

        public string CoverImagePath { get; set; }
        public virtual ICollection<Author> Authors { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
    }
}