Mortal Kombat
Este es un juego basado en Mortal Kombat desarrollado en C# para ser jugado en consola.

Características Principales
Selección de Personajes: Elige entre una variedad de personajes del universo de Mortal Kombat, cada uno con estadísticas generadas aleatoriamente.
Combate 1v1 y Torneos: Participa en combates uno contra uno o en torneos para demostrar tu destreza en el campo de batalla.
Historial de Ganadores: Consulta el historial de ganadores para ver quiénes han sido los campeones del torneo.
Interfaz de Usuario Interactiva: Navega a través de un menú interactivo para seleccionar tus opciones de juego.
Estructura del Proyecto
Menu.cs
Este archivo contiene la lógica del menú interactivo del juego. Aquí se define la navegación del menú y las acciones que se realizan al seleccionar cada opción.

ControlarMenu: Método principal que controla la navegación del menú y las interacciones del usuario.
DisplayMenu: Método que muestra las opciones del menú en la consola y resalta la opción seleccionada.
Combate.cs
Este archivo maneja la lógica de los combates dentro del juego, incluyendo combates uno contra uno y torneos.

RealizarCombate1v1: Método para realizar un combate uno contra uno entre dos personajes.
Torre: Método para gestionar y realizar un torneo entre varios personajes.
Program.cs
Este es el punto de entrada principal del programa. Aquí se inicializan los objetos necesarios y se llama al método ControlarMenu para comenzar la interacción del usuario.

Instalación
Clona este repositorio en tu máquina local.
Abre el proyecto en tu IDE de C# preferido.
Asegúrate de tener instalado .NET 5.0 o superior.
Compila y ejecuta el proyecto.

Cómo Jugar
Mostrar Personajes: Visualiza una lista de todos los personajes disponibles y sus estadísticas.
Realizar Combate 1v1: Inicia un combate entre dos personajes seleccionados (puede seleccionar jugar contra un bot o contra un amigo).
Realizar Torneo: Participa en un torneo donde tienes 3 tipos de niveles con varios personajes para determinar quién es el mejor luchador .
Leer Historial de Ganadores: Consulta el historial de ganadores para ver quiénes han sido los campeones anteriores.
Salir del Juego: Cierra el juego y termina la sesión.
Requisitos del Sistema
Sistema Operativo: Windows, macOS, Linux
Lenguaje: C#
Framework: .NET 5.0 o superior
Memoria: 4 GB de RAM (mínimo)
Espacio en Disco: 100 MB
Créditos
Desarrollador: Nieva Luciano Fabrizio (https://github.com/LucianoNieva)
Recursos Gráficos: ASCII art tomado de https://www.asciiart.eu y https://ascii.co.uk/art/mortalkombat
Documentación:
https://stackoverflow.com
https://www.w3schools.com/cs/index.php
https://learn.microsoft.com/es-es/dotnet/csharp
https://www.youtube.com
