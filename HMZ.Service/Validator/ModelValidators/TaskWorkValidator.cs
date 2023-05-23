using HMZ.DTOs.Queries.Catalog;
using HMZ.Service.Extensions;
using HMZ.Service.Services.TaskWorkServices;
using System.ComponentModel.DataAnnotations;

namespace HMZ.Service.Validator.ModelValidators
{
    public class TaskWorkValidator : IValidator<TaskWorkQuery>
    {
        private readonly ITaskWorkService _taskWorkService;
        public TaskWorkValidator(ITaskWorkService taskWorkService)
        {
            _taskWorkService = taskWorkService;
        }

        public async Task<List<ValidationResult>> ValidateAsync(TaskWorkQuery entity, string? userName = null, bool? isUpdate = false)
        {
            if (entity == null)
            {
                return new List<ValidationResult>(){
                    new ValidationResult("Entity is null", new[] { nameof(entity) })
                };
            }
            // check exist for add  
            if (isUpdate == false)
            {
                var taskWork = await _taskWorkService.GetByCodeAsync(entity.Code);
                if (taskWork.Entity != null)
                {
                    return new List<ValidationResult>(){
                    new ValidationResult("TaskWork is exist", new[] { nameof(entity.Code) })
                };
                }
            }

            var result = new List<ValidationResult>(){
                ValidatorCustom.IsRequired(nameof(entity.RoomId), entity.RoomId),
                ValidatorCustom.IsRequired(nameof(entity.UserId), entity.UserId),
                ValidatorCustom.IsRequired(nameof(entity.SubjectId), entity.SubjectId),
            };
            return result;
        }
    }
}
