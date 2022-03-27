using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductDetailDto:IDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ColorName { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public List<ProductImage> ProductImages { get; set; }

    }
}
