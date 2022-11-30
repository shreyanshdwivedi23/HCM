using InsuranceMemberPortal.DTO;
using InsuranceMemberPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceMemberPortal.Services
{
    public class InsurancePolicyService : IMemberInsurancePolicy
    {
        private readonly InsuranceMemberDBContext _dbContext;
        private IConfiguration _config;

        public InsurancePolicyService(InsuranceMemberDBContext context, IConfiguration config)
        {
            _dbContext = context;
            _config = config;
        }

        public async Task<string> Login(TblUserRegistration user, bool Isregister)
        {
            try
            {
                return "";
            }
            catch (Exception ex)
            {
                return "Something went wrong";
            }

        }
         

        public TblInsurancePolicy AddInsurancePolicy(TblInsurancePolicy member)
        {
            var memberreg = _dbContext.TblMemberRegistrations.Where(t => t.UserId == member.UserId);
            member.PolicyCreatedDate = DateTime.Now;
            member.PolicyCreatedBy = member.UserId;
            //member.UserId = member.UserId;
            //member.MemberId = memberreg.Count() > 0 ? memberreg.FirstOrDefault().MemberId : 0;
            _dbContext.TblInsurancePolicies.Add(member);
            _dbContext.SaveChanges();
            return member;
        }
        
        public List<search> GetAllMemberPolicy(search data)
        {
            if (data.MemberFirstName == "" && data.MemberLastName =="" && data.MemberId==0 && data.PolicyId==0 && data.PolicyStatusId == 0)
            {
                var result = from p in _dbContext.TblInsurancePolicies
                             join s in _dbContext.TblPolicyStatuses on p.PolicyStatusId equals s.PolicyStatusId
                             join m in _dbContext.TblMemberRegistrations on p.MemberId equals m.MemberId
                             join r in _dbContext.TblUserRegistrations on m.UserId equals r.UserId                            
                             join t in _dbContext.TblPolicyTypes on p.PolicyTypeId equals t.PolicyTypeId
                             select new search()
                             {
                                 MemberFirstName = m.MemberFirstName,
                                 MemberLastName = m.MemberLastName,
                                 MemberId = m.MemberId,
                                 PolicyId = p.PolicyId,
                                 PolicyStatusName = s.PolicyStatusName,
                                 PolicyPremiumAmount = p.PolicyPremiumAmount,
                                 PolicyEffectiveDate = p.PolicyEffectiveDate,
                                 MemberUsername = r.UserName,
                                 MemberNo = m.MemberNo,
                                 PolicyType = t.PolicyType
                             };
                return result.ToList();
            }
            else {
                var result = from p in _dbContext.TblInsurancePolicies
                             join s in _dbContext.TblPolicyStatuses on p.PolicyStatusId equals s.PolicyStatusId
                             join m in _dbContext.TblMemberRegistrations on p.MemberId equals m.MemberId
                             join r in _dbContext.TblUserRegistrations on m.UserId equals r.UserId
                             join t in _dbContext.TblPolicyTypes on p.PolicyTypeId equals t.PolicyTypeId
                             where m.MemberFirstName == data.MemberFirstName || m.MemberLastName == data.MemberLastName
                             || p.MemberId == data.MemberId || p.PolicyStatusId == data.PolicyStatusId 
                             || p.PolicyId == data.PolicyId
                             select new search()
                             {
                                 MemberFirstName = m.MemberFirstName,
                                 MemberLastName = m.MemberLastName,
                                 MemberId = m.MemberId,
                                 PolicyId = p.PolicyId,
                                 PolicyStatusName = s.PolicyStatusName,
                                 PolicyPremiumAmount = p.PolicyPremiumAmount,
                                 PolicyEffectiveDate = p.PolicyEffectiveDate,
                                 MemberUsername = r.UserName,
                                 MemberNo = m.MemberNo,
                                 PolicyType = t.PolicyType
                             };
                return result.ToList();
            }
            
            
        }

        public List<TblPolicyType> GetPolicyType()
        {
            return _dbContext.TblPolicyTypes.ToList();
        }

        public List<TblPolicyStatus> GetPolicyStatus()
        {
            return _dbContext.TblPolicyStatuses.ToList();
        }

        public string PolicyExistCheck(TblInsurancePolicy policy)
        {
            var response = "";
            if (_dbContext.TblInsurancePolicies.Any(x => x.PolicyTypeId == policy.PolicyTypeId && x.MemberId ==policy.MemberId))
            {
                return response = "Policy already exists against the member.";
            }
            else
            {
                return response;
            }

        }

        public TblInsurancePolicy GetAllMemberPolicyByPID(TblInsurancePolicy policy)
        {
            
            if (_dbContext.TblInsurancePolicies.Any(x => x.PolicyId == policy.PolicyId))
            {
                return _dbContext.TblInsurancePolicies.Where(x => x.PolicyId == policy.PolicyId).FirstOrDefault();
            }
            else
            {
                return null;
            }

        }

        public string UpdateInsuranceolicy(TblInsurancePolicy policy)
        {
            
            var existingBook = _dbContext.TblInsurancePolicies.Where(x => x.PolicyId == policy.PolicyId).FirstOrDefault<TblInsurancePolicy>();
            var policyExits = "";

            if (policy.PolicyTypeId != existingBook.PolicyTypeId)
            {
                policyExits = PolicyExistCheck(policy);
            }

            if (existingBook != null && policyExits =="")
            {
                policy.PolicyUpdatedDate = System.DateTime.Now;
                existingBook.PolicyUpdatedBy = policy.UserId;
                existingBook.PolicyPremiumAmount = policy.PolicyPremiumAmount;


                existingBook.PolicyStatusId = policy.PolicyStatusId;
                existingBook.PolicyTypeId = policy.PolicyTypeId;
                existingBook.PolicyEffectiveDate = policy.PolicyEffectiveDate;
                
                _dbContext.SaveChanges();
            }
            else
            {
                return policyExits;
            }
            return policyExits;
            //return existingBook;
        }

        //public IEnumerable<TblInsurancePolicy> GetAllMemberPolicyById(int id)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public IEnumerable<TblPolicyStatus> GetPolicyStatus()
        //{
        //    throw new System.NotImplementedException();
        //}

        //public TblInsurancePolicy UpdateInsurancePolicy(TblInsurancePolicy member)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
