using coursework.Controllers.Helpers;
using coursework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace coursework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Домашняя страница";

            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, 5))
            {
                return RedirectToAction("Login", "MyAccount");
            }

            return View();
        }
    }
}
