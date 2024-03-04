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
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, true))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            var loginLogs = db.LoginLogs.Include(l => l.Users);
            return View(loginLogs.ToList());
        }

        // GET: LoginLogs/Details/5
        public ActionResult Details(int? id)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, true))
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

        // GET: LoginLogs/Create
        public ActionResult Create()
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, true))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: LoginLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Username,LoginTime")] LoginLogs loginLogs)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, true))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            if (ModelState.IsValid)
            {
                db.LoginLogs.Add(loginLogs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", loginLogs.UserId);
            return View(loginLogs);
        }

        // GET: LoginLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, true))
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
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", loginLogs.UserId);
            return View(loginLogs);
        }

        // POST: LoginLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Username,LoginTime")] LoginLogs loginLogs)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, true))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            if (ModelState.IsValid)
            {
                db.Entry(loginLogs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username", loginLogs.UserId);
            return View(loginLogs);
        }

        // GET: LoginLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, true))
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

        // POST: LoginLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, true))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            LoginLogs loginLogs = db.LoginLogs.Find(id);
            db.LoginLogs.Remove(loginLogs);
            db.SaveChanges();
            return RedirectToAction("Index");
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
