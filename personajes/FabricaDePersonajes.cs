
using InfoPokemonAPI;
using personaje;

namespace fabrica
{
    public class FabricaDePersonajes
    {
        private List<string> nombresUsados = new List<string>();
        private ServicioWeb servicioWeb = new ServicioWeb();

        public async Task<Personaje> CrearPersonaje(int id)
        {
            Random random = new Random();
            string nombre;

            do
            {
                nombre = Enum.GetName(typeof(NombrePersonajes), random.Next(Enum.GetNames(typeof(NombrePersonajes)).Length));
            } while (nombresUsados.Contains(nombre));

            nombresUsados.Add(nombre);
            string tipo = Enum.GetName(typeof(Tipo), random.Next(1, Enum.GetNames(typeof(Tipo)).Length));
            int edad = random.Next(0, 300);
            var fechaNac = DateTime.Now.AddYears(-edad);
            int Salud = 100;
            int velocidad;
            int destreza;
            int fuerza;
            int nivel;
            int armadura;;

            // Obtener datos de Pokémon
            int id_poke = random.Next(1, 151); // Asumiendo los primeros 150 Pokémon
            Poke poke = await servicioWeb.TraerInformacionPokemon<Poke>($"https://pokeapi.co/api/v2/pokemon/{id_poke}");

            velocidad = poke.stats[5].base_stat/10;
            armadura = poke.stats[3].base_stat/10;
            destreza = poke.stats[2].base_stat/10;
            fuerza = poke.stats[1].base_stat/10;
            nivel = poke.stats[0].base_stat/10;


            Personaje personaje = new Personaje(nombre, tipo, fechaNac, edad, id, velocidad, destreza, armadura, fuerza, nivel, Salud);
            return personaje;
        }

        public async Task<List<Personaje>> CrearPersonajes(int cantidad)
        {
            var listaPjs = new List<Personaje>();

            for (int i = 0; i < cantidad; i++)
            {
                listaPjs.Add(await CrearPersonaje(i + 1));
            }

            return listaPjs;
        }

        public void MostrarPersonaje(List<Personaje> personajes)
        {
            foreach (var personaje in personajes)
            {
                var datosPj = personaje.Datos;
                var caracteristicasPJ = personaje.Caracteristicas;

                Console.WriteLine("ID: " + datosPj.Id);
                Console.WriteLine("Nombre: " + datosPj.Name);
                Console.WriteLine("Tipo: " + datosPj.Tipo);
                Console.WriteLine("Fecha de Nacimiento: " + datosPj.FechaNacimiento.ToString("dd/MM/yyyy"));
                Console.WriteLine("Edad: " + datosPj.Edad);
                Console.WriteLine("Velocidad: " + caracteristicasPJ.Velocidad);
                Console.WriteLine("Destreza: " + caracteristicasPJ.Destreza);
                Console.WriteLine("Fuerza: " + caracteristicasPJ.Fuerza);
                Console.WriteLine("Nivel: " + caracteristicasPJ.Nivel);
                Console.WriteLine("Armadura: " + caracteristicasPJ.Armadura);
                Console.WriteLine("Salud: " + caracteristicasPJ.Salud);
                Console.WriteLine();
            }
        }

    }
}
