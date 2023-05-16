using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Views;
using HMZ.Service.Helpers;
using HMZ.Service.Services.BaseService;
namespace HMZ.Service.Services.UserServices
{
    public interface IUserService : IBaseService<UserQuery, UserView, UserFilter>
    {
        Task<DataResult<UserView>> Register(RegisterQuery entity);
        Task<DataResult<UserView>> Login(LoginQuery entity);
        Task<DataResult<UserView>> GetByUserName(string userName);
        Task<DataResult<bool>> ChangeRole(ChangeRoleQuery entity);
        Task<DataResult<bool>> UpdatePassword(UpdatePasswordQuery entity);
        Task<DataResult<bool>> UploadAvatar(UploadAvatarQuery entity);
        Task<DataResult<bool>> ForgotPassword(string email, string host);
        Task<DataResult<bool>> ResetPassword(UpdatePasswordQuery entity);
        Task<DataResult<bool>> LockUser(string username, bool isLock);
    }
}
