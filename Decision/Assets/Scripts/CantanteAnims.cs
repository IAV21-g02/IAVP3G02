using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CantanteAnims : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private NavMeshAgent nMA;
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
        if (nMA.velocity.x != 0 || nMA.velocity.z != 0)
        {
            anim.Play("caminar");
        }
        else
        {
            anim.Play("Baile");
        }
        anim.SetFloat("VelX", nMA.velocity.x);
        anim.SetFloat("VelZ", nMA.velocity.z);
    }
}
