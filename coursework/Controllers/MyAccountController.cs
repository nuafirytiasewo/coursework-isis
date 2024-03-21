using coursework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace coursework.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: MyAccount
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //метод входа в систему 
        public ActionResult Login(Login user) 
        { 
            using (ADOModelDB db=new ADOModelDB())
            {
                //ищем в базе данных такие же значения логина и пароля
                var result = db.Employees.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
                //если нашли то
                if (result != null)
                {
                    //запись в сессию(куки) переменной с логином и ролью
                    Session["UserId"] = result.EmployeeID;
                    Session["Username"] = result.Username;
                    Session["RoleId"] = result.RoleID;
                    
                    // Создаем запись в таблице журнала
                    LoginLogs log = new LoginLogs
                    {
                        EmployeeID = result.EmployeeID,
                        Username = result.Username,
                        LoginTime = DateTime.Now
                    };
                    db.LoginLogs.Add(log);
                    db.SaveChanges();
                    
                    //перенаправление на домашнюю страницу
                    return RedirectToAction("Index", "Home");
                }
                //если не нашли то выводим ошибку
                else 
                { 
                    TempData["msg"] = "Неправильный логин или пароль!"; 
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            //очищаем куки
            Session.Clear();
            //перенаправление на страницу с входом
            return RedirectToAction("Login", "MyAccount");
        }
    }
}