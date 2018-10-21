using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lifestyle.Models
{
    public class User
    {
        [DisplayName("UserId")]
        public int UserId { get; set; }

        [DisplayName("Почтовый ящик")]
        [Required(ErrorMessage = "Пожалуйста, введите Ваш адрес электронной почты")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Пожалуйста, введите действительный адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите пароль")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Пожалуйста, введите дату рождения")]
        [DisplayName("Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DisplayName("Рост")]
        public int ?Height { get; set; }

        [DisplayName("Вес")]
        public int ?Weight { get; set; }

        [DisplayName("Пол")]
        public bool? Sex { get; set; }
    }
}