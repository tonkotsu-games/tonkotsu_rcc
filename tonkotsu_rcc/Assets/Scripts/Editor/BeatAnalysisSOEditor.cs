using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes;

[CustomEditor(typeof(BeatAnalysisSO))]
public class BeatAnalysisSOEditor : Editor
{

    public override void OnInspectorGUI()
    {
        BeatAnalysisSO myTarget = target as BeatAnalysisSO;

        DrawDefaultInspector();
        if(GUILayout.Button("Analyse Beat"))
        {
            Undo.RecordObject(target, "Analysed Beat");
            myTarget.ResultList = myTarget.AnalyseClip();
        }

        if(myTarget.ResultList != null)
        {
            GUILayout.TextField(myTarget.ResultList.Count.ToString());
        }
            
    }
}
