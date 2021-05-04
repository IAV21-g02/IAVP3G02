using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct barcasSala{
    public GameObject barca;
    public bool estado;
}

public class SalaBehaviour : MonoBehaviour
{
    public GameObject[] barcas;
    public bool[] estado;

    public GameObject[] objetivos;
    public bool[] estadoActivoBarca;
    public string navNombreSala;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
