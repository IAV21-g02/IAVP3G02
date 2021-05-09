using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;


public class CeldaBehaviour : MonoBehaviour
{
  public GameObject cantante;
    public GameObject objetivo;
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
            other.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            cantante.transform.SetPositionAndRotation(objetivo.transform.position, other.gameObject.transform.rotation);
            cantante.transform.Rotate(new Vector3(0, 1, 0), 180);
        }
      
    }
}