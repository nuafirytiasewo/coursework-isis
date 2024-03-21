using coursework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using coursework.Controllers.Helpers;


namespace coursework.Controllers.Admin
{
    public class AdministrationController : Controller
    {
        // GET: Administration
        public ActionResult Index()
        {
            ViewBag.Title = "Админ панель";

            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, 1))
            {
                return RedirectToAction("Login", "MyAccount");
            }

            return View();
        }
    }
}