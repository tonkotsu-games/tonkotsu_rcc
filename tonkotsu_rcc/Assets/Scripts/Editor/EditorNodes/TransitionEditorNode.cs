using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class TransitionEditorNode : DrawNode
{
    public void Init(StateEditorNode enterState, Transition transition)
    {
        //this.enterState = enterState;
    }

    public override void DrawWindow(BaseEditorNodes baseNode)
    {
        EditorGUILayout.LabelField("");
        BaseEditorNodes enterNode = BehaviorEditor.editorSettings.currentGraph.GetNodeWithIndex(baseNode.enterNode);

        Transition transition = enterNode.stateReferences.currentState.GetTransition(baseNode.transitionReference.transitionId);

        transition.condition = (Condition)EditorGUILayout.ObjectField(transition.condition, typeof(Condition), false);

        if (transition.condition == null)
        {
            EditorGUILayout.LabelField("No Condition!");
        }
        else
        {
            if (baseNode.isDuplicate)
            {
                EditorGUILayout.LabelField("Condition is a duplicate");
            }
            else
            {
                //if (transition != null)
                //{
                //    transition.disable = EditorGUILayout.Toggle("Disable", transition.disable);
                //}
            }
        }

        if (baseNode.transitionReference.previousCondition != transition.condition)
        {
            baseNode.transitionReference.previousCondition = transition.condition;

            baseNode.isDuplicate = BehaviorEditor.editorSettings.currentGraph.IsTransitionNodeDuplicate(baseNode);
            if (!baseNode.isDuplicate)
            {
                //BehaviorEditor.currentGraph.SetNode(this);
            }
        }
    }

    public override void DrawCurve(BaseEditorNodes baseNode)
    {
        Rect rect = baseNode.windowRect;
        rect.y += baseNode.windowRect.height * 0.5f;
        rect.width = 1;
        rect.height = 1;

        BaseEditorNodes enterNode = BehaviorEditor.editorSettings.currentGraph.GetNodeWithIndex(baseNode.enterNode);
        if (enterNode == null)
        {
            BehaviorEditor.editorSettings.currentGraph.DeleteNode(baseNode.id);
        }
        else
        {
            Rect rectangle = enterNode.windowRect;
            BehaviorEditor.DrawNodeCurve(rectangle, rect, true, Color.green);
        }
    }
}