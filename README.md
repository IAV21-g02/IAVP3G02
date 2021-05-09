
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

- **Barcas**: son los objetos necesarios para atravesar las tuberías y acceder a las zonas más inhóspitas de la **Ópera**. 

- **Piano y muebles**: son objetos interactables por el **Vizconde** de modo que si los golpea hará enfadar al **Fantasma** para que deje todo lo que esté haciendo y así atraer su atención, puesto que éste irá rápidamente a arreglar sus objetos más preciados.

- **Palancas**: existen dos tipos de palancas: **Palancas Barcas** y **Palancas Lámparas**.
    - **Palancas Barcas**: se usan para llamar a la barca (como si fuera un ascensor) en caso de que no se encuentre disponible en ese momento. 
    - **Palancas Lámparas**: las usa el **Fantasma** para dejar al **Público** sin luz, dado que es muy tímido y no quiere que nadie lo vea, de manera que al activar alguna de las palancas, la lámpara correspondiente se precipitará haciendo que el **Público** corra despavorido.  

- **Lámparas**: son los objetos que se usan para que el **Público** respire tranquilo y no tema encontrarse en sus asientos disfrutando del espectáculo dado por la cantante. En caso de que se active su palanca correspondiente, la lámpara caerá, haciendo que el **Público** huya a una zona segura. Si el **Vizconde** se acerca a alguna de lámparas caídas e interactúa con ella, la recolocará, pero el **Público** no volverá a sus astientos hasta que estén todas las lámparas en sus sitio.

### 2.3 Escenario

![image](https://user-images.githubusercontent.com/48771457/117317749-166e9d80-ae8a-11eb-9f47-addf808d99d9.png)

La acción transcurre en la **Ópera**, la cual se divide en las siguientes secciones: 
- **Véstibulo**: adyacente al **Palco de Butacas**, es la sala a la que se desplazarán el público desde el **Palco de Butacas** cuando alguna de las lámparas se haya caido.

- **Palco de Butacas**: conectado con el **Véstibulo** y el **Escenario**. Es la sala en la que de forma general se encontrará el público y sobre la cual caen las lámparas al activar las palancas de los **Palcos**.

- **Escenario**: Lugar en el que comienza cantando nuestra bella diva y que cuenta con conexiones directas a los **Palcos Este y Oeste**, al **Patio de Butacas** y a las **Bambalinas**. Será el primer lugar en el que el fantasma trate de buscar a la cantante puesto que pasa la mayoría de su tiempo en esta estancia. Esta zona será intransitable para el fantasma si hay alguna persona que se encuentre en el **Palco de Butacas**, por lo que tendrá que asustarles antes de pasar por este lugar.

- **Bambalinas**: lugar de reposo durante los descansos de la cantante. Está conectado con los **Sótanos Este y Oeste** además de con el **Escenario**. Es la siguiente estancia en la que el fantasma tratará de buscar a la diva.

- **Palcos Este y Oeste**: zonas en las que se encuentran las **Palancas** que propician la caida de las **Lámparas** que se encuentran en el Palco de Butacas. El **Palco Oeste** es la habitación de origen del Vizconde y cuenta con conexiones al **Sótano Oeste** y al **Escenario. Por otro lado, el **Palco Este** cuenta con conexiones directas al **Escenario** y al **Sótano Este**.

- **Sótanos**: son los lugares más sombríos de toda la Ópera. Contamos con 3 de ellos: norte, este y oeste, además de que las conexiones de los mismos, a diferencia del resto de estancias (exceptuando la sala de música), se hace mediante unas tuberías que solo son transitables en barcas. Como son canales muy estrechos solo hay una barca por tubería y si dicha barca se encuentra en el otro extremo de la conexión, será necesario activar una palanca para traerla a la orilla opuesta. El **Sótano Norte** conecta mediante barcas con los otros dos sótanos y con la **Sala de Música**, mientras que el **Sótano Oeste** conecta con el **Sótano Norte** y con la **Sala de Música**. Finalmente el **Sótano Este** solo está conectado mediante tuberías al **Sótano Norte**.

- **Sala de Música**: se trata del lugar de descanso del **Fantasma**, su habitación privada. Es allí donde maquina sus malévolos planes para secuestrar a la cantante y donde le gusta probar su música. Es una sala muy especial, por lo que el más mínimo inidicio de actividad en esta habitación le causará estragos, teniendo que volver inmediatamente a ella. Posee conexiones con el **Sótano Este** y la **Sala Norte**.

- **Sala Norte (incluye Celda)**: es el rincón de la Ópera donde el fantasma lleva a la cantante para que no se escape y que así solamente el pueda disfrutar sus bailes y sus cantos. Esta sala tiene a su vez un pequeño zulo que usa a modo de **Celda**. Está conectada mediante tuberías con la **Sala de Música**, **Sótano Oeste** y **Sótano Este**


Para facilitar la navegación y las conexiones entre las distintas áreas, hemos creado varias **zonas de navegación** como se puede apreciar en la imagen para controlar más fácilmente el movimiento de las entidades. Por ejemplo, la zona de las **tuberías** (marcada en la imagen con color rojo) es intransitable para todos los personajes y solo se puede pasar a través de ella utilizando las **barcas**. Esto se hace modificando en los **NavMeshAgent** las zonas por las que dichas entidades pueden moverse.

![image](https://user-images.githubusercontent.com/48771457/117318697-f4294f80-ae8a-11eb-92fb-e63c9ebf7e75.png)

## 4. Implementación de **IA**
### 4.1 Bolt
Esta herramienta se ha usado para dotar de inteligencia a los siguientes agentes: **Cantante** y **Público**, aunque también se usan algunos **Scripts** para mejorar su comportamiento.

#### 4.1.1 **Público**
El público se va a mover entre tres estados básicos: **Sentado**, **Corriendo** y **EnVestíbulo**. Mientras está en sentado estará pendiente sobre la caída de las lámparas, de manera que cuando caiga alguna, correrá hacia el vestíbulo (durante su estado _corriendo_). Una vez llegue allí, pasará a estar en el estado *EnVestíbulo* que, al igual que en el estado sentado, esperará hasta que todas las lámparas estén en su sitio, en cuyo caso volverá a pasar a su estado _corriendo_, para dirigirse hacia su asiento correspondiente y pasar a estado sentado de nuevo. También, durante el estado _corriendo_ estará pendiente sobre qué pasa con las lámparas, ya que antes de que llegue a su objetivo acutal las lámparas se podrían recolocar o volver a caer, de manera que cambiaría la dirección de su objetivo.

![image](https://user-images.githubusercontent.com/47497948/117544536-494b9980-b022-11eb-8209-16b6f468464e.png)

#### 4.1.2 **Cantante**
La cantante es más compleja que el público. Ésta posee más estados: **CantandoEscenario**, **Secuestrada**, **Perdida**, **Encarcelada**, **Siguiendo**.

- **CantandoEscenario**: mientras se encuentre en este estado estará moviéndose entre las bambalinas y el escenario. Cuando se encuentre en alguno de los dos lugares procerá de la misma manera, bailará durante un tiempo (escogido de forma aleatorio) y luego cambiará de zona. Este estado se verá alterado por el fantasma en caso de que le secuestre. 

- **Secuestrada**: cuando el **Fantasma** consiga echarle mano a la cantante, ésta pasará a estar secuestrada, de forma que será transportada por el **Fantasma** hasta que pueda cambiar de estado, es decir, cuando llegue a la celda, cuando el **Vizconde** noquee al **Fantasma** o cuando el **Vizconde** rompa los objetos del **Fantasma**.

- **Perdida**: en caso de que la cantante se encuentre en una zona desconocida y no estñe secuestrada ni esté siguiendo al vizconde, pasará a estar perdida, de forma que comienza a deambular por los alrededores intentado encontrar alguna zona conocida.

- **Encarcelada**: la cantante estará esperando a ser rescatada de la celda, de manera que, para no perder el tiempo y por órdenes del fantasma, no dejará de bailar y practicar su espéctaculo. La única forma de salir de este estado es que el **Vizconde** abra la puerta de la celda y se la lleve.

- **Siguiendo**: cuando la cantante se encuentre fuera del escenario o las bambalinas, es decir, cuando se encuentre perdida, podrá pasar a seguir al **Vizconde** cuando entre en su campo de visión.

![image](https://user-images.githubusercontent.com/47497948/117328697-2a1f0180-ae94-11eb-9a04-c9b692464674.png)

### 4.2 Behaviour Designer - Fantasma
Esta herramienta está siendo usada para generar la inteligencia del **Fantasma** aunque también se usan **Scripts** de apoyo para mejorar su comportamiento.

Según como se muestra en la siguiente imagen, los estados del **Fantasma** se irán procesando según su prioridad (ramas izquierdas más prioritarias).

Para empezar tenemos la acción **Entry** usada para entrar en el árbol de comportamiento, seguida de **Repeater** usada para estar preguntando en cada tick en qué estado debe estar. Finalmente se tiene un **Selector** utilizado para tomar decisiones sobre qué estado se debería procesar; en caso de que el estado de la izquierda no se pueda usar pasará de forma automática a procesar eL siguiente, el de la derecha.

![image](https://user-images.githubusercontent.com/47497948/117545286-5f0e8e00-b025-11eb-817d-63fc50a5ad6c.png)

A continuación se explicarán las diversas ramas de forma ordenada de izquierda a derecha:


- **1 - Navegando**:  en primer lugar, se comprueba si el fantasma está subido en alguna barca, dado que durante este estado no debería poder hacer ninguna otra acción más que esperar a llegar al destino de la barca.

- **2 - Noqueado**: si no está navegando, se pregunta si el fantasma está noqueado, puesto que es el estado más prioritario, ya que inmoviliza por completo al fantasma durante un tiempo.

- **3 - Muebles rotos**: por otro lado, tenemos los muebles rotos, dado que el **Fantasma** debería dejar todo lo que está haciendo para ir a comprobar sus objetos personales. Esta secuencia se encarga de asigar como objetivo la **Sala de Música**.

- **4 - CantanteEncontrada**: en caso de que el **Fantasma** esté buscando a la cantante y la vea (entre en su campo de visión) irá a por ella tan rápido como sea posible para secuestrarla.

- **5 - CaminarHaciaCantante**: si la cantante no está en el rango de visión, entonces habrá que buscarla de manera que habrá que ir a comprobar si está en el escenario o en las bambalinas, de manera que el **Fantasma** se preguntará si puede ir caminando; si es así, tomará los comportamientos designados en esta secuencia.

- **6 - IrEnBarcaCantante**: si se ha llegado a este estado, quiere decir que no se puede ir caminando a por la cantante, por lo que habrá que coger las barcas, es decir, este estado determina cuál sería la mejor ruta de barcas a tomar para ir, posteriormente, caminando a por la cantante.

- **7 - LlevarCantanteCeldaCaminando**: cuando el **Fantasma** tenga a la cantante entre sus brazos se la llevará a la celda, de manera que, al igual que antes, se preguntará si puede ir caminando; si es así, tomará los comportamientos designados en esta secuencia.

- **8 - LlevarCantanteCeldaBarca**: en caso de que no pueda acceder a la celda caminando, entonces tomará la mejor ruta en barca hasta que pueda ir caminando hacia la celda.

- **9 - TocarPiano**: cuando el **Fantasma** consiga, por fin, su objetivo se dirigirá a la sala de música para tocar el piano y disfrutar de su victoria. Dentro de esta secuencia determinará si puede ir caminando o ha de coger barcas, pero, en cualquier caso, se moverá hasta la sala de música.

### 4.3 Scripts
- Relacionados con el fantasma:

    - **Fantasma.cs**: script asoaciado al **Fantasma** que se encarga de gestionar y mejorar su comportamiento. Dentro de **Behaviour designer** se usan diversas funcionalidades de este script tales como: buscar caminos, buscar la mejor barca, gestión de colisiones (**__OnCollision y OnTrigger__**), secuestrar y soltar y a la cantante, entre otros.
    - **FantasmaAnim.cs**: gestiona las animaciones del fantasma en función de su estado.
    - **PianoBehaviour.cs**: detecta la colisión con el fantasma de manera que provoca que éste cambie de estado y pueda tocarlo. Además, gestiona el input en caso de que esté colisionando con el **Vizconde** para que lo pueda romper.

-   Relacionados con las barcas:
    - **BarcaComportamiento.cs**: gestiona el comportamiento de las barcas determinando quién está subido en la barca (**Fanstasma** o **Vizconde**), así como transportar por su tubería correspondente a cualquiera de los usuarios.
    - **PalancaBarca.cs**: Llama a la barca de la tubería correspondiente en caso de que se encuentre alojado en el otro extremo del tunel.

- Relacionados con la celda:
    - **CeldaBehaviour.cs**: para conseguir que el fantasma suelte a la cantante dentro de la celda.
    - **PalancaCeldaBehaviour.cs**: se usa para abrir la celda cuando la cantante esté encarcelada.

- Relacionados con el escenario:
    - **PalancaLampara.cs**: se usa para que el fantasma pueda tirar la lámpara asociada a la palanca para que el público huya despavorido.
    - **LuzEscenario.cs**: se usa para crear un ambiente fiestero con las luces de la ópera, alternando su rotación.
    - **PublicoContador.cs**: se encarga de contar el número de personas que se encuentrar dentro del vestíbulo, para así manejar con esos datos el comportamiento del fantasma que decide si atravesar el escenario.

- Relacionados con la cantante:
    - **SingerVision.cs**: determina si el Vizconde está en su rango de detección o no, avisando a los nodos de comportamientos construidos con la heramienta **Bolt** anteriormente mencionada para que proceda commo deba.
    - **CantanteAnims.cs**: gestiona las animaciones de la cantante en función de su estado.

- Relacionados con el Vizconde:
    - **VizcondeAnim.cs**: gestiona las animaciones del Vizconde en función de su estado.
    - **VizcondeBehaviour.cs**: gestiona el comportamiento por input del Vizconde, es decir, el movimiento, la gestión de cámaras y el botón para interactuar con el entorno.


## 5 Pruebas realizadas

### 5.1 Muebles rotos

- **Cantante secuestrada**: En este vídeo se observa el comportamiendo del **Fantasma** cuando le rompen los muebles y ha secuestrado a la cantante.
[![image](https://user-images.githubusercontent.com/47497948/117545761-72225d80-b027-11eb-92e3-9f4f19884582.png)](https://www.youtube.com/watch?v=XLPWkCgSnvk&ab_channel=IA_UCM_Tester)

- **Cantante en libertad**:  En este vídeo se observa el comportamiendo del **Fantasma** cuando le rompen los muebles y la cantante no ha sido secuestrada todavía.
[![image](https://user-images.githubusercontent.com/47497948/117545818-b7df2600-b027-11eb-9420-17f9ac79bdfb.png)
](https://www.youtube.com/watch?v=QmOxamvzS28&ab_channel=IA_UCM_Tester)

### 5.2 Recorrido completo del fantasma
En este vídeo se muestra un recorrido completo del **Fantasma**, es decir, desde el momento en que empieza la ejecución y decide ir a buscarla, hasta que la secuestra, la lleva a la celda y se va a tocar música para celebrar su victoria.

[![image](https://user-images.githubusercontent.com/47497948/117546984-70f42f00-b02d-11eb-8be4-fe4fc213d172.png)](https://www.youtube.com/watch?v=Uprv-nw8kYE&ab_channel=IA_UCM_Tester)
## 6 Recursos de terceros empleados
- Pseudocódigo del libro: [**AI for Games, Third Edition**](https://ebookcentral.proquest.com/lib/universidadcomplutense-ebooks/detail.action?docID=5735527) de **Millington**

- Código de [**Federico Peinado**](https://github.com/federicopeinado) habilitado para la asignatura.

- [**Mixamo**](https://www.mixamo.com/) para las mallas y las animaciones empleadas como assets. 

- [**ProBuilder**](https://unity3d.com/es/unity/features/worldbuilding/probuilder) para el diseño de las salas del escenario.

- [**Bolt**](https://unity.com/es/products/unity-visual-scripting) y [**Behaviour Designer**](https://opsive.com/assets/behavior-designer/) para inteligencia artificial en **Unity**
