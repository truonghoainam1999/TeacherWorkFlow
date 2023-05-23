using HMZ.DTOs.Queries.Catalog;
using HMZ.Service.Extensions;
using HMZ.Service.Services.ClassRoomService;
using System.ComponentModel.DataAnnotations;

namespace HMZ.Service.Validator.ModelValidators
{
    public class ClassRoomValidator : IValidator<ClassRoomQuery>
    {
        private readonly IClassRoomService _classRoomService;
        public ClassRoomValidator(IClassRoomService classRoomService)
        {
            _classRoomService = classRoomService;
        }

        public async Task<List<ValidationResult>> ValidateAsync(ClassRoomQuery entity, string? userName = null, bool? isUpdate = false)
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
                var classRoom = await _classRoomService.GetByCodeAsync(entity.Code);
                if (classRoom.Entity != null)
                {
                    return new List<ValidationResult>(){
                    new ValidationResult("ClassRoom is exist", new[] { nameof(entity.Code) })
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
