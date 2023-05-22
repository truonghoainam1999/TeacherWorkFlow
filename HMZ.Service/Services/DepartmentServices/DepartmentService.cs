using HMZ.Database.Entities;
using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Views;
using HMZ.SDK.Extensions;
using HMZ.Service.Helpers;
using HMZ.Service.Services.BaseService;
using Microsoft.EntityFrameworkCore;

namespace HMZ.Service.Services.DepartmentServices
{
	public class DepartmentService  : ServiceBase<IUnitOfWork>, IDepartmentService
    {
        private readonly IServiceProvider _serviceProvider;

        public DepartmentService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider) : base(unitOfWork)
        {
			_serviceProvider = serviceProvider;
        }

        public Task<DataResult<bool>> CreateAsync(DepartmentQuery entity)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<int>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async  Task<DataResult<DepartmentView>> GetAll()
        {
            var departments = await _unitOfWork.GetRepository<Department>().AsQueryable()
                      .Select(x => new DepartmentView()
                      {
                          Id = x.Id,
                          Name = x.Name,
                          Code = x.Code,
						  Phone = x.Phone,
                          CreatedBy = x.CreatedBy,
                          CreatedAt = x.CreatedAt,
                          UpdatedAt = x.UpdatedAt,
                          IsActive = x.IsActive,
                      }).ToListAsync();
            var response = new DataResult<DepartmentView>();
            response.Items = departments;
            return response;
        }

        public Task<DataResult<DepartmentView>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<DepartmentView>> GetPageList(BaseQuery<DepartmentFilter> query)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<int>> UpdateAsync(DepartmentQuery entity, string id)
        {
            throw new NotImplementedException();
        }
    }
}
