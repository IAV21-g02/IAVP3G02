using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct barcasSala{
    public GameObject barca;
    public bool estado;
}

public class SalaBehaviour : MonoBehaviour
{
    //  Barcas de la sala
    public GameObject[] barcas;
    //  Si es el estado es true -> la barca está en esta sala
    public bool[] estado;
    public bool[] estadoActivoBarca;
    //  Objetivos a los que las barcas se mueven desde esta sala
    public GameObject[] objetivosActivos;


    bool hayBarca(GameObject barca,int index)
    {
        if (estadoActivoBarca[index] == barca.GetComponent<BarcaComportamiento>().getEstadoBarca()) return true;
        else return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fantasma"))
        {
            barcasSala[] barcasInfo = new barcasSala[barcas.Length];
            for (int i = 0; i < barcas.Length; i++)
            {
                barcasInfo[i].barca = barcas[i];
                barcasInfo[i].estado = estado[i];
            }
            collision.gameObject.GetComponent<Fantasma>().actualizaBarcas(barcasInfo);
        }
    }

    public void actualizaSala(GameObject barca)
    {
        for (int i = 0; i < barcas.Length; i++)
        {
            if (barcas[i].Equals(barca))
            {
                estado[i] = true;
                return;
            }
        }
    }

    public barcasSala[] getBarcasSala()
    {
        barcasSala[] actBarcas = new barcasSala[barcas.Length];
        for (int i = 0; i < barcas.Length; i++)
        {
            actBarcas[i].barca = barcas[i];
            actBarcas[i].estado = estado[i];
        }
        return actBarcas;
    }
}
