using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;


public class CantanteAnims : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private NavMeshAgent nMA;
    public BehaviorTree behaviorTree;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
        nMA = GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        int estado = (int)behaviorTree.GetVariable("Estado").GetValue();

        bool atrapada = (bool)behaviorTree.GetVariable("Atrapada").GetValue();

        if (atrapada)
        {
            anim.Play("EnBrazos");
        }

        if (nMA.velocity.x != 0 || nMA.velocity.z != 0)
        {
            anim.Play("caminar");
        }
        else if(!atrapada)
        {
            anim.Play("Baile");
        }
        //anim.SetFloat("VelX", nMA.velocity.x);
        //anim.SetFloat("VelZ", nMA.velocity.z);
        anim.SetFloat("VelX", Mathf.Abs(nMA.velocity.x));
        anim.SetFloat("VelZ", Mathf.Abs(nMA.velocity.z));
    }
}
