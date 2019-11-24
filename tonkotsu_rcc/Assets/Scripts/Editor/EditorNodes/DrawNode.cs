using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DrawNode : ScriptableObject
{
    public abstract void DrawWindow(BaseEditorNodes baseNodes);

    public abstract void DrawCurve(BaseEditorNodes baseNodes);
}