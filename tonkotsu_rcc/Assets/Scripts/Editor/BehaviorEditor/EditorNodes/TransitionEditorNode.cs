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
        if (enterNode == null)
        {
            return;
        }

        if (enterNode.stateReferences.currentState == null)
        {
            BehaviorEditor.editorSettings.currentGraph.DeleteNode(baseNode.id);
            return;
        }

        Transition transition = enterNode.stateReferences.currentState.GetTransition(baseNode.transitionReference.transitionId);

        if (transition == null)
        {
            return;
        }

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
                GUILayout.Label(transition.condition.description);

                BaseEditorNodes targetNode = BehaviorEditor.editorSettings.currentGraph.GetNodeWithIndex(baseNode.targetNode);
                if (targetNode != null)
                {
                    if (!targetNode.isDuplicate)
                    {
                        transition.targetState = targetNode.stateReferences.currentState;
                    }
                    else
                    {
                        transition.targetState = null;
                    }
                }
                else
                {
                    transition.targetState = null;
                }
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

        if (baseNode.targetNode > 0)
        {
            BaseEditorNodes enterCondition = BehaviorEditor.editorSettings.currentGraph.GetNodeWithIndex(baseNode.targetNode);
            if (enterNode == null)
            {
                baseNode.targetNode = -1;
            }
            else
            {
                rect = baseNode.windowRect;
                rect.x += rect.width;
                Rect endRect = enterCondition.windowRect;
                endRect.x -= endRect.width * 0.5f;
                BehaviorEditor.DrawNodeCurve(rect, endRect, false, Color.green);
            }

        }
    }
}