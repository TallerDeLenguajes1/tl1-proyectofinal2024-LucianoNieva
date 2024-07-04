using personaje;
using fabrica;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace JSON
{
    public class PersonajesJSON
    {
        /* public void GuardarPersonajes(List<Personaje> personajes, string archivo)
         {

             string jsonString = System.Text.Json.JsonSerializer.Serialize(personajes);
             File.WriteAllText(archivo, jsonString);

         }*/

        public void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo)
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(personajes);

            using (var archivo = new FileStream(nombreArchivo, FileMode.Create))
            {
                using (var strWriter = new StreamWriter(archivo))
                {
                    strWriter.WriteLine(jsonString);
                }
            }
        }

        public List<Personaje> LeerPersonajes(string archivo)
        {
            if (Existe(archivo))
            {
                string Json = File.ReadAllText(archivo);
                return System.Text.Json.JsonSerializer.Deserialize<List<Personaje>>(Json);
            }
            else
            {
                Console.WriteLine("No se encontro el archivo");
                return new List<Personaje>();
            }

        }

        public bool Existe(string archivo)
        {
            if (!File.Exists(archivo))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

    public class HistorialJson
    {
        public void GuardarGanador(List<Personaje> gana, string nombreArch)
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(gana);

            using (var archivo = new FileStream(nombreArch, FileMode.Create))
            {
                using (var strWriter = new StreamWriter(archivo))
                {
                    strWriter.WriteLine(jsonString);
                }
            }
        }


        public List<Personaje> LeerGanador(string archivo)
        {
            if (Existe(archivo))
            {
                string Json = File.ReadAllText(archivo);
                return System.Text.Json.JsonSerializer.Deserialize<List<Personaje>>(Json);
            }
            else
            {
                Console.WriteLine("No se encontro el archivo");
                return new List<Personaje>();
            }
        }

        public bool Existe(string archivo)
        {
            if (!File.Exists(archivo))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class ArchivoPersonajes
    {
        private PersonajesJSON personajesJSON = new PersonajesJSON();

        public List<Personaje> GuardarYLeer(List<Personaje> listaPjs, string archivo)
        {
            if (personajesJSON.Existe(archivo))
            {
                return personajesJSON.LeerPersonajes(archivo);
            }
            else
            {
                personajesJSON.GuardarPersonajes(listaPjs, archivo);
            }

            return personajesJSON.LeerPersonajes(archivo);
        }
    }
}