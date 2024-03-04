using coursework.Controllers.Helpers;
using coursework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace coursework.Controllers
{
    public class SelectionsController : Controller
    {
        private readonly ADOModelDB db = new ADOModelDB();
        // GET: Selections
        public ActionResult Index1()
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            // 1 выборка: Получение топ-5 клиентов, которые оставили наибольшее количество заявок
            var top5ClientsByRequests = db.Requests
                .GroupBy(r => r.ClientID)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Join(db.Clients,
                      r => r.Key, // ClientID из группировки
                      c => c.ClientID, // ClientID из коллекции клиентов
                      (group, client) => new ModelForSelection_1 { 
                          LastName = client.LastName, 
                          FirstName = client.FirstName, 
                          Patronymic = client.Patronymic, 
                          Count = group.Count() 
                      })
                .ToList();

            return View(top5ClientsByRequests);
        }
    }
}