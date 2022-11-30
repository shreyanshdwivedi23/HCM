using System;
using System.Collections.Generic;

#nullable disable

namespace InsuranceMemberPortal.Models
{
    public partial class TblPolicyType
    {
        public int PolicyTypeId { get; set; }
        public string PolicyType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? MemberCreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
