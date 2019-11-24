using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BehaviorGraph : ScriptableObject
{
    public List<BaseEditorNodes> windows = new List<BaseEditorNodes>();
    public int idCount;
    List<int> indexToDelete = new List<int>();

    #region Checkers
    public BaseEditorNodes GetNodeWithIndex(int index)
    {
        for (int i = 0; i < windows.Count; i++)
        {
            if (windows[i].id == index)
            {
                return windows[i];
            }
        }
        return null;
    }

    public void DeleteWindowIfNeeded()
    {
        for (int index = 0; index < indexToDelete.Count; index++)
        {
            BaseEditorNodes baseNode = GetNodeWithIndex(indexToDelete[index]);
            if (baseNode != null)
            {
                windows.Remove(baseNode);
            }
        }
        indexToDelete.Clear();
    }

    public void DeleteNode(int index)
    {
        indexToDelete.Add(index);
    }

    public bool IsStateEditorNodeDuplicate(BaseEditorNodes baseNode)
    {
        for (int index = 0; index < windows.Count; index++)
        {
            if (windows[index].id == baseNode.id)
            {
                continue;
            }
            if (windows[index].stateReferences.currentState == baseNode.stateReferences.currentState && !windows[index].isDuplicate)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsTransitionNodeDuplicate(BaseEditorNodes baseNode)
    {
        BaseEditorNodes baseNodeCheck = GetNodeWithIndex(baseNode.enterNode);
        if (baseNodeCheck == null)
        {
            return false;
        }

        for (int i = 0; i < baseNodeCheck.stateReferences.currentState.transitions.Count; i++)
        {
            Transition transition = baseNode.stateReferences.currentState.transitions[i];
            if (transition.condition == baseNode.transitionReference.previousCondition &&
                baseNode.transitionReference.transitionId != transition.id)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}