using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarcaComportamiento : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private Transform otherGameObject;
    public Transform Objetivo1;
    public Transform Objetivo2;
    //  True si la barca está en el objetivo 1 : false objetivo 2
    private bool Estado = true;
    private bool reposo;
    private Character personaje = Character.None;
    private GameObject currentCharacter = null;

    enum Character
    {
        Fantasma, Vizconde, None
    }

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }


    private void OnTriggerEnter(Collider other)
    {
        otherGameObject = other.gameObject.transform;
        if ( personaje == Character.None && other.CompareTag("Fantasma") || other.CompareTag("Vizconde")) 
        {
            Debug.Log("Se sube personaje");
            currentCharacter = otherGameObject.gameObject;
            navMesh.enabled = true;

            if (other.CompareTag("Fantasma")) personaje = Character.Fantasma;
            else personaje = Character.Vizconde;

            otherGameObject.GetComponent<Rigidbody>().isKinematic = true;
            if(personaje == Character.Fantasma) otherGameObject.GetComponent<NavMeshAgent>().enabled = false;
            
            mueveBarca(Estado);
        }
        else if (other.CompareTag("objetivoBarca1")&& !Estado)
        {
            Debug.Log("OBEJTIVO 1");

            Estado = true;
            reposo = true;
            navMesh.enabled = false;

            //Soltamos personaje
            if (currentCharacter != null)
            {
                Debug.Log("Dejamos personaje");
                currentCharacter.transform.position = otherGameObject.position + Objetivo1.forward * 10;
                //currentCharacter.transform.SetPositionAndRotation(otherGameObject.position + Objectivo2.forward * 10, Quaternion.identity);
                currentCharacter.GetComponent<Rigidbody>().isKinematic = false;
                if (personaje == Character.Fantasma) currentCharacter.GetComponent<NavMeshAgent>().enabled = true;
            }
            personaje = Character.None;
            currentCharacter = null;

        }
        else if (other.CompareTag("objetivoBarca2")&& Estado)
        {
            Estado = false;
            reposo = true;
            navMesh.enabled = false;

            Debug.Log("OBEJTIVO 2");
            //Soltamos personaje
            if (currentCharacter != null)
            {
                Debug.Log("Dejamos personaje");
                currentCharacter.transform.position = otherGameObject.position + Objetivo2.forward * 10;
                //currentCharacter.transform.SetPositionAndRotation(, Quaternion.identity);
                currentCharacter.GetComponent<Rigidbody>().isKinematic = false;
                if (personaje == Character.Fantasma) currentCharacter.GetComponent<NavMeshAgent>().enabled = true;
            }
            personaje = Character.None;
            currentCharacter = null;
        }
    }

    private void Update()
    {
        if (personaje != Character.None && currentCharacter != null )
        {
            currentCharacter.transform.SetPositionAndRotation(transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        }
    }

    public void mueveBarca(bool status)
    {
        Debug.Log("Llamada a mueveBarca");
        reposo = false;
        navMesh.enabled = true;

        if (status)
        {
            Estado = true;
            Debug.Log(Objetivo2.position);
            navMesh.SetDestination(Objetivo2.position);
        }
        else
        {
            Estado = false;
            Debug.Log(Objetivo1.position);
            navMesh.SetDestination(Objetivo1.position);
        }
    }

    public bool getEstadoBarca()
    {
        return Estado;
    }

    public bool barcaEnReposo()
    {
        return reposo;
    }
}
