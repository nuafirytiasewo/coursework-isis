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
    public class RequestsController : Controller
    {
        private ADOModelDB db = new ADOModelDB();

        // GET: Requests
        public ActionResult Index()
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            
            var requests = db.Requests.Include(r => r.Clients).Include(r => r.Employees).Include(r => r.Services);
            return View(requests.ToList());
        }

        // GET: Requests/Details/5
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
            Requests requests = db.Requests.Find(id);
            if (requests == null)
            {
                return HttpNotFound();
            }
            return View(requests);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "LastName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName");
            ViewBag.ServiceID = new SelectList(db.Services, "ServiceID", "Name");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestID,OpenDate,Status,Description,ClientID,ServiceID,EmployeeID")] Requests requests)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            if (ModelState.IsValid)
            {
                db.Requests.Add(requests);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "LastName", requests.ClientID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", requests.EmployeeID);
            ViewBag.ServiceID = new SelectList(db.Services, "ServiceID", "Name", requests.ServiceID);
            return View(requests);
        }

        // GET: Requests/Edit/5
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
            Requests requests = db.Requests.Find(id);
            if (requests == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "LastName", requests.ClientID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", requests.EmployeeID);
            ViewBag.ServiceID = new SelectList(db.Services, "ServiceID", "Name", requests.ServiceID);
            return View(requests);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestID,OpenDate,Status,Description,ClientID,ServiceID,EmployeeID")] Requests requests)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            if (ModelState.IsValid)
            {
                db.Entry(requests).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "LastName", requests.ClientID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", requests.EmployeeID);
            ViewBag.ServiceID = new SelectList(db.Services, "ServiceID", "Name", requests.ServiceID);
            return View(requests);
        }

        // GET: Requests/Delete/5
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
            Requests requests = db.Requests.Find(id);
            if (requests == null)
            {
                return HttpNotFound();
            }
            return View(requests);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            
            Requests requests = db.Requests.Find(id);
            db.Requests.Remove(requests);
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
