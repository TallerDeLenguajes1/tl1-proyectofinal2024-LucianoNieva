using System;
using System.Media;

namespace Soundtrack
{
    public class Musica
    {
        private SoundPlayer player;

        public Musica(string filePath)
        {
            player = new SoundPlayer(filePath);
        }

        public void Play()
        {
            player.PlayLooping(); // Para que el sonido se reproduzca en bucle
        }

        public void Stop()
        {
            player.Stop();
        }
    }
}
