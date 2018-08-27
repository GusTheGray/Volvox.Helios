using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Integration.Infrastructure
{
    /// <summary>
    /// This class is for facilitating integration testing ONLY
    /// Client credential grant to Discord will return a valid token for the owner of the bod who's creds are used
    /// https://discordapp.com/developers/docs/topics/oauth2#client-credentials-grant
    /// </summary>
    public class DiscordClientCredAuth
    {
        private readonly HttpClient _client;
        private readonly string clientId;
        private readonly string clientSecret;

        private const string discordTokenEndpoint = "https://discordapp.com/api/oauth2/token";

        public DiscordClientCredAuth()
        {

            //clientId = configuration["Discord:ClientID"];
            //clientSecret = configuration["Discord:ClientSecret"];

            clientId = "471893228237619200";
            clientSecret = "1ilEfMIEuuoTumay_fX_kVvIh6bQ17zt";

            _client = new HttpClient();
        }


        public async Task<string> GetToken()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(discordTokenEndpoint),
                Content = new StringContent("grant_type=client_credentials")
            };

            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" };
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));

            var response = await _client.SendAsync(request);
          
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();
            var token = JObject.Parse(data)["access_token"].ToString();

            return token;
        }


    }
}
