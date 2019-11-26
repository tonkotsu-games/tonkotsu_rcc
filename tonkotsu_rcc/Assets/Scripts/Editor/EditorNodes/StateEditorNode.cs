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
            baseNode.stateReferences.previousState = baseNode.stateReferences.currentState;
            if (!baseNode.isDuplicate)
            {
                Vector2 position = new Vector2(baseNode.windowRect.x, baseNode.windowRect.y);
                position.x = baseNode.windowRect.width * 2;

                SetupReordableList(baseNode);

                for (int i = 0; i < baseNode.stateReferences.currentState.transitions.Count; i++)
                {
                    position.y += i * 100;
                    BehaviorEditor.AddTransitionNodeFromTransition(baseNode.stateReferences.currentState.transitions[i], baseNode, position);
                }
            }
        }

        if (baseNode.isDuplicate)
        {
            EditorGUILayout.LabelField("State is a duplicate");
            baseNode.windowRect.height = 100;
            return;
        }

        if (baseNode.stateReferences.currentState != null)
        {
            if (!baseNode.collapse)
            {
                if (baseNode.stateReferences.serializedState == null)
                {
                    SetupReordableList(baseNode);
                }

                EditorGUILayout.LabelField("");
                baseNode.stateReferences.stateOnEnterList.DoLayoutList();
                EditorGUILayout.LabelField("");
                baseNode.stateReferences.stateOnExecuteList.DoLayoutList();
                EditorGUILayout.LabelField("");
                baseNode.stateReferences.stateOnExitList.DoLayoutList();

                baseNode.stateReferences.serializedState.ApplyModifiedProperties();

                float standardRectHeight = 300;
                standardRectHeight += (baseNode.stateReferences.stateOnEnterList.count +
                                       baseNode.stateReferences.stateOnExecuteList.count +
                                       baseNode.stateReferences.stateOnExitList.count + 2) * 20;
                baseNode.windowRect.height = standardRectHeight;
            }
        }
    }

    void SetupReordableList(BaseEditorNodes baseNode)
    {
        baseNode.stateReferences.serializedState = new SerializedObject(baseNode.stateReferences.currentState);

        baseNode.stateReferences.stateOnEnterList = new ReorderableList(baseNode.stateReferences.serializedState, baseNode.stateReferences.serializedState.FindProperty("stateOnEnter"), true, true, true, true);
        baseNode.stateReferences.stateOnExecuteList = new ReorderableList(baseNode.stateReferences.serializedState, baseNode.stateReferences.serializedState.FindProperty("stateOnExecute"), true, true, true, true);
        baseNode.stateReferences.stateOnExitList = new ReorderableList(baseNode.stateReferences.serializedState, baseNode.stateReferences.serializedState.FindProperty("stateOnExit"), true, true, true, true);

        HandleReordableList(baseNode.stateReferences.stateOnEnterList, "State on enter");
        HandleReordableList(baseNode.stateReferences.stateOnExecuteList, "State on execute");
        HandleReordableList(baseNode.stateReferences.stateOnExitList, "State on exit");
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