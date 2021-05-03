using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaBarca : MonoBehaviour
{
    //  True si la barca está en el objetivo 1 : false objetivo 2
    public bool Estado;
    public BarcaComportamiento bC;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if ( bC.barcaEnReposo() && other.CompareTag("Fantasma") || other.CompareTag("Vizconde") && bC.getEstadoBarca() == Estado)
    //    {
    //        bC.mueveBarca(Estado);
    //    }
    //}

    public void TraeBarca()
    {
        if (bC.barcaEnReposo() && bC.getEstadoBarca() == Estado)
        {
            bC.mueveBarca(Estado);
        }
    }
}
