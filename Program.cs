using System;
using System.Collections.Generic;
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
        var archivoPersonajes = @"D:\Facultad\Taller\TrabajosPracticos\tl1-proyectofinal2024-LucianoNieva\JSON\personajes.json";
        var archivoHistorial = @"D:\Facultad\Taller\TrabajosPracticos\tl1-proyectofinal2024-LucianoNieva\JSON\historial.json";
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

        Console.WriteLine("\nSeleccione una opcion ");
        Console.WriteLine("\n1) Mostrar personajes ");
        Console.WriteLine("\n2) Realizar combate 1v1 ");
        Console.WriteLine("\n3) Realizar torneo ");
        Console.WriteLine("\n4) Leer historial de ganadores");

        int selecMenu = -1;

        while (selecMenu != 0)
        {
            if (int.TryParse(Console.ReadLine(), out selecMenu))
            {
                switch (selecMenu)
                {
                    case 1:
                        fabrica.MostrarPersonaje(pjFabricados);
                        break;

                    case 2:
                        var pjGanador = await combate.turno(pjFabricados, pjFabricados); // Agregado await
                        listGanadores.Add(pjGanador);
                        historial.GuardarGanador(listGanadores, archivoHistorial);
                        break;

                    case 3:
                        var pjGanador2 = await combate.combateTorre(pjFabricados, pjFabricados, combate); // Agregado await
                        listGanadores.Add(pjGanador2);
                        historial.GuardarGanador(listGanadores, archivoHistorial);
                        break;

                    case 4:
                        var leerPJ = historial.LeerGanador(archivoHistorial);
                        Console.WriteLine("\n--Historial ganadores--\n");
                        fabrica.MostrarPersonaje(leerPJ);
                        break;

                    case 0:
                        Console.WriteLine("Saliendo del juego...");
                        break;

                    default:
                        Console.WriteLine("Selecciona una opción correcta.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Selecciona una opción correcta.");
                selecMenu = -1;
            }

            Console.WriteLine("\nSeleccione otra opción ");
            Console.WriteLine("\n1) Mostrar personajes ");
            Console.WriteLine("\n2) Realizar combate 1v1 ");
            Console.WriteLine("\n3) Realizar torneo ");
            Console.WriteLine("\n4) Leer historial de ganadores");
            Console.WriteLine("\n0) Salir del juego");

            int.TryParse(Console.ReadLine(), out selecMenu);
        }
    }
}
