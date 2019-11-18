using UnityEngine;

public abstract class BaseNodes
{
    protected NodeState currentNodeState;


    public NodeState nodeState
    {
        get { return currentNodeState; }
    }

    public BaseNodes() { }

    public abstract NodeState Evaluate();
}