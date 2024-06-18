using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Personaje
{
    private string tipo;
    private string name;
    private string apodo;
    private DateTime fechaNacimiento;
    private int edad;
    private int velocidad, destreza, fuerza, nivel, armadura, salud, id;

    public string Tipo { get => tipo; set => tipo = value; }
    public string Name { get => name; set => name = value; }
    public string Apodo { get => apodo; set => apodo = value; }
    public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
    public int Edad { get => edad; set => edad = value; }
    public int Velocidad { get => velocidad; set => velocidad = value; }
    public int Destreza { get => destreza; set => destreza = value; }
    public int Fuerza { get => fuerza; set => fuerza = value; }
    public int Nivel { get => nivel; set => nivel = value; }
    public int Armadura { get => armadura; set => armadura = value; }
    public int Salud { get => salud; set => salud = value; }
    public int Id { get => id; set => id = value; }

}
enum NombrePersonajes
{
    LiuKang,
    SubZero,
    Scorpion,
    Raiden,
    Kitana,
    JohnnyCage,
    SonyaBlade,
    ShangTsung,
    Mileena,
    Kano
}

enum Tipo
{
    Ninja,
    Hechicero,
    DiosDelTrueno,
    MonjeShaolin,
    ArtesMarciales,
    Comandante,
    LiderClan
}

public class FabricaDePersonajes
{
    public Personaje CrearPersonajesAleatorios()
    {

        Random random = new Random();
        var personaje = new Personaje();

        personaje.Name = Enum.GetName(typeof(NombrePersonajes), random.Next(1, Enum.GetNames(typeof(NombrePersonajes)).Length));
        personaje.Tipo = Enum.GetName(typeof(Tipo), random.Next(1, Enum.GetNames(typeof(Tipo)).Length));
        personaje.Edad = random.Next(0, 300);
        personaje.FechaNacimiento = DateTime.Now.AddYears(-personaje.Edad);
        personaje.Salud = 100;
        personaje.Velocidad = random.Next(1, 11);
        personaje.Destreza = random.Next(1, 6);
        personaje.Fuerza = random.Next(1, 11);
        personaje.Nivel = random.Next(1, 11);
        personaje.Armadura = random.Next(1, 11);

        return personaje;
    }

    public void MostrarPersonaje(List<Personaje> personajes)
    {
        int i = 1;
        foreach (var datosPJ in personajes)
        {
            Console.WriteLine("ID: " + i++);
            Console.WriteLine("Nombre: " + datosPJ.Name);
            Console.WriteLine("Tipo: " + datosPJ.Tipo);
            Console.WriteLine("Apodo: " + datosPJ.Apodo);
            Console.WriteLine("Fecha de Nacimiento: " + datosPJ.FechaNacimiento.ToString("dd/MM/yyyy"));
            Console.WriteLine("Edad: " + datosPJ.Edad);
            Console.WriteLine("Velocidad: " + datosPJ.Velocidad);
            Console.WriteLine("Destreza: " + datosPJ.Destreza);
            Console.WriteLine("Fuerza: " + datosPJ.Fuerza);
            Console.WriteLine("Nivel: " + datosPJ.Nivel);
            Console.WriteLine("Armadura: " + datosPJ.Armadura);
            Console.WriteLine("Salud: " + datosPJ.Salud);
            Console.WriteLine();
        }
    }
}


public class PersonajesJSON
{
    /* public void GuardarPersonajes(List<Personaje> personajes, string archivo)
     {

         string jsonString = System.Text.Json.JsonSerializer.Serialize(personajes);
         File.WriteAllText(archivo, jsonString);

     }*/

    public void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo)
    {
        string jsonString = System.Text.Json.JsonSerializer.Serialize(personajes);

        using (var archivo = new FileStream(nombreArchivo, FileMode.Create))
        {
            using (var strWriter = new StreamWriter(archivo))
            {
                strWriter.WriteLine(jsonString);
            }
        }
    }

    public List<Personaje> LeerPersonajes(string archivo)
    {
        if (File.Exists(archivo))
        {
            string Json = File.ReadAllText(archivo);
            return System.Text.Json.JsonSerializer.Deserialize<List<Personaje>>(Json);
        }
        else
        {
            Console.WriteLine("No se encontro el archivo");
            return null;
        }

    }

    public bool Existe(string archivo)
    {
        if (!File.Exists(archivo))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}

public class HistorialJson
{
    public void GuardarGanador(Personaje gana, string archivo)
    {
        if (!File.Exists(archivo))
        {
            File.Create(archivo);
        }

        string jsonString = JsonConvert.SerializeObject(gana);
        File.WriteAllText(archivo, jsonString);
    }


    public List<Personaje> LeerGanador(string archivo)
    {

        string Json = File.ReadAllText(archivo);
        return System.Text.Json.JsonSerializer.Deserialize<List<Personaje>>(Json);
    }

    public bool Existe(string archivo)
    {
        if (!File.Exists(archivo))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

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

        defensor.Salud = defensor.Salud - danioProvocado;
    }

    public void turno(Personaje p1,Personaje p2)
    {
        while (p1.Salud > 0 || p2.Salud > 0)
        {
            realizarCombate(p1,p2);

            if (p2.Salud <= 0){
                Console.WriteLine("El ganador fue" + p1.Name);
                break;
            }

            realizarCombate(p2,p1);
        }

        if (p1.Salud <= 0){
            Console.WriteLine("El ganador fue" + p2.Name);
        }

    }

}

public class seleccion
{
    public Personaje seleccionarPersonaje(List<Personaje> personaje, int id)
    {
        foreach (var item in personaje)
        {
            if (item.Id == id)
            {
                return item;
            }
        }

        return null;
    }

    public void personajeSeleccionado(Personaje pj)
    {
            Console.WriteLine("El personaje seleccionado es: \n");
            Console.WriteLine("Nombre: " + pj.Name);
            Console.WriteLine("Tipo: " + pj.Tipo);
            Console.WriteLine("Apodo: " + pj.Apodo);
            Console.WriteLine("Fecha de Nacimiento: " + pj.FechaNacimiento.ToString("dd/MM/yyyy"));
            Console.WriteLine("Edad: " + pj.Edad);
            Console.WriteLine("Velocidad: " + pj.Velocidad);
            Console.WriteLine("Destreza: " + pj.Destreza);
            Console.WriteLine("Fuerza: " + pj.Fuerza);
            Console.WriteLine("Nivel: " + pj.Nivel);
            Console.WriteLine("Armadura: " + pj.Armadura);
            Console.WriteLine("Salud: " + pj.Salud);
            Console.WriteLine();
    }
}




class Program
{
    static void Main(string[] args)
    {
        var fabrica = new FabricaDePersonajes();
        var pjJson = new PersonajesJSON();
        var historial = new HistorialJson();
        var combate = new Combate();
        var seleccion = new seleccion();
        var archivoPersonajes = @"D:\Facultad\Taller\TrabajosPracticos\tl1-proyectofinal2024-LucianoNieva\archivo.txt";
        var archivoHistorial = @"D:\Facultad\Taller\TrabajosPracticos\tl1-proyectofinal2024-LucianoNieva\historial.txt";

        var personajes = new List<Personaje>();

        if (pjJson.Existe(archivoPersonajes))
        {
            personajes = pjJson.LeerPersonajes(archivoPersonajes);
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                personajes.Add(fabrica.CrearPersonajesAleatorios());
            }
            pjJson.GuardarPersonajes(personajes, archivoPersonajes);

        }

        personajes = pjJson.LeerPersonajes(archivoPersonajes);
        fabrica.MostrarPersonaje(personajes);

        var personajeSeleccionado = seleccion.seleccionarPersonaje(personajes,1);

        seleccion.personajeSeleccionado(personajeSeleccionado);


    }
}
