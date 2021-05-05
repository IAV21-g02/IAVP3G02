using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using Bolt;
using Ludiq;

public class CeldaBehaviour : MonoBehaviour
{

    public BehaviorTree behaviorTree;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fantasma"))
        {
            other.gameObject.GetComponent<Fantasma>().setFantasmaEstado(2);
            behaviorTree.SetVariableValue("Atrapada", false);
            //Variables.Application.Set("Atrapada", false);


        }
    }
}