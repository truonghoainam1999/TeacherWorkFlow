using HMZ.Database.Data;
using HMZ.DTOs.Models;
using HMZ.DTOs.Queries;
using HMZ.Service.MailServices;
using HMZ.Service.Services;
using HMZ.Service.Services.DashboardServices;
using HMZ.Service.Services.DepartmentServices;
using HMZ.Service.Services.FileServices;
using HMZ.Service.Services.PermissionServices;
using HMZ.Service.Services.RoleServices;
using HMZ.Service.Services.SubjectServices;
using HMZ.Service.Services.TokenServices;
using HMZ.Service.Services.UserServices;
using HMZ.Service.Validator;
using HMZ.Service.Validator.ModelValidators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HMZ.Service.Extensions
{
    public static class HMZExtensionService
    {
        public static IServiceCollection AddHMZServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region  Entity DI
            // Default DI
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            // DI Authen
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            // Custom DI

            services.AddTransient(typeof(IUserService), typeof(UserService));
			services.AddTransient(typeof(IFileService), typeof(FileService));
            services.AddTransient(typeof(IMailService), typeof(MailService));
            services.AddTransient(typeof(IPermissionService), typeof(PermissionService));
            services.AddTransient(typeof(IRoleService), typeof(RoleService));
            services.AddTransient(typeof(IDashboardService), typeof(DashboardService));
            services.AddTransient(typeof(ISubjectService), typeof(SubjectService));
            services.AddTransient(typeof(IDepartmentService), typeof(DepartmentService));
            
            


            #endregion

            #region Microservice DI
            #endregion

            #region Validate Extension DI

            services.AddTransient(typeof(IValidator<RegisterQuery>), typeof(RegisterValidator));
            services.AddTransient(typeof(IValidator<UpdatePasswordQuery>), typeof(UpdatePasswordValidator));
            services.AddTransient(typeof(IValidator<UpdatePasswordQuery>), typeof(ResetPasswordValidator));
            services.AddTransient(typeof(IValidator<LoginQuery>), typeof(LoginValidator));
            services.AddTransient(typeof(IValidator<PermissionQuery>), typeof(PermissionValidator));
            services.AddTransient(typeof(IValidator<RoleQuery>), typeof(RoleValidator));
            services.AddTransient(typeof(IValidator<SubjectQuery>), typeof(SubjectValidator));



            #endregion

            #region Extension DI
            // Add ConnectionString
            services.AddDbContext<HMZContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
            ));
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            #endregion
            return services;
        }
    }
}
