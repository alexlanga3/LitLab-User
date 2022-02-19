using LitLabGames.User.Domain.Interfaces;
using LitLabGames.User.ServiceLibrary.DTOs;
using LitLabGames.User.ServiceLibrary.Implementations;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace LitLabGames.ServiceLibrary.Tests
{
    public class UserServiceShould
    {
        readonly Mock<ILogger<UserService>> mockLogger;
        readonly Mock<IUserDomainService> mockUserDomainService;
        private readonly UserService userService;

        public UserServiceShould()
        {
            mockLogger = new Mock<ILogger<UserService>>();
            mockUserDomainService = new Mock<IUserDomainService>();
            userService = new UserService(mockLogger.Object, mockUserDomainService.Object);

            mockLogger.Setup(x => x.Log(LogLevel.Debug, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
        }

        [Fact]
        public void GetUserByName_When_ReturnUser_Not_Null()
        {
            mockUserDomainService.Setup(x => x.GetUserByName(It.IsAny<string>())).Returns(new User.DataAccess.Entities.User());

            //Act
            var result = userService.GetUserByName("test");

            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public void SaveAsync_When_SaveChangesAsync_Fails_Return_False()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            mockUserDomainService.Setup(x => x.AddUser(It.IsAny<User.DataAccess.Entities.User>()));
            mockUserDomainService.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            //Act
            var result = userService.SaveAsync(new UserDTO());

            //Assert
            Assert.False(result.Result);

        }

        [Fact]
        public void SaveAsync_When_SaveChangesAsync_Ok_Return_True()
        {
            mockUserDomainService.Setup(x => x.AddUser(It.IsAny<User.DataAccess.Entities.User>()));
            mockUserDomainService.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            //Act
            var result = userService.SaveAsync(new UserDTO());

            //Assert
            Assert.True(result.Result);

        }

        [Fact]
        public void DeleteAsync_When_SaveChangesAsync_Fails_Return_False()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            mockUserDomainService.Setup(x => x.GetUserByName(It.IsAny<string>())).Returns(new User.DataAccess.Entities.User());
            mockUserDomainService.Setup(x => x.DeleteUserByName(It.IsAny<User.DataAccess.Entities.User>()));
            mockUserDomainService.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            //Act
            var result = userService.DeleteAsync("test");

            //Assert
            Assert.False(result.Result);

        }

        [Fact]
        public void DeleteAsync_When_SaveChangesAsync_Ok_Return_True()
        {
            mockUserDomainService.Setup(x => x.AddUser(It.IsAny<User.DataAccess.Entities.User>()));
            mockUserDomainService.Setup(x => x.GetUserByName(It.IsAny<string>())).Returns(new User.DataAccess.Entities.User());
            mockUserDomainService.Setup(x => x.DeleteUserByName(It.IsAny<User.DataAccess.Entities.User>()));
            mockUserDomainService.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            //Act
            var result = userService.DeleteAsync("test");

            //Assert
            Assert.True(result.Result);

        }

        [Fact]
        public void UpdateAsync_When_SaveChangesAsync_Fails_Return_False()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            mockUserDomainService.Setup(x => x.GetUserByName(It.IsAny<string>())).Returns(new User.DataAccess.Entities.User());
            mockUserDomainService.Setup(x => x.UpdateUser(It.IsAny<User.DataAccess.Entities.User>()));
            mockUserDomainService.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            //Act
            var result = userService.UpdateAsync( new UserDTO());

            //Assert
            Assert.False(result.Result);

        }

        [Fact]
        public void UpdateAsync_When_SaveChangesAsync_Ok_Return_True()
        {
            mockUserDomainService.Setup(x => x.AddUser(It.IsAny<User.DataAccess.Entities.User>()));
            mockUserDomainService.Setup(x => x.GetUserByName(It.IsAny<string>())).Returns(new User.DataAccess.Entities.User());
            mockUserDomainService.Setup(x => x.UpdateUser(It.IsAny<User.DataAccess.Entities.User>()));
            mockUserDomainService.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            //Act
            var result = userService.UpdateAsync(new UserDTO());

            //Assert
            Assert.True(result.Result);

        }
    }
}
