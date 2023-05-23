using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Catalog;
using HMZ.Service.Extensions;
using HMZ.Service.Services.ScheduleService;
using HMZ.Service.Services.SubjectServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.Service.Validator.ModelValidators
{
    public class ScheduleValidator : IValidator<ScheduleQuery>
    {
        private readonly IScheduleService _schduleService;
        public ScheduleValidator(IScheduleService scheduleService)
        {
            _schduleService = scheduleService;
        }

        public async Task<List<ValidationResult>> ValidateAsync(ScheduleQuery entity, string? userName = null, bool? isUpdate = false)
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
                var subject = await _schduleService.GetByCodeAsync(entity.Code);
                if (subject.Entity != null)
                {
                    return new List<ValidationResult>(){
                    new ValidationResult("Subject is exist", new[] { nameof(entity.Code) })
                };
                }
            }

            var result = new List<ValidationResult>(){
                ValidatorCustom.IsRequired(nameof(entity.RoomId), entity.RoomId),
            };
            return result;
        }
    }
}
