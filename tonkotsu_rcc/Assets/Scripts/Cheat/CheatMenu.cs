using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Reflection;
using System.Linq;

public class CheatMenu : Singleton<CheatMenu>
{
    MethodInfo[] methods;

    List<int> markedObjects;


    public CheatMenu()
    {
        markedObjects = new List<int>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            GenerateCheats();
        }
    }

    private void OpenCheats()
    {
        //3 Base buttons generated here (created by Benny soon^TM)

        //Button1: Toggle Virtual Controller Visualization

        //Button2: Toggle BeatDebugger (visual)

        //Button3: ShowCheats (toggle)
        //GUILayout.Toggle
        //GUILayout.Button
    }

    private void GenerateCheats()
    {
        //Find all objects in scene
        var objects = GameObject.FindObjectsOfType((typeof(GameObject)));

        Debug.Log("Objects: " + objects.Length);

        GameObject[] gameObjects = new GameObject[objects.Length];
        Debug.Log("GameObjects: " + gameObjects.Length);

        //convert Objects to GameObjects
        int index = 0;
        foreach (Object specificObject in objects)
        {
            gameObjects[index] = specificObject as GameObject;
            index++;
        }
        index = 0;

        //Get all their Components
        List<Component> components = new List<Component>();
        if (gameObjects != null)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                var foundComponents = gameObjects[i].GetComponents<Component>();

                foreach (Component component in foundComponents)
                {
                    components.Add(component);
                }
            }

            Debug.Log("Components: " + components.Count);

            for (int i = 0; i < components.Count; i++)
            {
                SaveAllCheatMethods(components[i]);
            }
        }
    }

    private void SaveAllCheatMethods(Component component)
    {
        methods = component.GetType().GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        .Where(x => x.GetCustomAttribute<CheatMethodAttribute>() != null).ToArray();

        foreach (var method in methods)
        {
            Debug.Log("Draw Buttons here instead");
            method.Invoke(component, null);
        }
    }
}
