using System;
using System.Threading;

namespace barra
{
    public class LoadingBar
    {
        public static void Show()
        {
            int total = 50;
            for (int i = 0; i <= total; i++)
            {
                Console.Write("\r[");
                Console.Write(new string('█', i));
                Console.Write(new string('░', total - i));
                Console.Write($"] {i * 2}%");
                Thread.Sleep(50); // Pausa de 50ms entre cada incremento
            }
            Console.Clear();
        }
    }
}
