using InsuranceMemberPortal.DTO;
using InsuranceMemberPortal.Models;
using InsuranceMemberPortal.Services;
using InsuranceMemberPortal.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace InsuranceMemberPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceMemberController : ControllerBase
    {
        private readonly IMemberInsurancePolicy _memberService;
        public InsuranceMemberController(IMemberInsurancePolicy memberService)
        {
            _memberService = memberService;
        }

        [HttpPost]
        [Route("GetAllMemberPolicyByPID")]
        public IActionResult GetAllMemberPolicyByPID(TblInsurancePolicy policy)
        {
            try
            {
                IActionResult response = Unauthorized();
                var result = _memberService.GetAllMemberPolicyByPID(policy);
                if (result != null)
                {
                    response = Ok(new { message = "Details fetched successfully", data= result });

                    return response;
                }
                else
                {
                    response = Ok(new { errmsg = "Error fetching details.", code = 404, message = "" });
                    return response;
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
            
        }

        [HttpPost]
        [Route("GetAllMemberPolicy")]
        public List<search> GetAllMemberPolicy(search data)
        {
            var memberList = _memberService.GetAllMemberPolicy(data);
            return memberList;
        }

        [HttpGet]
        [Route("GetPolicyStatus")]
        public List<TblPolicyStatus> GetPolicyStatus()
        {
            var statusList = _memberService.GetPolicyStatus();
            return statusList;
        }

        [HttpGet]
        [Route("GetPolicyType")]
        public List<TblPolicyType> GetPolicyType()
        {
            var typeList = _memberService.GetPolicyType();
            return typeList;
        }

        [HttpPost]
        [Route("AddInsurancePolicy")]
        public IActionResult AddInsurancePolicy([FromBody] TblInsurancePolicy member)
        {
            try
            {
                IActionResult response = Unauthorized();
                var policyExits = _memberService.PolicyExistCheck(member);
                if (policyExits == "")
                {
                    var userdata = _memberService.AddInsurancePolicy(member); ;

                    response = Ok(new { message = "Policy details saved successfully." });

                    return response;
                }
                else
                {
                    response = Ok(new { errmsg = policyExits, code = 403, message = "" });
                    return response;
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        [Route("UpdateInsuranceolicy")]
        public IActionResult UpdateInsuranceolicy([FromBody] TblInsurancePolicy member)
        {
            try
            {
                IActionResult response = Unauthorized();
                //var policyExits = _memberService.PolicyExistCheck(member);
                //if (policyExits == "")
                //{
                var userdata = _memberService.UpdateInsuranceolicy(member); ;

                if (userdata == "")
                {
                    response = Ok(new { message = "Policy details updated successfully." });
                }
                else
                {
                    response = Ok(new { errmsg = userdata, code = 403, message = "" });
                }

                return response;
                //}
                //else
                //{
                //    response = Ok(new { errmsg = policyExits, code = 403, message = "" });
                //    return response;
                //}
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        //[HttpPost]
        //[Route("GetMemberID")]
        //public int GetMemberID([FromBody] int member)
        //{
        //    try
        //    {
        //        IActionResult response = Unauthorized();
        //        var memberExit = _memberService.GetMemberID(member);
        //        return memberExit;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}

    }
     
}
