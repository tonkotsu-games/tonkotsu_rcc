using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommentEditorNode : DrawNode
{
    string comment = "Here could stay your comment";


    public override void DrawWindow(BaseEditorNodes baseNode)
    {
        comment = GUILayout.TextArea(comment, 200);
    }
    public override void DrawCurve(BaseEditorNodes baseNodes)
    {
        throw new System.NotImplementedException();
    }
}