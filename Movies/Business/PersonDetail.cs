﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Movies.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.Business
{
    public class PersonDetail
    {
        public Guid PersonId { get; set; }
        public string? Image { get; set; }
        public string? NamePerson { get; set; }
        [Required]
        public string? NationId { get; set; }
        public string? NationName { get; set; }
        [Required]
        public string? Role { get; set; }
        public string? DoB { get; set; }
    }
}
