using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;

public class SingerVision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vizconde")
        {
            Variables.Application.Set("VizcondeEnRango", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Vizconde")
        {
            Variables.Application.Set("VizcondeEnRango", false);
        }
    }

}
