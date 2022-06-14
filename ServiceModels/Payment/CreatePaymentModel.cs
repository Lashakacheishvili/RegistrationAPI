using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceModels.Payment
{
    public class CreatePaymentModel
    {
        public int ChildId { get; set; }
        public int ReasonId { get; set; }
        public decimal Amount { get; set; }
    }
}
