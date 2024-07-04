using JSON;
using personaje;
using seleccionPersonaje;
using CrearApi;

namespace combates
{
    public class Combate
    {
        public void realizarCombate(Personaje atacante, Personaje defensor)
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
            Console.WriteLine($"La salud de {datosPj2.Name} es de: {caracteristicas2.Salud}");
        }

        public async Task<Personaje> turno(List<Personaje> p1, List<Personaje> p2)
        {
            var seleccion = new Seleccion();
            Personaje pjSeleccionado = null;
            Personaje pjSeleccionado2 = null;

            Console.WriteLine("\nSeleccione el id del personaje que desea usar:");
            int.TryParse(Console.ReadLine(), out int op);

            if (op >= 1 && op <= 10)
            {
                pjSeleccionado = seleccion.seleccionarPersonaje(p1, op);
                seleccion.personajeSeleccionado(pjSeleccionado);

                Console.WriteLine("\nSeleccione el id del personaje que será su oponente:");
                int.TryParse(Console.ReadLine(), out int selec2);

                if (op == selec2)
                {
                    Console.WriteLine("\nSelección incorrecta, seleccione otro id");
                    int.TryParse(Console.ReadLine(), out selec2);
                }

                pjSeleccionado2 = seleccion.seleccionarPersonaje(p2, selec2);
            }
            else
            {
                Console.WriteLine("\nNo seleccionó un ID correcto.");
            }

            string weather = await WeatherApi.GetWeatherAsync();
            WeatherApi.AdjustCharacterStats(pjSeleccionado, weather);
            WeatherApi.AdjustCharacterStats(pjSeleccionado2, weather);

            while (pjSeleccionado.Caracteristicas.Salud > 0 && pjSeleccionado2.Caracteristicas.Salud > 0)
            {
                realizarCombate(pjSeleccionado, pjSeleccionado2);

                if (pjSeleccionado2.Caracteristicas.Salud <= 0)
                {
                    Console.WriteLine("El ganador fue " + pjSeleccionado.Datos.Name);
                    pjSeleccionado.Caracteristicas.Salud = 100;
                    pjSeleccionado2.Caracteristicas.Salud = 100;
                    ManejarFatality(pjSeleccionado);
                    return pjSeleccionado;
                }

                realizarCombate(pjSeleccionado2, pjSeleccionado);

                if (pjSeleccionado.Caracteristicas.Salud <= 0)
                {
                    Console.WriteLine("\nPerdiste la batalla.");
                    Console.WriteLine("El ganador fue " + pjSeleccionado2.Datos.Name);
                    Console.WriteLine("---------COMBATE FINALIZADO--------");
                    pjSeleccionado.Caracteristicas.Salud = 100;
                    pjSeleccionado2.Caracteristicas.Salud = 100;
                    return pjSeleccionado2;
                }
            }
            return null;
        }

        public Personaje turnoTorneo(Personaje p1, Personaje p2)
        {
            while (p1.Caracteristicas.Salud > 0 && p2.Caracteristicas.Salud > 0)
            {
                realizarCombate(p1, p2);

                if (p2.Caracteristicas.Salud <= 0)
                {
                    Console.WriteLine("El ganador fue " + p1.Datos.Name);
                    p1.Caracteristicas.Salud = 100;
                    p2.Caracteristicas.Salud = 100;
                    ManejarFatality(p1);
                    return p1;
                }

                realizarCombate(p2, p1);

                if (p1.Caracteristicas.Salud <= 0)
                {
                    Console.WriteLine("\nPerdiste la batalla.");
                    Console.WriteLine("El ganador fue " + p2.Datos.Name);
                    Console.WriteLine("---------COMBATE FINALIZADO--------");
                    p1.Caracteristicas.Salud = 100;
                    p2.Caracteristicas.Salud = 100;
                    return p2;
                }
            }
            return null;
        }


        public async Task<Personaje> combateTorre(List<Personaje> pjPrincipal, List<Personaje> PjSecundario, Combate combate)
        {
            var random = new Random();
            var seleccion = new Seleccion();
            Personaje pjSeleccionado = null;
            Personaje pjSeleccionado2 = null;

            Console.WriteLine("\nSeleccione el id del personaje que desea usar:");
            int.TryParse(Console.ReadLine(), out int op);

            if (op >= 1 && op <= 10)
            {
                pjSeleccionado = seleccion.seleccionarPersonaje(pjPrincipal, op);
                seleccion.personajeSeleccionado(pjSeleccionado);
            }
            else
            {
                Console.WriteLine("\nNo seleccionó un ID correcto.");
            }

            Console.WriteLine("\nSeleccione la longitud de la torre:");
            Console.WriteLine("1. Torre corta (3 niveles)");
            Console.WriteLine("2. Torre común (5 niveles)");
            Console.WriteLine("3. Torre larga (7 niveles)");
            Console.Write("Seleccione una opción: ");

            int.TryParse(Console.ReadLine(), out int nivel);

            switch (nivel)
            {
                case 1:
                    nivel = 3;
                    break;

                case 2:
                    nivel = 5;
                    break;

                case 3:
                    nivel = 7;
                    break;

                default:
                    nivel = 3; // Por defecto a la torre corta
                    break;
            }

            for (int i = 0; i < nivel; i++)
            {
                pjSeleccionado2 = PjSecundario[random.Next(PjSecundario.Count)];

                while (pjSeleccionado2 == pjSeleccionado)
                {
                    pjSeleccionado2 = PjSecundario[random.Next(PjSecundario.Count)];
                }

                Console.WriteLine($"\nNivel {i + 1}: {pjSeleccionado.Datos.Name} vs {pjSeleccionado2.Datos.Name}");
                string weather = await WeatherApi.GetWeatherAsync();
                WeatherApi.AdjustCharacterStats(pjSeleccionado, weather);
                WeatherApi.AdjustCharacterStats(pjSeleccionado2, weather);

                var pjGanador = combate.turnoTorneo(pjSeleccionado, pjSeleccionado2); // Agregado await

                if (pjGanador == pjSeleccionado)
                {
                    Console.WriteLine($"\nEl personaje {pjGanador.Datos.Name} ganó el nivel {i + 1}");
                }
                else
                {
                    Console.WriteLine($"\nEl personaje {pjSeleccionado.Datos.Name} perdió en el nivel {i + 1}");
                    return pjGanador;
                }
            }

            if (pjSeleccionado.Caracteristicas.Salud > 0)
            {
                Console.WriteLine("\n¡Felicidades, ganaste el torneo!");
                Console.WriteLine($"\nEl personaje {pjSeleccionado.Datos.Name} ganó todos los niveles");
                Console.WriteLine("-------------------------------------------------------------");
                return pjSeleccionado;
            }

            return null;
        }

        private static void ManejarFatality(Personaje pjSeleccionado)
        {
            Console.WriteLine("\nDesea realizar una fatality?");
            Console.WriteLine("1) Sí");
            Console.WriteLine("0) No");
            int.TryParse(Console.ReadLine(), out int fatality);
            if (fatality == 0)
            {
                Console.WriteLine("---------COMBATE FINALIZADO--------");
            }
            else if (fatality == 1)
            {
                var random2 = new Random();
                int i = random2.Next(0, 2);

                if (i == 0)
                {
                    Console.WriteLine("Falló la combinación. No se realizó la fatality.");
                    Console.WriteLine("No recibiste una bonificación.");
                    Console.WriteLine("---------COMBATE FINALIZADO--------");
                }
                else
                {
                    Console.WriteLine("La fatality se realizó con éxito.");
                    Console.WriteLine($"Recibiste una bonificación + 5 de fuerza y + 5 de armadura.");
                    pjSeleccionado.Caracteristicas.Fuerza += 5;
                    pjSeleccionado.Caracteristicas.Armadura += 5;
                    Console.WriteLine("---------COMBATE FINALIZADO--------");
                }
            }
        }

    }
}
