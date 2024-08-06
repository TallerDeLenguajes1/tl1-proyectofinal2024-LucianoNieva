using personaje;
using fabrica;
using JSON;
using combates;
using barra;
using ascii;
using Soundtrack;


public class Program
{
    public static async Task Main(string[] args)
    {
        var fabrica = new FabricaDePersonajes();
        var gestionPersonajes = new GestionPersonajes();
        var historial = new HistorialJson();
        var combate = new Combate();
        var archivoPersonajes = @"personajes.json";
        var archivoHistorial = @"historial.json";
        var listGanadores = new List<Personaje>();
        var mostrarAscii = new Ascii();
        string direccionMusica = @"D:\Facultad\Taller\TrabajosPracticos\tl1-proyectofinal2024-LucianoNieva\audio\videoplayback.wav";

        Console.WriteLine("\nCargando el juego...\n");
        LoadingBar.Show();
        Musica musica = new Musica(direccionMusica);
        musica.Play();
        mostrarAscii.MostrarLogo();
        
        Console.WriteLine("\nPresione una tecla para empezar..");
        Console.ReadKey();
        Console.Clear();
        musica.controlarMusica();
        var pjFabricados = await gestionPersonajes.CargarOcrearPersonajes(archivoPersonajes);
        await MenuInteractivo.Menu.OpcionesMenu(fabrica, historial, combate, archivoHistorial, pjFabricados, listGanadores);
    }
}



