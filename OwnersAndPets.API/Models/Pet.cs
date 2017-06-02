using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OwnersAndPets.API.Models
{
    public class Pet
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [RegularExpression(@"^[а-яА-ЯёЁa-zA-Z0-9]+$", ErrorMessage = "Use only letters and numbers!")]
        [MaxLength(25, ErrorMessage = "Max length 25 chars")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Owner is required!")]
        public long OwnerId { get; set; }

        //public Owner Owner { get; set; }
    }
}