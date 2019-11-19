using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StateEditorNode : BaseEditorNodes
{
    bool collapse;
    public BaseNodes currentState;
    BaseNodes previousState;

    public List<BaseEditorNodes> dependecies = new List<BaseEditorNodes>();

    public override void DrawWindow()
    {
        if (currentState == null)
        {
            EditorGUILayout.LabelField("Add state to modify: ");
        }
        else
        {
            if (!collapse)
            {
                windowRect.height = 300;
            }
            else
            {
                windowRect.height = 100;
            }
            collapse = EditorGUILayout.Toggle(" ", collapse);
        }

        currentState = (BaseNodes)EditorGUILayout.ObjectField(currentState, typeof(BaseNodes), false);

        if(previousState != currentState)
        {
            previousState = currentState;
            ClearReference();
            for (int i = 0; i < currentState.transitions.Count; i++)
            {
                dependecies.Add(BehaviorEditor.AddTransitionNode(i, currentState.transitions[i], this));
            }
        }
        if(currentState != null)
        {

        }
    }

    public override void DrawCurve()
    {

    }

    public Transition AddTransition()
    {
        return currentState.AddTransition();
    }

    public void ClearReference()
    {
        BehaviorEditor.ClearList(dependecies);
        dependecies.Clear();
    }
}