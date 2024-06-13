using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class personaje
{

    private string tipo;
    private string name;

    private string apodo;

    private DateTime fechaNacimiento;

    private int edad;

    private int velocidad,destreza,fuerza,nivel,armadura,salud;

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


}

enum nombrePersonajes
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

enum tipo
{
    Ninja,
    Hechicero,
    DiosDelTrueno,
    MonjeShaolin,
    ArtesMariales,
    Comandante,
    LiderClan
}

