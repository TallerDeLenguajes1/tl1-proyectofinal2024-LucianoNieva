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
            Console.WriteLine($"\nLa salud de {defensor.Name} es de: {defensor.Salud}");
        }

        public Personaje turno(Personaje p1, Personaje p2)
        {
            while (p1.Salud > 0 || p2.Salud > 0)
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
                    }else
                    {
                        if (fatality == 1)
                        {
                            var random2 = new Random();
                            int i = random2.Next(0, 1);

                            if (i == 0)
                            {
                                Console.WriteLine("Fallo la combinacion. no se realizo la fatality");
                                Console.WriteLine("---------COMBATE FINALIZADO--------");
                            }else
                            {
                                if (i == 1)
                                {
                                    Console.WriteLine("La fatality se realizo con exito");
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
                    Console.WriteLine("El ganador fue " + p2.Name);
                    Console.WriteLine("\nDesea realizar una fatality?");
                    Console.WriteLine("1) Si");
                    Console.WriteLine("0) No");
                    int.TryParse(Console.ReadLine(), out int fatality);
                    if (fatality == 0)
                    {
                        Console.WriteLine("---------COMBATE FINALIZADO--------");
                        return p2;
                    }else
                    {
                        if (fatality == 1)
                        {
                            var random2 = new Random();
                            int i = random2.Next(0,1);

                            if (i == 0)
                            {
                                
                                Console.WriteLine("Fallo la combinacion. no se realizo la fatality");
                                Console.WriteLine("---------COMBATE FINALIZADO--------");
                            }else
                            {
                                if (i == 1)
                                {
                                    Console.WriteLine("La fatality se realizo con exito");
                                    Console.WriteLine("---------COMBATE FINALIZADO--------");
                                }
                            }
                        }
                    }
                    return p2;
                }
            }
            return null;
        }

        public void combateTorre(Personaje pjPrincipal ,int nivel){

            var random = new Random();
            var seleccion = new Seleccion();

            switch (nivel)
            {
                case 3:
                    nivel = 3;
                    break;

                case 5:
                    nivel = 5;
                    break;

                case 7:
                    nivel = 7;
                    break;
            }

            for (int i = 0; i < nivel; i++)
            {
                var pj2 = seleccion.seleccionarPersonaje(personajes, i);
            }

        }

    }
}

