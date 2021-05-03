using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;

public class PalancaLampara : MonoBehaviour
{
    public GameObject lampara;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fantasma"))
        {
            lampara.GetComponent<Rigidbody>().useGravity = true;
            int numLamparas = (int)Variables.Application.Get("LamparasCaidas");
            numLamparas++;
            Variables.Application.Set("LamparasCaidas", numLamparas);
        }
    }
}
