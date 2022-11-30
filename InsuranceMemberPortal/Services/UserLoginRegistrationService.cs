using InsuranceMemberPortal.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using InsuranceMemberPortal.DTO;
using Microsoft.Extensions.Logging;

namespace InsuranceMemberPortal.Services.Implementation
{
    public class UserLoginRegistrationService : IUserLoginRegistration
    {
        private readonly InsuranceMemberDBContext _dbContext;
        private IConfiguration _config;

        public UserLoginRegistrationService(InsuranceMemberDBContext context, IConfiguration config)
        {
            _dbContext = context;
            _config = config;
        }

        public int GetMemberID(int userId)
        {
            var Id = (from r in _dbContext.TblMemberRegistrations
                      where r.UserId == userId
                      select new { memberId = r.MemberId }).Single();
            if (Id.memberId > 0)
            {
                return Id.memberId;
            }
            else
            {
                return 0;
            }
        }

        public async Task<string> Login(TblUserRegistration user, bool Isregister)
        {
            try
            {
                var userdata = AuthenticateUser(user, Isregister);
                if (user != null)
                {
                    var tokenString = GenerateToken(userdata);
                    return tokenString;
                }
                else
                {
                    return "Invalid data";
                }
            }
            catch (Exception ex)
            {
                return "Something went wrong";
            }

        }

        public TblUserRegistration DuplicateUserCheck(TblUserRegistration login, bool IsRegister)
        {
            if (_dbContext.TblUserRegistrations.Any(x => x.UserName == login.UserName))
            {
                return _dbContext.TblUserRegistrations.Where(x => x.UserName == login.UserName).FirstOrDefault();
            }
            else
            {
                return null;
            }
            
        }

        public string DuplicateMemberUserCheck(TblMemberRegistration login)
        {
            var response = "";
            if (_dbContext.TblUserRegistrations.Any(x => x.UserName == login.MemberUsername))
            {
                return response = "Username already exists";           
            }
            else if(_dbContext.TblMemberRegistrations.Any(x=> x.MemberFirstName == login.MemberFirstName))
            {
                return response = "First name already exists";
            }
            else if (_dbContext.TblMemberRegistrations.Any(x => x.MemberLastName == login.MemberLastName))
            {
                return response = "Last name already exists";
            }
            else
            {
                return response;
            }

        }
        public TblUserRegistration AuthenticateUser(TblUserRegistration login, bool IsRegister)
        {
            if (IsRegister)
            {
                login.UserCreatedDate = DateTime.Now;
                _dbContext.TblUserRegistrations.Add(login);
                _dbContext.SaveChanges();
                //if (login.UserRole == "Member")
                //{
                //    TblMemberRegistration member = new TblMemberRegistration();
                //    Random rnd = new Random();

                //    member.MemberFirstName = login.FirstName;
                //    member.MemberLastName = login.LastName;
                //    member.MemberAddress = login.Address;
                //    member.MemberCity = login.City;
                //    member.MemberEmail = login.Email;
                //    member.MemberDateOfBirth = login.DateOfBirth;
                //    member.MemberState = login.State;
                    
                //    member.UserId = login.UserId;
                //    member.MemberCreatedBy = login.UserId;
                //    member.MemberCreatedDate = DateTime.Now;


                //    _dbContext.TblMemberRegistrations.Add(member);
                //    _dbContext.SaveChanges();
                //}
                return login;
            }
            else
            {
                if (_dbContext.TblUserRegistrations.Any(x => x.UserName == login.UserName && x.UserPassword == login.UserPassword))
                {
                    return _dbContext.TblUserRegistrations.Where(x => x.UserName == login.UserName && x.UserPassword == login.UserPassword).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }

        }

        public string GenerateToken(TblUserRegistration login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var memberId = 0;
            if (login.UserRole == "Member")
            {
                memberId = GetMemberID(login.UserId);
            }

            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login.UserName),
                    new Claim(ClaimTypes.Role, login.UserRole),
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString( login.UserId)),
                    new Claim(ClaimTypes.Email, Convert.ToString(memberId))
                }),
                Expires = DateTime.Now.AddMinutes(120),
                SigningCredentials = credentials
            };
            var TokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = TokenHandler.CreateToken(token);
            return TokenHandler.WriteToken(tokenGenerated).ToString();
        }


        public TblMemberRegistration Register(TblMemberRegistration member, bool IsRegister)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            if (IsRegister)
            {
                member.MemberCreatedDate = DateTime.Now;
                TblUserRegistration user = new TblUserRegistration();
                user.UserName = member.MemberUsername;
                user.UserPassword = member.MemberPassword;
                user.UserRole = "Member";
                user.UserCreatedDate = DateTime.Now;
                _dbContext.TblUserRegistrations.Add(user);
                _dbContext.SaveChanges();

                member.UserId = user.UserId;

                _dbContext.TblMemberRegistrations.Add(member);
                _dbContext.SaveChanges();
                transaction.Commit();
                return member;
            }             
            else
            {
                return null;
            }
        }
        
        //private TblUserRegistration AuthenticateUser(TblUserRegistration login, bool IsRegister)
        //{
        //    if (IsRegister)
        //    {
        //        _dbContext.TblUserRegistrations.Add(login);
        //        _dbContext.SaveChanges();
        //        return login;
        //    }
        //    else
        //    {
        //        if (_dbContext.TblUserRegistrations.Any(x => x.UserName == login.UserName && x.UserPassword == login.UserPassword))
        //        {
        //            return _dbContext.TblUserRegistrations.Where(x => x.UserName == login.UserName && x.UserPassword == login.UserPassword).FirstOrDefault();
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //private string GenerateToken(TblUserRegistration login)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, login.UserId.ToString()),
        //            new Claim(ClaimTypes.Email, login.UserName.ToString()),
        //            new Claim(ClaimTypes.Gender, login.UserRole.ToString())

        //        }),
        //        Expires = DateTime.Now.AddMinutes(120),
        //        SigningCredentials = credentials
        //    };
        //    var TokenHandler = new JwtSecurityTokenHandler();
        //    var tokenGenerated = TokenHandler.CreateToken(token);
        //    return TokenHandler.WriteToken(tokenGenerated).ToString();
        //}
        //public Task<TblUserRegistration> RegisterUserAsync(TblUserRegistration userModel)
        //{
        //    ResponseDTO response = new ResponseDTO();
        //    var user = AuthenticateUser(userModel, true);
        //    return user;
        //}

        //public Task<TblUserRegistration> LoginUserAsync(TblUserRegistration userModel)
        //{
        //    // var response = Unauthorized();
        //    var user = AuthenticateUser(userModel, false);
        //    if (user != null)
        //    {
        //        var tokenString = GenerateToken(user);  
        //    }
        //    return user;
        //}
    }
}
