using HMZ.Database.Entities;
using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Views;
using HMZ.SDK.Extensions;
using HMZ.Service.Extensions;
using HMZ.Service.Helpers;
using HMZ.Service.Services.BaseService;
using HMZ.Service.Validator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HMZ.Service.Services.DepartmentServices
{
	public class DepartmentService  : ServiceBase<IUnitOfWork>, IDepartmentService
    {
        private readonly IServiceProvider _serviceProvider;

        public DepartmentService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider) : base(unitOfWork)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<DataResult<bool>> CreateAsync(DepartmentQuery entity)
        {
            var result = new DataResult<bool>();
            // Validate
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<DepartmentQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                result.Errors.AddRange(resultValidator.JoinError());
                return result;
            }
            var department = new Department
            {
                Name = entity.Name,
                Phone = entity.Phone,
            };
            await _unitOfWork.GetRepository<Department>().Add(department);
            result.Entity = await _unitOfWork.SaveChangesAsync() > 0;
            if (result.Entity == false)
            {
                result.Errors.Add("Error while saving");
                return result;
            }
            return result;
        }

        public async Task<DataResult<int>> DeleteAsync(string id)
        {
            var result = new DataResult<int>();
            var department = await _unitOfWork.GetRepository<Department>().GetByIdAsync(Guid.Parse(id));
            if (department == null)
            {
                result.Errors.Add("Permission not found");
                return result;
            }
            _unitOfWork.GetRepository<Department>().Delete(department, false);
            result.Entity = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<DataResult<DepartmentView>> GetAll()
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

        public async Task<DataResult<DepartmentView>> GetByCodeAsync(string depatertmentCode)
        {
            var result = new DataResult<DepartmentView>();
            var department = await _unitOfWork.GetRepository<Department>().AsQueryable().FirstOrDefaultAsync(x => x.Code == depatertmentCode);
            if (department == null)
            {
                result.Errors.Add("Department not found");
                return result;
            }
            result.Entity = new DepartmentView
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Phone = department.Phone,
            };
            return result;
        }

        public async Task<DataResult<DepartmentView>> GetByIdAsync(string id)
        {
            var result = new DataResult<DepartmentView>();
            var department = await _unitOfWork.GetRepository<Department>().GetByIdAsync(Guid.Parse(id));
            if (department == null)
            {
                result.Errors.Add("Department not found");
                return result;
            }
            result.Entity = new DepartmentView
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Phone = department.Phone,

                CreatedBy = department.CreatedBy,
                CreatedAt = department.CreatedAt,
                UpdatedAt = department.UpdatedAt,
                IsActive = department.IsActive,
            };
            return result;
        }

        public async Task<DataResult<DepartmentView>> GetPageList(BaseQuery<DepartmentFilter> query)
        {
            var subjects = await _unitOfWork.GetRepository<Department>().AsQueryable()
                     .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                     .Take(query.PageSize.Value)
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
                     })
                     .ApplyFilter(query)
                     .OrderByColums(query.SortColums, true).ToListAsync();

            var response = new DataResult<DepartmentView>();
            response.TotalRecords = await _unitOfWork.GetRepository<Department>().AsQueryable().CountAsync();
            response.Items = subjects;
            return response;
        }

        public async Task<DataResult<int>> UpdateAsync(DepartmentQuery entity, string id)
        {
            var result = new DataResult<int>();
            var department = await _unitOfWork.GetRepository<Department>().GetByIdAsync(Guid.Parse(id));
            if (department == null)
            {
                result.Errors.Add("Department not found");
                return result;
            }
            // Validate
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<DepartmentQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                result.Errors.AddRange(resultValidator.JoinError());
                return result;
            }
            department.Name = entity.Name;
            department.Code = entity.Code;
            department.Phone = entity.Phone;

            _unitOfWork.GetRepository<Department>().Update(department);
            result.Entity = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
