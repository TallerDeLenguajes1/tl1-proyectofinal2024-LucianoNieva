using System.Text.Json.Serialization;


namespace DatosApis
{

    public class ClimaApi
    {
        public class AllDay
        {
            [JsonPropertyName("weather")]
            public string weather { get; set; }

            [JsonPropertyName("icon")]
            public int icon { get; set; }

            [JsonPropertyName("temperature")]
            public double temperature { get; set; }

            [JsonPropertyName("temperature_min")]
            public double temperature_min { get; set; }

            [JsonPropertyName("temperature_max")]
            public double temperature_max { get; set; }

            [JsonPropertyName("wind")]
            public Wind wind { get; set; }

            [JsonPropertyName("cloud_cover")]
            public CloudCover cloud_cover { get; set; }

            [JsonPropertyName("precipitation")]
            public Precipitation precipitation { get; set; }
        }

        public class CloudCover
        {
            [JsonPropertyName("total")]
            public int total { get; set; }
        }

        public class Current
        {
            [JsonPropertyName("icon")]
            public string icon { get; set; }

            [JsonPropertyName("icon_num")]
            public int icon_num { get; set; }

            [JsonPropertyName("summary")]
            public string summary { get; set; }

            [JsonPropertyName("temperature")]
            public double temperature { get; set; }

            [JsonPropertyName("wind")]
            public Wind wind { get; set; }

            [JsonPropertyName("precipitation")]
            public Precipitation precipitation { get; set; }

            [JsonPropertyName("cloud_cover")]
            public int cloud_cover { get; set; }
        }

        public class Daily
        {
            [JsonPropertyName("data")]
            public List<Datum> data { get; set; }
        }

        public class Datum
        {
            [JsonPropertyName("date")]
            public DateTime date { get; set; }

            [JsonPropertyName("weather")]
            public string weather { get; set; }

            [JsonPropertyName("icon")]
            public int icon { get; set; }

            [JsonPropertyName("summary")]
            public string summary { get; set; }

            [JsonPropertyName("temperature")]
            public double temperature { get; set; }

            [JsonPropertyName("wind")]
            public Wind wind { get; set; }

            [JsonPropertyName("cloud_cover")]
            public CloudCover cloud_cover { get; set; }

            [JsonPropertyName("precipitation")]
            public Precipitation precipitation { get; set; }

            [JsonPropertyName("day")]
            public string day { get; set; }

            [JsonPropertyName("all_day")]
            public AllDay all_day { get; set; }

            [JsonPropertyName("morning")]
            public object morning { get; set; }

            [JsonPropertyName("afternoon")]
            public object afternoon { get; set; }

            [JsonPropertyName("evening")]
            public object evening { get; set; }
        }

        public class Hourly
        {
            [JsonPropertyName("data")]
            public List<Datum> data { get; set; }
        }

        public class Precipitation
        {
            [JsonPropertyName("total")]
            public int total { get; set; }

            [JsonPropertyName("type")]
            public string type { get; set; }
        }

        public class Root
        {
            [JsonPropertyName("lat")]
            public string lat { get; set; }

            [JsonPropertyName("lon")]
            public string lon { get; set; }

            [JsonPropertyName("elevation")]
            public int elevation { get; set; }

            [JsonPropertyName("timezone")]
            public string timezone { get; set; }

            [JsonPropertyName("units")]
            public string units { get; set; }

            [JsonPropertyName("current")]
            public Current current { get; set; }

            [JsonPropertyName("hourly")]
            public Hourly hourly { get; set; }

            [JsonPropertyName("daily")]
            public Daily daily { get; set; }
        }

        public class Wind
        {
            [JsonPropertyName("speed")]
            public double speed { get; set; }

            [JsonPropertyName("angle")]
            public int angle { get; set; }

            [JsonPropertyName("dir")]
            public string dir { get; set; }
        }





    }





}