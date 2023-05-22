using System.ComponentModel.DataAnnotations;
using HMZ.DTOs.Queries;
using HMZ.Service.Extensions;
using HMZ.Service.Services.SubjectServices;

namespace HMZ.Service.Validator.ModelValidators
{
    public class SubjectValidator: IValidator<SubjectQuery>
    {
        private readonly ISubjectService _subjectService;
        public SubjectValidator(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
       
        public  async Task<List<ValidationResult>> ValidateAsync(SubjectQuery entity, string? userName = null, bool? isUpdate = false)
        {
            if (entity == null)
            {
                return new List<ValidationResult>(){
                    new ValidationResult("Entity is null", new[] { nameof(entity) })
                };
            }
            // check exist for add  
            if (isUpdate != true)
            {
                var subject = await _subjectService.GetByCodeAsync(entity.Code);
                if (subject != null)
                {
                    return new List<ValidationResult>(){
                    new ValidationResult("Permission is exist", new[] { nameof(entity.Code) })
                };
                }
            }

            var result = new List<ValidationResult>(){
                ValidatorCustom.IsRequired(nameof(entity.Name), entity.Name),
                ValidatorCustom.IsRequired(nameof(entity.DepartmentId), entity.DepartmentId),
            };
            return await Task.FromResult(result);
        }
    }
}