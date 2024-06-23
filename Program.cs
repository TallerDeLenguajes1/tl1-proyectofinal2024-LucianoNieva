using System;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using personaje;
using fabrica;
using JSON;
using combates;
using seleccionPersonaje;
using barra;
class Program
{
    static void Main(string[] args)
    {
        var fabrica = new FabricaDePersonajes();
        var pjJson = new PersonajesJSON();
        var historial = new HistorialJson();
        var combate = new Combate();
        var seleccion = new Seleccion();
        var archivoPersonajes = @"C:\Users\lucia\OneDrive\Escritorio\tallertp\tl1-proyectofinal2024-LucianoNieva\texto.json";
        var archivoHistorial = @"C:\Users\lucia\OneDrive\Escritorio\tallertp\tl1-proyectofinal2024-LucianoNieva\historial.json";
        var personajes = new List<Personaje>();
        var listGanadores = new List<Personaje>();

        if (pjJson.Existe(archivoPersonajes))
        {
            personajes = pjJson.LeerPersonajes(archivoPersonajes);
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                var nuevoPJ = fabrica.CrearPersonajesAleatorios();
                nuevoPJ.Id = i + 1;
                personajes.Add(nuevoPJ);
            }
            pjJson.GuardarPersonajes(personajes, archivoPersonajes);

        }
        personajes = pjJson.LeerPersonajes(archivoPersonajes);

        Console.WriteLine("\nBienvenido a Mortal Kombat\n");
        Console.WriteLine("                     _..gggggppppp.._                       \n" +
                         "                  _.gd$$$$$$$$$$$$$$$$$$bp._                  \n" +
                         "               .g$$$$$$P^^\"\"j$$b\"\"\"\"^^T$$$$$$p.               \n" +
                         "            .g$$$P^T$$b    d$P T;       \"\"^^T$$$p.            \n" +
                         "          .d$$P^\"  :$; `  :$;                \"^T$$b.          \n" +
                         "        .d$$P'      T$b.   T$b                  `T$$b.        \n" +
                         "       d$$P'      .gg$$$$bpd$$$p.d$bpp.           `T$$b       \n" +
                         "      d$$P      .d$$$$$$$$$$$$$$$$$$$$bp.           T$$b      \n" +
                         "     d$$P      d$$$$$$$$$$$$$$$$$$$$$$$$$b.          T$$b     \n" +
                         "    d$$P      d$$$$$$$$$$$$$$$$$$P^^T$$$$P            T$$b    \n" +
                         "   d$$P    '-'T$$$$$$$$$$$$$$$$$$bggpd$$$$b.           T$$b   \n" +
                         "  :$$$      .d$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$p._.g.     $$$;  \n" +
                         "  $$$;     d$$$$$$$$$$$$$$$$$$$$$$$P^\"^T$$$$P^^T$$$;    :$$$  \n" +
                         " :$$$     :$$$$$$$$$$$$$$:$$$$$$$$$_    \"^T$bpd$$$$,     $$$; \n" +
                         " $$$;     :$$$$$$$$$$$$$$bT$$$$$P^^T$p.    `T$$$$$$;     :$$$ \n" +
                         ":$$$      :$$$$$$$$$$$$$$P `^^^'    \"^T$p.    lb`TP       $$$;\n" +
                         ":$$$      $$$$$$$$$$$$$$$              `T$$p._;$b         $$$;\n" +
                         "$$$;      $$$$$$$$$$$$$$;                `T$$$$:Tb        :$$$\n" +
                         "$$$;      $$$$$$$$$$$$$$$                        Tb    _  :$$$\n" +
                         ":$$$     d$$$$$$$$$$$$$$$.                        $b.__Tb $$$;\n" +
                         ":$$$  .g$$$$$$$$$$$$$$$$$$$p...______...gp._      :$`^^^' $$$;\n" +
                         " $$$;  `^^'T$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$p.    Tb._, :$$$ \n" +
                         " :$$$       T$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$b.   \"^\"  $$$; \n" +
                         "  $$$;       `$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$b      :$$$  \n" +
                         "  :$$$        $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$;     $$$;  \n" +
                         "   T$$b    _  :$$`$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$;   d$$P   \n" +
                         "    T$$b   T$g$$; :$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$  d$$P    \n" +
                         "     T$$b   `^^'  :$$ \"^T$$$$$$$$$$$$$$$$$$$$$$$$$$$ d$$P     \n" +
                         "      T$$b        $P     T$$$$$$$$$$$$$$$$$$$$$$$$$;d$$P      \n" +
                         "       T$$b.      '       $$$$$$$$$$$$$$$$$$$$$$$$$$$$P       \n" +
                         "        `T$$$p.          d$$$$$$$$$$$$$$$$$$$$$$$$$$P'        \n" +
                         "          `T$$$$p..__..g$$$$$$$$$$$$$$$$$$$$$$$$$$P'          \n" +
                         "            \"^$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$^\"            \n" +
                         "               \"^T$$$$$$$$$$$$$$$$$$$$$$$$$$P^\"               \n" +
                         "                   \"\"\"^^^T$$$$$$$$$$P^^^\"\"\"");

        Console.WriteLine("\nCargando el juego...\n");
        LoadingBar.Show();


        Console.WriteLine("\nSeleccione una opcion: ");
        Console.WriteLine("\n1) Mostrar personajes: ");
        Console.WriteLine("\n2) Realizar combate 1v1: ");
        Console.WriteLine("\n3) Realizar torneo: ");
        Console.WriteLine("\n4) Leer historial de ganadores");

        int.TryParse(Console.ReadLine(), out int selecMenu);

        while (selecMenu != 0)
        {
            switch (selecMenu)
            {
                case 1:
                    fabrica.MostrarPersonaje(personajes);
                    break;

                case 2:
                    Console.WriteLine("\nSeleccione el id del personaje que desea usar:");
                    string selec = Console.ReadLine();
                    int.TryParse(selec, out int op);

                    if (op >= 1 && op <= 10)
                    {
                        var personajeSeleccionado = seleccion.seleccionarPersonaje(personajes, op);
                        seleccion.personajeSeleccionado(personajeSeleccionado);
                        personajes.Remove(personajeSeleccionado);

                        Console.WriteLine("\nSeleccione el id del personaje que sera su oponente:");
                        int.TryParse(Console.ReadLine(), out int selec2);

                        var personajeSeleccionado2 = seleccion.seleccionarPersonaje(personajes, selec2);
                        var pjGanador = combate.turno(personajeSeleccionado, personajeSeleccionado2);
                        listGanadores.Add(pjGanador);
                        historial.GuardarGanador(listGanadores, archivoHistorial);
                    }
                    else
                    {
                        Console.WriteLine("\nNo selecciono un ID correcto.");
                    }
                    break;
                case 3:
                    Console.WriteLine("\nEn mantenimiento");
                    break;

                case 4:
                    var leerPJ = historial.LeerGanador(archivoHistorial);
                    Console.WriteLine("\n--Historial ganadores--\n");
                    fabrica.MostrarPersonaje(leerPJ);
                    break;

            }

            Console.WriteLine("\nSeleccione otra opcion: ");
            Console.WriteLine("\n1) Mostrar personajes: ");
            Console.WriteLine("\n2) Realizar combate 1v1: ");
            Console.WriteLine("\n3) Realizar torneo: ");
            Console.WriteLine("\n4) Leer historial de ganadores");
            Console.WriteLine("\n0) Salir del juego");

            int.TryParse(Console.ReadLine(), out selecMenu);
        }


    }

   
}
