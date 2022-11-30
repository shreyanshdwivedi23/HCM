using InsuranceMemberPortal.Models;
using System.Collections;
using System.Threading.Tasks;

namespace InsuranceMemberPortal.Services
{
    public interface IUserLoginRegistration
    {
        Task<string> Login(TblUserRegistration login, bool Isregister);
        TblMemberRegistration Register(TblMemberRegistration member, bool Isregister);
        TblUserRegistration AuthenticateUser(TblUserRegistration login, bool IsRegister);

        TblUserRegistration DuplicateUserCheck(TblUserRegistration login, bool IsRegister);
        string GenerateToken(TblUserRegistration login);

        string DuplicateMemberUserCheck(TblMemberRegistration login);

        //int GetMemberID(int userId);
        //Task<TblUserRegistration> RegisterUserAsync(TblUserRegistration userModel);

        //Task<TblUserRegistration> LoginUserAsync(TblUserRegistration userModel);
    }
}
