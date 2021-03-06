﻿using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceSystem.Data.Models;
using ServiceSystem.Infrastructure;

namespace ServiceSystem.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            const string AdministratorUserName = "admin@service.com";
            const string AdministratorPassword = "admin";

            if (!context.Roles.Any())
            {
                // Create admin role
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
                roleManager.Create(role);

                // Create Engineers role
                role = new IdentityRole { Name = GlobalConstants.EngineerRoleName };
                roleManager.Create(role);

                // Create admin user
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                userManager.PasswordValidator = new PasswordValidator() { RequiredLength = 5 };
                var user = new User { UserName = AdministratorUserName, Email = AdministratorUserName };
                var result = userManager.Create(user, AdministratorPassword);

                // Assign user to admin role
                userManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
