# Astrotime
Actividad 3 del curso Desarrollo de Videojuegos 1

Elaborado por: Rodolfo Mora-Zamora.

## Descripción del juego

- **Género:** Top-Down Action Adventure
- **Engine:** Unity 3D 2022.3.27f1
- **Disponible en:** https://ceibasoft.itch.io/astrotime?secret=jOqUSn0RZGRG0mXkjXX0UvlczqA
- **Código disponible en:** https://github.com/Rjmoraza/astrotime

## Sistema de Eventos

Se creó un sistema de eventos centralizado en un ScriptableObject llamado LevelManager. 

![image-20240627184226672](README.assets/image-20240627184226672.png)

Al iniciar un nuevo nivel se limpian todos los eventos garantizando que ningún objeto de niveles pasados quede escuchando un evento. También se reinicia el valor del `lastIndex` que se requiere inicie en 0 en cada nivel.

![image-20240627184628771](README.assets/image-20240627184628771.png)

Este método es llamado desde el Awake del script `GameManager` que se ejecuta al inicio de cada nivel y antes de que los demás objetos registren sus listeners a los distintos eventos del `LevelManager`.

![image-20240627184759153](README.assets/image-20240627184759153.png)

Por ejemplo el script encargado de manejar al jugador, llamado `PlayerController` registra listeners en varios de los eventos para poder reproducir efectos de sonido en cada respuesta:

![image-20240627185002365](README.assets/image-20240627185002365.png)

## Uso de Interfaces

El script `CombinationBlock` implementa la interfaz `Interactable` que permite al jugador interactuar con el bloque manteniendo un acoplamiento bajo. La interfaz actúa como intermediario de forma que el `PlayerController` no depende de `CombinationBlock` ni viceversa.

![image-20240627185230902](README.assets/image-20240627185230902.png)

## Funciones anónimas y sistema de diálogo

Se implementó un sistema de diálogo simple que permite al jugador elegir si quiere o no activar un bloque. Para la correcta operación de este sistema de diálogo, y con el fin de mantener bajo acoplamiento, se utilizaron funciones anónimas que se ejecutan cuando el jugador escoge una opción:

![image-20240627185449456](README.assets/image-20240627185449456.png)

En el script `DialogManager` se encuentran dos acciones que reciben una función anónima cuando se activa el diálogo: 

![image-20240627185612788](README.assets/image-20240627185612788.png)

En el script `CombinationBlock` se envían las funciones respectivas que cambian el estado del bloque según la decisión del jugador. 

![image-20240627185718623](README.assets/image-20240627185718623.png)

Esto permite al `DialogManager` ejecutar la acción correcta con el bloque que invocó el diálogo sin necesidad de crear una dependencia circular con `CombinationBlock`.

## BlendTree

Para las animaciones del personaje se crearon 8 secuencias. 4 para la secuencia de Idle en las direcciones Arriba, Abajo, Izquierda y Derecha. Otras 4 para la secuencia de Run igual en cada dirección. Estas animaciones se configuraron en un BlendTree permitiendo al AnimatorController seleccionar el clip más apropiado según la velocidad de movimiento y dirección del personaje.

![image-20240627190031287](README.assets/image-20240627190031287.png)

Cuando se recibe un valor de entrada para el movimiento, se envía este valor multiplicado por una constante, esto garantiza que se supera el umbral del BlendTree y se reproduce el clip de movimiento correcto. Por otro lado si no se recibe ningún input de movimiento, se envía la última dirección registrada con magnitud más pequeña, esto el BlendTree lo interpreta como que debe reproducir los clips de Idle.

![image-20240627190113991](README.assets/image-20240627190113991.png)

