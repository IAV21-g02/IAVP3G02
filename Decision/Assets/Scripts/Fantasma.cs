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

[System.Serializable]
public class salaVisitada
{
    public GameObject sala;
    public bool visitado;
}

public class Fantasma : MonoBehaviour
{
    public BehaviorTree behaviorTree;
    public int Estado = 0;
    public List<GameObject> path;
    public salaVisitada[] salas;

    private NavMeshAgent navMeshAgent;
    private SalaBehaviour salaActual;
    private GameObject salaObj;
    private bool imposible;
 

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

    public List<GameObject> CalculaCamino( bool imposible, SalaBehaviour salaAct , List<GameObject> path)
    {
        //caso base
        if (salaAct .gameObject == salaObj || imposible == true) return path;
        marcarVisitado(salaAct.gameObject, true);
            GameObject[] salasVecinas = salaAct.ConsultaVecinos();
            int i = 0;
            bool haycamino = false;
            //comprovamos si podemos ir al destino
            while (!haycamino && i < salasVecinas.Length)
            {
                if (salasVecinas[i] == salaObj)
                {
                    path.Add(salaAct.gameObject);
                    haycamino = true;
                    return path;
                }
                i++;
            }
            //si no hay camino directo al objetivo

            for (i = 0; i < salasVecinas.Length; i++)
            {
                if (!isVisitado(salasVecinas[i]))
                {
                SalaBehaviour s = salasVecinas[i].GetComponent<SalaBehaviour>();
                   path.Add(s.gameObject);
                   path = CalculaCamino(imposible, s, path);
                    
                    //sacar la sala del path
                    path.Remove(salaActual.gameObject);
                    //devolvemos los valores originales
                    salaActual = auxsala;
                }

            }
        
           
        

    }

    private salaVisitada getSala(GameObject sala)
    {
        for(int i =0; i < salas.Length; i++)
        {
            if (salas[i].sala == sala) return salas[i];
        }
        return null;
    }
    private void marcarVisitado(GameObject sala, bool visit)
    {
        salaVisitada s = getSala(sala);
        s.visitado = visit;
        
    }
    private bool isVisitado(GameObject sala)
    {
        return getSala(sala).visitado;
    }

    private void reiniciaBusqueda()
    {
        for (int i = 0; i < salas.Length; i++)
        {
            salas[i].visitado = false;
        }
        
    }

}
