namespace ServiceSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.Users;
    using Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class UsersController : BaseController
    {
        private DbContext dbContext;

        public UsersController(DbContext context)
        {
            this.dbContext = context;
        }

        public ActionResult Index()
        {
            var userStore = new UserStore<User>(this.dbContext);
            var userManager = new UserManager<User>(userStore);
            var users = userManager.Users.ToList();

            var userModels = new List<UserInRoleViewModel>();

            string role = GlobalConstants.EngineerRoleName;
            foreach (User user in users)
            {
                var userModel = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    IsInRole = userManager.IsInRole(user.Id, role)
                };

                userModels.Add(userModel);
            }

            this.ViewBag.RoleName = role;

            return this.View(userModels);
        }

        public ActionResult ExcludeUser(string id)
        {
            var userStore = new UserStore<User>(this.dbContext);
            var userManager = new UserManager<User>(userStore);
            try
            {
                userManager.RemoveFromRole(id, GlobalConstants.EngineerRoleName);
            }
            catch (Exception e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Index");
            }

            this.TempData["Success"] = "User successfully excluded from the role";
            return this.RedirectToAction("Index");
        }

        public ActionResult IncludeUser(string id)
        {
            var userStore = new UserStore<User>(this.dbContext);
            var userManager = new UserManager<User>(userStore);
            try
            {
                userManager.AddToRole(id, GlobalConstants.EngineerRoleName);
            }
            catch (Exception e)
            {
                this.TempData["Error"] = e.Message;
                return this.RedirectToAction("Index");
            }

            this.TempData["Success"] = "User successfully included in the role";
            return this.RedirectToAction("Index");
        }
    }
}