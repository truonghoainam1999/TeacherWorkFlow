using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries.Catalog;
using HMZ.DTOs.Views;
using HMZ.Service.Helpers;
using HMZ.Service.Services.BaseService;

namespace HMZ.Service.Services.ScheduleService
{
    public interface IScheduleService : IBaseService<ScheduleQuery, ScheduleView, ScheduleFilter>
    {
        Task<DataResult<ScheduleView>> GetByCodeAsync(string scheduleCode);
    }
}
