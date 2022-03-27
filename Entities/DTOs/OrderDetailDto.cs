using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class OrderDetailDto : IDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public string CargoCompanyName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Descriptinon { get; set; }
        public bool Status { get; set; }

    }
}
