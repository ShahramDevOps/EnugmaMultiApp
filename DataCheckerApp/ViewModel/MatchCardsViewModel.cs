using System.Text.Json.Serialization;

namespace DataCheckerApp.ViewModel
{
    public class MatchCardsViewModel
    {
        [JsonPropertyName("matchCards")]
        public bool[] MatchCards { get; set; }
    }
}
