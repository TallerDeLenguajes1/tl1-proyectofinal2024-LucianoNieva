using JSON;
using personaje;
using seleccionPersonaje;


namespace combates
{
    public class Combate
    {

        public void realizarCombate(Personaje atacante, Personaje defensor)
        {
            var random = new Random();
            int ataque = atacante.Destreza * atacante.Fuerza * atacante.Nivel;
            int efectividad = random.Next(1, 101);
            int defensa = defensor.Armadura * defensor.Velocidad;
            const int Ajuste = 500;

            int danioProvocado = ((ataque * efectividad) - defensa) / Ajuste;
            Console.WriteLine($"\nEl atacante {atacante.Name} realizo un danio de: {danioProvocado}");

            defensor.Salud = defensor.Salud - danioProvocado;
            Console.WriteLine($"La salud de {defensor.Name} es de: {defensor.Salud}");
        }

        public Personaje turno(Personaje p1, Personaje p2)
        {
            while (p1.Salud > 0 && p2.Salud > 0)
            {
                realizarCombate(p1, p2);

                if (p2.Salud <= 0)
                {
                    Console.WriteLine("El ganador fue " + p1.Name);

                    Console.WriteLine("\nDesea realizar una fatality?");
                    Console.WriteLine("1) Si");
                    Console.WriteLine("0) No");
                    int.TryParse(Console.ReadLine(), out int fatality);
                    if (fatality == 0)
                    {
                        Console.WriteLine("---------COMBATE FINALIZADO--------");
                        return p1;
                    }
                    else
                    {
                        if (fatality == 1)
                        {
                            var random2 = new Random();
                            int i = random2.Next(0, 2);

                            if (i == 0)
                            {
                                Console.WriteLine("Fallo la combinacion. no se realizo la fatality");
                                Console.WriteLine("No recibiste una bonificacion");
                                Console.WriteLine("---------COMBATE FINALIZADO--------");
                            }
                            else
                            {
                                if (i == 1)
                                {
                                    Console.WriteLine("La fatality se realizo con exito");
                                    Console.WriteLine($"Recibiste una bonificacion + 6 de fuerza y + 6 de armadura");
                                    p1.Fuerza += 5;
                                    p1.Armadura += 5;
                                    Console.WriteLine("---------COMBATE FINALIZADO--------");
                                }
                            }
                        }
                    }
                    return p1;
                }

                realizarCombate(p2, p1);

                if (p1.Salud <= 0)
                {
                    Console.WriteLine("\nPerdiste la batalla");
                    Console.WriteLine("El ganador fue " + p2.Name);
                    Console.WriteLine("---------COMBATE FINALIZADO--------");
                    return p2;
                }
            }
            return null;
        }

        public void combateTorre(Personaje pjPrincipal, List<Personaje> PjSecundario, Combate combate, HistorialJson Json, string archivoHistorial)
        {

            var random = new Random();

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
            }



            for (int i = 0; i < nivel; i++)
            {
                var pj2 = PjSecundario[random.Next(PjSecundario.Count)];
                PjSecundario.Remove(pj2);

                Console.WriteLine($"Nivel {i + 1}: {pjPrincipal.Name} vs {pj2.Name}");

                var pjGanador = combate.turno(pjPrincipal, pj2);

                if (pjGanador == pjPrincipal)
                {
                    Console.WriteLine($"\nEl personaje {pjGanador.Name} gano el nivel {i + 1}");
                }
                else
                {
                    Console.WriteLine($"\nEl personaje {pjPrincipal.Name} perdio en el nivel {i + 1} ");
                    break;
                }
            }

            if (pjPrincipal.Salud > 0)
            {   
                Console.WriteLine("\nFelicidades ganaste el torneo");
                Console.WriteLine($"\nEl personaje {pjPrincipal.Name} ganó todos los niveles");
                Console.WriteLine("-------------------------------------------------------------");
                Json.GuardarGanador(new List<Personaje> { pjPrincipal }, archivoHistorial);
            }

        }

    }
}