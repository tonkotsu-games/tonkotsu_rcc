using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Reflection;
using System.Linq;

public class CheatMenu : MonoBehaviour
{
    static List<int> markedObjects;


    static CheatMenu()
    {
        markedObjects = new List<int>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            HierarchyItemCB();
        }
    }

    private void HierarchyItemCB()
    {
        var objects = GameObject.FindObjectsOfType((typeof(GameObject)));

        Debug.Log("Objects: " + objects.Length);

        GameObject[] gameObjects = new GameObject[objects.Length];
        Debug.Log("GameObjects: " + gameObjects.Length);

        int index = 0;
        foreach(Object specificObject in objects)
        {
            gameObjects[index] = specificObject as GameObject;
            index++;
        }
        index = 0;
        

        if (gameObjects != null)
        {
            List<Component> components = new List<Component>();

            for(int i = 0; i < gameObjects.Length; i++)
            {
                var foundComponents = gameObjects[i].GetComponents<Component>();

                foreach(Component component in foundComponents)
                {
                    components.Add(component);
                }  
            }

            Debug.Log("Components: " + components.Count);
            
            for (int i = 0; i < components.Count; i++)
            {
                AllCheatMethods(components[i]);
            }
        }
    }

    private static void AllCheatMethods(Component component)
    {
        MethodInfo[] methods = component.GetType().GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        .Where(x => x.GetCustomAttribute<CheatMethodAttribute>() != null).ToArray();


        foreach(var method in methods)
        {
            Debug.Log("Invoked");
            method.Invoke(component, null);
        }
    }
}
