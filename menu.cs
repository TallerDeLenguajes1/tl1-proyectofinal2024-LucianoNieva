
using personaje;
using JSON;
using fabrica;
using combates;

namespace MenuInteractivo
{
    public static class Menu
    {
        private static int seleccionar = 0;

        public static async Task OpcionesMenu(FabricaDePersonajes fabrica, HistorialJson historial, Combate combate, string archivoHistorial, List<Personaje> pjFabricados, List<Personaje> listGanadores)
        {
            Console.Clear();
            string[] opciones = {
                "Mostrar personajes",
                "Realizar combate 1v1",
                "Realizar torneo",
                "Leer historial de ganadores",
                "Salir del juego"
            };

            while (true)
            {
                Console.Clear();
                MostrarMenu(opciones);
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    seleccionar = (seleccionar - 1 + opciones.Length) % opciones.Length;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    seleccionar = (seleccionar + 1) % opciones.Length;
                }
                else if (key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    switch (seleccionar)
                    {
                        case 0:
                            fabrica.MostrarPersonaje(pjFabricados);
                            break;
                        case 1:
                            await combate.RealizarCombate1v1(pjFabricados,combate,listGanadores,historial,archivoHistorial);
                            break;
                        case 2:
                            await combate.Torre(pjFabricados, pjFabricados, combate,listGanadores,historial,archivoHistorial);
                            break;
                        case 3:
                            var leerPJ = historial.LeerGanador(archivoHistorial);
                            Console.WriteLine("\n--Historial ganadores--\n");
                            fabrica.MostrarPersonaje(leerPJ);
                            break;
                        case 4:
                            Console.WriteLine("Saliendo del juego...");
                            return;
                    }
                    Console.WriteLine("Presione cualquier tecla para volver al menú principal...");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
        }

        private static void MostrarMenu(string[] opciones)
        {
            Console.WriteLine("\nSeleccione una opcion:");
            for (int i = 0; i < opciones.Length; i++)
            {
                if (i == seleccionar)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"> {opciones[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {opciones[i]}");
                }
            }
        }
    }
}
