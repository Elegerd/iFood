using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lifestyle.Models
{
    public class Authorization
    {
        [DisplayName("Почтовый ящик")]
        [Required(ErrorMessage = "Пожалуйста, введите Ваш адрес электронной почты")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Пожалуйста, введите действительный адрес электронной почты")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите пароль")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }


    public class Authentication
    {
        [DisplayName("Почтовый ящик")]
        [Required(ErrorMessage = "Пожалуйста, введите Ваш адрес электронной почты")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Пожалуйста, введите действительный адрес электронной почты")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите пароль")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите контрольный пароль")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Контрольный пароль")]
        public string ControlPassword { set; get; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Пожалуйста, введите дату рождения")]
        [DisplayName("Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}