using coursework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
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
                var result = db.Users.Where(x => x.Username == user.Username && x.Password == user.Password);
                //если нашли то
                if (result.Count() != 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                //если не нашли то выводим ошибку
                else 
                { 
                    TempData["msg"] = "Неправильно ввели"; 
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}