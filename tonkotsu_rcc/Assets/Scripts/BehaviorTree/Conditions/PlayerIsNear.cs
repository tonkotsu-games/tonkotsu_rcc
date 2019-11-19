using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerIsNear", menuName = "Behavior Condition/Player is near")]
public class PlayerIsNear : Condition
{
    public override bool checkCondition(NodeState currentState)
    {
        switch (currentState)
        {
            case NodeState.SUCCESS:
                return true;
            case NodeState.FAILURE:
                return false;
            default:
                return false;
        }
    }
}