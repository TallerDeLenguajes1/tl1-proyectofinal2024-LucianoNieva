using personaje;


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
                    Console.WriteLine("---------COMBATE FINALIZADO--------");
                    return p1;
                }

                realizarCombate(p2, p1);

                if (p1.Salud <= 0)
                {
                    Console.WriteLine("El ganador fue " + p2.Name);
                    Console.WriteLine("---------COMBATE FINALIZADO--------");
                    return p2;
                }
            }
            return null;
        }

    }
}

