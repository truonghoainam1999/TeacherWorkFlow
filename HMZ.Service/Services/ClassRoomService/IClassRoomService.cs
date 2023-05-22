using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Catalog;
using HMZ.DTOs.Views;
using HMZ.Service.Helpers;
using HMZ.Service.Services.BaseService;

namespace HMZ.Service.Services.ClassRoomService
{
    public interface IClassRoomService : IBaseService<ClassRoomQuery, ClassRoomView, ClassRoomFilter>
    {
        Task<DataResult<ClassRoomView>> GetAll();
        Task<DataResult<ClassRoomView>> GetByCodeAsync(string departmentCode);
    }
}
