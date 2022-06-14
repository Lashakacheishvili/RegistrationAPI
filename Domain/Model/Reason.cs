using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Model
{
    public class Reason:BaseEntity
    {
        public int Id { get; set; }
        public int? ParrentId { get; set; }
        public User Parrent { get; set; }
        public int? ChildId { get; set; }
        public User Child { get; set; }
        [Column(TypeName = "varchar(350)")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Amount { get; set; }
        public string DeleteDescription { get; set; }
        public bool Required { get; set; }
        public bool IsSuccessed { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
