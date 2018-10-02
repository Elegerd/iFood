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
        public int UserId { get; set; }

        public string Email { get; set; }

        [DisplayName("Пароль")]

        public string Password { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DisplayName("Рост")]
        public int Height { get; set; }

        [DisplayName("Вес")]
        public int Weight { get; set; }

        [DisplayName("Пол")]
        public bool? Sex { get; set; }
    }
}