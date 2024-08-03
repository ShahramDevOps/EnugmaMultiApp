using System.Text.Json.Serialization;

namespace GetEnigmaCorrect.ViewModel
{
    public class EnigmaViewModel
    {
        [JsonPropertyName("enigmaId")]
        public string EnigmaId { get; set; }

        [JsonPropertyName("passphrase")]
        public List<string> PassPhrase { get; set; }

        [JsonPropertyName("expireAt")]
        public int ExpireAt { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("tonAmount")]
        public double TonAmount { get; set; }

        [JsonPropertyName("completedAt")]
        public int CompletedAt { get; set; }

        [JsonPropertyName("countTry")]
        public string CountTry { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
