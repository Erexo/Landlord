using ASP;
using FluentAssertions;
using Infrastructure.Commands.Users;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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
        public async Task successfully_get_jwt_token()
            => await logIn();

        [Fact]
        public async Task successfully_change_user_password()
        {
            var token = await logIn();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

            ChangeUserPassword user = new ChangeUserPassword
            {
                Password = "qwer123",
                NewPassword = "qwer321"
            };
            string body = JsonConvert.SerializeObject(user);
            var response = await _client.PutAsync("account/password", new StringContent(body, Encoding.UTF8, "application/json"));

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NoContent);
        }

        private async Task<JwtDTO> logIn()
        {
            LoginUser user = new LoginUser
            {
                Login = "login1",
                Password = "qwer123"
            };
            string body = JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("login", new StringContent(body, Encoding.UTF8, "application/json"));

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            var obj = JsonConvert.DeserializeObject<JwtDTO>(await response.Content.ReadAsStringAsync());
            return obj;
        }
    }
}
