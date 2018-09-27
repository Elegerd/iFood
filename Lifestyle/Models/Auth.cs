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
        [DisplayName("Логин")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }


    public class Authentication
    {
        [DisplayName("Логин")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Контрольный пароль")]
        public string ControlPassword { set; get; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DisplayName("Пол")]
        public int Sex { get; set; }
    }
}