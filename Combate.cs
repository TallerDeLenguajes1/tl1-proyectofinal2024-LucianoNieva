
using personaje;
using seleccionPersonaje;
using CrearApi;
using ascii;
using JSON;

namespace combates
{
    public class Combate
    {
        public async Task realizarAtaqueYDefensa(Personaje atacante, Personaje defensor)
        {
            var random = new Random();
            var datosPj = atacante.Datos;
            var caracteristicas = atacante.Caracteristicas;
            var datosPj2 = defensor.Datos;
            var caracteristicas2 = defensor.Caracteristicas;

            int ataque = caracteristicas.Destreza * caracteristicas.Fuerza * caracteristicas.Nivel;
            int efectividad = random.Next(1, 101);
            int defensa = caracteristicas2.Armadura * caracteristicas2.Velocidad;
            const int Ajuste = 500;

            int danioProvocado = ((ataque * efectividad) - defensa) / Ajuste;
            Console.WriteLine($"\nEl atacante {datosPj.Name} realizó un daño de: {danioProvocado}");

            caracteristicas2.Salud -= danioProvocado;
            controlarSaludNoNegativa(caracteristicas2);

            Console.WriteLine($"La salud de {datosPj2.Name} es de: {caracteristicas2.Salud}");

            await Task.Delay(1000); // Pausa para mostrar la acción
        }

        private static void controlarSaludNoNegativa(Caracteristicas caracteristicas2)
        {
            if (caracteristicas2.Salud < 0)
            {
                caracteristicas2.Salud = 0;
            }
        }

        public async Task RealizarCombate1v1(List<Personaje> personajes, Combate combate, List<Personaje> listGanadores, HistorialJson historial, string archivoHistorial)
        {
            Console.WriteLine("¿Quieres jugar contra un bot o un amigo?");
            Console.WriteLine("1) Contra un bot");
            Console.WriteLine("2) Contra un amigo");
            int.TryParse(Console.ReadLine(), out int opcion);

            if (opcion == 1)
            {
                var pjGanador = await combate.pelea1v1(personajes);
                guardarGanador(listGanadores, historial, archivoHistorial, pjGanador);
            }
            else if (opcion == 2)
            {
                var pjGanador = await combate.pelea1v1Amigo(personajes);
                guardarGanador(listGanadores, historial, archivoHistorial, pjGanador);
            }
            else
            {
                Console.WriteLine("Opción no válida.");
            }
        }

        public void guardarGanador(List<Personaje> listGanadores, HistorialJson historial, string archivoHistorial, Personaje pjGanador)
        {
            listGanadores.Add(pjGanador);
            historial.GuardarGanador(listGanadores, archivoHistorial);
        }

        public async Task<Personaje> pelea1v1Amigo(List<Personaje> personajes)
        {
            var seleccion = new Seleccion();
            Personaje pjSeleccionado = SeleccionarPJ(personajes, seleccion);
            Personaje pjSeleccionado2 = SeleccionarPJ(personajes, seleccion, pjSeleccionado);

            await Task.Delay(1500);
            Console.Clear();
            Console.WriteLine($"{pjSeleccionado.Datos.Name} VS {pjSeleccionado2.Datos.Name}");
            await verificarBonificacionClima(pjSeleccionado, pjSeleccionado2);

            var asci = new Ascii();

            while (pjSeleccionado.Caracteristicas.Salud > 0 && pjSeleccionado2.Caracteristicas.Salud > 0)
            {

                Console.WriteLine($"\n{pjSeleccionado.Datos.Name}");
                Console.WriteLine("¿Qué quieres hacer?");
                Console.WriteLine("1) Atacar");
                Console.WriteLine("2) Defender");
                int.TryParse(Console.ReadLine(), out int accion);

                if (accion == 1)
                {
                    await realizarAtaqueYDefensa(pjSeleccionado, pjSeleccionado2);
                    if (pjSeleccionado2.Caracteristicas.Salud <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("El ganador fue " + pjSeleccionado.Datos.Name);
                        asci.mostrarPJ(pjSeleccionado);
                        pjSeleccionado.Caracteristicas.Salud = 100;
                        pjSeleccionado2.Caracteristicas.Salud = 100;
                        ManejarFatality(pjSeleccionado);
                        return pjSeleccionado;
                    }
                }
                else if (accion == 2)
                {
                    Console.WriteLine("Defendiendo...");
                    await realizarAtaqueYDefensa(pjSeleccionado2, pjSeleccionado);
                    await Task.Delay(1000);

                    if (pjSeleccionado.Caracteristicas.Salud <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("El ganador fue " + pjSeleccionado2.Datos.Name);
                        asci.mostrarPJ(pjSeleccionado2);
                        pjSeleccionado.Caracteristicas.Salud = 100;
                        pjSeleccionado2.Caracteristicas.Salud = 100;
                        return pjSeleccionado2;
                    }
                }

                Console.WriteLine($"\n{pjSeleccionado2.Datos.Name}");
                Console.WriteLine("¿Qué quieres hacer?");
                Console.WriteLine("1) Atacar");
                Console.WriteLine("2) Defender");
                int.TryParse(Console.ReadLine(), out int accion2);

                if (accion2 == 1)
                {
                    await realizarAtaqueYDefensa(pjSeleccionado2, pjSeleccionado);
                    if (pjSeleccionado.Caracteristicas.Salud <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("El ganador fue " + pjSeleccionado2.Datos.Name);
                        asci.mostrarPJ(pjSeleccionado2);
                        pjSeleccionado2.Caracteristicas.Salud = 100;
                        pjSeleccionado.Caracteristicas.Salud = 100;
                        ManejarFatality(pjSeleccionado2);
                        return pjSeleccionado2;
                    }
                }
                else if (accion2 == 2)
                {
                    Console.WriteLine("Defendiendo...");
                    await realizarAtaqueYDefensa(pjSeleccionado, pjSeleccionado2);
                    await Task.Delay(1000);

                    if (pjSeleccionado2.Caracteristicas.Salud <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("El ganador fue " + pjSeleccionado.Datos.Name);
                        asci.mostrarPJ(pjSeleccionado);
                        pjSeleccionado.Caracteristicas.Salud = 100;
                        pjSeleccionado2.Caracteristicas.Salud = 100;
                        return pjSeleccionado;
                    }
                }

            }
            return null;
        }


        public async Task<Personaje> pelea1v1(List<Personaje> personajes)
        {
            var seleccion = new Seleccion();
            Personaje pjSeleccionado = SeleccionarPJ(personajes, seleccion);
            Personaje pjSeleccionado2 = SeleccionarPJ(personajes, seleccion, pjSeleccionado);
            await Task.Delay(1000);
            Console.Clear();
            Console.WriteLine($"{pjSeleccionado.Datos.Name} VS {pjSeleccionado2.Datos.Name}");
            await verificarBonificacionClima(pjSeleccionado, pjSeleccionado2);

            return await realizarCombate(pjSeleccionado, pjSeleccionado2);
        }

        private async Task<Personaje> realizarCombate(Personaje p1, Personaje p2)
        {
            var asci = new Ascii();
            while (p1.Caracteristicas.Salud > 0 && p2.Caracteristicas.Salud > 0)
            {
                await realizarAtaqueYDefensa(p1, p2);

                if (p2.Caracteristicas.Salud <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("El ganador fue " + p1.Datos.Name);
                    p1.Caracteristicas.Salud = 100;
                    p2.Caracteristicas.Salud = 100;
                    ManejarFatality(p1);
                    return p1;
                }

                await realizarAtaqueYDefensa(p2, p1);

                if (p1.Caracteristicas.Salud <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("\nPerdiste la batalla.");
                    Console.WriteLine("El ganador fue " + p2.Datos.Name);
                    asci.mostrarPJ(p2);
                    Console.WriteLine("---------COMBATE FINALIZADO--------");
                    p1.Caracteristicas.Salud = 100;
                    p2.Caracteristicas.Salud = 100;
                    return p2;
                }
            }
            return null;
        }

        public async Task<Personaje> combateTorre(List<Personaje> pjPrincipal, List<Personaje> PjSecundario, Combate combate, List<Personaje> listGanadores, HistorialJson historial, string archivoHistorial)
        {
            var random = new Random();
            var seleccion = new Seleccion();
            Personaje pjSeleccionado = SeleccionarPJ(pjPrincipal, seleccion);
            int nivel = seleccionarNivel();
            int contador = 0;

            var pjOponentes = new List<Personaje>();

            for (int i = 0; i < nivel; i++)
            {
                Personaje pjSeleccionado2 = PjSecundario[random.Next(PjSecundario.Count)];

                while (pjSeleccionado2 == pjSeleccionado || pjOponentes.Contains(pjSeleccionado2))
                {
                    pjSeleccionado2 = PjSecundario[random.Next(PjSecundario.Count)];
                }

                pjOponentes.Add(pjSeleccionado2);
            }

            foreach (var oponente in pjOponentes)
            {
                

                Console.WriteLine($"\nNivel {contador + 1}: {pjSeleccionado.Datos.Name} vs {oponente.Datos.Name}");
                await verificarBonificacionClima(pjSeleccionado, oponente);

                var pjGanador = await realizarCombate(pjSeleccionado, oponente);

                if (pjGanador == pjSeleccionado)
                {
                    Console.WriteLine($"\nEl personaje {pjGanador.Datos.Name} ganó el nivel {contador + 1}");
                }
                else
                {
                    Console.WriteLine($"\nEl personaje {pjSeleccionado.Datos.Name} perdió en el nivel {contador + 1}");
                    return pjGanador;
                }

                contador++;
            }


            Console.WriteLine("\n¡Felicidades, ganaste el torneo!");
            Console.WriteLine($"\nEl personaje {pjSeleccionado.Datos.Name} ganó todos los niveles");
            guardarGanador(listGanadores, historial, archivoHistorial, pjSeleccionado);
            Console.WriteLine("-------------------------------------------------------------");
            return pjSeleccionado;
        }


    

    private async Task verificarBonificacionClima(Personaje pjSeleccionado, Personaje pjSeleccionado2)
    {
        string weather = await WeatherApi.GetWeatherAsync();
        Console.WriteLine($"El clima en esta batalla es: {weather}");
        WeatherApi.AdjustCharacterStats(pjSeleccionado, weather);
        WeatherApi.AdjustCharacterStats(pjSeleccionado2, weather);
    }

    private static int seleccionarNivel()
    {
        Console.WriteLine("\nSeleccione la longitud de la torre:");
        Console.WriteLine("1. Torre corta (3 niveles)");
        Console.WriteLine("2. Torre común (5 niveles)");
        Console.WriteLine("3. Torre larga (7 niveles)");
        Console.Write("Seleccione una opción: ");

        int.TryParse(Console.ReadLine(), out int nivel);

        return nivel switch
        {
            1 => 3,
            2 => 5,
            3 => 7,
            _ => 3, // Por defecto a la torre corta
        };
    }

    private static void ManejarFatality(Personaje pjSeleccionado)
    {
        var asci = new Ascii();
        asci.Fatality();
        Console.WriteLine("\nDesea realizar una fatality?");
        Console.WriteLine("1) Sí");
        Console.WriteLine("0) No");
        int.TryParse(Console.ReadLine(), out int fatality);
        if (fatality == 0)
        {
            asci.mostrarPJ(pjSeleccionado);
            Console.WriteLine("---------COMBATE FINALIZADO--------");
        }
        else if (fatality == 1)
        {
            var random2 = new Random();
            int i = 1;

            if (i == 0)
            {
                Console.WriteLine("Falló la combinación. No se realizó la fatality.");
                Console.WriteLine("No recibiste una bonificación.");
                asci.mostrarPJ(pjSeleccionado);
                Console.WriteLine("---------COMBATE FINALIZADO--------");
            }
            else
            {
                Console.WriteLine("La fatality se realizó con éxito.");
                Console.WriteLine($"Recibiste una bonificación + 5 de fuerza y + 5 de armadura.");
                pjSeleccionado.Caracteristicas.Fuerza += 5;
                pjSeleccionado.Caracteristicas.Armadura += 5;
                asci.mostrarPJ(pjSeleccionado);
                Console.WriteLine("---------COMBATE FINALIZADO--------");
            }
        }
    }

    private static Personaje SeleccionarPJ(List<Personaje> personajes, Seleccion seleccion, Personaje pjExistente = null)
    {
        if (pjExistente == null)
        {
            Console.WriteLine("\nSeleccione el id del personaje que desea usar:");
        }
        else
        {
            Console.WriteLine("\nSeleccione el id del personaje que será su oponente:");
        }

        int.TryParse(Console.ReadLine(), out int op);

        if (op >= 1 && op <= 10)
        {
            var pjSeleccionado = seleccion.seleccionarPersonaje(personajes, op);
            seleccion.personajeSeleccionado(pjSeleccionado);
            return pjSeleccionado;
        }
        else
        {
            Console.WriteLine("\nNo seleccionó un ID correcto.");
            return SeleccionarPJ(personajes, seleccion, pjExistente);
        }
    }


}
}
