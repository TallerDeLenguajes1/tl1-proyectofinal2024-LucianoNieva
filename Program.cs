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
        var archivoPersonajes = @"C:\Users\lucia\OneDrive\Escritorio\tallertp\tl1-proyectofinal2024-LucianoNieva\JSON\personajes.json";
        var archivoHistorial = @"C:\Users\lucia\OneDrive\Escritorio\tallertp\tl1-proyectofinal2024-LucianoNieva\JSON\historial.json";
        var pjFabricados = fabrica.crearPersonajes(10);
        var listGanadores = new List<Personaje>();
        var mostrarAscii = new Ascii();

        pjFabricados = GuardarYLeer.GuardarYLeer(pjFabricados, archivoPersonajes);

        Console.WriteLine("\nCargando el juego...\n");
        LoadingBar.Show();
        Console.WriteLine("\nBienvenido a Mortal Kombat\n");
        mostrarAscii.MostrarLogo();
        Console.WriteLine("\nPresione una tecla para empezar..");
        Console.ReadKey();

        await MenuIteractivo.Menu.NewMethod(fabrica, historial, combate, archivoHistorial, pjFabricados, listGanadores);
    }
}
