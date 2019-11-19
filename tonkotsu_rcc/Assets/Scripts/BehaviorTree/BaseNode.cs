using System.Collections.Generic;
using UnityEngine;


public abstract class BaseNodes : ScriptableObject
{
    protected NodeState currentNodeState;

    public List<Transition> transitions = new List<Transition>();

    public NodeState nodeState
    {
        get { return currentNodeState; }
    }

    public BaseNodes() { }

    public abstract NodeState Evaluate();

    public Transition AddTransition()
    {
        Transition transitionValue = new Transition();
        transitions.Add(transitionValue);
        return transitionValue;
    }
}