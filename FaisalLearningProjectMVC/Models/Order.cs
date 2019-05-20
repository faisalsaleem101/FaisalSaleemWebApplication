using System;
using System.ComponentModel.DataAnnotations;

namespace FaisalLearningProjectMVC.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public int ShipperId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Required Date")]
        public DateTime RequiredDate { get; set; }
        [Display(Name = "Shipped Date")]
        public DateTime? ShippedDate { get; set; }
        public Decimal Freight { get; set; }
        [Display(Name = "Ship Name")]
        public string ShipName { get; set; }

        [Display(Name = "Ship Adrress")]
        public string ShipAddress { get; set; }
        [Display(Name = "Ship City")]
        public string ShipCity { get; set; }
        [Display(Name = "Ship Region")]
        public string ShipRegion { get; set; }

        [Display(Name = "Ship Post Code")]

        public string ShipPostalCode { get; set; }

        [Display(Name = "Ship Country")]

        public string ShipCountry { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
