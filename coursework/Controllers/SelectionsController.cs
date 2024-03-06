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

        public ActionResult Index2()
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            // 2 выборка: Выбор информации о сотруднике с наивысшей зарплатой
            var highestSalary = db.Employees.Max(e => e.Salary);
            var employee = db.Employees.FirstOrDefault(e => e.Salary == highestSalary);
            var highestPaidEmployee = employee != null ? new[] { new ModelForSelection_2
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Salary = employee.Salary
            } } : new ModelForSelection_2[0];

            return View(highestPaidEmployee);

        }
        //3. Получение топ-3 сотрудников, которые выполнили наибольшее количество заявок в определенном месяце:
        public ActionResult Index3(DateTime? specificMonth = null)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            var top3EmployeesByRequests = db.Requests
                    .Where(r => r.OpenDate.Value.Year == specificMonth.Value.Year && r.OpenDate.Value.Month == specificMonth.Value.Month)
                    .GroupBy(r => r.EmployeeID)
                    .OrderByDescending(g => g.Count())
                    .Take(3)
                    .Select(g => new
                    {
                        EmployeeId = g.Key,
                        Count = g.Count(),
                        Employee = db.Employees.FirstOrDefault(e => e.EmployeeID == g.Key)
                    })
                    .Select(x => new ModelForSelection3
                    {
                        LastName = x.Employee.LastName,
                        FirstName = x.Employee.FirstName,
                        Patronymic = x.Employee.Patronymic,
                        Count = x.Count,
                    });

            return View(top3EmployeesByRequests.ToList());
        }

        // 4. Выборка всех заявок, у которых статус "Обработка", с информацией о клиентах, услугах и сотрудниках
        public ActionResult Index4()
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            var processingRequestsInfo = db.Requests
                    .Where(r => r.Status == "Обработка")
                    .Join(db.Clients, r => r.ClientID, c => c.ClientID, (r, c) => new { Request = r, Client = c })
                    .Join(db.Services, rc => rc.Request.ServiceID, s => s.ServiceID, (rc, s) => new { rc.Request, rc.Client, Service = s })
                    .Join(db.Employees, rcs => rcs.Request.EmployeeID, e => e.EmployeeID, (rcs, e) => new { rcs.Request, rcs.Client, rcs.Service, Employee = e })
                    .Select(x => new ModelForSelection4
                    {
                        RequestID = x.Request.RequestID,
                        ClientLastName = x.Client.LastName,
                        ClientFirstName = x.Client.FirstName,
                        ClientPatronymic = x.Client.Patronymic,
                        ServiceName = x.Service.Name,
                        EmployeeLastName = x.Employee.LastName,
                        EmployeeFirstllName = x.Employee.FirstName,
                        EmployeePatronymic = x.Employee.Patronymic,
                        Status = x.Request.Status
                    })
                    .ToList();
            return View(processingRequestsInfo);
        }

        // 5. Выборка должностей и количества сотрудников на каждой должности
        public ActionResult Index5()
        {
            // Проверяем, аутентифицирован ли пользователь
            if (!AuthenticationHelper.CheckAuthentication(Session, ViewBag, false))
            {
                return RedirectToAction("Login", "MyAccount");
            }
            var positionEmployeeCount = db.Positions
                .GroupJoin(db.Employees, pos => pos.Id, emp => emp.PositionID, (pos, emp) => new { Position = pos, Employees = emp })
                .Select(x => new ModelForSelection5
                {
                    PositionTitle = x.Position.Title,
                    EmployeeCount = x.Employees.Count()
                })
                .ToList();

            return View(positionEmployeeCount);
        }

    }
}