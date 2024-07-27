// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Text.Json.Serialization;
// POKEMON
namespace InfoPokeAPI
{
    public class Move
    {
        [JsonPropertyName("move")]
        public Move2 move { get; set; }
    }

    public class Move2
    {
        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("url")]
        public string url { get; set; }
    }

    public class Poke
    {
        [JsonPropertyName("height")]
        public int height { get; set; }

        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("moves")]
        public List<Move> moves { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("order")]
        public int order { get; set; }

        [JsonPropertyName("stats")]
        public List<Stat> stats { get; set; }

        [JsonPropertyName("types")]
        public List<Type> types { get; set; }

        [JsonPropertyName("weight")]
        public int weight { get; set; }
    }

    public class Stat
    {
        [JsonPropertyName("base_stat")]
        public int base_stat { get; set; }

        [JsonPropertyName("effort")]
        public int effort { get; set; }

        [JsonPropertyName("stat")]
        public Stat2 stat { get; set; }
    }

    public class Stat2
    {
        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("url")]
        public string url { get; set; }
    }

    public class Type
    {
        [JsonPropertyName("slot")]
        public int slot { get; set; }

        [JsonPropertyName("type")]
        public Type2 type { get; set; }
    }

    public class Type2
    {
        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("url")]
        public string url { get; set; }
    }

// TIPOS
    public class Result
    {
        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("url")]
        public string url { get; set; }
    }

    public class TiposPoke
    {
        [JsonPropertyName("count")]
        public int count { get; set; }

        [JsonPropertyName("results")]
        public List<Result> results { get; set; }
    } 
}
    