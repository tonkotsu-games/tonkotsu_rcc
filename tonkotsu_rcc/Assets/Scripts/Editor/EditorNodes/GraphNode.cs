﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphNode : BaseEditorNodes
{
    BehaviorGraph previousGraph;

    public override void DrawWindow()
    {

        if (BehaviorEditor.currentGraph == null)
        {
            EditorGUILayout.LabelField("Add Graph to modify: ");
        }

        BehaviorEditor.currentGraph = (BehaviorGraph)EditorGUILayout.ObjectField(BehaviorEditor.currentGraph, typeof(BehaviorGraph), false);

        if (BehaviorEditor.currentGraph == null)
        {
            if (previousGraph != null)
            {
                //clear windows
                previousGraph = null;
            }
            EditorGUILayout.LabelField("No Graph assigned!");
            return;
        }
        
        if(previousGraph != BehaviorEditor.currentGraph)
        {
            previousGraph = BehaviorEditor.currentGraph;
            BehaviorEditor.LoadGraph();
        }
    }

    public override void DrawCurve()
    {
        base.DrawCurve();
    }
}