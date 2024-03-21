using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using coursework.Controllers.Helpers;
using coursework.Models;

namespace coursework.Controllers.Admin
{
    public class LoginLogsController : Controller
    {
        private ADOModelDB db = new ADOModelDB();

        // GET: LoginLogs
        public ActionResult Index()
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, 1))
            {
                return RedirectToAction("Login", "MyAccount");
            }

            var loginLogs = db.LoginLogs.Include(l => l.Employees);
            return View(loginLogs.ToList());
        }

        // GET: LoginLogs/Details/5
        public ActionResult Details(int? id)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, 1))
            {
                return RedirectToAction("Login", "MyAccount");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginLogs loginLogs = db.LoginLogs.Find(id);
            if (loginLogs == null)
            {
                return HttpNotFound();
            }
            return View(loginLogs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
