using DataCheckerApp.ViewModel;
using System.Text;
using System.Text.Json;

namespace DataCheckerApp.ApiService
{
    public static class Services
    {
        public static async Task<EnigmaViewModel> CheckStartAsync(string authorizationHeader)
        {
            var url = "https://api.rockyrabbit.io/api/v1/mine/enigma/sync";
            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", authorizationHeader);

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EnigmaViewModel>(responseContent);
        }

        public static async Task DelayedSendRequestAsync(string auth, List<string> passPhrase, EnigmaViewModel enigma, int delayMilliseconds)
        {
            await Task.Delay(delayMilliseconds);
            await SendRequestAsync(auth, passPhrase, enigma);
        }

        public static async Task SendRequestAsync(string authentication, List<string> passPhrase, EnigmaViewModel enigma)
        {
            var response = await RequestAsync(authentication, passPhrase, enigma);
            var matchCards = JsonSerializer.Deserialize<MatchCardsViewModel>(response);
            if (matchCards.MatchCards != null)
            {
                var isSuccess = await InitializeDataRequestAsync(passPhrase, matchCards.MatchCards);
                if (isSuccess)
                    Console.WriteLine("Send data Is Success");
            }
        }

        public static async Task<string> RequestAsync(string authentication, List<string> passPhrase, EnigmaViewModel enigma)
        {
            using var client = new HttpClient();
            var url = "https://api.rockyrabbit.io/api/v1/mine/enigma";
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                enigmaId = enigma.EnigmaId,
                passphrase = string.Join(",", passPhrase)
            });

            request.Headers.Add("Authorization", authentication);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<bool> InitializeDataRequestAsync(List<string> passPhrase, bool[] matchCards)
        {
            using var client = new HttpClient();
            var url = "http://5.199.166.124/api/Home/InsertData";
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                passPhrase,
                matchCards
            });

            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync());
        }
    }
}