using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceModels.Reason
{
    public class CreateEditReasonModel
    {
        public int? Id { get; set; }
        public int? ChildId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool Required { get; set; }
    }
}
