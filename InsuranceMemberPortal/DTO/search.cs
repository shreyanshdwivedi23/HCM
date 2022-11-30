using System;

namespace InsuranceMemberPortal.DTO
{
    public class search
    {
        public int MemberId { get; set; }

        public string UserRole { get; set; }

        public string UserName { get; set; }
        public string MemberNo { get; set; }
        public string MemberFirstName { get; set; }
        public string MemberLastName { get; set; }
        public string MemberUsername { get; set; }
        public int PolicyId { get; set; }
        public int PolicyTypeId { get; set; }
        public int? PolicyPremiumAmount { get; set; }
        public DateTime? PolicyEffectiveDate { get; set; }
        public int? PolicyStatusId { get; set; }
        public string PolicyStatusName { get; set; }
        public string PolicyType { get; set; }
        public int? UserId { get; set; }
    }
}
