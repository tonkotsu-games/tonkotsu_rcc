using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public interface IHierarchyEffector
{
    HierarchyDisplayElement GetHierarchyEffect();
}


public class HierarchyDisplayElement
{
    private Type type;
    string text;
    Color color;
    Texture texture;

    public HierarchyDisplayElement(string s)
    {
        type = Type.Text;
        text = s;
    }

    public HierarchyDisplayElement(Color c)
    {
        type = Type.Colored;
        color = c;
    }

    public HierarchyDisplayElement(Texture t)
    {
        type = Type.Texture;
        texture = t;
    }


    public float DisplayAndReturnX(Rect r)
    {
        Rect newRect;
        switch (type)
        {
            case Type.Text:
                float width = text.Length * 12;
                newRect = new Rect(r.x - width, r.y, width, 15);
                EditorGUI.LabelField(newRect, text);
                return r.x - width -5;

            case Type.Colored:
                newRect = new Rect(r.x -15, r.y, 15, 15);
                EditorGUI.DrawRect(newRect, color);
                    return r.x - 20;

            case Type.Texture:
                newRect = new Rect(r.x -15, r.y, 15, 15);
                GUI.DrawTexture(newRect, texture);
                return r.x - 20;

            default:
                return r.x;
        }

    }

    public enum Type
    {
        Text,
        Colored,
        Texture
    }
}
