using System.Collections.Generic;

namespace personaje
{
    public class Personaje
{
    private string tipo;
    private string name;
    private string apodo;
    private DateTime fechaNacimiento;
    private int edad;
    private int velocidad;
    private int  destreza;

    private int armadura;
    private int fuerza; 
    private int nivel;
    private int salud;
    private int id;

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
}