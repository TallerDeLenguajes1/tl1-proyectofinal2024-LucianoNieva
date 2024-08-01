
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
            const int Ajuste = 1000;

            int danioProvocado = ((ataque * efectividad) - defensa) / Ajuste;


            Console.WriteLine($"\nEl atacante {datosPj.Name} realizó un daño de: {danioProvocado}");
            controlDanio(danioProvocado);

            caracteristicas2.Salud -= danioProvocado;


            controlarSaludNoNegativa(caracteristicas2);

            Console.WriteLine($"La salud de {datosPj2.Name} es de: {caracteristicas2.Salud}");

            await Task.Delay(1000);
        }

        private static void controlDanio(int danioProvocado)
        {
            if (danioProvocado > 15)
            {

                Console.WriteLine("REALIZO UN GOLPE CRITICO!");
            }
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
                var pjGanador = await combate.pelea1v1Bot(personajes);
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
            return await PeleaAmigo(pjSeleccionado, pjSeleccionado2, asci);
        }

        private async Task<Personaje> PeleaAmigo(Personaje pjSeleccionado, Personaje pjSeleccionado2, Ascii asci)
        {
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
                        return await ControlarSaludPJ(pjSeleccionado, pjSeleccionado2, asci);
                    }
                }
                else if (accion == 2)
                {
                    Console.WriteLine("Defendiendo...");
                    await realizarAtaqueYDefensa(pjSeleccionado2, pjSeleccionado);
                    await Task.Delay(1000);

                    if (pjSeleccionado.Caracteristicas.Salud <= 0)
                    {
                        return await ControlarSaludPJ(pjSeleccionado, pjSeleccionado2, asci);
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
                        return await ControlarSaludPJ(pjSeleccionado, pjSeleccionado2, asci);
                    }
                }
                else if (accion2 == 2)
                {
                    Console.WriteLine("Defendiendo...");
                    await realizarAtaqueYDefensa(pjSeleccionado, pjSeleccionado2);
                    await Task.Delay(1000);

                    if (pjSeleccionado2.Caracteristicas.Salud <= 0)
                    {
                        return await ControlarSaludPJ(pjSeleccionado, pjSeleccionado2, asci);
                    }
                }

            }
            return null;
        }

        public async Task<Personaje> pelea1v1Bot(List<Personaje> personajes)
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
                    return await ControlarSaludPJ(p1, p2, asci);
                }

                await realizarAtaqueYDefensa(p2, p1);

                if (p1.Caracteristicas.Salud <= 0)
                {
                    return await ControlarSaludPJ(p1, p2, asci);
                }
            }
            return null;
        }

        private static async Task<Personaje> ControlarSaludPJ(Personaje p1, Personaje p2, Ascii asci)
        {
            Console.Clear();
            Console.WriteLine("El ganador fue " + p1.Datos.Name);
            p1.Caracteristicas.Salud = 100;
            p2.Caracteristicas.Salud = 100;
            asci.Finish();
            await ManejarFatality(p1);
            return p1;
        }

        public async Task<Personaje> Torre(List<Personaje> pjPrincipal, List<Personaje> PjSecundario, Combate combate, List<Personaje> listGanadores, HistorialJson historial, string archivoHistorial)
        {
            var random = new Random();
            var seleccion = new Seleccion();
            Personaje pjSeleccionado = SeleccionarPJ(pjPrincipal, seleccion);
            int nivel = seleccionarNivelTorre();
            int contador = 0;
            var pjOponentes = new List<Personaje>();
            CargarOponentes(PjSecundario, random, pjSeleccionado, nivel, pjOponentes);
            return await realizarCombateTorre(listGanadores, historial, archivoHistorial, pjSeleccionado, contador, pjOponentes);
        }

        private async Task<Personaje> realizarCombateTorre(List<Personaje> listGanadores, HistorialJson historial, string archivoHistorial, Personaje pjSeleccionado, int contador, List<Personaje> pjOponentes)
        {
            var restaura = pjSeleccionado.Caracteristicas.Fuerza;
            var restaura2 = pjSeleccionado.Caracteristicas.Armadura;
            foreach (var oponente in pjOponentes)
            {
                Console.WriteLine($"\nNivel {contador + 1}: {pjSeleccionado.Datos.Name} vs {oponente.Datos.Name}");
                await verificarBonificacionClima(pjSeleccionado, oponente);

                var pjGanador = await realizarCombate(pjSeleccionado, oponente);

                if (pjGanador == pjSeleccionado)
                {
                    await MostrarTransicionDeNivel(pjGanador, contador + 1);
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
            pjSeleccionado.Caracteristicas.Fuerza = restaura;
            pjSeleccionado.Caracteristicas.Armadura = restaura2;
            Console.WriteLine("\nPor ganar el torneo ganaste +3 Fuerza y +3 Armadura");
            bonificacionTorneo(pjSeleccionado);
            guardarGanador(listGanadores, historial, archivoHistorial, pjSeleccionado);
            Console.WriteLine("-------------------------------------------------------------");
            return pjSeleccionado;
        }

        private static void bonificacionTorneo(Personaje pjSeleccionado)
        {
            pjSeleccionado.Caracteristicas.Fuerza += 3;
            pjSeleccionado.Caracteristicas.Armadura += 3;
        }

        private async Task MostrarTransicionDeNivel(Personaje pj, int nivelActual)
        {
            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine($"        {pj.Datos.Name} ");
            Console.WriteLine($"        Nivel {nivelActual}");
            Console.WriteLine("==================================");
            Console.WriteLine("\nSubiendo al siguiente nivel...");
            await Task.Delay(2000);
        }

        private static void CargarOponentes(List<Personaje> PjSecundario, Random random, Personaje pjSeleccionado, int nivel, List<Personaje> pjOponentes)
        {
            for (int i = 0; i < nivel; i++)
            {
                Personaje pjSeleccionado2 = PjSecundario[random.Next(PjSecundario.Count)];

                while (pjSeleccionado2 == pjSeleccionado || pjOponentes.Contains(pjSeleccionado2))
                {
                    pjSeleccionado2 = PjSecundario[random.Next(PjSecundario.Count)];
                }

                pjOponentes.Add(pjSeleccionado2);
            }


            Console.Clear();
            MostrarOponentesTorre(pjOponentes);

        }

        private static void MostrarOponentesTorre(List<Personaje> pjOponentes)
        {
            int cont = 0;
            Console.WriteLine("Estos seran tus oponentes!\n");
            foreach (var item in pjOponentes)
            {
                Console.WriteLine($"Nivel {1 + cont} {item.Datos.Name} ");
                cont++;
            }
        }

        private async Task verificarBonificacionClima(Personaje pjSeleccionado, Personaje pjSeleccionado2)
        {
            string weather = await ApiClima.TraerInfoClima();
            Console.WriteLine($"El clima en esta batalla es: {weather}");
            ApiClima.controlarClimaConPersonaje(pjSeleccionado, weather);
            ApiClima.controlarClimaConPersonaje(pjSeleccionado2, weather);
        }

        private static int seleccionarNivelTorre()
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
                _ => 3,
            };
        }

        private static async Task ManejarFatality(Personaje pjSeleccionado)
        {
            var asci = new Ascii();
            Console.WriteLine("\nRealizando FATALITY..");
            await Task.Delay(1500);

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
                asci.Fatality();
                Console.WriteLine($"Recibiste una bonificación +2 de fuerza y +2 de armadura.");
                pjSeleccionado.Caracteristicas.Fuerza += 2;
                pjSeleccionado.Caracteristicas.Armadura += 2;
                asci.mostrarPJ(pjSeleccionado);
                Console.WriteLine("---------COMBATE FINALIZADO--------");
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
                if (pjSeleccionado == pjExistente)
                {
                    Console.WriteLine("\nNo puede seleccionar el mismo personaje.");
                    return SeleccionarPJ(personajes, seleccion, pjExistente);
                }
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
