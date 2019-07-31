using System.Collections.Generic;

namespace FaisalLearningProjectMVC.Models
{
    public class Shipper
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
