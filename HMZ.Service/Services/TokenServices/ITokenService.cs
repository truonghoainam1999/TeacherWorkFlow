using HMZ.Database.Entities;
using HMZ.DTOs.Queries;

namespace HMZ.Service.Services.TokenServices
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
