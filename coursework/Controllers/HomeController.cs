using coursework.Models;
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
            ViewBag.Title = "Домашняя страница";

            // Проверяем, аутентифицирован ли пользователь
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "MyAccount");
            }

            // Проверяем роль пользователя
            using (ADOModelDB db = new ADOModelDB())
            {
                var userId = (int)Session["UserId"]; // Получаем идентификатор пользователя из сессии
                var userRole = db.Users.Find(userId)?.RoleId; // Находим пользователя в базе и получаем его роль

                if (userRole != null)
                {
                    if (userRole == 1) // Предполагая, что 1 - это роль администратора
                    {
                        // Разрешаем доступ к админской части страницы
                        ViewBag.Message = "Добро пожаловать, Администратор!";
                    }
                    else
                    {
                        // Ограничиваем доступ пользователям с другими ролями
                        ViewBag.Message = "У вас недостаточно прав для просмотра этой страницы.";
                    }
                }
                else
                {
                    // Обработка ситуации, если роль не найдена
                    ViewBag.Message = "Ошибка при определении роли пользователя.";
                }
            }

            return View();
        }
    }
}
