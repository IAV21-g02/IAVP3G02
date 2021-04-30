using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarcaComportamiento : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private Transform otherGameObject;
    public Transform Objectivo1;
    public Transform Objectivo2;
    //  True si la barca está en el objetivo 1 : false objetivo 2
    private bool Estado = true;
    private bool EnBarca = false;
    private bool reposo;

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }


    private void OnTriggerEnter(Collider other)
    {
        otherGameObject = other.gameObject.transform;
        if ( !EnBarca && other.CompareTag("Fantasma") || other.CompareTag("Vizconde")) 
        {
            EnBarca = true;
            mueveBarca(Estado);
        }
        else if (other.CompareTag("objetivoBarca1"))
        {
            Estado = true;
            EnBarca = false;
            otherGameObject.SetPositionAndRotation(otherGameObject.position + Objectivo1.forward * 5, Quaternion.identity);
            reposo = true;
        }
        else if (other.CompareTag("objetivoBarca2"))
        {
            Estado = false;
            EnBarca = false;
            otherGameObject.SetPositionAndRotation(otherGameObject.position + Objectivo2.forward * 5, Quaternion.identity);
            reposo = true;
        }
    }

    private void Update()
    {
        if (EnBarca)
        {
            otherGameObject.SetPositionAndRotation(transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        }
    }

    public void mueveBarca(bool status)
    {
        if (status)
        {
            navMesh.SetDestination(Objectivo2.position);
        }
        else
        {
            navMesh.SetDestination(Objectivo1.position);
        }
    }

    public bool barcaEnReposo()
    {
        return reposo;
    }
}
