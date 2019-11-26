using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Editor/Settings")]
public class EditorSettings : ScriptableObject
{
    public BehaviorGraph currentGraph;
    public StateEditorNode stateNode;
    public PortalEditorNode portalNode;
    public TransitionEditorNode transitionNode;
    public CommentEditorNode commentNode;
    public bool makeTransition;

    public BaseEditorNodes AddNodeOnGraph(DrawNode type,float width,float height,string title, Vector2 position)
    {
        BaseEditorNodes baseNode = new BaseEditorNodes();
        baseNode.drawNode = type;
        baseNode.windowRect.width = width;
        baseNode.windowRect.height = height;
        baseNode.windowTitle = title;
        baseNode.windowRect.x = position.x;
        baseNode.windowRect.y = position.y;
        currentGraph.windows.Add(baseNode);
        baseNode.transitionReference = new TransitionNodeReference();
        baseNode.stateReferences = new StateNodeReferences();
        baseNode.id = currentGraph.idCount;
        currentGraph.idCount++;
        return baseNode;
    }
}