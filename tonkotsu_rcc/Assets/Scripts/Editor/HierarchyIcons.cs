using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor;
using System.Reflection;
using System.Linq;

[InitializeOnLoad]
public class HierarchyIcons
{
    static Texture2D texture;
    static List<int> markedObjects;

    static HierarchyIcons()
    {
        // Init
        texture = AssetDatabase.LoadAssetAtPath("Assets/Scripts/Editor/Textures/ExclamationMark.png", typeof(Texture2D)) as Texture2D;
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
        markedObjects = new List<int>();
    }

    static void HierarchyItemCB(int instanceID, Rect selectionRect)
    {
        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (go)
        {
            var components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                FieldInfo[] fieldsRequired = GetAllRequired(components, i);

                FieldInfo[] fieldsSerializeField = GetAllSerializedFields(fieldsRequired);

                bool componentHasAllRef = true;

                for (int j = 0; j < fieldsSerializeField.Length; j++)
                {
                    if(fieldsSerializeField[j] != null)
                    {
                        if (fieldsSerializeField[j].GetValue(components[i]) == null)
                        {
                            componentHasAllRef = false;
                        }
                    }                   
                }
                if (componentHasAllRef == false)
                {
                    DrawIcon(selectionRect);
                }
            }
        }
    }

    private static FieldInfo[] GetAllRequired(Component[] components, int i)
    {
        return components[i].GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(f => f.GetCustomAttribute<RequiredAttribute>() != null).ToArray();
    }

    private static FieldInfo[] GetAllSerializedFields(FieldInfo[] fieldsArray)
    {
        FieldInfo[] foundFields = new FieldInfo[fieldsArray.Length];

        for(int i = 0; i < fieldsArray.Length; i++)
        {
            if(fieldsArray[i].GetCustomAttribute<SerializeField>() != null)
            {
                foundFields[i] = fieldsArray[i];
            }
        }
        
        return foundFields;
    }

    private static void DrawIcon(Rect selectionRect)
    {
        // place the icoon to the right of the list:
        Rect r = new Rect(selectionRect);
        r.width = 20;
        r.x = r.width - 20;
        GUI.Label(r, texture);
    }
}
