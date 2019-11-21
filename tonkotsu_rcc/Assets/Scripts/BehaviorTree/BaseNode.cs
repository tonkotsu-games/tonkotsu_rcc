using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public abstract class BaseNodes : ScriptableObject
{
    
    protected NodeState currentNodeState;


    public NodeState nodeState
    {
        get { return currentNodeState; }
    }

    public BaseNodes() { }

    public abstract NodeState Evaluate();

}