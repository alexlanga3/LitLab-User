using LitLabGames.User.API.Controllers;
using LitLabGames.User.API.Models;
using LitLabGames.User.ServiceLibrary.DTOs;
using LitLabGames.User.ServiceLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace LitLabGames.API.Tests
{
    public class UserControllerShould
    {
        readonly Mock<ILogger<UserController>> mockLogger;
        readonly Mock<IUserService> mockUserService;
        private readonly UserController userController;

        public UserControllerShould()
        {
            mockLogger = new Mock<ILogger<UserController>>();
            mockUserService = new Mock<IUserService>();
            userController = new UserController(mockLogger.Object, mockUserService.Object);
        }

        [Fact]
        public void Get_BadRequest_When_Name_Is_Empty()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));

            //Act
            var result = userController.Get("");

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Get_Ok_When_Name_Is_NotNull()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Debug, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            mockUserService.Setup(x => x.GetUserByName(It.IsAny<string>())).Returns(new User.ServiceLibrary.DTOs.UserDTO());

            //Act
            var result = userController.Get("test");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Post_BadRequest_When_Model_Is_Null()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));

            //Act
            var result = userController.Post(null);

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Post_BadRequest_When_ExtraValidationOnUser_Is_False()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            mockUserService.Setup(x => x.DoExtraValidationOnUser(It.IsAny<UserDTO>())).Returns(false);

            //Act
            var result = userController.Post(new UserViewModel());

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Post_BadRequest_When_SaveAsync_Is_False()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            mockUserService.Setup(x => x.DoExtraValidationOnUser(It.IsAny<UserDTO>())).Returns(false);
            mockUserService.Setup(x => x.SaveAsync(It.IsAny<UserDTO>())).ReturnsAsync(false);

            //Act
            var result = userController.Post(new UserViewModel());

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }


        [Fact]
        public void Post_Ok_When_SaveAsync_Is_True()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Debug, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            mockUserService.Setup(x => x.DoExtraValidationOnUser(It.IsAny<UserDTO>())).Returns(true);
            mockUserService.Setup(x => x.SaveAsync(It.IsAny<UserDTO>())).ReturnsAsync(true);

            var userViewModelTest = new UserViewModel()
            {
                Nick = "test",
                Direction = "test",
                Name = "test",
                LastName = "test",
                Email = "test",
                PhoneNumber = "687.868686"
            };

            //Act
            var result = userController.Post(new UserViewModel());

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Delete_BadRequest_When_Name_Is_Empty()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));

            //Act
            var result = userController.Delete("");

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Delete_BadRequest_When_DeleteAsync_Is_False()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            mockUserService.Setup(x => x.DeleteAsync(It.IsAny<string>())).ReturnsAsync(false);

            //Act
            var result = userController.Delete("");

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Delete_Ok_When_SaveAsync_Is_True()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Debug, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            mockUserService.Setup(x => x.DeleteAsync(It.IsAny<string>())).ReturnsAsync(true);

            //Act
            var result = userController.Delete("test");

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Update_BadRequest_When_Model_Is_Null()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));

            //Act
            var result = userController.Put(null);

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Update_BadRequest_When_ExtraValidationOnUser_Is_False()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            mockUserService.Setup(x => x.DoExtraValidationOnUser(It.IsAny<UserDTO>())).Returns(false);

            //Act
            var result = userController.Put(new UserViewModel());

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Update_BadRequest_When_SaveAsync_Is_False()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            mockUserService.Setup(x => x.DoExtraValidationOnUser(It.IsAny<UserDTO>())).Returns(false);
            mockUserService.Setup(x => x.UpdateAsync(It.IsAny<UserDTO>())).ReturnsAsync(false);

            //Act
            var result = userController.Put(new UserViewModel());

            //Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }


        [Fact]
        public void Update_Ok_When_SaveAsync_Is_True()
        {
            mockLogger.Setup(x => x.Log(LogLevel.Debug, It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()));
            mockUserService.Setup(x => x.DoExtraValidationOnUser(It.IsAny<UserDTO>())).Returns(true);
            mockUserService.Setup(x => x.UpdateAsync(It.IsAny<UserDTO>())).ReturnsAsync(true);

            var userViewModelTest = new UserViewModel()
            {
                Nick = "test",
                Direction = "test",
                Name = "test",
                LastName = "test",
                Email = "test",
                PhoneNumber = "687.868686"
            };

            //Act
            var result = userController.Put(new UserViewModel());

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
