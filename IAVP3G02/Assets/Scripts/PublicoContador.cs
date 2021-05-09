using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;
using BehaviorDesigner.Runtime;


public class PublicoContador : MonoBehaviour
{
    public BehaviorTree behaviorTree;

    public void setPublico()
    {
        //Debug.Log("llamamos al metodo setPublico");
        behaviorTree.SetVariableValue("Publico", Variables.Application.Get("Publico"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Publico"))
        {
            Debug.Log("Publico dentro");
        }
    }
}
