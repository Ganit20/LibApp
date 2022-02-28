using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Customer>>();

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.MembershipTypes.Any())
                {
                    context.MembershipTypes.AddRange(
               new MembershipType
               {
                   Id = 1,
                   Name = "Pay as You Go",
                   SignUpFee = 0,
                   DurationInMonths = 0,
                   DiscountRate = 0
               },
               new MembershipType
               {
                   Id = 2,
                   Name = "Monthly",
                   SignUpFee = 30,
                   DurationInMonths = 1,
                   DiscountRate = 10
               },
               new MembershipType
               {
                   Id = 3,
                   Name = "Quaterly",
                   SignUpFee = 90,
                   DurationInMonths = 3,
                   DiscountRate = 15
               },
               new MembershipType
               {
                   Id = 4,
                   Name = "Yearly",
                   SignUpFee = 300,
                   DurationInMonths = 12,
                   DiscountRate = 20
               }
           );
                }
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
              new IdentityRole
              {
                  Id = Guid.NewGuid().ToString(),
                  Name = "User",
                  NormalizedName = "user"
              },
              new IdentityRole
              {
                  Id = Guid.NewGuid().ToString(),
                  Name = "StoreManager",
                  NormalizedName = "storemanager"
              },
              new IdentityRole
              {
                  Id = Guid.NewGuid().ToString(),
                  Name = "Owner",
                  NormalizedName = "owner"
              }
          );
                }
                if (!context.Genre.Any())
                {
                    context.Genre.AddRange(
                    new Genre
                    {
                        Id = 1,
                        Name = "Fantasy"
                    },
                    new Genre
                    {
                        Id = 2,
                        Name = "Romance"
                    },
                    new Genre
                    {
                        Id = 3,
                        Name = "Sci-Fi"
                    },
                    new Genre
                    {
                        Id = 4,
                        Name = "Criminal"
                    },
                    new Genre
                    {
                        Id = 5,
                        Name = "Biography"
                    },
                    new Genre
                    {
                        Id = 6,
                        Name = "Horror"
                    },
                    new Genre
                    {
                        Id = 7,
                        Name = "Mystery"
                    }

                );
                }
                if (!context.Books.Any())
                {
                    context.Books.AddRange(
               new Book
               {
                   GenreId = 1,
                   Name = "AAA",
                   AuthorName = "AAA",
                   ReleaseDate = DateTime.Parse("01/01/2022"),
                   DateAdded = DateTime.Now,
                   NumberInStock = 10
               },
               new Book
               {
                   GenreId = 3,
                   Name = "BBB",
                   AuthorName = "BBB",
                   ReleaseDate = DateTime.Parse("01/01/2021"),
                   DateAdded = DateTime.Now,
                   NumberInStock = 10
               },
               new Book
               {
                   GenreId = 2,
                   Name = "CCC",
                   AuthorName = "CCC",
                   ReleaseDate = DateTime.Parse("01/01/2019"),
                   DateAdded = DateTime.Now,
                   NumberInStock = 10
               },
               new Book
               {
                   GenreId = 4,
                   Name = "DDD",
                   AuthorName = "DDD",
                   ReleaseDate = DateTime.Parse("01/01/2018"),
                   DateAdded = DateTime.Now,
                   NumberInStock = 10
               });
                }
                   
                    context.SaveChanges();
                }

            using (var context = new ApplicationDbContext(
           serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Customers.Any())
                {
                    var hash = new PasswordHasher<Customer>();

                    var c1 = new Customer
                    {
                        Name = "User",
                        Email = "User@gmail.com",
                        NormalizedEmail = "user@gmail.com",
                        UserName = "User@gmail.com",
                        NormalizedUserName = "User@gmail.com",
                        MembershipTypeId = 1,
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        PasswordHash = hash.HashPassword(null, "1234")
                    };
                    userManager.CreateAsync(c1).Wait();
                    userManager.AddToRoleAsync(c1, "User").Wait();


                    var c2 = new Customer
                    {
                        Name = "StoreManager",
                        Email = "StoreManager",
                        NormalizedEmail = "storemanager@gmail.com",
                        UserName = "StoreManager@gmail.com",
                        NormalizedUserName = "StoreManager@gmail.com",
                        MembershipTypeId = 1,
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        PasswordHash = hash.HashPassword(null, "1234")
                    };
                    userManager.CreateAsync(c2).Wait();
                    userManager.AddToRoleAsync(c2, "StoreManager").Wait();


                    var c3 = new Customer
                    {
                        Name = "Owner",
                        Email = "Owner@gmail.com",
                        NormalizedEmail = "Owner@gmail.com",
                        UserName = "Owner@gmail.com",
                        NormalizedUserName = "Owner@gmail.com",
                        MembershipTypeId = 1,
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        PasswordHash = hash.HashPassword(null, "1234")
                    };
                    userManager.CreateAsync(c3).Wait();
                    userManager.AddToRoleAsync(c3, "Owner").Wait();


                    context.SaveChanges();
                }
            }
                using (var context = new ApplicationDbContext(
              serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
                {
                    if (!context.Comments.Any())
                    {
                        context.Comments.AddRange(
                      new Comment
                      {
                          Added = DateTime.Now,
                          CustomerId = context.Customers.FirstOrDefault()?.Id,
                          BookId = 3,
                          Content = "Comment",
                          Id = Guid.NewGuid().ToString(),
                          IsLike = true
                      });
                    context.SaveChanges();
                }
            }
        }
    }
}