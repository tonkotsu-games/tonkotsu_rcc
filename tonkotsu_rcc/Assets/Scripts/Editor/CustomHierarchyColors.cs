using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[InitializeOnLoad]
public class CustomHierarchyColors : MonoBehaviour
{
    private static Vector2 offset = new Vector2(0, 2);
    private static bool white = false;

    static CustomHierarchyColors()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {

        var obj = EditorUtility.InstanceIDToObject(instanceID);
        if (obj != null)
        {
            white = UnityEngine.Random.value < 0.5f;
            Rect newRect = new Rect(selectionRect.xMax - 100, selectionRect.y, 100, selectionRect.height);

            EditorGUI.DropShadowLabel(newRect, "ERROR FIX MEE", new GUIStyle()
            {
                normal = new GUIStyleState() { textColor = white ? Color.white : Color.red }
            });
            
            EditorApplication.RepaintHierarchyWindow();
        }

    }
}
