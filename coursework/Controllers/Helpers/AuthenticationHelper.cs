using coursework.Models;
using System.Web;

namespace coursework.Controllers.Helpers
{
    public class AuthenticationHelper
    {
        public static bool CheckAuthentication(HttpSessionStateBase session, dynamic ViewBag, int MaxRole)
        {
            bool access = false;
            // Проверяем, аутентифицирован ли пользователь
            if (session["username"] == null)
            {
                return false;
            }
            using (ADOModelDB db = new ADOModelDB())
            {
                var userId = (int)session["UserId"]; // Получаем идентификатор пользователя из сессии
                var userRole = db.Employees.Find(userId)?.RoleID; // Находим пользователя в базе и получаем его роль
                var userName = (string)session["Username"]; // Получаем идентификатор пользователя и его роль из сессии
                
                if (userRole != null)
                {
                    var roleName = db.Roles.Find(userRole)?.Name; // Получаем название роли из базы данных
                    // Максимальная роль для действий должна быть меньше или равна роли пользователя для доступа в систему
                    if (MaxRole >= userRole)
                    {
                        ViewBag.Message = $"Добро пожаловать, {roleName} ({userName})!";
                        access = true;
                    }
                    // Ограничиваем доступ пользователям если роль пользователя больше, чем нужна
                    else if (MaxRole < userRole)
                    {
                        ViewBag.Message = $"{roleName} ({userName}), Вам доступ запрещен!";
                    }
                    // Отображаем приветствие для общего доступа
                    else
                    {
                        ViewBag.Message = $"Добро пожаловать, {roleName} ({userName})!";
                        access = true;
                    }
                }
                else
                {
                    // Обработка ситуации, если роль не найдена
                    ViewBag.Message = "Ошибка при определении роли пользователя.";
                    return false;
                }
                //доступ для вьюх
                ViewBag.access = access;
                ViewBag.userRole = userRole;
            }

            return true;
        }
    }
}
