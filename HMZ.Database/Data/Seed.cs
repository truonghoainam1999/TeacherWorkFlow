using HMZ.Database.Entities;
using HMZ.Database.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace HMZ.Database.Data
{
    public class Seed
    {
        public async static Task<int> SeedUser(UserManager<User> userManager, RoleManager<Role> roleManager, HMZContext context)
        {
            if (userManager.Users.Any())
            {
                return 0;
            }
            string path = Directory.GetCurrentDirectory() + "\\Resoucres\\";
           
            if (!File.Exists(path + "UserSeedData.json"))
            {
                CreateUser(userManager, roleManager, context);
            }
            if (!File.Exists(path + "PermissionSeedData.json"))
            {
                CreatePermission(userManager, roleManager, context);
            }

            var users = await File.ReadAllTextAsync(path+ "UserSeedData.json");
            var usersToSeed = System.Text.Json.JsonSerializer.Deserialize<List<User>>(users);
            var roles = new List<Role>
            {
                new Role(){ Name=EUserRoles.Admin.ToString()},
                new Role(){ Name=EUserRoles.Member.ToString()},
                new Role(){ Name=EUserRoles.Mod.ToString()},
                new Role(){ Name=EUserRoles.Author.ToString()},

            };

            // add permission
            var permissions = await File.ReadAllTextAsync(path + "PermissionSeedData.json");
            var permissionsToSeed = System.Text.Json.JsonSerializer.Deserialize<List<Permission>>(permissions);
            foreach (var permission in permissionsToSeed)
            {
                await context.Permissions.AddAsync(permission);
            }
            await context.SaveChangesAsync();

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);

                // add permission to role admin
                if (role.Name.Equals(EUserRoles.Admin.ToString()))
                {
                    context.RolePermissions.AddRange(permissionsToSeed.Select(x => new RolePermission()
                    {
                        RoleId = role.Id,
                        PermissionId = x.Id.Value,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                    }));
                }

            }
            await context.SaveChangesAsync();


            // add user
            foreach (var user in usersToSeed)
            {
                user.UserName = user.UserName.ToLower();
                user.CreatedAt = DateTime.Now;
                user.IsActive = true;
                user.CreatedBy = "System";
                await userManager.CreateAsync(user, "Abc12345@");
                if (user.UserName.Equals(EUserRoles.Admin.ToString().ToLower()))
                {
                    await userManager.AddToRoleAsync(user, EUserRoles.Admin.ToString());
                    await userManager.AddToRoleAsync(user, EUserRoles.Mod.ToString());

                }
                else if (user.UserName.Equals(EUserRoles.Mod.ToString().ToLower()))
                {
                    await userManager.AddToRoleAsync(user, EUserRoles.Mod.ToString());
                }
                else if (user.UserName.Equals(EUserRoles.Author.ToString().ToLower()))
                {
                    await userManager.AddToRoleAsync(user, EUserRoles.Author.ToString());
                }
                else
                {
                    await userManager.AddToRoleAsync(user, EUserRoles.Member.ToString());
                }

            };
            return usersToSeed.Count;
        }

        private static void CreatePermission(UserManager<User> userManager, RoleManager<Role> roleManager, HMZContext context)
        {
            var permission = new List<Permission>
            {
                // Permission
                new Permission(){ Key="ReadPermission", Description="Read Permission",Value="READ"},
                new Permission(){ Key="WritePermission", Description="Write Permission",Value="WRITE"},
                // User
                new Permission(){ Key="UserRead", Description="User Read",Value="READ"},
                new Permission(){ Key="UserWrite", Description="User Write",Value="WRITE"},
                // Role
                new Permission(){ Key="RoleRead", Description="Role Read",Value="READ"},
                new Permission(){ Key="RoleWrite", Description="Role Write",Value="WRITE"},
                // Notification
                new Permission(){ Key="NotificationRead", Description="Notification Read",Value="READ"},
                new Permission(){ Key="NotificationWrite", Description="Notification Write",Value="WRITE"},
            };
            // Save Permission to FILE
            var path = Directory.GetCurrentDirectory() + "\\Resoucres\\";
            var permissions = System.Text.Json.JsonSerializer.Serialize(permission);
            File.WriteAllText(path + "PermissionSeedData.json", permissions);

        }

        private static void CreateUser(UserManager<User> userManager, RoleManager<Role> roleManager, HMZContext context)
        {
            var users = new List<User>
            {
                new User(){ UserName="admin", Email="dangcongvinh328@gmail.com",FirstName="Admin",LastName="Admin",DateOfBirth = new DateTime(01,01,1999)},
                new User(){ UserName="mod", Email="mod@hmz.com", FirstName="Mod",LastName="Mod",DateOfBirth = new DateTime(01,01,1999)},
                new User(){ UserName="member", Email="user@hmz.com", FirstName="User",LastName="User",DateOfBirth = new DateTime(01,01,1999)}
            };
            // Save User to FILE
            var path = Directory.GetCurrentDirectory() + "\\Resoucres\\";
            var usersToSeed = System.Text.Json.JsonSerializer.Serialize(users);
            File.WriteAllText(path + "UserSeedData.json", usersToSeed);
        }
    }
}
