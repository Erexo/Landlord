using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using FluentAssertions;
using Xunit;
using ASP;
using Newtonsoft.Json;
using Infrastructure.DTO;
using Infrastructure.Commands.Users;
using System.Text;
using System.Net;

namespace Tests
{
    public class IntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public IntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder()
                        .UseStartup<Startup>());
            _client = _server.CreateClient();
        }
        
        public async Task<UserDTO> get_user(string login)
        {
            var response = await _client.GetAsync($"users/{login}");
            response.EnsureSuccessStatusCode();
            
            string responseString = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDTO>(responseString);
            user.Login.ShouldBeEquivalentTo(login);
            return user;
        }

        [Fact]
        public async Task when_valid_login_user_should_exist()
            => await get_user("login1");

        [Fact]
        public async Task when_invalid_login_user_should_not_exist()
        {
            var response = await _client.GetAsync($"users/login0");
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task successfully_register_new_user()
        {
            CreateUser user = new CreateUser
            {
                Login = "test",
                Password = "secret",
                Email = "test@example.com"
            };
            string body = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("users", new StringContent(body, Encoding.UTF8, "application/json"));

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);
            await get_user("test");
        }
    }
}
