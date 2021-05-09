using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.UI;
using BehaviorDesigner.Runtime;
using Bolt;
using Ludiq;

public enum Estado : int
{
    BuscandoCantante, Secuestrando, TocandoMusica, RepararMuebles, Noqueado, EnBarca, EnCamino
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
    public List<GameObject> path;
    public salaVisitada[] salas;
    public SalaBehaviour salaInicial;

    private NavMeshAgent navMeshAgent;
    private SalaBehaviour salaActual;
    private GameObject salaObj;
    private bool imposible;

    void Start()
    {
        behaviorTree.SetVariableValue("Estado", 0);
        navMeshAgent = GetComponent<NavMeshAgent>();
        salaActual = salaInicial;
    }

    public void OnCollisionEnter(Collision collision)
    {
        int estado = (int)behaviorTree.GetVariable("Estado").GetValue();
        if (collision.gameObject.CompareTag("Sala") || collision.gameObject.CompareTag("Escenario")) 
        {
            //Se guarda la sala en la que se entra
            if (collision.gameObject.GetComponent<SalaBehaviour>())
            {
                salaActual = collision.gameObject.GetComponent<SalaBehaviour>();
                behaviorTree.SetVariableValue("EstoyEnSalaBehaviour", true);
            }
            else behaviorTree.SetVariableValue("EstoyEnSalaBehaviour", false);
        }
        else if ( estado != (int)Estado.RepararMuebles && estado != (int) Estado.Noqueado
            && collision.gameObject.CompareTag("Cantante")) 
        {
            setFantasmaEstado(1);
            behaviorTree.SetVariableValue("Atrapada", true);
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            //TO DO hacer esto cuando al fantasma le rompen los muebles, cuando le noquean y cuando deja a la cantante en la celda
        }
    }

    public void puedoIrCaminando(Transform pos)
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            NavMeshPath path = new NavMeshPath();
            bool hayPath;
            NavMesh.CalculatePath(transform.position, pos.position, navMeshAgent.areaMask, path);

            //Debug.Log("Corners del path: " + path.corners.Length);

            if (path.corners.Length > 0 && (path.corners[path.corners.Length - 1] - pos.position).magnitude < 0.5) hayPath = true;
            //else if (path.corners.Length == 0) hayPath = true;
            else hayPath = false;

            behaviorTree.SetVariableValue("PuedoIrCaminando", hayPath);
        }
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

    public void EligeCamino(GameObject salaDestino)
    {
        //Reiniciamos antes de calcular el camino a seguir (Nodos de salas)
        reiniciaBusqueda();

        if (!salaDestino.GetComponent<SalaBehaviour>()) { Debug.Log("La liaste parda con las salas amigo"); return; }

        List<GameObject> path = new List<GameObject>();
        //Sala a la que queremos ir
        SetSalaObj(salaDestino);
        //Calculamos como llegar desde la sala en la que nos encontramos
        CalculaCamino(salaActual, path);

        bool sentido;
        GameObject objetoBarca;

        //El fantasma no puede salir de la sala sin pulsar un boton
        if (path.Count == 0)
        {
            objetoBarca = salaActual.BarcaDisponible(salaDestino, out sentido);
            if (!sentido && objetoBarca)
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
            if (sentido && navMeshAgent.isActiveAndEnabled)
            {
                BarcaComportamiento b = objetoBarca.GetComponent<BarcaComportamiento>();

                if (b.getEstadoBarca())navMeshAgent.SetDestination(b.GetObjetivo1().position);
                else navMeshAgent.SetDestination(b.GetObjetivo2().position);
                navMeshAgent.Resume();

                path.Remove(path[0]);

            }
            //else Debug.LogError("ESTO NO DEBERIA PASAR XD");
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

    public void liberaCantante()
    {
        Variables.Application.Set("Secuestrada", false);
        behaviorTree.SetVariableValue("Atrapada", false);
    }

    public void setFantasmaEstado(int nuEstado)
    {
        var estadoAnterior = behaviorTree.GetVariable("Estado");
        behaviorTree.SetVariableValue("EstadoAnterior", estadoAnterior);
        behaviorTree.SetVariableValue("Estado", nuEstado);
    }

    public void setFantasmaEstado(SharedInt nuEstado)
    {
        SharedInt newEstado = new SharedInt();
        newEstado.SetValue(nuEstado.Value);
        behaviorTree.SetVariableValue("EstadoAnterior", behaviorTree.GetVariable("Estado"));
        behaviorTree.SetVariableValue("Estado", newEstado);
    }

    public SharedInt getEstadoAnterior()
    {
        var estadoAnterior = behaviorTree.GetVariable("EstadoAnterior");
        return (SharedInt)estadoAnterior;
    }

    public void irAlSotanoCercano(GameObject sotanoEste, GameObject sotanoOeste)
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.SetDestination(sotanoEste.transform.position);
        float distancePalancaEste = navMeshAgent.remainingDistance;
        navMeshAgent.SetDestination(sotanoOeste.transform.position);
        float distancePalancaOeste = navMeshAgent.remainingDistance;

        if (distancePalancaEste <= distancePalancaOeste)
        {
            navMeshAgent.SetDestination(sotanoEste.transform.position);
            behaviorTree.SetVariableValue("GoToSala", sotanoEste);
        }
        else{
            navMeshAgent.SetDestination(sotanoOeste.transform.position);
            behaviorTree.SetVariableValue("GoToSala", sotanoOeste);

        }
    }

    public void setCantanteAtrapada()
    {
        behaviorTree.SetVariableValue("Atrapada", true);
    }

    public bool EstoyEnSala(GameObject sala)
    {
        Debug.Log(sala);
        if(salaActual != null && sala == salaActual.gameObject) return true;
        else  return false;
    }

    public void SetSalaActual(SalaBehaviour sala) {
        salaActual = sala;
    }
    
}
