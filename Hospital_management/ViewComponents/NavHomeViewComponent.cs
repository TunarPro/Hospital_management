using Hospital_management.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Hospital_management.ViewComponents
{
    public class NavHomeViewComponent : ViewComponent
    {
        private readonly Hospital_DbContext _db;

        public NavHomeViewComponent(Hospital_DbContext _db)
        {
            this._db = _db;
        }

        public IViewComponentResult Invoke()
        {
            var userId = Convert.ToInt32(Request.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "Id").Value);
            var user = _db.Users.SingleOrDefault(x => x.UsersId == userId);

            return View(user);
        }
    }
}
