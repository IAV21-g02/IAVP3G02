using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicoAnimControl : MonoBehaviour
{
    Animator animator;
    Vector3 posIni;
    Vector3 diff;
    public Transform objetivo;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        posIni = transform.position;
        diff = -transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Sentada") && (transform.position.x > posIni.x + 0.1 || transform.position.x < posIni.x - 0.1)) {
            transform.localPosition += diff;
            //transform.position += diff;
            animator.SetBool("Correr", true);
            animator.SetBool("Sentada", false);
        }
        else if (animator.GetBool("Correr") && (transform.position.x < posIni.x + 0.1 && transform.position.x > posIni.x - 0.1))
        {
            transform.localPosition -= diff;
            animator.SetBool("Sentada", true);
            animator.SetBool("Correr", false);
        }
        else if (animator.GetBool("Correr") && (transform.position.x < objetivo.position.x + 0.5 && transform.position.x > objetivo.position.x - 0.5))
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Correr", false);
        }
        else if (animator.GetBool("Idle") && (transform.position.x > objetivo.position.x + 1.0 || transform.position.x < objetivo.position.x - 1.0))
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
