using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    public class Payment: BaseEntity
    {
        public int Id { get; set; }
        public int? ParrentId { get; set; }
        public User ParrentUser { get; set; }
        public int? ChildId { get; set; }
        public User ChildUser { get; set; }
        public decimal Amount { get; set; }
        public int ReasonId { get; set; }
        public Reason Reason { get; set; }
    }
}
