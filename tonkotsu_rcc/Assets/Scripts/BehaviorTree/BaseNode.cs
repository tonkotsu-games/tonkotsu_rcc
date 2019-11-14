public abstract class BaseNode
{
    protected NodeState currentNodeState;

    public NodeState nodeState
    {
        get { return currentNodeState; }
    }

    public BaseNode() { }

    public abstract NodeState Evaluate();
}