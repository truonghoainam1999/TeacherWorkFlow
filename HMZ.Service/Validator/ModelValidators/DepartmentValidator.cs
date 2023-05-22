using HMZ.DTOs.Queries;
using HMZ.Service.Extensions;
using HMZ.Service.Services.DepartmentServices;
using HMZ.Service.Services.SubjectServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.Service.Validator.ModelValidators
{
    public class DepartmentValidator : IValidator<DepartmentQuery>
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentValidator(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<List<ValidationResult>> ValidateAsync(DepartmentQuery entity, string? userName = null, bool? isUpdate = false)
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
                var department = await _departmentService.GetByCodeAsync(entity.Code);
                if (department.Entity != null)
                {
                    return new List<ValidationResult>(){
                    new ValidationResult("Department is exist", new[] { nameof(entity.Code) })
                };
                }
            }

            var result = new List<ValidationResult>(){
                ValidatorCustom.IsRequired(nameof(entity.Name), entity.Name),
            };
            return result;
        }
    }
}
