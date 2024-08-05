using System;
using System.Runtime.InteropServices;
using System.Media;

namespace Soundtrack
{
    public class Musica
    {
        private string _rutaArchivo;
        private SoundPlayer _player;

        public Musica(string rutaArchivo)
        {
            _rutaArchivo = rutaArchivo;
            _player = new SoundPlayer(_rutaArchivo);
        }

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        public void Play()
        {
            SetVolume(2500); // Ajusta este valor para cambiar el volumen (0 - 65535)
            _player.PlayLooping();
        }

        public void Stop()
        {
            _player.Stop();
        }

        public void SetVolume(int volume)
        {
            uint newVolume = ((uint)volume & 0x0000ffff) | ((uint)volume << 16);
            waveOutSetVolume(IntPtr.Zero, newVolume);
        }

        public void controlarMusica()
        {

            Console.WriteLine("Desea quitar musica? 1 si 0 no");
            int.TryParse(Console.ReadLine(), out int music);
            if (music == 1)
            {
                Stop();
            }
            else
            {
                Play();
            }
        }
    }
}
