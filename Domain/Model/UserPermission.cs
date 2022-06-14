using Common.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
   public class UserPermission : IdentityRole<int>
    {
        /// <summary>
        /// კოლექცია Identity როლების
        /// </summary>
        public virtual ICollection<IdentityRoleClaim<int>> Claims { get; } = new List<IdentityRoleClaim<int>>();
        /// <summary>
        /// კოლექცია მომხმარებლების
        /// </summary>
        public virtual ICollection<IdentityUserRole<int>> Users { get; } = new List<IdentityUserRole<int>>();
        /// <summary>
        /// უფლებები
        /// </summary>
        public Permission Permission { get; set; }
        /// <summary>
        /// შექმნის თარიღი
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// პროფილის განახლების ბოლო თარიღი
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// პროფილის გაუქმების თარიღი
        /// </summary>
        public DateTime? DeleteDate { get; set; }
    }
}
