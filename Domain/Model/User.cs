using Common.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class User : IdentityUser<int>
    {
        /// <summary>
        /// რა სახელით უნდა გამოჩნდეს
        /// </summary>
        [Column(TypeName = "varchar(800)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string PersonalNumber { get; set; }
        /// <summary>
        /// პროფილის სურათი
        /// </summary>
        public string ProfileImage { get; set; }
        /// <summary>
        /// მომხმარებლის სტატუსი
        /// </summary>
        public UserStatus UserStatus { get; set; }
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
        /// <summary>
        /// ავტორიზაცის ბოლო თარიღი
        /// </summary>
        public DateTime? LastLoginAt { get; set; }
        /// <summary>
        /// ბალანსი
        /// </summary>
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Balance { get; set; }
        /// <summary>
        /// აქტიური არის თუ არა
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// მომხმარებლის როლი
        /// </summary>
        public UserRole UserRole { get; set; }
        public int ChildCount { get; set; }
        public int? ParrentId { get; set; }
        public User Parrent { get; set; }
        public virtual ICollection<IdentityUserRole<int>> Roles { get; set; }
    }
}
