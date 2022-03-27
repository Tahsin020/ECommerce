﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class OrderPaymentRequestModel
    {
        public string CardNumber { get; set; }
        public string ExpireYear { get; set; }
        public string ExpireMonth { get; set; }
        public string Cvc { get; set; }
        public string CardHolderFullName { get; set; }
        public int CustomerId { get; set; }
        public Order[] Orders { get; set; }
        public decimal Amount { get; set; }
    }
}
