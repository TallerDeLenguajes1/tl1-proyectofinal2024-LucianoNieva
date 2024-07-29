using System.Threading.Tasks;
using personaje;
using fabrica;
using JSON;
using combates;
using barra;
using ascii;
using Soundtrack;

namespace Juego
{
    public class IniciarJuego
    {
        public static async Task Iniciar()
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

            var pjFabricados = await gestionPersonajes.CargarOcrearPersonajes(archivoPersonajes);

            Console.WriteLine("\nCargando el juego...\n");
            LoadingBar.Show();
            Musica musica = new Musica(direccionMusica);
            musica.Play();
            mostrarAscii.MostrarLogo();
            Console.WriteLine("\nPresione una tecla para empezar..");
            Console.ReadKey();
            await MenuInteractivo.Menu.NewMethod(fabrica, historial, combate, archivoHistorial, pjFabricados, listGanadores);
        }
    }
}
