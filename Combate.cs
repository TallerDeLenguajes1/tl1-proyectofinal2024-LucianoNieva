using JSON;
using personaje;
using seleccionPersonaje;
using CrearApi;
using ascii;

namespace combates
{
    public class Combate
    {
        public void realizarAtaqueYDefensa(Personaje atacante, Personaje defensor)
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

        public async Task<Personaje> pelea1v1(List<Personaje> p1, List<Personaje> p2)
        {
            var seleccion = new Seleccion();
            Personaje pjSeleccionado = null;

            pjSeleccionado = SeleccionarPJ(p1, seleccion, pjSeleccionado);
            Personaje pjSeleccionado2 = SeleccionarPJ(p1, seleccion, pjSeleccionado);

            await verificarBonificacionClima(pjSeleccionado, pjSeleccionado2);
            return seleccionarModoCombate(pjSeleccionado, pjSeleccionado2);

        }

        private Personaje seleccionarModoCombate(Personaje pjSeleccionado, Personaje pjSeleccionado2)
        {
            Console.WriteLine("\nDesea realizar combate contra otro jugador o contra bot 1 bot 2 amigo:");
            int.TryParse(Console.ReadLine(), out int n);

            if (n == 1)
            {
                return realizarCombate(pjSeleccionado, pjSeleccionado2);
            }
            else
            {
                return realizarCombate2v2(pjSeleccionado, pjSeleccionado2);
            }
        }

        private static Personaje SeleccionarPJ(List<Personaje> p1, Seleccion seleccion, Personaje pjSeleccionado)
        {

            if (pjSeleccionado == null)
            {
                Console.WriteLine("\nSeleccione el id del personaje que desea usar:");
                int.TryParse(Console.ReadLine(), out int op);


                if (op >= 1 && op <= 10)
                {
                    pjSeleccionado = seleccion.seleccionarPersonaje(p1, op);
                    seleccion.personajeSeleccionado(pjSeleccionado);
                    return pjSeleccionado;
                }
                else
                {
                    Console.WriteLine("\nNo seleccionó un ID correcto.");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("\nSeleccione el id del personaje que será su oponente:");
                int.TryParse(Console.ReadLine(), out int selec2);

                while (pjSeleccionado.Datos.Id == selec2)
                {
                    Console.WriteLine("\nSelección incorrecta, seleccione otro id");
                    int.TryParse(Console.ReadLine(), out selec2);
                }

                return seleccion.seleccionarPersonaje(p1, selec2);

            }

        }

        public Personaje realizarCombate(Personaje p1, Personaje p2)
        {
            var asci = new Ascii();
            while (p1.Caracteristicas.Salud > 0 && p2.Caracteristicas.Salud > 0)
            {
                realizarAtaqueYDefensa(p1, p2);

                if (p2.Caracteristicas.Salud <= 0)
                {
                    Console.WriteLine("El ganador fue " + p1.Datos.Name);
                    p1.Caracteristicas.Salud = 100;
                    p2.Caracteristicas.Salud = 100;
                    ManejarFatality(p1);
                    return p1;
                }

                realizarAtaqueYDefensa(p2, p1);

                if (p1.Caracteristicas.Salud <= 0)
                {
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
        public Personaje realizarCombate2v2(Personaje p1, Personaje p2)
        {
            while (p1.Caracteristicas.Salud > 0 && p2.Caracteristicas.Salud > 0)
            {
                Console.WriteLine("\n Personaje 1 desea atacar(1) o defender (2):");
                int.TryParse(Console.ReadLine(), out int op);

                if (op == 1)
                {
                    realizarAtaqueYDefensa(p1, p2);

                    if (p2.Caracteristicas.Salud <= 0)
                    {
                        Console.WriteLine("El ganador fue " + p1.Datos.Name);
                        p1.Caracteristicas.Salud = 100;
                        p2.Caracteristicas.Salud = 100;
                        ManejarFatality(p1);
                        return p1;
                    }
                }
                else
                {
                    if (op == 2)
                    {
                        realizarAtaqueYDefensa(p2, p1);

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
                }

                Console.WriteLine("\n Personaje 2 desea atacar(1) o defender (2):");
                int.TryParse(Console.ReadLine(), out int op2);

                if (op2 == 1)
                {
                    realizarAtaqueYDefensa(p2, p1);

                    if (p1.Caracteristicas.Salud <= 0)
                    {
                        Console.WriteLine("El ganador fue " + p2.Datos.Name);
                        p1.Caracteristicas.Salud = 100;
                        p2.Caracteristicas.Salud = 100;
                        ManejarFatality(p2);
                        return p2;
                    }
                }
                else
                {
                    if (op2 == 2)
                    {
                        realizarAtaqueYDefensa(p1, p2);

                        if (p2.Caracteristicas.Salud <= 0)
                        {
                            Console.WriteLine("\nPerdiste la batalla.");
                            Console.WriteLine("El ganador fue " + p1.Datos.Name);
                            Console.WriteLine("---------COMBATE FINALIZADO--------");
                            p1.Caracteristicas.Salud = 100;
                            p2.Caracteristicas.Salud = 100;
                            ManejarFatality(p1);
                            return p1;
                        }
                    }
                }
            }
            return null;

        }

        public async Task<Personaje> combateTorre(List<Personaje> pjPrincipal, List<Personaje> PjSecundario, Combate combate)
        {
            var random = new Random();
            var seleccion = new Seleccion();
            Personaje pjSeleccionado = null;

            pjSeleccionado = SeleccionarPJ(pjPrincipal, seleccion, pjSeleccionado);
            int nivel = seleccionarNivel();

            for (int i = 0; i < nivel; i++)
            {
                Personaje pjSeleccionado2 = PjSecundario[random.Next(PjSecundario.Count)];

                while (pjSeleccionado2 == pjSeleccionado)
                {
                    pjSeleccionado2 = PjSecundario[random.Next(PjSecundario.Count)];
                }

                Console.WriteLine($"\nNivel {i + 1}: {pjSeleccionado.Datos.Name} vs {pjSeleccionado2.Datos.Name}");
                await verificarBonificacionClima(pjSeleccionado, pjSeleccionado2);

                var pjGanador = combate.realizarCombate(pjSeleccionado, pjSeleccionado2); // Agregado await

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

            Console.WriteLine("\n¡Felicidades, ganaste el torneo!");
            Console.WriteLine($"\nEl personaje {pjSeleccionado.Datos.Name} ganó todos los niveles");
            Console.WriteLine("-------------------------------------------------------------");
            return pjSeleccionado;
        }

        private static async Task verificarBonificacionClima(Personaje pjSeleccionado, Personaje pjSeleccionado2)
        {
            string weather = await WeatherApi.GetWeatherAsync();
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

            return nivel;
        }

        private static void ManejarFatality(Personaje pjSeleccionado)
        {
            var asci = new Ascii();
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
                int i = random2.Next(0, 2);

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

    }
}
