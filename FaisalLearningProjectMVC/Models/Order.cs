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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Required Date")]
        public DateTime RequiredDate { get; set; }
        [Display(Name = "Shipped Date")]
        public DateTime? ShippedDate { get; set; }
        public Decimal Freight { get; set; }
        [Display(Name = "Order Name")]
        public string ShipName { get; set; }

        [Display(Name = "Adrress")]
        public string ShipAddress { get; set; }
        [Display(Name = "City")]
        public string ShipCity { get; set; }
        [Display(Name = "Region")]
        public string ShipRegion { get; set; }

        [Display(Name = "Post Code")]

        public string ShipPostalCode { get; set; }

        [Display(Name = "Country")]

        public string ShipCountry { get; set; }


        public bool IsActive { get; set; }

        public virtual Customer Customer { get; set; }
        public string CustomerName => Customer?.ContactName ?? string.Empty;

        public virtual Shipper Shipper { get; set; }



    }
}
