using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicoAnimControl : MonoBehaviour
{
    Animator animator;
    Vector3 posIni;
    public Transform objetivo;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        posIni = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //bool fire = Input.GetButtonDown("Fire1");
        //
        //animator.SetFloat("Forward", v);
        //animator.SetFloat("Strafe", h);
        //animator.SetBool("Fire", fire);

        if (animator.GetBool("Sentada") && (transform.position.x > posIni.x + 0.1 || transform.position.x < posIni.x - 0.1)) {
            animator.SetBool("Correr", true);
            animator.SetBool("Sentada", false);
        }
        else if (animator.GetBool("Correr") && (transform.position.x < posIni.x + 0.1 && transform.position.x > posIni.x - 0.1))
        {
            animator.SetBool("Sentada", true);
            animator.SetBool("Correr", false);
        }
        else if (animator.GetBool("Correr") && (transform.position.x < objetivo.position.x + 0.5 && transform.position.x > objetivo.position.x - 0.5))
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Correr", false);
        }
        else if (animator.GetBool("Idle") && (transform.position.x > objetivo.position.x + 0.5 || transform.position.x < objetivo.position.x - 0.5))
        {
            animator.SetBool("Correr", true);
            animator.SetBool("Idle", false);
        }
    }

    //void OnTriggerEnter(Collider col)
    //{
    //    if (animator.GetBool("Correr") && col.gameObject.CompareTag("objetivo"))
    //    {
    //        animator.SetBool("Idle", true);
    //        animator.SetBool("Correr", false);
    //    }
    //}
    //
    //void OnTriggerEnd(Collider col)
    //{
    //    if (animator.GetBool("Idle") && col.gameObject.CompareTag("objetivo"))
    //    {
    //        animator.SetBool("Correr", true);
    //        animator.SetBool("Idle", false);
    //    }
    //}
}
