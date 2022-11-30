using System;
using System.Collections.Generic;

#nullable disable

namespace InsuranceMemberPortal.Models
{
    public partial class TblInsurancePolicy
    {
        public int PolicyId { get; set; }
        public int PolicyTypeId { get; set; }
        public int? PolicyPremiumAmount { get; set; }
        public DateTime? PolicyEffectiveDate { get; set; }
        public int? PolicyStatusId { get; set; }
        public int? UserId { get; set; }
        public DateTime? PolicyCreatedDate { get; set; }
        public int? PolicyCreatedBy { get; set; }
        public DateTime? PolicyUpdatedDate { get; set; }
        public int? PolicyUpdatedBy { get; set; }
        public int? MemberId { get; set; }

        public virtual TblMemberRegistration Member { get; set; }
        public virtual TblPolicyStatus PolicyStatus { get; set; }
    }
}
