using System.Text.Json.Serialization;

namespace GetEnigmaCorrect.ViewModels
{
    public class WalletSuccessfulViewModel
    {
        [JsonPropertyName("addressWallet")]
        public string AddressWallet { get; set; }

        [JsonPropertyName("clicker")]
        public Clicker Clicker { get; set; }

        [JsonPropertyName("passphrase")]
        public List<string> Passphrase { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class Clicker
    {
        [JsonPropertyName("balance")]
        public int Balance { get; set; }

        [JsonPropertyName("totalBalance")]
        public int TotalBalance { get; set; }

        [JsonPropertyName("availableTaps")]
        public int AvailableTaps { get; set; }

        [JsonPropertyName("maxTaps")]
        public int MaxTaps { get; set; }

        [JsonPropertyName("earnPerTap")]
        public int EarnPerTap { get; set; }

        [JsonPropertyName("tapsRecoverPerSec")]
        public int TapsRecoverPerSec { get; set; }

        [JsonPropertyName("lastSyncUpdate")]
        public int LastSyncUpdate { get; set; }

        [JsonPropertyName("earnPassivePerSec")]
        public int EarnPassivePerSec { get; set; }

        [JsonPropertyName("earnPassivePerHour")]
        public int EarnPassivePerHour { get; set; }

        [JsonPropertyName("lastPassiveEarn")]
        public int LastPassiveEarn { get; set; }

        [JsonPropertyName("lastFriendEarn")]
        public int LastFriendEarn { get; set; }

        [JsonPropertyName("lastFriendEarnTon")]
        public int LastFriendEarnTon { get; set; }

        [JsonPropertyName("lastContentEarn")]
        public int LastContentEarn { get; set; }
    }
}
