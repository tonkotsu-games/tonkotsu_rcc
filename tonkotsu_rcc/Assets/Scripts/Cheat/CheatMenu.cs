using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Reflection;
using System.Linq;

public class CheatMenu : Singleton<CheatMenu>
{
    [SerializeField] VirtualController virtualController;
    [SerializeField] Texture2D[] virtualControllerTextures;

    List<MethodInfo> methodsList = new List<MethodInfo>();
    List<Component> componentListToMethod = new List<Component>();

    bool cheatMenuOpen;
    bool drawCheatButtons;
    bool drawController;

    protected override void Awake()
    {
        base.Awake();

        if(virtualController == null)
        {
            virtualController = gameObject.AddComponent<VirtualController>();
            virtualController.ChangeInputType(VirtualControllerInputType.Player);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            GenerateCheats();
            cheatMenuOpen = !cheatMenuOpen;
        }
    }

    private void OpenCheats()
    {
        //Button1: Toggle Virtual Controller Visualization
        if (GUI.Button(new Rect(10, 10, 150, 50), "Virtual Controller"))
        {
            drawController = !drawController;

        }
        //Button2: Toggle BeatDebugger (visual)
        if (GUI.Button(new Rect(10, 70, 150, 50), "Visualize Beat"))
        {
            BeatHandler.BeatVisualize();
        }
        //Button3: ShowCheats (toggle)
        if (GUI.Button(new Rect(10, 130, 150, 50), "Show Cheats"))
        {
            drawCheatButtons = !drawCheatButtons;
        }

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
        MethodInfo[] methods = component.GetType().GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        .Where(x => x.GetCustomAttribute<CheatMethodAttribute>() != null).ToArray();

        foreach (var method in methods)
        {
            if (!methodsList.Contains(method))
            {
                methodsList.Add(method);
                componentListToMethod.Add(component);
            }
        }
    }

    private void OnGUI()
    {
        if (cheatMenuOpen)
        {
            OpenCheats();
        }

        if(drawCheatButtons)
        {
            DrawCheatButtons();
        }

        if (drawController)
        {
            virtualController.DrawGUI(virtualControllerTextures);
        }

    }
    private void DrawCheatButtons()
    {
        int offsetX = 0;
        int offsetY = 0;
        int index = 0;
        int buttonsInRow = 10;
        int allowedButtons = buttonsInRow;
        foreach(var method in methodsList)
        {
            if(GUI.Button(new Rect(200 + offsetX, 130 + offsetY, 200, 50), componentListToMethod[index].gameObject.name + " " + method.Name.ToString()))
            {
                method.Invoke(componentListToMethod[index], null);
            }
            offsetY += 60;

            if (index == allowedButtons)
            {
                offsetX += 200;
                offsetY = 0;
                allowedButtons += buttonsInRow;
            }
            index += 1;
        }
    }
}