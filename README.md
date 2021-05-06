
![image](https://user-images.githubusercontent.com/47497948/117285670-8ff59400-ae68-11eb-9c51-37ec3cf95891.png)
# Descripción
Esta es la tercera práctica de la asignatura **Inteligencia Artificial para Videojuegos** de la **Universidad Complutense de Madrid**. 

Utilizamos pivotal tracker como herramienta de gestión del proyecto. Puedes ver nuestra organización [aquí](https://www.pivotaltracker.com/n/projects/2490634)

Consiste en la implementación de entidades con inteligencia artificial siguiendo el esquema y la estructura planteada en el enunciado **IAV-Práctica-3.pdf**.

Plantea un ejercicio basado en **El fantasma de la Ópera** donde el fantasma estará controlado por la herramienta **Behaviour Designer** de **Unity** mediante la cual tomará decisiones de manera inteligente para ir a secuestrar a la cantante, reparar sus muebles, apagar las luces, tomar el mejor camino, etc. Por otro lado, la cantante y el público se han gestionado con **Bolt** para tomar decisiones inteligentes como el fantasma. Por un lado, la cantante estará yendo y viniendo entre el escenario y las bambalinas hasta que el fantasma la secuestre, en cuyo caso pasará a estar secuestrada. Si se consigue liberar mediante las distintas acciones que pueda tomar el **Vizconde** (el jugador) pasará a estar merodeando perdida (si no reconoce el sitio), a excepción de que detecte que el Vizonde anda cerca, en cuyo caso lo seguirá. Si el fantasma consigue llevar a la chica a la celda, entonces estará encarcelada hasta que el Vizconde la libere.

# Documentación técnica del proyecto

El proyecto está implementado con **Unity** y con herramientas del mismo como **Bolt** y **Behaviour Designer**

## 1 Mecánicas
- **Flechas de dirección o WADS**: encargadas del movimiento del jugador

        Flecha arriba (W) -> movimiento en el eje Z positivo
        Flecha abajo (S)-> movimiento en el eje Z negativo
        Flecha derecha (D)-> movimiento positivo en el eje X
        Flecha izquierda (A)-> movimiento negativo en el eje X

- **Barra espaciadora**: alterna entre los distintos modos de la cámara

- **Tecla E**: para interactuar con diversas entidades del entorno.

## 2 Entidades y escenario
### 2.1 Entidades
### 2.2 Escenario
## 3. Implementación de **IA**
## 3.1 Bolt
## 3.2 Behaviour Designer
## 3.3 Scripts
## 4 Pruebas realizadas

## 5 Recursos de terceros empleados
- Pseudocódigo del libro: [**AI for Games, Third Edition**](https://ebookcentral.proquest.com/lib/universidadcomplutense-ebooks/detail.action?docID=5735527) de **Millington**

- Código de [**Federico Peinado**](https://github.com/federicopeinado) habilitado para la asignatura.

- [**Mixamo**](https://www.mixamo.com/) para las mallas y las animaciones empleadas como assets. 
