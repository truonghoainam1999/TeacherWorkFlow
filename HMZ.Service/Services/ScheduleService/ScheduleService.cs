using HMZ.Database.Entities;
using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
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
using System;
using System.ComponentModel.DataAnnotations;

namespace HMZ.Service.Services.ScheduleService
{
    public class ScheduleService : ServiceBase<IUnitOfWork>, IScheduleService
    {
        private readonly IServiceProvider _serviceProvider;

        public ScheduleService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider) : base(unitOfWork)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<DataResult<bool>> CreateAsync(ScheduleQuery entity)
        {
            var result = new DataResult<bool>();
            // Validate
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<ScheduleQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                result.Errors.AddRange(resultValidator.JoinError());
                return result;
            }
            var schdule = new Schedule
            {
                Time = entity.Time,
                Day = entity.Date,
                Week = entity.Week,
                RoomId = Guid.Parse(entity.RoomId)
            };
            await _unitOfWork.GetRepository<Schedule>().Add(schdule);
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
            var subject = await _unitOfWork.GetRepository<Schedule>().GetByIdAsync(Guid.Parse(id));
            if (subject == null)
            {
                result.Errors.Add("Permission not found");
                return result;
            }
            _unitOfWork.GetRepository<Schedule>().Delete(subject, false);
            result.Entity = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<DataResult<ScheduleView>> GetByCodeAsync(string scheduleCode)
        {
            var result = new DataResult<ScheduleView>();
            var schedule = await _unitOfWork.GetRepository<Schedule>().AsQueryable().FirstOrDefaultAsync(x => x.Code == scheduleCode);
            if (schedule == null)
            {
                result.Errors.Add("Schedule not found");
                return result;
            }
            result.Entity = new ScheduleView
            {
                Id = schedule.Id,
                Time = schedule.Time,
                Day = schedule.Day,
                Week = schedule.Week,
                RoomId = schedule.RoomId.ToString(),
            };
            return result;
        }

        public async Task<DataResult<ScheduleView>> GetByIdAsync(string id)
        {
            var result = new DataResult<ScheduleView>();
            var schedule = await _unitOfWork.GetRepository<Schedule>().GetByIdAsync(Guid.Parse(id));
            if (schedule == null)
            {
                result.Errors.Add("Schedule not found");
                return result;
            }
            result.Entity = new ScheduleView
            {
                Id = schedule.Id,
                Code = schedule.Code,
                Time = schedule.Time,
                Day = schedule.Day,
                Week = schedule.Week,
                RoomId = schedule.RoomId.ToString(),
                CreatedBy = schedule.CreatedBy,
                CreatedAt = schedule.CreatedAt,
                UpdatedAt = schedule.UpdatedAt,
                IsActive = schedule.IsActive,
            };
            return result;
        }

        public async Task<DataResult<ScheduleView>> GetPageList(BaseQuery<ScheduleFilter> query)
        {
            var schedules = await _unitOfWork.GetRepository<Schedule>().AsQueryable()
                     .Include(x => x.Room)
                     .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                     .Take(query.PageSize.Value)
                     .Select(x => new ScheduleView()
                     {
                         Id = x.Id,
                         Code = x.Code,
                         Time = x.Time,
                         Day = x.Day,
                         Week = x.Week,
                         RoomId = x.RoomId.ToString(),
                         RoomName = x.Room.Name,

                         CreatedBy = x.CreatedBy,
                         CreatedAt = x.CreatedAt,
                         UpdatedAt = x.UpdatedAt,
                         IsActive = x.IsActive,
                     })
                     .ApplyFilter(query)
                     .OrderByColums(query.SortColums, true).ToListAsync();

            var response = new DataResult<ScheduleView>();
            response.TotalRecords = await _unitOfWork.GetRepository<Schedule>().AsQueryable().CountAsync();
            response.Items = schedules;
            return response;
        }

        public async Task<DataResult<int>> UpdateAsync(ScheduleQuery entity, string id)
        {
            var result = new DataResult<int>();
            var schedules = await _unitOfWork.GetRepository<Schedule>().GetByIdAsync(Guid.Parse(id));
            if (schedules == null)
            {
                result.Errors.Add("Schedule not found");
                return result;
            }
            // Validate
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<ScheduleQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                result.Errors.AddRange(resultValidator.JoinError());
                return result;
            }
            schedules.Day = entity.Date;
            schedules.Time = entity.Time;
            schedules.Week = entity.Week;
            schedules.RoomId = Guid.Parse(entity.RoomId);

            _unitOfWork.GetRepository<Schedule>().Update(schedules);
            result.Entity = await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
