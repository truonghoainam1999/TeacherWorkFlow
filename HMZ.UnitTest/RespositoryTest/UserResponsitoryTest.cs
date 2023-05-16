using HMZ.Database.Data;
using HMZ.Database.Entities;
using HMZ.Service.Services;
using HMZ.Service.Services.UserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace HMZ.UnitTest.RespositoryTest
{

    public class UserRepositoryTests
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _serviceProvider;

        public UserRepositoryTests()
        {
            // context using inmemory
            var options = new DbContextOptionsBuilder<HMZContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _userRepository = new Repository<User>(new HMZContext(options));
            _serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            _unitOfWork = new UnitOfWork(new HMZContext(options), _serviceProvider);
        }

        [Fact]
        public async System.Threading.Tasks.Task AddUser_ShouldAddUserToList()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Now,
                Email = "john@example.com"
            };

            // Act
            await _userRepository.Add(user);

            // Assert
            Assert.Equal(1, await _unitOfWork.SaveChangesAsync());

        }

        [Fact]
        public async System.Threading.Tasks.Task GetUser_ShouldReturnCorrectUser()
        {
            // Arrange
            var user1 = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Now,
                Email = "john@example.com"
            };

            var user2 = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                DateOfBirth = DateTime.Now,
                Email = "jane@example.com"
            };

            await _userRepository.Add(user1);
            await _userRepository.Add(user2);

            // Act
            var result = await _userRepository.GetByIdAsync(user2.Id);

            // Assert
            Assert.Equal(user2, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetUser_ShouldReturnNullWhenUserNotFound()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Now,
                Email = "john@example.com"
            };

            await _userRepository.Add(user);

            // Act
            var result = await _userRepository.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Now,
                Email = "john@example.com"
            };

            // Act
            await _userRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();

            // Assert
            Assert.Equal(1, await _unitOfWork.GetRepository<User>().AsQueryable().CountAsync());


        }
    }


}
