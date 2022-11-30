using System;
using System.Collections.Generic;

#nullable disable

namespace InsuranceMemberPortal.Models
{
    public partial class TblMemberRegistration
    {
        public TblMemberRegistration()
        {
            TblInsurancePolicies = new HashSet<TblInsurancePolicy>();
        }

        public int MemberId { get; set; }
        public string MemberNo { get; set; }
        public string MemberFirstName { get; set; }
        public string MemberLastName { get; set; }
        public string MemberUsername { get; set; }
        public string MemberPassword { get; set; }
        public string MemberConfirmPassword { get; set; }
        public string MemberAddress { get; set; }
        public string MemberState { get; set; }
        public string MemberCity { get; set; }
        public string MemberEmail { get; set; }
        public DateTime? MemberDateOfBirth { get; set; }
        public int UserId { get; set; }
        public DateTime? MemberCreatedDate { get; set; }
        public int? MemberCreatedBy { get; set; }
        public DateTime? MemberupdatedDate { get; set; }
        public int? MemberupdatedBy { get; set; }

        public virtual TblUserRegistration User { get; set; }
        public virtual ICollection<TblInsurancePolicy> TblInsurancePolicies { get; set; }
    }
}
