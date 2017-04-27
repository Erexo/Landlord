using System;
using Xunit;
using FluentAssertions;
using Moq;
using Infrastructure.Services;
using Infrastructure.Repositories;
using Core.Domain;
using System.Threading.Tasks;

namespace Tests.Unit
{
    public class Users
    {
        [Fact]
        public async Task register_async_should_invoke_add_async_on_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            var userService = new UserService(userRepositoryMock.Object);
            await userService.RegisterAsync("login5", "qwer123", "user@email.com");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
