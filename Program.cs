using System;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using personaje;
using fabrica;
using JSON;
using combates;
using seleccionPersonaje;

class Program
{
    static void Main(string[] args)
    {
        var fabrica = new FabricaDePersonajes();
        var pjJson = new PersonajesJSON();
        var historial = new HistorialJson();
        var combate = new Combate();
        var seleccion = new Seleccion();
        var archivoPersonajes = @"C:\Users\lucia\OneDrive\Escritorio\tallertp\tl1-proyectofinal2024-LucianoNieva\texto.json";
        var archivoHistorial = @"C:\Users\lucia\OneDrive\Escritorio\tallertp\tl1-proyectofinal2024-LucianoNieva\historial.json";

        var personajes = new List<Personaje>();

        if (pjJson.Existe(archivoPersonajes))
        {
            personajes = pjJson.LeerPersonajes(archivoPersonajes);
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                var nuevoPJ = fabrica.CrearPersonajesAleatorios();
                nuevoPJ.Id = i + 1;
                personajes.Add(nuevoPJ);
            }
            pjJson.GuardarPersonajes(personajes, archivoPersonajes);

        }

        personajes = pjJson.LeerPersonajes(archivoPersonajes);


        fabrica.MostrarPersonaje(personajes);

        Console.WriteLine("Seleccione el id del personaje que desea usar");
        string selec = Console.ReadLine();
        int.TryParse(selec, out int op);
        var personajeSeleccionado = seleccion.seleccionarPersonaje(personajes,op);
        var personajeSeleccionado2 = seleccion.seleccionarPersonaje(personajes, 2);

        var pjGanador = combate.turno(personajeSeleccionado,personajeSeleccionado2);
        historial.GuardarGanador(pjGanador,archivoHistorial);
        var leerPJ = historial.LeerGanador(archivoHistorial);
        fabrica.MostrarPersonaje(new List<Personaje> { leerPJ });
        


        seleccion.personajeSeleccionado(personajeSeleccionado);
        personajes.Remove(personajeSeleccionado);


        Console.WriteLine("Estos son los personajes restantes");
        fabrica.MostrarPersonaje(personajes);


        


    }
}
