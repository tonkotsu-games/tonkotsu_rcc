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
    static Texture2D referenceTexture;
    static Texture2D balanceTexture;
    static List<int> markedObjects;

    static HierarchyIcons()
    {
        // Init
        referenceTexture = AssetDatabase.LoadAssetAtPath("Assets/Textures/ScriptingTextures/ExclamationMark.png", typeof(Texture2D)) as Texture2D;
        balanceTexture = AssetDatabase.LoadAssetAtPath("Assets/Textures/ScriptingTextures/1200px-Question_mark_grey.svg.png", typeof(Texture2D)) as Texture2D;
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyIconCB;
        EditorApplication.update += UpdateCB;
    }

    static void UpdateCB()
    {
        Debug.LogError("Update CB");

        markedObjects = new List<int>();

        var foundGameObjects = Object.FindObjectsOfType (typeof(GameObject)) as GameObject[];
        Debug.Log("found go: " + foundGameObjects.Length);
        if (foundGameObjects.Length != 0)
        {
            foreach(GameObject go in foundGameObjects)
            {
                var components = go.GetComponents<Component>();
                for (int i = 0; i < components.Length; i++)
                {
                    FieldInfo[] fieldsRequired = GetAllRequired(components, i);

                    FieldInfo[] fieldsSerializeField = GetAllSerializedFields(fieldsRequired);
                    Debug.Log("fields " + fieldsSerializeField.Length);
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
                        markedObjects.Add(go.GetInstanceID());
                    }
                }           
                Debug.Log(markedObjects.Count);
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

    static void HierarchyIconCB(int instanceID, Rect selectionRect)
    {

        // place the icoon to the right of the list:
        Rect r = new Rect(selectionRect);
        r.width = 20;
        r.x = r.width - 20;

        //Debug.LogError("HierarchyIconCB");
        //Debug.Log("markedObjects.Count: " + markedObjects.Count);
        if (markedObjects.Contains (instanceID)) 
        {
            GUI.Label(r, referenceTexture);
        }      
    }
}
