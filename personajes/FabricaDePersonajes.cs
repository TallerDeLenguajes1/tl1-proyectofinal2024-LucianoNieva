using personaje;

namespace fabrica
{
    public class FabricaDePersonajes
    {
        private List<string> nombresUsados = new List<string>();

        
        public Personaje CrearPersonaje(int id)
        {

            Random random = new Random();
            
            string nombre;

            do
            {
                nombre = Enum.GetName(typeof(NombrePersonajes), random.Next(Enum.GetNames(typeof(NombrePersonajes)).Length));
                                
            } while (nombresUsados.Contains(nombre));

            nombresUsados.Add(nombre);
            string name = nombre;
            string tipo = Enum.GetName(typeof(Tipo), random.Next(1, Enum.GetNames(typeof(Tipo)).Length));
            int edad = random.Next(0, 300);
            var fechaNac = DateTime.Now.AddYears(-edad);
            int Salud = 100;
            int velocidad = random.Next(1, 11);
            int destreza = random.Next(1, 11);
            int fuerza = random.Next(1, 11);
            int nivel = random.Next(1, 11);
            int armadura = random.Next(1, 11);

            Personaje personaje = new Personaje(name,tipo,fechaNac,edad,id,velocidad,destreza,armadura,fuerza,nivel,Salud);
            return personaje;
        }


        public List<Personaje> crearPersonajes(int cantidad){

            var listaPjs = new List<Personaje>();

            for (int i = 0; i < cantidad; i++)
            {
                listaPjs.Add(CrearPersonaje(i+1));
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