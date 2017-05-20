using ASP;
using FluentAssertions;
using Infrastructure.Commands;
using Infrastructure.Commands.Users;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Integration
{
    public class Users
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public Users()
        {
            _server = new TestServer(new WebHostBuilder()
                        .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task When_valid_login_user_should_exist()
            => await Get_user("login1");

        [Fact]
        public async Task When_invalid_login_user_should_not_exist()
        {
            var response = await _client.GetAsync($"users/login0");
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Successfully_register_and_remove_user()
        {
            CreateUser createUser = new CreateUser
            {
                Login = "test",
                Password = "secret",
                Email = "test@example.com"
            };

            var createResponse = await _client.SendAsync(BuildMessage(HttpMethod.Post, createUser, "users"));

            createResponse.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);
            await Get_user("test");

            RemoveUser removeUser = new RemoveUser
            {
                Login = "test",
                Password = "secret"
            };

            var removeResponse = await _client.SendAsync(BuildMessage(HttpMethod.Delete, removeUser, "users"));
            removeResponse.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NoContent);

            var checkResponse = await _client.GetAsync($"users/test");
            checkResponse.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        private async Task<UserDTO> Get_user(string login)
        {
            var response = await _client.GetAsync($"users/{login}");
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDTO>(responseString);
            user.Login.ShouldBeEquivalentTo(login);
            return user;
        }

        private HttpRequestMessage BuildMessage(HttpMethod method, ICommand command, string uri)
        {
            string body = JsonConvert.SerializeObject(command);

            return new HttpRequestMessage
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json"),
                Method = method,
                RequestUri = new Uri($"http://{_server.BaseAddress.Host}/{uri}")
            };
        }
    }
}
