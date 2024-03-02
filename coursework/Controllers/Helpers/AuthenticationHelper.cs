using coursework.Models;
using System.Web;

namespace coursework.Controllers.Helpers
{
    public class AuthenticationHelper
    {
        public static bool CheckAuthentication(HttpSessionStateBase session, dynamic ViewBag, bool adminPanel)
        {
            // Проверяем, аутентифицирован ли пользователь
            if (session["username"] == null)
            {
                return false;
            }
            using (ADOModelDB db = new ADOModelDB())
            {
                var userId = (int)session["UserId"]; // Получаем идентификатор пользователя из сессии
                var userRole = db.Users.Find(userId)?.RoleId; // Находим пользователя в базе и получаем его роль
                var userName = (string)session["username"]; // Получаем идентификатор пользователя и его роль из сессии
                
                if (userRole != null)
                {
                    var roleName = db.Roles.Find(userRole)?.Name; // Получаем название роли из базы данных
                    // Разрешаем доступ к админской части страницы для администратора
                    if (adminPanel && userRole == 1)
                    {
                        ViewBag.Message = $"Добро пожаловать, {roleName} ({userName})!";
                    }
                    // Ограничиваем доступ пользователям с другими ролями в админской части
                    else if (adminPanel)
                    {
                        ViewBag.Message = $"{roleName} ({userName}), Вам доступ запрещен!";
                    }
                    // Отображаем приветствие для общего доступа
                    else
                    {
                        ViewBag.Message = $"Добро пожаловать, {roleName} ({userName})!";
                    }
                }
                else
                {
                    // Обработка ситуации, если роль не найдена
                    ViewBag.Message = "Ошибка при определении роли пользователя.";
                    return false;
                }

                ViewBag.userRole = userRole;
            }

            return true;
        }
    }
}
