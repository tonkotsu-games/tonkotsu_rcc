using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.IO;

[CreateAssetMenu(menuName = "Behavior Editor/Nodes/State Node")]
public class StateEditorNode : DrawNode
{
    public override void DrawWindow(BaseEditorNodes baseNode)
    {
        if (baseNode.stateReferences.currentState == null)
        {
            EditorGUILayout.LabelField("Add state to modify: ");
        }
        else
        {
            if (baseNode.collapse)
            {
                baseNode.windowRect.height = 100;
            }
            baseNode.collapse = EditorGUILayout.Toggle(" ", baseNode.collapse);
        }

        baseNode.stateReferences.currentState = (State)EditorGUILayout.ObjectField(baseNode.stateReferences.currentState, typeof(State), false);

        if (baseNode.previousCollapse != baseNode.collapse)
        {
            baseNode.previousCollapse = baseNode.collapse;
        }

        if (baseNode.stateReferences.previousState != baseNode.stateReferences.currentState)
        {
            //baseNode.serializedState = null;
            baseNode.isDuplicate = BehaviorEditor.editorSettings.currentGraph.IsStateEditorNodeDuplicate(baseNode);
        }

        if (baseNode.isDuplicate)
        {
            EditorGUILayout.LabelField("State is a duplicate");
            baseNode.windowRect.height = 100;
            return;
        }

        if (baseNode.stateReferences.currentState != null)
        {
            SerializedObject serializedState = new SerializedObject(baseNode.stateReferences.currentState);

            ReorderableList stateOnEnterList;
            ReorderableList stateOnExecuteList;
            ReorderableList stateOnExitList;

            stateOnEnterList = new ReorderableList(serializedState, serializedState.FindProperty("stateOnEnter"), true, true, true, true);
            stateOnExecuteList = new ReorderableList(serializedState, serializedState.FindProperty("stateOnExecute"), true, true, true, true);
            stateOnExitList = new ReorderableList(serializedState, serializedState.FindProperty("stateOnExit"), true, true, true, true);

            if (!baseNode.collapse)
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
                standardRectHeight += (stateOnEnterList.count + stateOnExecuteList.count + stateOnExitList.count + 2) * 20;
                baseNode.windowRect.height = standardRectHeight;
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

    public override void DrawCurve(BaseEditorNodes baseNode)
    {

    }

    public Transition AddTransition(BaseEditorNodes baseNode)
    {
        return baseNode.stateReferences.currentState.AddTransition();
    }

    public void ClearReference()
    {
        //BehaviorEditor.ClearList(dependecies);
        //dependecies.Clear();
    }
}