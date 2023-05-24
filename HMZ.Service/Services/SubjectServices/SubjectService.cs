
using System.ComponentModel.DataAnnotations;
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

namespace HMZ.Service.Services.SubjectServices
{
    public class SubjectService : ServiceBase<IUnitOfWork>, ISubjectService
    {
        private readonly IServiceProvider _serviceProvider;

        public SubjectService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider) : base(unitOfWork)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<DataResult<bool>> CreateAsync(SubjectQuery entity)
        {
            var result = new DataResult<bool>();
            // Validate
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<SubjectQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                result.Errors.AddRange(resultValidator.JoinError());
                return result;
            }
            var subject = new Subject
            {
                Name = entity.Name,
                Description = entity.Description,
                DepartmentId = Guid.Parse(entity.DepartmentId)
            };
            await _unitOfWork.GetRepository<Subject>().Add(subject);
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
            var subject = await _unitOfWork.GetRepository<Subject>().GetByIdAsync(Guid.Parse(id));
            if (subject == null)
            {
                result.Errors.Add("Permission not found");
                return result;
            }
            _unitOfWork.GetRepository<Subject>().Delete(subject, false);
            result.Entity = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<DataResult<SubjectView>> GetByCodeAsync(string subjectCode)
        {
            var result = new DataResult<SubjectView>();
            var subject = await _unitOfWork.GetRepository<Subject>().AsQueryable().FirstOrDefaultAsync(x => x.Code == subjectCode);
            if (subject == null)
            {
                result.Errors.Add("Subject not found");
                return result;
            }
            result.Entity = new SubjectView
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description,
                DepartmentId = subject.DepartmentId.ToString(),
                DepartmentName = subject.Department.Name
            };
            return result;
        }

        public async Task<DataResult<SubjectView>> GetByIdAsync(string id)
        {
            var result = new DataResult<SubjectView>();
            var subject = await _unitOfWork.GetRepository<Subject>()
                .AsQueryable().Include(x => x.Department).FirstOrDefaultAsync(x=>x.Id == Guid.Parse(id));
               
            if (subject == null)
            {
                result.Errors.Add("Subject not found");
                return result;
            }
            result.Entity = new SubjectView
            {
                Id = subject.Id,
                Code = subject.Code,
                Name = subject.Name,
                Description = subject.Description,
                DepartmentId = subject.DepartmentId.ToString(),
                DepartmentName = subject.Department?.Name,

                CreatedBy = subject.CreatedBy,
                CreatedAt = subject.CreatedAt,
                UpdatedAt = subject.UpdatedAt,
                IsActive = subject.IsActive,
            };
            return result;
        }

        public async Task<DataResult<SubjectView>> GetPageList(BaseQuery<SubjectFilter> query)
        {
            var subjects = await _unitOfWork.GetRepository<Subject>().AsQueryable()
                     .Include(x => x.Department)
                     .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                     .Take(query.PageSize.Value)
                     .Select(x => new SubjectView()
                     {
                         Id = x.Id,
                         Name = x.Name,
                         Code = x.Code,
                         Description = x.Description,
                         DepartmentId = x.DepartmentId.ToString(),
                         DepartmentName = x.Department.Name,

                         CreatedBy = x.CreatedBy,
                         CreatedAt = x.CreatedAt,
                         UpdatedAt = x.UpdatedAt,
                         IsActive = x.IsActive,
                     })
                     .ApplyFilter(query)
                     .OrderByColums(query.SortColums, true).ToListAsync();

            var response = new DataResult<SubjectView>();
            response.TotalRecords = await _unitOfWork.GetRepository<Subject>().AsQueryable().CountAsync();
            response.Items = subjects;
            return response;
        }

        public async Task<DataResult<int>> UpdateAsync(SubjectQuery entity, string id)
        {
            var result = new DataResult<int>();
            var subject = await _unitOfWork.GetRepository<Subject>()
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if (subject == null)
            {
                result.Errors.Add("Subject not found");
                return result;
            }
            // Validate
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<SubjectQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                result.Errors.AddRange(resultValidator.JoinError());
                return result;
            }
            subject.Name = entity.Name;
            subject.Description = entity.Description;
            subject.DepartmentId = Guid.Parse(entity.DepartmentId);

			_unitOfWork.GetRepository<Subject>().Update(subject);
            result.Entity = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<DataResult<SubjectView>> GetAll()
        {
            var subject = await _unitOfWork.GetRepository<Subject>().AsQueryable()
                      .Include(x=> x.Department)
                      .Select(x => new SubjectView()
                      {
                          Id = x.Id,
                          Name = x.Name,
                          Description = x.Description,
                          DepartmentId = x.DepartmentId.ToString(),
                          DepartmentName = x.Department.Name,
                          CreatedBy = x.CreatedBy,
                          CreatedAt = x.CreatedAt,
                          UpdatedAt = x.UpdatedAt,
                          IsActive = x.IsActive,
                      }).ToListAsync();
            var response = new DataResult<SubjectView>();
            response.Items = subject;
            return response;
        }
    }
}