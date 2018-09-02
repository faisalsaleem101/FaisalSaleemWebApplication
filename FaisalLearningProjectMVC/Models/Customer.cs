using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaisalLearningProjectMVC.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        //public string Address { get; set; }
        //public string City { get; set; }
        //public string Region { get; set; }
        //public string PostCode { get; set; }
        //public string County { get; set; }
        //public string Phone { get; set; }
        //public string Fax { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
