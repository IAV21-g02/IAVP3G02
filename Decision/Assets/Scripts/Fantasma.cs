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

    private NavMeshAgent navMeshAgent;

    public GameObject[] barcas;
    public bool[] estado;

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


    public void actualizaBarcas(barcasSala[] newBarcas)
    {
        for (int i = 0; i < newBarcas.Length; i++)
        {
            for (int j = 0; j < barcas.Length; j++)
            {
                if (barcas[j].Equals(newBarcas[i].barca))
                {
                    estado[j] = newBarcas[i].estado;
                }
            }
        }
    }

}
