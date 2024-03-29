﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReastosMenu.Entities.Identity;
using System.Reflection.Emit;

namespace ReastosMenu.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var hasher = new PasswordHasher<ApplicationUser>();

            //create a role
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Admin", NormalizedName = "ADMIN" });

            //create a user
            builder.Entity<ApplicationUser>().HasData(
               new ApplicationUser
               {
                   Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                   UserName = "restomenuadmin",
                   NormalizedUserName = "RESTOMENUADMIN",
                   PasswordHash = hasher.HashPassword(null, "Pass123@."),
                   FirstName = "Admin",
                   LastName = "Admin"

               }
               );

            //asign admin role to the user we created
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"

            });
        }
    }
}
