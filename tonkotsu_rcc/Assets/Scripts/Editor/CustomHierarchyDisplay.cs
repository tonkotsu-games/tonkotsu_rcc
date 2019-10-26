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
    private static bool flicker = false;

    static CustomHierarchyColors()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceID);
        if (obj != null && obj is GameObject go)
        {
            float x = selectionRect.xMax - 10;
            var effectors = go.GetComponents<IHierarchyEffector>();

            foreach (var effector in effectors)
            {
                var element = effector.GetHierarchyEffect();
                if(element == null)
                {
                    continue;
                }
                x = element.DisplayAndReturnX(new Rect(x, selectionRect.y, selectionRect.width, selectionRect.height));
            }
        }
    }
}
