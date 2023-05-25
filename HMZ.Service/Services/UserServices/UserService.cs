using System.ComponentModel.DataAnnotations;
using HMZ.Database.Entities;
using HMZ.Database.Enums;
using HMZ.DTOs.Fillters;
using HMZ.DTOs.Queries;
using HMZ.DTOs.Queries.Base;
using HMZ.DTOs.Views;
using HMZ.SDK.Extensions;
using HMZ.Service.Extensions;
using HMZ.Service.Helpers;
using HMZ.Service.MailServices;
using HMZ.Service.Services.BaseService;
using HMZ.Service.Services.TokenServices;
using HMZ.Service.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace HMZ.Service.Services.UserServices
{
    public class UserService : ServiceBase<IUnitOfWork>, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMailService _mailService;
        private readonly IServiceProvider _serviceProvider;
        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager
        , IServiceProvider serviceProvider, SignInManager<User> signInManager, ITokenService tokenService, IMailService mailService) : base(unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mailService = mailService;
            _serviceProvider = serviceProvider;
        }
        public async Task<DataResult<bool>> CreateAsync(UserQuery entity)
        {
            var response = new DataResult<bool>();
            if (entity == null)
            {
                response.Errors.Add("User is null");
                return response;
            }
            var user = new User()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Image = "https://icon-library.com/images/default-user-icon/default-user-icon-13.jpg",
                Email = entity.Email,
                UserName = entity.Email.Split("@")[0].ToLower(),
                DateOfBirth = entity.DateOfBirth.Value,
                CreatedAt = DateTime.Now,
            };
            var result = await _userManager.CreateAsync(user, entity.Password);
            if (!result.Succeeded)
            {
                response.Errors.Add("Create user failed");
                return response;
            }
            entity.Role = entity.Role != null && entity.Role.Length > 0 ? entity.Role : new string[] { EUserRoles.Member.ToString() };
            var roleResult = await _userManager.AddToRolesAsync(user, entity.Role);
            if (!roleResult.Succeeded)
            {
                response.Errors.Add("Add role failed");
                return response;
            }
            response.Entity = true;
            return response;
        }
        public async Task<DataResult<int>> DeleteAsync(string id)
        {
            var response = new DataResult<int>();
            if (string.IsNullOrEmpty(id))
            {
                response.Errors.Add("Id is null");
                return response;
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                response.Errors.Add("User not found");
                return response;
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                response.Errors.Add(result.Errors.First().Description);
                return response;
            }
            response.Entity = 1;
            return response;
        }
        public async Task<DataResult<UserView>> GetByIdAsync(string id)
        {
            var response = new DataResult<UserView>();
            if (string.IsNullOrEmpty(id))
            {
                response.Errors.Add("Id is null");
                return response;
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                response.Errors.Add("User not found");
                return response;
            }
            response.Entity = new UserView()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Username = user.UserName,
                Image = user.Image,
                Roles = _userManager.GetRolesAsync(user).Result.ToList(),
                // default  properties
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                CreatedBy = user.CreatedBy,
                IsActive = user.IsActive
            };
            return response;
        }
        public Task<DataResult<UserView>> GetPageList(BaseQuery<UserFilter> query)
        {
            var users = _userManager.Users.ApplyFilter(query)
                    .Skip((query.PageNumber.Value - 1) * query.PageSize.Value)
                    .Take(query.PageSize.Value)
                    .OrderByColums(query.SortColums, true).ToList();

            var response = new DataResult<UserView>();
            response.TotalRecords = users.Count();
            response.Items = users.Select(x => new UserView()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                DateOfBirth = x.DateOfBirth,
                Username = x.UserName,
                Image = x.Image,
                Roles = _userManager.GetRolesAsync(x).Result.ToList(),
                // default  properties
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                CreatedBy = x.CreatedBy,
                IsActive = x.IsActive
            }).ToList();
            return System.Threading.Tasks.Task.FromResult(response);
        }
        public async Task<DataResult<int>> UpdateAsync(UserQuery entity, string id)
        {
            var response = new DataResult<int>();
            if (entity == null)
            {
                response.Errors.Add("Entity is null");
                return response;
            }
            if (string.IsNullOrEmpty(id))
            {
                response.Errors.Add("Id is null");
                return response;
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                response.Errors.Add("User not found");
                return response;
            }
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Email = entity.Email;
            user.DateOfBirth = entity.DateOfBirth.Value;
            user.UserName = entity.Email.Split("@")[0].ToLower();

            user.UpdatedAt = DateTime.Now;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                response.Errors.Add(result.Errors.Select(x => x.Description).FirstOrDefault());
                return response;
            }
            response.Entity = 1;
            return response;
        }
        public async Task<DataResult<UserView>> Login(LoginQuery entity)
        {
            var response = new DataResult<UserView>();
            // Validate
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<LoginQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                response.Errors.AddRange(resultValidator.JoinError());
                return response;
            }

            // check email or username
            if (entity.Email.Contains("@"))
            {
                var user = await _userManager.FindByEmailAsync(entity.Email);
                var result = await _signInManager.CheckPasswordSignInAsync(user, entity.Password, entity.RememberMe.Value);
                if (result.IsLockedOut)
                {
                    response.Errors.Add("Account is locked");
                    return response;
                }
                if (!result.Succeeded)
                {
                    response.Errors.Add("Password is incorrect! Wrong input more than 5 times will be locked");
                    return response;
                }
                response.Entity = new UserView()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    Username = user.UserName,
                    Image = user.Image,
                    Roles = _userManager.GetRolesAsync(user).Result.ToList(),
                    Token = await _tokenService.CreateToken(user),
                    // default  properties
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                    CreatedBy = user.CreatedBy,
                    IsActive = user.IsActive
                };
                return response;
            }
            else
            {
                var user = await _userManager.FindByNameAsync(entity.Email);
                var result = await _signInManager.CheckPasswordSignInAsync(user, entity.Password, entity.RememberMe.Value);
                if (result.IsLockedOut)
                {
                    response.Errors.Add("Account is locked");
                    return response;
                }
                if (!result.Succeeded)
                {
                    response.Errors.Add("Password is incorrect! Wrong input more than 5 times will be locked");
                    return response;
                }
                response.Entity = new UserView()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    Username = user.UserName,
                    Image = user.Image,
                    Roles = _userManager.GetRolesAsync(user).Result.ToList(),
                    Token = await _tokenService.CreateToken(user),
                    // default  properties
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                    CreatedBy = user.CreatedBy,
                    IsActive = user.IsActive
                };
                return response;
            }
        }
        public async Task<DataResult<UserView>> Register(RegisterQuery entity)
        {
            var response = new DataResult<UserView>();
            // Validate
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<RegisterQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                response.Errors.AddRange(resultValidator.JoinError());
                return response;
            }
            var user = new User()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                DateOfBirth = entity.DateOfBirth.HasValue ? entity.DateOfBirth.Value : DateTime.Now,
                UserName = entity.Email.Split("@")[0].ToLower(),
                CreatedAt = DateTime.Now,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, entity.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, EUserRoles.Member.ToString());
            }
            if (result.Errors.Any())
            {
                response.Errors.AddRange(result.Errors.Select(x => x.Description));
                return response;
            }
            response.Entity = new UserView()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Username = user.UserName,
                Image = user.Image,
                Roles = _userManager.GetRolesAsync(user).Result.ToList(),
                // default  properties
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                CreatedBy = user.CreatedBy,
                IsActive = user.IsActive
            };
            return response;
        }
        public async Task<DataResult<bool>> ChangeRole(ChangeRoleQuery entity)
        {
            var response = new DataResult<bool>();
            if (entity == null)
            {
                response.Errors.Add("Entity is null");
                return response;
            }
            var user = await _userManager.FindByIdAsync(entity.UserId);
            if (user == null)
            {
                response.Errors.Add("User not found");
                return response;
            }
            var roles = _userManager.GetRolesAsync(user).Result.ToList();
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                response.Errors.Add(result.Errors.Select(x => x.Description).FirstOrDefault());
                return response;
            }
            result = await _userManager.AddToRolesAsync(user, entity.Roles);
            if (!result.Succeeded)
            {
                response.Errors.Add(result.Errors.Select(x => x.Description).FirstOrDefault());
                return response;
            }
            response.Entity = true;
            return response;
        }
        public async Task<DataResult<UserView>> GetByUserName(string userName)
        {
            var response = new DataResult<UserView>();
            if (string.IsNullOrEmpty(userName))
            {
                response.Errors.Add("UserName is null");
                return response;
            }
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                response.Errors.Add("User not found");
                return response;
            }
            response.Entity = new UserView()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Username = user.UserName,
                Image = user.Image,
                Roles = _userManager.GetRolesAsync(user).Result.ToList(),
                // default  properties
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                CreatedBy = user.CreatedBy,
                IsActive = user.IsActive
            };
            return response;
        }
        public async Task<DataResult<bool>> UpdatePassword(UpdatePasswordQuery entity)
        {
            var response = new DataResult<bool>();

            // Validate
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<UpdatePasswordQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                response.Errors.AddRange(resultValidator.JoinError());
                return response;
            }

            var user = await _userManager.FindByEmailAsync(entity.Email);
            if (user == null)
            {
                response.Errors.Add("User not found");
                return response;
            }
            var result = await _userManager.ChangePasswordAsync(user, entity.OldPassword, entity.NewPassword);
            if (!result.Succeeded)
            {
                response.Errors.Add(result.Errors.Select(x => x.Description).FirstOrDefault());
                return response;
            }
            response.Entity = true;
            return response;
        }
        public async Task<DataResult<bool>> UploadAvatar(UploadAvatarQuery entity)
        {
            var response = new DataResult<bool>();
            if (entity == null)
            {
                response.Errors.Add("Entity is null");
                return response;
            }
            var user = await _userManager.FindByIdAsync(entity.UserId);
            if (user == null)
            {
                response.Errors.Add("User not found");
                return response;
            }
            user.Image = entity.ImagePath;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                response.Errors.Add(result.Errors.Select(x => x.Description).FirstOrDefault());
                return response;
            }
            response.Entity = true;
            return response;
        }

        public async Task<DataResult<bool>> ForgotPassword(string email, string host)
        {
            var response = new DataResult<bool>();
            if (string.IsNullOrEmpty(email))
            {
                response.Errors.Add("Email is null");
                return response;
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                response.Errors.Add("User not found");
                return response;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // get localhost url
            string url = $"{host}/ResetPassword?token={token}&email={email}";
            MailQuery mailQuery = new MailQuery()
            {
                ToEmails = new List<string>() { email },
                Subject = "Reset Password",
                Body = $"<h1>Reset Password</h1> <p>To reset your password, click here: <a href='{url}'>Reset Password</a></p>"
            };
            await _mailService.SendEmailAsync(mailQuery);
            response.Entity = true;
            return response;

        }

        public async Task<DataResult<bool>> ResetPassword(UpdatePasswordQuery entity)
        {
            var response = new DataResult<bool>();
            // Validate
            await using var scope = _serviceProvider.CreateAsyncScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<UpdatePasswordQuery>>();
            List<ValidationResult> resultValidator = new List<ValidationResult>();
            if (validator != null)
                resultValidator = await validator.ValidateAsync(entity);
            if (resultValidator.HasError())
            {
                response.Errors.AddRange(resultValidator.JoinError());
                return response;
            }
            var user = await _userManager.FindByEmailAsync(entity.Email);
            var result = await _userManager.ResetPasswordAsync(user, entity.Token, entity.NewPassword);
            if (!result.Succeeded)
            {
                response.Errors.Add(result.Errors.Select(x => x.Description).FirstOrDefault());
                return response;
            }
            response.Entity = true;
            return response;
        }

        public async Task<DataResult<bool>> LockUser(string username, bool isLock)
        {
            var response = new DataResult<bool>();
            if (string.IsNullOrEmpty(username))
            {
                response.Errors.Add("UserName is null");
                return response;
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                response.Errors.Add("User not found");
                return response;
            }
            user.LockoutEnabled = isLock;
            user.LockoutEnd = isLock ? DateTime.Now.AddYears(100) : null;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                response.Errors.Add(result.Errors.Select(x => x.Description).FirstOrDefault());
                return response;
            }
            response.Entity = true;
            return response;
        }

        public async Task<DataResult<UserView>> GetAll()
        {
            var users = await _unitOfWork.GetRepository<User>().AsQueryable()
                      .Select(x => new UserView()
                      {
                          Id = x.Id,
                          IdString = x.Id.ToString().ToLower(),
                          Username = x.UserName,
                          FirstName = x.FirstName,
                          LastName = x.LastName,
                          Email = x.Email,
                          DateOfBirth = x.DateOfBirth,
                          CreatedBy = x.CreatedBy,
                          CreatedAt = x.CreatedAt,
                          UpdatedAt = x.UpdatedAt,
                          IsActive = x.IsActive,
                      }).ToListAsync();
            var response = new DataResult<UserView>();
            response.Items = users;
            return response;
        }
    }
}
