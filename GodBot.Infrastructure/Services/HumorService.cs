using GodBot.Core.Interfaces;
using System.Text.Json;

namespace GodBot.Infrastructure.Services
{
    public class HumorService : IHumorService
    {
        private readonly HttpClient _client;

        public HumorService()
        {
            _client = new HttpClient();
        }

        #region GetRandomJokeAsync

        /// <inheritdoc/>
        public async Task<string> GetRandomJokeAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.humorapi.com/jokes/random?api-key={AppSettings.HumorApiKey}")
            };

            using var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(await response.Content.ReadAsStringAsync()) ?? new Dictionary<string, object>();

            return data.TryGetValue("joke", out object value) ? value.ToString() : string.Empty;
        }

        #endregion GetRandomJokeAsync

        #region GetRandomMemeAsync

        /// <inheritdoc/>
        public async Task<string> GetRandomMemeAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.humorapi.com/memes/random?api-key={AppSettings.HumorApiKey}")
            };

            using var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(await response.Content.ReadAsStringAsync()) ?? new Dictionary<string, object>();

            return data.TryGetValue("url", out object value) ? value.ToString() : string.Empty;
        }

        #endregion GetRandomMemeAsync
    }
}