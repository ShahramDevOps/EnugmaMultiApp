using System.Text.Json.Serialization;

namespace GetEnigmaCorrect.ViewModel
{
    public class MatchCardsViewModel
    {
        [JsonPropertyName("matchCards")]
        public bool[] MatchCards { get; set; }
    }
}
