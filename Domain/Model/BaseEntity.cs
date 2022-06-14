using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
    public class BaseEntity
    {
        public DateTime CreateDate { get; set; } = DateTime.Now.ToUniversalTime().AddHours(4);
    }
}
