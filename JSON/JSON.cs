using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using personaje;
using fabrica;

namespace JSON
{
    public class PersonajesJSON
    {
        public void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo)
        {
            string jsonString = JsonSerializer.Serialize(personajes);

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
                string json = File.ReadAllText(archivo);
                return JsonSerializer.Deserialize<List<Personaje>>(json);
            }
            else
            {
                Console.WriteLine("No se encontró el archivo.");
                return new List<Personaje>();
            }
        }

        public bool Existe(string archivo)
        {
            return File.Exists(archivo);
        }
    }

    public class HistorialJson
    {
        public void GuardarGanador(List<Personaje> gana, string nombreArchivo)
        {
            string jsonString = JsonSerializer.Serialize(gana);

            using (var archivo = new FileStream(nombreArchivo, FileMode.Create))
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
                string json = File.ReadAllText(archivo);
                return JsonSerializer.Deserialize<List<Personaje>>(json);
            }
            else
            {
                Console.WriteLine("No se encontró el archivo.");
                return new List<Personaje>();
            }
        }

        public bool Existe(string archivo)
        {
            return File.Exists(archivo);
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
                return personajesJSON.LeerPersonajes(archivo);
            }
        }
    }

    public class GestionPersonajes
    {
        private ArchivoPersonajes archivoPersonajes = new ArchivoPersonajes();
        private PersonajesJSON personajesJSON = new PersonajesJSON();
        private FabricaDePersonajes fabrica = new FabricaDePersonajes();

        public async Task<List<Personaje>> CargarOcrearPersonajes(string archivo)
        {
            Console.WriteLine("¿Desea usar los personajes ya cargados o crear nuevos?");
            Console.WriteLine("1) Usar personajes cargados");
            Console.WriteLine("2) Crear nuevos personajes");
            int.TryParse(Console.ReadLine(), out int opcion);

            if (opcion == 1)
            {
                if (personajesJSON.Existe(archivo))
                {
                    return personajesJSON.LeerPersonajes(archivo);
                }
                else
                {
                    Console.WriteLine("No hay personajes cargados. Seleccione la opción de cargar personajes nuevos.");
                    return await CargarOcrearPersonajes(archivo); // Recurre a la misma función para volver a preguntar
                }
            }
            else if (opcion == 2)
            {
                var listaPjs = await fabrica.CrearPersonajes(10);
                personajesJSON.GuardarPersonajes(listaPjs, archivo);
                return listaPjs;
            }
            else
            {
                Console.WriteLine("Opción no válida.");
                return await CargarOcrearPersonajes(archivo); // Recurre a la misma función para volver a preguntar
            }
        }
    }
}
