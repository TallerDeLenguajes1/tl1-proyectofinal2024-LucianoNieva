# MORTAL KOMBAT
Este es un juego de rol basado en Mortal Kombat desarrollado en C# para ser jugado en consola.

## Características Principales

- **Selección de Personajes:** Elige entre una variedad de personajes del universo de Mortal Kombat, cada uno con estadísticas generadas aleatoriamente.
- **Combate 1v1 y Torneos:** Participa en combates uno contra uno o en torneos para demostrar tu destreza en el campo de batalla.
- **Historial de Ganadores:** Consulta el historial de ganadores para ver quiénes han sido los campeones del torneo.
- **Interfaz de Usuario Interactiva:** Navega a través de un menú interactivo para seleccionar tus opciones de juego.

## Estructura del Proyecto

### `Menu.cs`

Este archivo contiene la lógica del menú interactivo del juego. Aquí se define la navegación del menú y las acciones que se realizan al seleccionar cada opción.

- **ControlarMenu:** Método principal que controla la navegación del menú y las interacciones del usuario.
- **DisplayMenu:** Método que muestra las opciones del menú en la consola y resalta la opción seleccionada.

### `Combate.cs`

Este archivo maneja la lógica de los combates dentro del juego, incluyendo combates uno contra uno y torneos.

- **RealizarCombate1v1:** Método para realizar un combate uno contra uno entre dos personajes.
- **Torre:** Método para gestionar y realizar un torneo entre varios personajes.

### `Program.cs`

Este es el punto de entrada principal del programa. Aquí se inicializan los objetos necesarios y se llama al método `ControlarMenu` para comenzar la interacción del usuario.

## Instalación

1. Clona este repositorio en tu máquina local.
2. Abre el proyecto en tu IDE de C# preferido.
3. Asegúrate de tener instalado .NET 5.0 o superior.
4. Compila y ejecuta el proyecto.

## Cómo Jugar

1. **Mostrar Personajes:** Visualiza una lista de todos los personajes disponibles y sus estadísticas.
2. **Realizar Combate 1v1:** Inicia un combate entre dos personajes seleccionados.
3. **Realizar Torneo:** Participa en un torneo con varios personajes para determinar quién es el mejor luchador.
4. **Leer Historial de Ganadores:** Consulta el historial de ganadores para ver quiénes han sido los campeones anteriores.
5. **Salir del Juego:** Cierra el juego y termina la sesión.

## Requisitos del Sistema

- **Sistema Operativo:** Windows, macOS, Linux
- **Lenguaje:** C#
- **Framework:** .NET 5.0 o superior
- **Memoria:** 4 GB de RAM (mínimo)
- **Espacio en Disco:** 100 MB

## Créditos

- **Desarrollador:**  Nieva Luciano Fabrizio (https://github.com/LucianoNieva)
- **Recursos Gráficos:** ASCII art tomado de https://www.asciiart.eu y https://ascii.co.uk/art/mortalkombat
- **Ayuda o Documentación:**
  - https://stackoverflow.com
  - https://www.w3schools.com/cs/index.php
  - https://learn.microsoft.com/es-es/dotnet/csharp
  - https://www.youtube.com

