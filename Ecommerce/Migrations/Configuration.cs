namespace Ecommerce.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Ecommerce.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Ecommerce.Models.ApplicationDbContext";
        }

        protected override void Seed(Ecommerce.Models.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Administrator" },
                new IdentityRole { Name = "Customer"});

            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("P@ssw0rd");
            ApplicationUser user = new ApplicationUser
                                    {
                                        UserName = "admin@mywebsite.com",
                                        PasswordHash = password,
                                        PhoneNumber = "0000000",
                                        FirstName = "Administrator",
                                        DateOfBirth = DateTime.Now,
                                        EmailConfirmed = true
                                    };
            context.Users.AddOrUpdate(u => u.UserName, user);
            user.Roles.Add(new IdentityUserRole
            {
                RoleId = context.Roles.Where(r => r.Name == "Administrator").First().Id,
                UserId = user.Id
            });
            context.Users.AddOrUpdate(user);

            context.Genres.AddOrUpdate(g => g.Description,
                new Genre { Description = "Science fiction" },
                new Genre { Description = "Satire" },
                new Genre { Description = "Action and Adventure" },
                new Genre { Description = "Romance" },
                new Genre { Description = "Mystery" },
                new Genre { Description = "Horror" },
                new Genre { Description = "Self help" },
                new Genre { Description = "Health" },
                new Genre { Description = "Guide" },
                new Genre { Description = "Travel" },
                new Genre { Description = "Children's" },
                new Genre { Description = "Religion" },
                new Genre { Description = "Spirituality & New Age" },
                new Genre { Description = "Science" },
                new Genre { Description = "Math" },
                new Genre { Description = "Anthology" },
                new Genre { Description = "Poetry" },
                new Genre { Description = "Encyclopedias" },
                new Genre { Description = "Dictionaries" },
                new Genre { Description = "Comics" },
                new Genre { Description = "Art" },
                new Genre { Description = "Cookbooks" },
                new Genre { Description = "Diaries" },
                new Genre { Description = "Journals" },
                new Genre { Description = "Prayer books" },
                new Genre { Description = "Series" },
                new Genre { Description = "Trilogy" },
                new Genre { Description = "Biographies" },
                new Genre { Description = "Autobiographies" },
                new Genre { Description = "Fantasy" }
                );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
