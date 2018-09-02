using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaisalLearningProjectMVC.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int CustomerId { get; set; }
        //public int EmployeeId { get; set; }
        //public int ShipperId { get; set; }
        //public DateTime OrderDate { get; set; }
        //public DateTime RequiredDate { get; set; }
        //public DateTime ShipDate { get; set; }
        //public Decimal Freight { get; set; }
        //public string ShipName { get; set; }
        //public string ShipAddress { get; set; }
        //public string ShipCity { get; set; }
        //public string ShipRegion { get; set; }
        //public string ShipPostalCode { get; set; }
        //public string ShipCountry { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
