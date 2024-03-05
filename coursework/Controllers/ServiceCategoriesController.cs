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

namespace coursework.Controllers
{
    public class ServiceCategoriesController : Controller
    {
        private ADOModelDB db = new ADOModelDB();

        // GET: ServiceCategories
        public ActionResult Index()
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            
            return View(db.ServiceCategories.ToList());
        }

        // GET: ServiceCategories/Details/5
        public ActionResult Details(int? id)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCategories serviceCategories = db.ServiceCategories.Find(id);
            if (serviceCategories == null)
            {
                return HttpNotFound();
            }
            return View(serviceCategories);
        }

        // GET: ServiceCategories/Create
        public ActionResult Create()
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            return View();
        }

        // POST: ServiceCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,Name,Description")] ServiceCategories serviceCategories)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            if (ModelState.IsValid)
            {
                db.ServiceCategories.Add(serviceCategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceCategories);
        }

        // GET: ServiceCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCategories serviceCategories = db.ServiceCategories.Find(id);
            if (serviceCategories == null)
            {
                return HttpNotFound();
            }
            return View(serviceCategories);
        }

        // POST: ServiceCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,Name,Description")] ServiceCategories serviceCategories)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            if (ModelState.IsValid)
            {
                db.Entry(serviceCategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceCategories);
        }

        // GET: ServiceCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCategories serviceCategories = db.ServiceCategories.Find(id);
            if (serviceCategories == null)
            {
                return HttpNotFound();
            }
            return View(serviceCategories);
        }

        // POST: ServiceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            ServiceCategories serviceCategories = db.ServiceCategories.Find(id);
            db.ServiceCategories.Remove(serviceCategories);
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
