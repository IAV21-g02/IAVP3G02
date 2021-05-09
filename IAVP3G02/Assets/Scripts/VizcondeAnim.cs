using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VizcondeAnim : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x != 0 || rb.velocity.z != 0)
        {
            anim.Play("Running");
            anim.SetBool("IdleUp", false);
        }
        else if(!anim.GetBool("IdleUp"))
        {
            anim.SetTrigger("Stop");
            anim.SetBool("IdleUp",true);
        }
        anim.SetFloat("VelX", rb.velocity.x);
        anim.SetFloat("VelZ", rb.velocity.z);
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.Play("Dance");
        }
    }
}
