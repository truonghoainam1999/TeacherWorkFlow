using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Queries.Catalog;
using HMZ.DTOs.Views;
using HMZ.Service.Extensions;
using HMZ.Service.Helpers;
using HMZ.Service.Services.BaseService;
using HMZ.Service.Validator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using HMZ.Database.Entities;
using HMZ.SDK.Extensions;
using HMZ.DTOs.Queries;

namespace HMZ.Service.Services.ClassRoomService
{
    public class ClassRoomService : ServiceBase<IUnitOfWork>, IClassRoomService
    {
        private readonly IServiceProvider _serviceProvider;

        public ClassRoomService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider) : base(unitOfWork)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<DataResult<bool>> CreateAsync(ClassRoomQuery entity)
        {
            var result = new DataResult<bool>();
            // Validate
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<ClassRoomQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                result.Errors.AddRange(resultValidator.JoinError());
                return result;
            }
            var classRoom = new ClassRoom
            {
                Name = entity.Name,

            };
            await _unitOfWork.GetRepository<ClassRoom>().Add(classRoom);
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
            var classRoom = await _unitOfWork.GetRepository<ClassRoom>().GetByIdAsync(Guid.Parse(id));
            if (classRoom == null)
            {
                result.Errors.Add("ClassRoom not found");
                return result;
            }
            _unitOfWork.GetRepository<ClassRoom>().Delete(classRoom, false);
            var res = await _unitOfWork.SaveChangesAsync();
            result.Entity = res;
            return result;
        }

        public async Task<DataResult<ClassRoomView>> GetByCodeAsync(string roomCode)
        {
            var result = new DataResult<ClassRoomView>();
            var room = await _unitOfWork.GetRepository<ClassRoom>().AsQueryable().FirstOrDefaultAsync(x => x.Code == roomCode);
            if (room == null)
            {
                result.Errors.Add("ClassRoom not found");
                return result;
            }
            result.Entity = new ClassRoomView
            {
               Id = room.Id,
               Code = room.Code,
               Name = room.Name,
            };
            return result;
        }

        public async Task<DataResult<ClassRoomView>> GetAll()
        {
            var classRooms = await _unitOfWork.GetRepository<ClassRoom>().AsQueryable()
                      .Select(x => new ClassRoomView()
                      {
                          Id = x.Id,
                          IdString = x.Id.ToString().ToLower(),
                          Name = x.Name,
                          Code = x.Code,
                          CreatedBy = x.CreatedBy,
                          CreatedAt = x.CreatedAt,
                          UpdatedAt = x.UpdatedAt,
                          IsActive = x.IsActive,
                      }).ToListAsync();
            var response = new DataResult<ClassRoomView>();
            response.Items = classRooms;
            return response;
        }

        public async Task<DataResult<ClassRoomView>> GetByIdAsync(string id)
        {
            var result = new DataResult<ClassRoomView>();
            var room = await _unitOfWork.GetRepository<ClassRoom>().GetByIdAsync(Guid.Parse(id));
            if (room == null)
            {
                result.Errors.Add("ClassRoom not found");
                return result;
            }
            result.Entity = new ClassRoomView
            {
                Id = room.Id,
                Code = room.Code,
                Name = room.Name,
                CreatedBy = room.CreatedBy,
                CreatedAt = room.CreatedAt,
                UpdatedAt = room.UpdatedAt,
                IsActive = room.IsActive,
            };
            return result;
        }

        public async Task<DataResult<ClassRoomView>> GetPageList(BaseQuery<ClassRoomFilter> query)
        {
            var subjects = await _unitOfWork.GetRepository<ClassRoom>().AsQueryable()
                     .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                     .Take(query.PageSize.Value)
                     .Select(x => new ClassRoomView()
                     {
                         Id = x.Id,
                         Code = x.Code,
                         Name = x.Name,
                         CreatedBy = x.CreatedBy,
                         CreatedAt = x.CreatedAt,
                         UpdatedAt = x.UpdatedAt,
                         IsActive = x.IsActive,
                     })
                     .ApplyFilter(query)
                     .OrderByColums(query.SortColums, true).ToListAsync();

            var response = new DataResult<ClassRoomView>();
            response.TotalRecords = await _unitOfWork.GetRepository<ClassRoom>().AsQueryable().CountAsync();
            response.Items = subjects;
            return response;
        }

        public async Task<DataResult<int>> UpdateAsync(ClassRoomQuery entity, string id)
        {
            var result = new DataResult<int>();
            var room = await _unitOfWork.GetRepository<ClassRoom>().GetByIdAsync(Guid.Parse(id));
            if (room == null)
            {
                result.Errors.Add("ClassRoom not found");
                return result;
            }
            // Validate
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<ClassRoomQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                result.Errors.AddRange(resultValidator.JoinError());
                return result;
            }
            room.Name = entity.Name;

            _unitOfWork.GetRepository<ClassRoom>().Update(room);
            result.Entity = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
