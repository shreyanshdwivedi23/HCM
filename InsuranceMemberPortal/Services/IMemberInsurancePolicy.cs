using InsuranceMemberPortal.DTO;
using InsuranceMemberPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceMemberPortal.Services
{
    public interface IMemberInsurancePolicy
    {

        Task<string> Login(TblUserRegistration login, bool Isregister);
        // for addign insurance
        TblInsurancePolicy AddInsurancePolicy(TblInsurancePolicy member);

        ////for updating insurance
        //TblInsurancePolicy UpdateInsurancePolicy(TblInsurancePolicy member);

        //// for getting all members in admin search
        List<search> GetAllMemberPolicy(search data);

        List<TblPolicyType> GetPolicyType();

        List<TblPolicyStatus> GetPolicyStatus();

        string PolicyExistCheck(TblInsurancePolicy policy);

        TblInsurancePolicy GetAllMemberPolicyByPID(TblInsurancePolicy policy);

        string UpdateInsuranceolicy(TblInsurancePolicy policy);




    }
}
