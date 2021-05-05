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
        behaviorTree.SetVariableValue("Publico", Variables.Application.Get("Publico"));
    }
}
