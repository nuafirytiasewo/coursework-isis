using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace coursework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /* аунтифекация (способ на стороне клиента) */
            try
            {
                //если в куки нет ничего 
                if (Session["Username"] == null)
                {
                    //перенаправление на домашнюю страницу
                    return RedirectToAction("Login", "MyAccount");
                }
                else
                {
                    
                }
            }
            catch (Exception)
            {
                //перенаправление на домашнюю страницу
                return RedirectToAction("Login", "MyAccount");
                //throw;
            }

            ViewBag.Title = "Домашняя страница";

            return View();
        }
    }
}
