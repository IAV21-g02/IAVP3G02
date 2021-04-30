using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaBarca : MonoBehaviour
{
    //  True si la barca está en el objetivo 1 : false objetivo 2
    public bool Estado;
    public BarcaComportamiento bC;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if ( bC.barcaEnReposo() && other.CompareTag("Fantasma") || other.CompareTag("Vizconde"))
        {
            bC.mueveBarca(Estado);
        }
    }
}
