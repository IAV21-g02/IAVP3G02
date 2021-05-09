using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;

public class PalancaCeldaBehaviour : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fantasma")
        {
            //Cambio de valor a la variable de BOLT 
            Variables.Application.Set("Encarcelada", true);
        }
        else if(other.gameObject.tag == "Vizconde")
        {
            Debug.Log("Desactivando Celda");
            //Cambio de valor a la variable de BOLT 
            Variables.Application.Set("Encarcelada", false);
        }
    }
}
