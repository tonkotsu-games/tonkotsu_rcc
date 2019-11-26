using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[System.Serializable]
public class BaseEditorNodes
{
    public int id;
    public DrawNode drawNode;
    public Rect windowRect;
    public string windowTitle;
    public int enterNode;
    public int targetNode;
    public bool isDuplicate;
    public string comment;
    public bool collapse;
    public bool previousCollapse;
    public StateNodeReferences stateReferences;
    public TransitionNodeReference transitionReference;

    public void DrawWindow()
    {
        if (drawNode != null)
        {
            drawNode.DrawWindow(this);
        }
    }

    public void DrawCurve()
    {
        if(drawNode!= null)
        {
            drawNode.DrawCurve(this);
        }
    }
}

[System.Serializable]
public class StateNodeReferences
{
    [HideInInspector]
    public State currentState;
    [HideInInspector]
    public State previousState;
    public SerializedObject serializedState;
    public ReorderableList stateOnEnterList;
    public ReorderableList stateOnExecuteList;
    public ReorderableList stateOnExitList;
}

[System.Serializable]
public class TransitionNodeReference
{
    [HideInInspector]
    public Condition previousCondition;
    public int transitionId;
}