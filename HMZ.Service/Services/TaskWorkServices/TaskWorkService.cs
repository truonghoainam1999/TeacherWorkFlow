using HMZ.Database.Entities;
using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Queries.Catalog;
using HMZ.DTOs.Views;
using HMZ.SDK.Extensions;
using HMZ.Service.Extensions;
using HMZ.Service.Helpers;
using HMZ.Service.Services.BaseService;
using HMZ.Service.Validator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace HMZ.Service.Services.TaskWorkServices
{
    public class TaskWorkService : ServiceBase<IUnitOfWork>, ITaskWorkService
    {
        private readonly IServiceProvider _serviceProvider;

        public TaskWorkService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider) : base(unitOfWork)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<DataResult<bool>> CreateAsync(TaskWorkQuery entity)
        {
            var result = new DataResult<bool>();
            // Validate
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<TaskWorkQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                result.Errors.AddRange(resultValidator.JoinError());
                return result;
            }
            var taskWork = new TaskWork
            {
                RoomId =  Guid.Parse(entity.RoomId),
                UserId = Guid.Parse(entity.UserId),
                SubjectId = Guid.Parse(entity.SubjectId),
            };
            await _unitOfWork.GetRepository<TaskWork>().Add(taskWork);
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
            var taskWork = await _unitOfWork.GetRepository<TaskWork>().GetByIdAsync(Guid.Parse(id));
            if (taskWork == null)
            {
                result.Errors.Add("TaskWork not found");
                return result;
            }
            _unitOfWork.GetRepository<TaskWork>().Delete(taskWork, false);
            result.Entity = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<DataResult<TaskWorkView>> GetAll()
        {
            var taskWorks = await _unitOfWork.GetRepository<TaskWork>().AsQueryable()
                      .Select(x => new TaskWorkView()
                      {
                          Id = x.Id,
                          RoomId = x.RoomId.ToString(),
                          UserId = x.UserId.ToString(),
                          SubjectId = x.SubjectId.ToString(),
                          CreatedBy = x.CreatedBy,
                          CreatedAt = x.CreatedAt,
                          UpdatedAt = x.UpdatedAt,
                          IsActive = x.IsActive,
                      }).ToListAsync();
            var response = new DataResult<TaskWorkView>();
            response.Items = taskWorks;
            return response;
        }

        public async Task<DataResult<TaskWorkView>> GetByCodeAsync(string taskWorkCode)
        {
            var result = new DataResult<TaskWorkView>();
            var taskWork = await _unitOfWork.GetRepository<TaskWork>().AsQueryable().FirstOrDefaultAsync(x => x.Code == taskWorkCode);
            if (taskWork == null)
            {
                result.Errors.Add("TaskWork not found");
                return result;
            }
            result.Entity = new TaskWorkView
            {
                Id = taskWork.Id,
                RoomId = taskWork.RoomId.ToString(),
                UserId = taskWork.UserId.ToString(), 
                SubjectId = taskWork.SubjectId.ToString(),

            };
            return result;
        }

        public async Task<DataResult<TaskWorkView>> GetByIdAsync(string id)
        {
            var result = new DataResult<TaskWorkView>();
            var taskWork = await _unitOfWork.GetRepository<TaskWork>().GetByIdAsync(Guid.Parse(id));
            if (taskWork == null)
            {
                result.Errors.Add("TaskWork not found");
                return result;
            }
            result.Entity = new TaskWorkView
            {
                Id = taskWork.Id,
                Code = taskWork.Code,
                RoomId = taskWork.RoomId.ToString(),
                UserId = taskWork.UserId.ToString(),
                SubjectId = taskWork.SubjectId.ToString(),

                CreatedBy = taskWork.CreatedBy,
                CreatedAt = taskWork.CreatedAt,
                UpdatedAt = taskWork.UpdatedAt,
                IsActive = taskWork.IsActive,
            };
            return result;
        }

        public async Task<DataResult<TaskWorkView>> GetPageList(BaseQuery<TaskWorkFilter> query)
        {
            var taskWork = await _unitOfWork.GetRepository<TaskWork>().AsQueryable()
                     .Include(x => x.ClassRoom)
                     .Include(x => x.User)
                     .Include(x => x.Subject)
                     .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                     .Take(query.PageSize.Value)
                     .Select(x => new TaskWorkView()
                     {
                         Id = x.Id,
                         RoomId = x.RoomId.ToString(),
                         UserId = x.UserId.ToString(),
                         SubjectId = x.SubjectId.ToString(),

                         CreatedBy = x.CreatedBy,
                         CreatedAt = x.CreatedAt,
                         UpdatedAt = x.UpdatedAt,
                         IsActive = x.IsActive,
                     })
                     .ApplyFilter(query)
                     .OrderByColums(query.SortColums, true).ToListAsync();

            var response = new DataResult<TaskWorkView>();
            response.TotalRecords = await _unitOfWork.GetRepository<TaskWork>().AsQueryable().CountAsync();
            response.Items = taskWork;
            return response;
        }

        public async Task<DataResult<int>> UpdateAsync(TaskWorkQuery entity, string id)
        {
            var result = new DataResult<int>();
            var taskWork = await _unitOfWork.GetRepository<TaskWork>().GetByIdAsync(Guid.Parse(id));
            if (taskWork == null)
            {
                result.Errors.Add("TaskWork not found");
                return result;
            }
            // Validate
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<TaskWorkQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                result.Errors.AddRange(resultValidator.JoinError());
                return result;
            }
            taskWork.RoomId = Guid.Parse(entity.RoomId);
            taskWork.UserId = Guid.Parse(entity.UserId);
            taskWork.SubjectId = Guid.Parse(entity.SubjectId);

            _unitOfWork.GetRepository<TaskWork>().Update(taskWork);
            result.Entity = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
