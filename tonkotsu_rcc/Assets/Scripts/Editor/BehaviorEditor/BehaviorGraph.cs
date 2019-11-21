using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BehaviorGraph : ScriptableObject
{
    public List<SavedStateNode> savedStateNodes = new List<SavedStateNode>();
    Dictionary<StateEditorNode, SavedStateNode> stateNodesDictionary = new Dictionary<StateEditorNode, SavedStateNode>();
    Dictionary<State, StateEditorNode> stateDictionary = new Dictionary<State, StateEditorNode>();

    public void Init()
    {
        stateNodesDictionary.Clear();
        stateDictionary.Clear();
    }

    public void SetStateNode(StateEditorNode node)
    {
        SavedStateNode savedNodes = GetSavedState(node);
        if(savedNodes == null)
        {
            savedNodes = new SavedStateNode();
            savedStateNodes.Add(savedNodes);
            stateNodesDictionary.Add(node, savedNodes);
        }
        savedNodes.state = node.currentState;
        savedNodes.position = new Vector2(node.windowRect.x, node.windowRect.y);
    }

    public void ClearStateNode(StateEditorNode node)
    {
        SavedStateNode savedNodes = GetSavedState(node);
        if(savedNodes != null)
        {
            savedStateNodes.Remove(savedNodes);
            stateNodesDictionary.Remove(node);
        }
    }

    SavedStateNode GetSavedState(StateEditorNode node)
    {
        SavedStateNode savedNodes = null;
        stateNodesDictionary.TryGetValue(node, out savedNodes);
        return savedNodes;
    }
}

[System.Serializable]
public class SavedStateNode
{
    public State state;
    public Vector2 position;
}

[System.Serializable]
public class SavedTransitions
{

}