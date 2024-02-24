using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace coursework.Models
{
    public class Login
    {
        //это модель для формы (сюда попадают введенные логин и пароль, чтобы потом сверить их с значениями в бд)
        [Required]
        public string Username {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}