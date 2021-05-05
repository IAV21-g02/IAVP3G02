using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;


public class PianoBehaviour : MonoBehaviour
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fantasma") && (int)behaviorTree.GetVariable("Estado").GetValue() == 2 )
        {
            behaviorTree.SetVariableValue("TocandoMusica", true);
        }
    }
}
