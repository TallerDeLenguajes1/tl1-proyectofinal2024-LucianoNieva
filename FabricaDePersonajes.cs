using personaje;

namespace fabrica
{
    public class FabricaDePersonajes
    {
        private List<string> nombresUsados = new List<string>();
        public Personaje CrearPersonajesAleatorios()
        {

            Random random = new Random();
            var personaje = new Personaje();


            string nombre;
            do
            {
                nombre = Enum.GetName(typeof(NombrePersonajes), random.Next(Enum.GetNames(typeof(NombrePersonajes)).Length));
                
            } while (nombresUsados.Contains(nombre));

            nombresUsados.Add(nombre);
            personaje.Name = nombre;
            personaje.Tipo = Enum.GetName(typeof(Tipo), random.Next(1, Enum.GetNames(typeof(Tipo)).Length));
            personaje.Edad = random.Next(0, 300);
            personaje.FechaNacimiento = DateTime.Now.AddYears(-personaje.Edad);
            personaje.Salud = 100;
            personaje.Velocidad = random.Next(1, 11);
            personaje.Destreza = random.Next(1, 6);
            personaje.Fuerza = random.Next(1, 11);
            personaje.Nivel = random.Next(1, 11);
            personaje.Armadura = random.Next(1, 11);

            return personaje;
        }

        public void MostrarPersonaje(List<Personaje> personajes)
        {

            foreach (var datosPJ in personajes)
            {
                Console.WriteLine("ID: " + datosPJ.Id);
                Console.WriteLine("Nombre: " + datosPJ.Name);
                Console.WriteLine("Tipo: " + datosPJ.Tipo);
                Console.WriteLine("Apodo: " + datosPJ.Apodo);
                Console.WriteLine("Fecha de Nacimiento: " + datosPJ.FechaNacimiento.ToString("dd/MM/yyyy"));
                Console.WriteLine("Edad: " + datosPJ.Edad);
                Console.WriteLine("Velocidad: " + datosPJ.Velocidad);
                Console.WriteLine("Destreza: " + datosPJ.Destreza);
                Console.WriteLine("Fuerza: " + datosPJ.Fuerza);
                Console.WriteLine("Nivel: " + datosPJ.Nivel);
                Console.WriteLine("Armadura: " + datosPJ.Armadura);
                Console.WriteLine("Salud: " + datosPJ.Salud);
                Console.WriteLine();
            }
        }
    }
}