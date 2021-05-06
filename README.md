
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

## 2 Entidades 
### 2.1 Personajes

- **Fantasma**: es la entidad con el comportamiento más complejo y alrededor de la cual gira el ejercicio. En la división general del juego es el personaje que aparece abajo a la izquierda. Sus decisiones vendrán dictaminadas por un árbol de comportamiento propio desarrollado en **Behaviour Designer**. Es capaz de coger las barcas, desplazarse por los sótanos, activar las palancas que hacen que las lámparas que caigan para asustar al público, secuestrar a la cantante, llevarla a una zona especifica del mapa (la celda) y volver a su sala inicial hasta que la cantante sea rescatada por el vizconde y llegue de nuevo al escenario a cantar.
 
- **Vizconde**: es el personaje controlado por el jugador. En la división general del juego es el personaje que aparece en la cámara de la derecha. Se encarga de reparar los desperfectos causados por el fantasma en la ópera (reparar lámparas) además de rescatar a la cantante cuando esta ha caido en las garras de dicha criatura y de moverse por el escenario cambiando la disposición de las barcas para dificultarle la tarea de secuestrar a la cantante. También puede destrozarle al fantasma su queridísimo piano, lo cual causará que este deje de hacer lo que sea que esté haciendo para repararlo con la mayor brevedad posible.

- **Cantante**: se trata del personaje que causa la discordia entre el fantasma y el vizconde. Ella trata de hacer diligentemente su trabajo de cantar en el **Escenario** aunque a veces irá a descansar para reponer su maravillosa voz a las **Bambalinas**. Sin embargo, el fantasma tratará de encerrarla en la celda ubicada en el **Sótano Norte** para ser solo él quién pueda deleitarse con su música a menos que nuestro jugador, el vizconde, la libere y la lleve de vuelta sana y salva al escenario para continuar con su actuación. Pese a trabajar en la Ópera, no conoce muy bien el lugar por lo que si se queda sola en una estancia que no conoce, empezará a merodear desorientada en busca de un lugar que si reconoza. La gestión de su comportamiento viene definida por una máquina de estados desarrollada en **Bolt**. 


- **Público**: ciudadanos comunes que han ido a la Ópera a disfrutar de la maravillosa voz de la cantante. Se encuentran por lo general siempre en el **Palco de Butacas**, a menos que en este ocurra algún indidente desafortunado como qué las lámparas del techo caigan por fuerzas sobrenaturales fantasmagóricas. En dicho caso, tratarán de huir a un lugar seguro, siendo este el **Véstibulo** puesto que es la sala más cercana a la salida de la Ópera. Su comportamiento viene dictaminado por una máquina de estados hecha en **Bolt**.

### 2.2 Entidades

- **Barcas**:

- **Piano**:

- **Palancas**:

- **Lámparas**:

### 3 Escenario

![image](https://user-images.githubusercontent.com/48771457/117318697-f4294f80-ae8a-11eb-92fb-e63c9ebf7e75.png)

La acción transcurre en la **Ópera**, la cual se divide en las siguientes secciones: 
- **Véstibulo**: adyacente al **Palco de Butacas**, es la sala a la que se desplazarán el público desde el **Palco de Butacas** cuando alguna de las lámparas se haya caido.

- **Palco de Butacas**: conectado con el **Véstibulo** y el **Escenario**. Es la sala en la que de forma general se encontrará el público y sobre la cual caen las lámparas al activar las palancas de los **Palcos**.

- **Escenario**: Lugar en el que comienza cantando nuestra bella diva y que cuenta con conexiones directas a los **Palcos Este y Oeste**, al **Patio de Butacas** y a las **Bambalinas**. Será el primer lugar en el que el fantasma trate de buscar a la cantante puesto que pasa la mayoría de su tiempo en esta estancia. Esta zona será intransitable para el fantasma si hay alguna persona que se encuentre en el **Palco de Butacas**, por lo que tendrá que asustarles antes de pasar por este lugar.

- **Bambalinas**: lugar de reposo durante los descansos de la cantante. Está conectado con los **Sótanos Este y Oeste** además de con el **Escenario**. Es la siguiente estancia en la que el fantasma tratará de buscar a la diva.

- **Palcos Este y Oeste**: Lugares en los que se encuentran las **Palancas** que propician la caida de las **Lámparas** que se encuentran en el Palco de Butacas. El **Palco Oeste** es la habitación de origen del Vizconde y cuenta con conexiones al **Sótano Oeste** y al **Escenario. Por otro lado, el **Palco Este** cuenta con conexiones directas al **Escenario** y al **Sótano Este**.

- **Sótanos**: son los lugares más sombríos de toda la Ópera. Contamos con 3 de ellos: norte, este y oeste, además de que las conexiones de los mismos, a diferencia del resto de estancias (exceptuando la sala de música), se hace mediante unas tuberías que solo son transitables en barcas. Como son canales muy estrechos solo hay una barca por tubería y si dicha barca se encuentra en el otro extremo de la conexión, será necesario activar una palanca para traerla a la orilla opuesta. El **Sótano Norte** conecta mediante barcas con los otros dos sótanos y con la **Sala de Música**, mientras que el **Sótano Oeste** conecta con el **Sótano Norte** y con la **Sala de Música**. Finalmente el **Sótano Este** solo está conectado mediante tuberías al **Sótano Norte**.

- **Sala de Música**:

- **Celda**:


Para facilitar la navegación y las conexiones entre las distintas áreas, hemos creado varias **zonas de navegación** como se puede apreciar en la imagen para controlar más fácilmente el moviemento de las entidades. Por ejemplo, la zona de las **tuberías** (marcada en la imagen con color rojo) es intransitable para todos los personajes y solo se puede pasar a través de ella utilizando las **barcas**. Esto se hace modificando en los **NavMeshAgent** las zonas por las que dichas entidades pueden moverse.

![image](https://user-images.githubusercontent.com/48771457/117317749-166e9d80-ae8a-11eb-9f47-addf808d99d9.png)

## 4. Implementación de **IA**
## 4.1 Bolt
## 4.2 Behaviour Designer
## 4.3 Scripts
## 5 Pruebas realizadas

## 5 Recursos de terceros empleados
- Pseudocódigo del libro: [**AI for Games, Third Edition**](https://ebookcentral.proquest.com/lib/universidadcomplutense-ebooks/detail.action?docID=5735527) de **Millington**

- Código de [**Federico Peinado**](https://github.com/federicopeinado) habilitado para la asignatura.

- [**Mixamo**](https://www.mixamo.com/) para las mallas y las animaciones empleadas como assets. 

- [**ProBuilder**](https://unity3d.com/es/unity/features/worldbuilding/probuilder) para el diseño de las salas del escenario.
