﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.Areas.Manage.Models
{
    public class CreateRoleMDto
    {
        [Required(ErrorMessage = "وارد کردن نام نقش ضروری است.")]
        public string slug { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
