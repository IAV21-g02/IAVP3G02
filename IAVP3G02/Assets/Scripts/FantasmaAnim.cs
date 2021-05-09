using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;


public class FantasmaAnim : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private BehaviorTree behaviorTree;

    private int estadoRegistrado;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();
        behaviorTree = GetComponentInParent<BehaviorTree>();
        estadoRegistrado = (int)behaviorTree.GetVariable("Estado").GetValue();
    }

    // Update is called once per frame
    void Update()
    {
        int estado = (int)behaviorTree.GetVariable("Estado").GetValue();
        if (estado == (int)Estado.BuscandoCantante)
        {
            animator.SetFloat("VelX", Mathf.Abs(agent.velocity.x));
            animator.SetFloat("VelZ", Mathf.Abs(agent.velocity.z));
        }

        if (estado == estadoRegistrado) return;

        estadoRegistrado = estado;

        switch (estado)
        {
            case (int)Estado.EnBarca:
                animator.SetTrigger("EnBarca");
                break;
            case (int)Estado.Noqueado:
                animator.SetTrigger("Noqueado");
                break;
            case (int)Estado.Secuestrando:
                animator.SetTrigger("Secuestro");
                break;
            default:
                break;
        }

        if ((bool)behaviorTree.GetVariable("TocandoMusica").GetValue())
            animator.SetTrigger("TocaPiano");
    }
}
