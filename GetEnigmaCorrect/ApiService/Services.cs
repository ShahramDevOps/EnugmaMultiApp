using GetEnigmaCorrect.ViewModel;
using GetEnigmaCorrect.ViewModels;
using System.Text;
using System.Text.Json;

namespace GetEnigmaCorrect.ApiService
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
                await InitializeDataRequestAsync(passPhrase, matchCards.MatchCards);
        }

        public static async Task SendCorrectRequestAsync(string authentication, List<string> passPhrase, EnigmaViewModel enigma)
        {
            var response = await RequestAsync(authentication, passPhrase, enigma);
            var walletSuccessful = JsonSerializer.Deserialize<WalletSuccessfulViewModel>(response);
            if (walletSuccessful.Passphrase != null)
                PrintSuccessfulResult(walletSuccessful.Passphrase);
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

        public static void PrintSuccessfulResult(List<string> walletPassPhrase)
        {
            foreach (string passphrase in walletPassPhrase)
                Console.WriteLine(passphrase);
        }

        public static async Task<bool> InitializeDataRequestAsync(List<string> passPhrase, bool[] matchCards)
        {
            using var client = new HttpClient();
            var url = "https://localhost:7221/api/Home/InsertData";
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

        public static async Task<List<string>> GetCorrectDataRequestAsync()
        {
            using var client = new HttpClient();
            var url = "https://localhost:7221/api/Home/GetData";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<string>>(jsonString);
        }
    }
}