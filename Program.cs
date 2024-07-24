using personaje;
using fabrica;
using JSON;
using combates;
using barra;
using ascii;

class Program
{
    static async Task Main(string[] args)
    {
        var fabrica = new FabricaDePersonajes();
        var GuardarYLeer = new ArchivoPersonajes();
        var historial = new HistorialJson();
        var combate = new Combate();
        var archivoPersonajes =  @"personajes.json";
        var archivoHistorial = @"historial.json";
        var pjFabricados = fabrica.crearPersonajes(10);
        var listGanadores = new List<Personaje>();
        var mostrarAscii = new Ascii();
        
        pjFabricados = GuardarYLeer.GuardarYLeer(pjFabricados, archivoPersonajes);

        Console.WriteLine("\nCargando el juego...\n");
        LoadingBar.Show();
        mostrarAscii.MostrarLogo();
        Console.WriteLine("\nPresione una tecla para empezar..");
        Console.ReadKey();

        await MenuInteractivo.Menu.NewMethod(fabrica, historial, combate, archivoHistorial, pjFabricados, listGanadores);
    }
}
