using System;
using System.Collections.Generic;
using Identity.DAL.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Identity.DAL.Models
{
    public class AppUser : IdentityUser, IAuditableEntity
    {
        public string FullName { get; set; }
        public string Configuration { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsLockedOut => this.LockoutEnabled && this.LockoutEnd >= DateTimeOffset.UtcNow;

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Navigation property for the roles.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        /// <summary>
        /// Navigation property for the claims.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
    }
}
