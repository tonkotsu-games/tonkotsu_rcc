using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.IO;


public class StateEditorNode : BaseEditorNodes
{
    bool collapse;
    public State currentState;
    State previousState;

    SerializedObject serializedState;
    ReorderableList stateOnExecuteList;
    ReorderableList stateOnEnterList;
    ReorderableList stateOnExitList;

    public List<BaseEditorNodes> dependecies = new List<BaseEditorNodes>();

    public override void DrawWindow()
    {
        if (currentState == null)
        {
            EditorGUILayout.LabelField("Add state to modify: ");
        }
        else
        {
            if (collapse)
            {
                windowRect.height = 100;
            }
            collapse = EditorGUILayout.Toggle(" ", collapse);
        }

        currentState = (State)EditorGUILayout.ObjectField(currentState, typeof(State), false);

        if(previousState != currentState)
        {
            serializedState = null;

            previousState = currentState;

            BehaviorEditor.currentGraph.SetStateNode(this);
            for (int i = 0; i < currentState.transitions.Count; i++)
            {

            }
        }
        if(currentState != null)
        {
            if(serializedState == null)
            {
                serializedState = new SerializedObject(currentState);
                stateOnEnterList = new ReorderableList(serializedState, serializedState.FindProperty("stateOnEnter"), true, true, true, true);
                stateOnExecuteList = new ReorderableList(serializedState, serializedState.FindProperty("stateOnExecute"), true, true, true, true);
                stateOnExitList = new ReorderableList(serializedState, serializedState.FindProperty("stateOnExit"), true, true, true, true);
            }

            if(!collapse)
            {
                serializedState.Update();
                HandleReordableList(stateOnEnterList, "On Enter");
                HandleReordableList(stateOnExecuteList, "On Execute");
                HandleReordableList(stateOnExitList, "On Exit");

                EditorGUILayout.LabelField("");
                stateOnEnterList.DoLayoutList();
                EditorGUILayout.LabelField("");
                stateOnExecuteList.DoLayoutList();
                EditorGUILayout.LabelField("");
                stateOnExitList.DoLayoutList();

                serializedState.ApplyModifiedProperties();

                float standardRectHeight = 300;
                standardRectHeight += (stateOnEnterList.count + stateOnExecuteList.count + stateOnExitList.count+2) * 20;
                windowRect.height = standardRectHeight;
            }
        }
    }

    void HandleReordableList(ReorderableList list, string targetName)
    {
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, targetName);
        };

        list.drawElementCallback = (Rect rect, int index, bool isActiv, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
        };
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