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
    public class UsersController : Controller
    {
        private ADOModelDB db = new ADOModelDB();

        // GET: Users
        public ActionResult Index()
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, true))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            
            var users = db.Users.Include(u => u.Roles);
            return View(users.ToList());
        }

        // GET: Users/Details/5
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
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, true))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,RoleId,LastName,FirstName,Patronymic")] Users users)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, true))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", users.RoleId);
            return View(users);
        }

        // GET: Users/Edit/5
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
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", users.RoleId);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,RoleId,LastName,FirstName,Patronymic")] Users users)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, true))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", users.RoleId);
            return View(users);
        }

        // GET: Users/Delete/5
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
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, true))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
