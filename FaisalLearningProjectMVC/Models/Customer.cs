using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FaisalLearningProjectMVC.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Display(Name = "Full Name")]
        public string ContactName { get; set; }
        [Display(Name = "Company")]
        public string CompanyName { get; set; }
        [Display(Name = "Job Title")]
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        [Display(Name = "Post Code")]
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
