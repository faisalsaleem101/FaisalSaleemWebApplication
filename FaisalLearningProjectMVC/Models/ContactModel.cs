using FaisalLearningProjectMVC.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FaisalLearningProjectMVC.Models
{
    public class ContactModel
    {
        [Required]
        public string Name { get; set; }

        public string Subject { get; set; }

        [GenericRequired]
        [EmailAddress]
        public string Email { get; set; }

        [GenericRequired]
        public string Message { get; set; }
    }
}
