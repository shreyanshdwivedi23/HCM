using InsuranceMemberPortal.Models;
using InsuranceMemberPortal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InsuranceMemberPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginRegisterController : ControllerBase
    {
        private readonly IUserLoginRegistration _userLoginRegistrationService;

        public LoginRegisterController(IUserLoginRegistration loginservice)
        {
            _userLoginRegistrationService = loginservice;
        }

        [HttpPost]
        [Route("login-user")]
        public IActionResult Login([FromBody] TblUserRegistration login)
        {

            try
            {
                IActionResult response = Unauthorized();
                var userdata = _userLoginRegistrationService.AuthenticateUser(login, false);
                if (userdata != null)
                {
                    var tokenString = _userLoginRegistrationService.GenerateToken(userdata);
                    response = Ok(new { token = tokenString });
                }
                else
                {
                    response = Ok(new { message = "Invalid User name or password." });
                }
                return response;
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        [Route("register-user")]
        public IActionResult RegisterUser([FromBody] TblUserRegistration login)
        {

            try
            {
                IActionResult response = Unauthorized();
                var userExits = _userLoginRegistrationService.DuplicateUserCheck(login, true);
                if (userExits == null) {
                    var userdata = _userLoginRegistrationService.AuthenticateUser(login, true);
                    
                    response = Ok(new { message="User registered successfully as Admin." });
                    
                    return response;
                }
                else
                {
                    response = Ok(new { errmsg = "Username already exists", code = 403, message="" });
                    return response;
                }
                
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        [Route("register-member")]
        public IActionResult RegisterMember([FromBody] TblMemberRegistration member)
        {
            try
            {
                IActionResult response = Unauthorized();
                TblUserRegistration user = new TblUserRegistration();
                user.UserName = member.MemberUsername;
                user.UserPassword = member.MemberPassword;
                var userExits = _userLoginRegistrationService.DuplicateMemberUserCheck(member);
                if (userExits == "")
                {
                    var userdata = _userLoginRegistrationService.Register(member, true);

                    response = Ok(new { message = "Member - "+ member.MemberNo + " registered successfully." });

                    return response;
                }
                else
                {
                    response = Ok(new { errmsg = "Username already exists", code = 403, message = "" });
                    return response;
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

    }
}
