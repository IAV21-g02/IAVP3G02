using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using System;
using UnityEngine.AI;
using Bolt;
using Ludiq;

public enum Estado : int
{
    BuscandoCantante, Secuestrando, TocandoMusica, RepararMuebles, Noqueado
};

public class Fantasma : MonoBehaviour
{
    public BehaviorTree behaviorTree;
    public int Estado = 0;
    public List<GameObject> path;

    private NavMeshAgent navMeshAgent;

    public GameObject[] barcas;
    public bool[] estado;
    private SalaBehaviour salaActual;
    private GameObject salaObj;

    // Start is called before the first frame update
    void Start()
    {
        behaviorTree.SetVariableValue("Estado", Estado);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Se guarda la sala en la que se entra
        if (collision.gameObject.GetComponent<SalaBehaviour>()) {
            salaActual = collision.gameObject.GetComponent<SalaBehaviour>();
        }    
    }

    public void BuscaCantante()
    {
        print("DONETE");
    }

    public void puedoIrCaminando(Transform pos)
    {
        behaviorTree.SetVariableValue("PuedoIrCaminando", navMeshAgent.CalculatePath(pos.position, navMeshAgent.path));
    }

    public void IrPalancaLuzCercana(Transform palancaEste, Transform palancaOeste)
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.SetDestination(palancaEste.position);
        float distancePalancaEste = navMeshAgent.remainingDistance;
        navMeshAgent.SetDestination(palancaOeste.position);
        float distancePalancaOeste = navMeshAgent.remainingDistance;
        if (distancePalancaEste <= distancePalancaOeste)
        {
            navMeshAgent.SetDestination(palancaEste.position);
        }
    }

    public void SetSalaObj(GameObject salaD) {
        salaObj = salaD;
    }

    public void EligeCamino() {

        GameObject[] salasVecinas = salaActual.ConsultaVecinos();
        path = new List<GameObject>();

        int i = 0;
        bool despeinado = false;
        while (!despeinado && i < salasVecinas.Length)
        {
            if (salasVecinas[i] == salaObj)
            {
                path.Add(salaActual.gameObject);
                despeinado = true;
            }
            i++;
        }

        for (i = 0; i < salasVecinas.Length; i++)
        {

        }
    }

    public void CalculaCamino(Transform pos)
    {
        if (salaActual.gameObject == salaObj) { 
            path
        }

    }

}
