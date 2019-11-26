using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Behavior Editor/Nodes/Portal Node")]
public class PortalEditorNode : DrawNode
{
    public override void DrawCurve(BaseEditorNodes baseNodes)
    {

    }

    public override void DrawWindow(BaseEditorNodes baseNodes)
    {
        baseNodes.stateReferences.currentState = (State)EditorGUILayout.ObjectField(baseNodes.stateReferences.currentState, typeof(State), false);
    }
}