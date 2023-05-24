using HMZ.Database.Entities;
using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Queries.Catalog;
using HMZ.DTOs.Views;
using HMZ.SDK.Extensions;
using HMZ.Service.Extensions;
using HMZ.Service.Helpers;
using HMZ.Service.MailServices;
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
        private readonly IMailService _mailService;

        public TaskWorkService(IUnitOfWork unitOfWork, IServiceProvider serviceProvider, IMailService mailService) : base(unitOfWork)
        {
            _serviceProvider = serviceProvider;
            _mailService = mailService;
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
                Id = Guid.NewGuid(),
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                RoomId = Guid.Parse(entity.RoomId),
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

            var mailRequest = new MailQuery
            {
                ToEmails = new List<string> { "dangcongvinh328@gmail.com" },
                Subject = "Thông báo tạo mới TaskWork",
                Body = "Đã có công việc mới mời bạn kiểm tra.",
                Url = "",
                Attachments = null,
            };

            var emailResult = await _mailService.SendEmailAsync(mailRequest);
            if (!emailResult.Entity)
            {
                result.Errors.Add("Error send mail");
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
            .Include(x => x.ClassRoom)
            .Include(x => x.User)
            .Include(x => x.Subject)
                      .Select(x => new TaskWorkView()
                      {
                          Id = x.Id,
                          Code = x.Code,
                          RoomName = x.ClassRoom.Name,
                          Username = x.User.UserName,
                          SubjectName = x.Subject.Name,
                          StartDate = x.StartDate.ToString("dd mmm yyyy"),
                          EndDate = x.EndDate.ToString("dd mmm yyyy"),
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
                StartDate = taskWork.StartDate.ToString("dd MMM yyyy"),
                EndDate = taskWork.EndDate.ToString("dd MMM yyyy"),
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
                         Code = x.Code,
                         RoomName = x.ClassRoom.Name,
                         Username = x.User.UserName,
                         SubjectName = x.Subject.Name,
                         RoomId = x.RoomId.ToString(),
                         UserId = x.UserId.ToString(),
                         SubjectId = x.SubjectId.ToString(),
                         StartDate = x.StartDate.ToString("dd MMM yyyy"),
                         EndDate = x.EndDate.ToString("dd MMM yyyy"),
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
            await ExecuteDailyTask();
            return response;
        }

        public async Task ExecuteDailyTask()
        {
            DateTime currentDate = DateTime.Today;
            DateTime checkDate = currentDate.AddDays(+2);

            var tasks = await _unitOfWork.GetRepository<TaskWork>()
                .AsQueryable()
                .Where(x => x.EndDate.Date == checkDate.Date || x.EndDate < currentDate)
                .ToListAsync();

            foreach (var task in tasks)
            {
                string subject;
                string body;

                if (task.EndDate.Date == checkDate.Date)
                {
                    subject = "Xin thông báo thời hạn công việc của bạn sắp đến hạn";
                    body = $"Công việc mã {task.Code} sẽ hết hạn vào ngày {task.EndDate.ToString("dd MMM yyyy")}.";
                }
                else
                {
                    subject = "Công việc đã quá hạn";
                    body = $"Công việc mã {task.Code} đã quá hạn.Vui lòng liên hệ hội đồng";
                }

                // Gửi email
                MailQuery mailRequest = new MailQuery
                {
                    ToEmails = new List<string> { task.User.Email }, 
                    Subject = subject,
                    Body = body
                };
                await _mailService.SendEmailAsync(mailRequest);
            }
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

            try
            {
                taskWork.StartDate = entity.StartDate;
                taskWork.EndDate = entity.EndDate;
                taskWork.RoomId = Guid.Parse(entity.RoomId);
                taskWork.UserId = Guid.Parse(entity.UserId);
                taskWork.SubjectId = Guid.Parse(entity.SubjectId);

                _unitOfWork.GetRepository<TaskWork>().Update(taskWork);
                await _unitOfWork.SaveChangesAsync();

                result.Entity = 1;
            }
            catch (Exception ex)
            {
                // Handle the exception and log the error
                result.Errors.Add("Error while update: " + ex.Message);
            }

            return result;
        }

    }
}
