using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class conexion
{
    public GameObject barca;
    public GameObject salaVecina;
    //true -> Se puede ir desde esta sala a la sala Vecina
    public bool conectado;
}

public class SalaBehaviour : MonoBehaviour
{
    //  Barcas de la sala
    //public GameObject[] barcas;
    ////  Si es el estado es true -> la barca está en esta sala
    public bool[] conex;
    ////A que salas puede ir desde la sala actual
    //public GameObject[] salasVecinas;

    public conexion[] conexiones;

    private void Start()
    {

    }

    bool hayBarca(GameObject barca, int index)
    {
        return conex[index] == barca.GetComponent<BarcaComportamiento>().getEstadoBarca();
    }

    private void actualizaSala()
    {
        //Se actualizan las conexiones de la sala en función de 
        //si la barca está o no en el muelle de la sala
        for (int i = 0; i < conexiones.Length; i++)
        {
            conexiones[i].conectado = hayBarca(conexiones[i].barca, i);
        }
    }

    //Devuelve las salas vecinas accesibles de la sala actual
    public GameObject[] ConsultaVecinos (){
        actualizaSala();
        GameObject[] salasV = new GameObject[conexiones.Length];
        for (int i = 0; i < conexiones.Length; i++) {
            if (conexiones[i].conectado) {
                salasV[i] = conexiones[i].salaVecina;
            }
        }

        return salasV;
    }

    /// <summary>
    /// A qué tubería hay que ir
    /// </summary>
    /// <param name="conectado">
    /// Para saber si hay que llamar a la barca 
    /// o ya está disponible
    /// </param>
    /// <returns></returns>
    public GameObject BarcaDisponible (GameObject salaDest, out bool conectado){
        for (int i = 0; i < conexiones.Length; i++) {
            if (salaDest == conexiones[i].salaVecina) {
                conectado = conexiones[i].conectado;
                return conexiones[i].barca;
            }
        }

        conectado = false;
        return null;
    }
}
