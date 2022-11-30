using System;
using System.Collections.Generic;

#nullable disable

namespace InsuranceMemberPortal.Models
{
    public partial class TblPolicyStatus
    {
        public TblPolicyStatus()
        {
            TblInsurancePolicies = new HashSet<TblInsurancePolicy>();
        }

        public int PolicyStatusId { get; set; }
        public string PolicyStatusName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<TblInsurancePolicy> TblInsurancePolicies { get; set; }
    }
}
