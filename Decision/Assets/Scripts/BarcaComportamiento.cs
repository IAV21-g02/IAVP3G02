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
    public PalancaBarca palancaBarca1;
    public PalancaBarca palancaBarca2;

    private SalaBehaviour salaObjetivo1;
    private SalaBehaviour salaObjetivo2;



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
        salaObjetivo1 = Objetivo1.GetComponentInParent<SalaBehaviour>();
        salaObjetivo2 = Objetivo2.GetComponentInParent<SalaBehaviour>();
    }


    private void OnTriggerEnter(Collider other)
    {
        otherGameObject = other.gameObject.transform;
        if ( personaje == Character.None && other.CompareTag("Fantasma") || other.CompareTag("Vizconde")) 
        {
            currentCharacter = otherGameObject.gameObject;
            navMesh.enabled = true;

            if (other.CompareTag("Fantasma")) personaje = Character.Fantasma;
            else personaje = Character.Vizconde;

            otherGameObject.GetComponent<Rigidbody>().isKinematic = true;
            if (personaje == Character.Fantasma)
            {
                otherGameObject.GetComponent<NavMeshAgent>().enabled = false;
                otherGameObject.GetComponent<Fantasma>().setFantasmaEstado(5);
            }
            mueveBarca(Estado);
        }
        else if (other.CompareTag("objetivoBarca1")&& !Estado)
        {
            Estado = true;
            reposo = true;
            navMesh.enabled = false;

            //Soltamos personaje
            if (currentCharacter != null)
            {
                currentCharacter.transform.position = otherGameObject.position + Objetivo1.forward * 10;
                currentCharacter.GetComponent<Rigidbody>().isKinematic = false;
                if (personaje == Character.Fantasma)
                {
                    Fantasma fantasmita = currentCharacter.GetComponent<Fantasma>();
                    currentCharacter.GetComponent<NavMeshAgent>().enabled = true;
                    fantasmita.setFantasmaEstado(fantasmita.getEstadoAnterior());
                    fantasmita.SetSalaActual(salaObjetivo1);

                }
            }
            personaje = Character.None;
            currentCharacter = null;

        }
        else if (other.CompareTag("objetivoBarca2")&& Estado)
        {
            Estado = false;
            reposo = true;
            navMesh.enabled = false;

            //Soltamos personaje
            if (currentCharacter != null)
            {
                currentCharacter.transform.position = otherGameObject.position + Objetivo2.forward * 10;
                currentCharacter.GetComponent<Rigidbody>().isKinematic = false;
                if (personaje == Character.Fantasma)
                {
                    Fantasma fantasmita = currentCharacter.GetComponent<Fantasma>();
                    currentCharacter.GetComponent<NavMeshAgent>().enabled = true;
                    fantasmita.setFantasmaEstado(fantasmita.getEstadoAnterior());
                    fantasmita.SetSalaActual(salaObjetivo2);
                }
            }
            personaje = Character.None;
            currentCharacter = null;
        }
    }

    private void Update()
    {
        if (personaje != Character.None && currentCharacter != null )
        {
            currentCharacter.transform.SetPositionAndRotation(transform.position + new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    public void mueveBarca(bool status)
    {
        reposo = false;
        navMesh.enabled = true;

        if (status)
        {
            Estado = true;
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

    public PalancaBarca GetPalancaBarca1() { return palancaBarca1; }
    public PalancaBarca GetPalancaBarca2() { return palancaBarca2; }

    public Transform GetObjetivo1() { return Objetivo1; }
    public Transform GetObjetivo2() { return Objetivo2; }


}
