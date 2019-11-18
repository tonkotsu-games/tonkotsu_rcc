using System.Collections.Generic;

public class Selector : BaseNode
{
    private List<BaseNode> nodes = new List<BaseNode>();

    public Selector(List<BaseNode> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach (BaseNode node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    {
                        continue;
                    }
                case NodeState.SUCCESS:
                    {
                        currentNodeState = NodeState.SUCCESS;
                        return currentNodeState;
                    }
                default:
                    {
                        continue;
                    }
            }
        }
        currentNodeState = NodeState.FAILURE;
        return currentNodeState;
    }
}