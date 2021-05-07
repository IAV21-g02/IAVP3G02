using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;


public class PianoBehaviour : MonoBehaviour
{

    public BehaviorTree behaviorTree;
    private bool pianoDestruido = false;
    private Fantasma fantasma;

    private void Start()
    {
        fantasma = behaviorTree.gameObject.GetComponent<Fantasma>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fantasma") && (int)behaviorTree.GetVariable("Estado").GetValue() == 2)
        {
            behaviorTree.SetVariableValue("TocandoMusica", true);
        }
    }

    public void CambiaEstadoPiano(bool destruido)
    {
        pianoDestruido = destruido;

        if (pianoDestruido) Debug.Log("Piano destruido");
        else Debug.Log("Piano reparado");

        behaviorTree.SetVariableValue("MueblesRotos", pianoDestruido);
    }

    public void OnTriggerStay(Collider collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.CompareTag("Vizconde") && !pianoDestruido)
        {
            CambiaEstadoPiano(true);
            int estadoFantasma = (int)behaviorTree.GetVariable("Estado").GetValue();

            //El fantasma se encuentra en una barca
            if (estadoFantasma == 5)
                behaviorTree.SetVariableValue("EstadoAnterior", (int)Estado.RepararMuebles);
            //El fantasma se encuentra haciendo otra cosa
            else
                behaviorTree.SetVariableValue("Estado", (int)Estado.RepararMuebles);
            
        }
        else if (collision.gameObject.CompareTag("Fantasma") && pianoDestruido)
        {
            CambiaEstadoPiano(false);
            //Si el fantasma arregla el piano se vuelve a buscar a la cantante
            behaviorTree.SetVariableValue("Estado", (int)Estado.BuscandoCantante);

        }
    }
}
