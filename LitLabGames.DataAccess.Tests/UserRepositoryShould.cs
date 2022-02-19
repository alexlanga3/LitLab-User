using LitLabGames.User.DataAccess.Context;
using LitLabGames.User.DataAccess.Interfaces;
using LitLabGames.User.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using Xunit;

namespace LitLabGames.DataAccess.Tests
{
    public class UserRepositoryShould
    {
        readonly Mock<IContextFactory> mockContextFactory;
        private readonly UserRepository _userRepository;
        private readonly LitLabContext litLabContext;

        public UserRepositoryShould()
        {
            mockContextFactory = new Mock<IContextFactory>();
            litLabContext = CreateContext();
            mockContextFactory.Setup(x => x.GetContext()).Returns(litLabContext);
            _userRepository = new UserRepository(mockContextFactory.Object);
        }

        private LitLabContext CreateContext()
        {
            var _contextOptions = new DbContextOptionsBuilder<LitLabContext>()
                .UseInMemoryDatabase("Filename=:memory:")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            return new LitLabContext(_contextOptions);
        }

        [Fact]
        public void GetUserByName_Returns_Expected_UserEntity_When_Name_Is_Provided()
        {
            litLabContext.Users.Add(new User.DataAccess.Entities.User()
            {
                Id = new Guid(),
                Nick = "test",
                Direction = "test",
                Name = "test",
                LastName = "test",
                Email = "test",
                PhoneNumber = "687.868686"
            });

            litLabContext.SaveChanges();

            //Act
            var result = _userRepository.GetUserByName("test");

            //Assert
            Assert.IsType<User.DataAccess.Entities.User>(result);
            Assert.Equal("test", result.Name);
        }
    }
}
