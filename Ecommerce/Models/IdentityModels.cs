using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;

namespace Ecommerce.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [DisplayName("Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Ecommerce.Models.Genre> Genres { get; set; }

        public System.Data.Entity.DbSet<Ecommerce.Models.Book> Books { get; set; }

        public System.Data.Entity.DbSet<Ecommerce.Models.Author> Authors { get; set; }

        public System.Data.Entity.DbSet<Ecommerce.Models.Cart> Carts { get; set; }

        public System.Data.Entity.DbSet<Ecommerce.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<Ecommerce.Models.OrderItem> OrderItems { get; set; }
    }
}