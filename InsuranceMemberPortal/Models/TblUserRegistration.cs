using System;
using System.Collections.Generic;

#nullable disable

namespace InsuranceMemberPortal.Models
{
    public partial class TblUserRegistration
    {
        public TblUserRegistration()
        {
            TblMemberRegistrations = new HashSet<TblMemberRegistration>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
        public DateTime? UserCreatedDate { get; set; }
        public int? UserCreatedBy { get; set; }

        public virtual ICollection<TblMemberRegistration> TblMemberRegistrations { get; set; }
    }
}
