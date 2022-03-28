using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order:IEntity
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int PaymentId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public int CargoCompanyId { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
