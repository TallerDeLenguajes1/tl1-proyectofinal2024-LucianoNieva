using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using personaje;


namespace CrearApi
{
    public class WeatherApi
    {
        private static readonly string apiKey = "6msprufjk45ihq3e3nvf1mfxj98kg88kplvzbd7v";
        private static readonly List<string> placeIds = new List<string>
        {
            "london", "tokyo", "paris", "berlin", "sydney", "mumbai"
        };

        private static string GetRandomPlaceId()
        {
            var random = new Random();
            int index = random.Next(placeIds.Count);
            return placeIds[index];
        }

        public static async Task<string> GetWeatherAsync()
        {
            try
            {
                using HttpClient client = new HttpClient();
                string placeId = GetRandomPlaceId(); // Obtener un placeId aleatorio
                string url = $"https://www.meteosource.com/api/v1/free/point?place_id={placeId}&sections=all&timezone=UTC&language=en&units=metric&key={apiKey}";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserializar el JSON usando Newtonsoft.Json
                var data = JObject.Parse(responseBody);
                string weather = data["current"]["summary"].ToString(); // Obtener el resumen del clima actual

                Console.WriteLine($"El clima en esta batalla es: {weather}");
                return weather;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el clima: {ex.Message}");
                return "Error";
            }
        }

        public static void AdjustCharacterStats(Personaje character, string weather)
        {
            bool bonus = false;

            switch (character.Datos.Name)
            {
                case "SubZero":
                case "Frost":
                    if (weather.Contains("Clear") || weather.Contains("Snow") || weather.Contains("Partly clear"))
                    {
                        character.Caracteristicas.Fuerza += 10;
                        character.Caracteristicas.Armadura += 5;
                        bonus = true;
                    }
                    break;
                case "Rain":
                    if (weather.Contains("Rain") || weather.Contains("Rain shower"))
                    {
                        character.Caracteristicas.Fuerza += 10;
                        character.Caracteristicas.Armadura += 5;
                        bonus = true;
                    }
                    break;
                case "Fujin":
                    if (weather.Contains("Wind") || weather.Contains("Windy"))
                    {
                        character.Caracteristicas.Fuerza += 10;
                        character.Caracteristicas.Armadura += 5;
                        bonus = true;
                    }
                    break;
                case "Raiden":
                    if (weather.Contains("Thunderstorm"))
                    {
                        character.Caracteristicas.Fuerza += 10;
                        character.Caracteristicas.Armadura += 5;
                        bonus = true;
                    }
                    break;
                case "Blaze":
                case "Scorpion":
                    if (weather.Contains("Sunny") || weather.Contains("Mostly sunny") || weather.Contains("Partly sunny"))
                    {
                        character.Caracteristicas.Fuerza += 10;
                        character.Caracteristicas.Armadura += 5;
                        bonus = true;
                    }
                    break;
                case "Tremor":
                    if (weather.Contains("Earthquake"))
                    {
                        character.Caracteristicas.Fuerza += 10;
                        character.Caracteristicas.Armadura += 5;
                        bonus = true;
                    }
                    break;
                case "Ermac":
                    if (weather.Contains("Energetic"))
                    {
                        character.Caracteristicas.Fuerza += 10;
                        character.Caracteristicas.Armadura += 5;
                        bonus = true;
                    }
                    break;
                case "Kabal":
                    if (weather.Contains("Windy") || weather.Contains("Wind"))
                    {
                        character.Caracteristicas.Fuerza += 10;
                        character.Caracteristicas.Armadura += 5;
                        bonus = true;
                    }
                    break;
            }

            if (bonus)
            {
                Console.WriteLine($"{character.Datos.Name} tiene +10 de fuerza y +5 de armadura aumentadas debido al clima.");
            }
            else
            {
                Console.WriteLine($"{character.Datos.Name} no obtiene bonificación del clima.");
            }
        }
    }

}
