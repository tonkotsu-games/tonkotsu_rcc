using System.Collections.Generic;

public class Sequence : BaseNode
{
    private List<BaseNode> nodes = new List<BaseNode>();

    public Sequence(List<BaseNode> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        bool childRunning = false;

        foreach (BaseNode node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    {
                        currentNodeState = NodeState.FAILURE;
                        return currentNodeState;
                    }
                case NodeState.SUCCESS:
                    {
                        continue;
                    }
                case NodeState.RUNNING:
                    {
                        childRunning = true;
                        continue;
                    }
                default:
                    {
                        currentNodeState = NodeState.SUCCESS;
                        return currentNodeState;
                    }
            }
        }
        currentNodeState = childRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return currentNodeState;
    }
}