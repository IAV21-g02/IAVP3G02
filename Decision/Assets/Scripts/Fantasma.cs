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
        if (collision.gameObject.GetComponent<SalaBehaviour>())
        {
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
        else navMeshAgent.SetDestination(palancaOeste.position);
    }

    public void SetSalaObj(GameObject salaD)
    {
        salaObj = salaD;
    }

    public void EligeCamino(SalaBehaviour salaDestino)
    {
        //Reiniciamos antes de calcular el camino a seguir (Nodos de salas)
        reiniciaBusqueda();

        if (salaActual == null) { Debug.Log("La liaste parda con las salas amigo"); return; }

        List<GameObject> path = new List<GameObject>();
        //Sala a la que queremos ir
        SetSalaObj(salaDestino.gameObject);
        //Calculamos como llegar desde la sala en la que nos encontramos
        CalculaCamino(salaActual, path);

        bool sentido;
        GameObject objetoBarca;

        //El fantasma no puede salir de la sala sin pulsar un boton
        if (path.Count == 0)
        {
            objetoBarca = salaActual.BarcaDisponible(salaDestino.gameObject, out sentido);
            if (!sentido)
            {
                BarcaComportamiento b = objetoBarca.GetComponent<BarcaComportamiento>();

                if (b.getEstadoBarca())
                {
                    b.GetPalancaBarca2().TraeBarca();
                    navMeshAgent.SetDestination(b.GetObjetivo2().position);
                }
                else
                {
                    b.GetPalancaBarca1().TraeBarca();
                    navMeshAgent.SetDestination(b.GetObjetivo1().position);
                }
            }
            else Debug.LogError("ESTO NO DEBERIA PASAR XD");

        }
        //El fantasma puede seguir un camino para llegar al objetivo
        //TODO: jajaja seguir pillando los elementos del path :3
        else
        {
            objetoBarca = path[0].GetComponent<SalaBehaviour>().BarcaDisponible(salaDestino.gameObject, out sentido);
            if (sentido)
            {
                BarcaComportamiento b = objetoBarca.GetComponent<BarcaComportamiento>();

                if (b.getEstadoBarca())navMeshAgent.SetDestination(b.GetObjetivo2().position);
                else navMeshAgent.SetDestination(b.GetObjetivo1().position);

                path.Remove(path[0]);

            }
            else Debug.LogError("ESTO NO DEBERIA PASAR XD");
        }

    }

    public List<GameObject> CalculaCamino(SalaBehaviour salaAct, List<GameObject> path)
    {
        //caso base
        if (salaAct.gameObject == salaObj) return path;
        marcarVisitado(salaAct.gameObject, true);
        GameObject[] salasVecinas = salaAct.ConsultaVecinos();
        int i = 0;
        bool haycamino = false;
        //comprobamos si podemos ir al destino
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
                List<GameObject> pathAux = CalculaCamino(s, path);

                if (pathAux.Count == path.Count) path.Remove(s.gameObject);
            }
        }

        return path;

    }

    private salaVisitada getSala(GameObject sala)
    {
        for (int i = 0; i < salas.Length; i++)
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
