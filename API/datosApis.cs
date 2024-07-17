using System.Text.Json.Serialization;


namespace DatosApis
{

    public class ClimaApi
    {
        public class AllDay
        {
            [JsonPropertyName("weather")]
            public string weather { get; set; }
        }





    }





}