
using personaje;
using JSON;
using fabrica;
using combates;

namespace MenuInteractivo
{
    public static class Menu
    {
        private static int selectedIndex = 0;

        public static async Task ControlarMenu(FabricaDePersonajes fabrica, HistorialJson historial, Combate combate, string archivoHistorial, List<Personaje> pjFabricados, List<Personaje> listGanadores)
        {
            Console.Clear();
            string[] options = {
                "Mostrar personajes",
                "Realizar combate 1v1",
                "Realizar torneo",
                "Leer historial de ganadores",
                "Salir del juego"
            };

            while (true)
            {
                Console.Clear();
                DisplayMenu(options);
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex + 1) % options.Length;
                }
                else if (key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    switch (selectedIndex)
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

        private static void DisplayMenu(string[] options)
        {
            Console.WriteLine("\nSeleccione una opcion:");
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"> {options[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }
            }
        }
    }
}
