using HMZ.Database.Entities;
using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Views;
using HMZ.SDK.Extensions;
using HMZ.Service.Helpers;
using HMZ.Service.Services.BaseService;
using Microsoft.EntityFrameworkCore;

namespace HMZ.Service.Services.Reports
{
	public class ReportService : ServiceBase<IUnitOfWork>, IReportService
	{
		private readonly IServiceProvider _serviceProvider;

		public ReportService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider) : base(unitOfWork)
		{
			_serviceProvider = serviceProvider;
		}

        //Trễ hạn
        public async Task<DataResult<ReportView>> GetDeadLinePageList(BaseQuery<ReportFilter> query)
        {
            DateTime currentDate = DateTime.Now.Date;

            var taskWorkQuery = _unitOfWork.GetRepository<TaskWork>().AsQueryable()
                .Include(x => x.ClassRoom)
                .Include(x => x.User)
                .Include(x => x.Subject)
                .Where(x => currentDate > x.EndDate);

            var taskWork = await taskWorkQuery
                .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                .Take(query.PageSize.Value)
                .Select(x => new ReportView()
                {
                    Id = x.Id,
                    Code = x.Code,
                    RoomName = x.ClassRoom.Name,
                    UserName = x.User.UserName,
                    SubjectName = x.Subject.Name,
                    RoomId = x.RoomId.ToString(),
                    UserId = x.UserId.ToString(),
                    SubjectId = x.SubjectId.ToString(),
                    IsDelay = x.EndDate.Date == currentDate ? true : (x.EndDate < currentDate ? false : (bool?)null),
                    EndDate = x.EndDate.ToString("dd MMM yyyy"),
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    IsActive = x.IsActive,
                })
                .ToListAsync();

            var response = new DataResult<ReportView>();
            response.Items = taskWork;
            return response;
        }

        // Đến hạn
        public async Task<DataResult<ReportView>> GetDeLayPageList(BaseQuery<ReportFilter> query)
        {
            DateTime currentDate = DateTime.Now.Date;

            var taskWorkQuery = _unitOfWork.GetRepository<TaskWork>().AsQueryable()
                .Include(x => x.ClassRoom)
                .Include(x => x.User)
                .Include(x => x.Subject)
                .Where(x => x.EndDate == currentDate);

            var taskWork = await taskWorkQuery
                .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                .Take(query.PageSize.Value)
                .Select(x => new ReportView()
                {
                    Id = x.Id,
                    Code = x.Code,
                    RoomName = x.ClassRoom.Name,
                    UserName = x.User.UserName,
                    SubjectName = x.Subject.Name,
                    RoomId = x.RoomId.ToString(),
                    UserId = x.UserId.ToString(),
                    SubjectId = x.SubjectId.ToString(),
                    IsDelay = x.EndDate.Date == currentDate ? true : (x.EndDate < currentDate ? false : (bool?)null),
                    EndDate = x.EndDate.ToString("dd MMM yyyy"),
                    CreatedBy = x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    IsActive = x.IsActive,
                })
                .ToListAsync();

            var response = new DataResult<ReportView>();
            response.Items = taskWork;
            return response;
        }

        //All task
        public async Task<DataResult<ReportView>> GetAllPageList(BaseQuery<ReportFilter> query)
		{
			DateTime currentDate = DateTime.Now.Date;

			var taskWorkQuery = _unitOfWork.GetRepository<TaskWork>().AsQueryable()
				.Include(x => x.ClassRoom)
				.Include(x => x.User)
				.Include(x => x.Subject)
				.Where(x => x.EndDate != null);

			var taskWork = await taskWorkQuery
                .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
				.Take(query.PageSize.Value)
				.Select(x => new ReportView()
				{
					Id = x.Id,
					Code = x.Code,
					RoomName = x.ClassRoom.Name,
					UserName = x.User.UserName,
					SubjectName = x.Subject.Name,
					RoomId = x.RoomId.ToString(),
					UserId = x.UserId.ToString(),
					SubjectId = x.SubjectId.ToString(),
					IsDelay = x.EndDate.Date == currentDate ? true : (x.EndDate < currentDate ? false : (x.EndDate > currentDate ? (bool?)null : null)),
					EndDate = x.EndDate.ToString("dd MMM yyyy"),
					CreatedBy = x.CreatedBy,
					CreatedAt = x.CreatedAt,
					UpdatedAt = x.UpdatedAt,
					IsActive = x.IsActive,
				})
				.ToListAsync();

			var response = new DataResult<ReportView>();
			response.Items = taskWork;
			return response;
		}
    }
}

