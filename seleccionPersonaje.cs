using personaje;

namespace seleccionPersonaje
{
public class Seleccion
{
    public Personaje seleccionarPersonaje(List<Personaje> personajes, int id)
    {
        foreach (var item in personajes)
        {
            if (item.Id == id)
            {
                return item;
            }
        }

        return null;
    }

    public void personajeSeleccionado(Personaje pj)
    {
            Console.WriteLine("El personaje seleccionado es: \n");
            Console.WriteLine("Nombre: " + pj.Name);
            Console.WriteLine("Tipo: " + pj.Tipo);
            Console.WriteLine("Apodo: " + pj.Apodo);
            Console.WriteLine("Fecha de Nacimiento: " + pj.FechaNacimiento.ToString("dd/MM/yyyy"));
            Console.WriteLine("Edad: " + pj.Edad);
            Console.WriteLine("Velocidad: " + pj.Velocidad);
            Console.WriteLine("Destreza: " + pj.Destreza);
            Console.WriteLine("Fuerza: " + pj.Fuerza);
            Console.WriteLine("Nivel: " + pj.Nivel);
            Console.WriteLine("Armadura: " + pj.Armadura);
            Console.WriteLine("Salud: " + pj.Salud);
            Console.WriteLine();
    }
}
}
