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

namespace Tests.Integration
{
    public class Account
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public Account()
        {
            _server = new TestServer(new WebHostBuilder()
                        .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task successfully_change_user_password()
        {
            ChangeUserPassword user = new ChangeUserPassword
            {
                Login = "login1",
                Password = "qwer123",
                NewPassword = "qwer321"
            };
            string body = JsonConvert.SerializeObject(user);
            var response = await _client.PutAsync("account/password", new StringContent(body, Encoding.UTF8, "application/json"));

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NoContent);
        }
    }
}
