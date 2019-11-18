using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BehaviorEditor : EditorWindow
{
    #region Variables
    static List<BaseEditorNodes> windows = new List<BaseEditorNodes>();
    Vector2 mousePosition;
    bool makeTransition;
    bool clickedOnWindow;
    BaseEditorNodes selectedNode;

    private void OnEnable()
    {
        windows.Clear();
    }

    public enum UserActions
    {
        AddNode,
        AddTransition,
        DeleteNode,
        AddCommentNode
    }
    #endregion

    #region Init
    [MenuItem("Behavior Editor/Editor")]
    static void ShowEditor()
    {
        BehaviorEditor editor = EditorWindow.GetWindow<BehaviorEditor>();
        editor.minSize = new Vector2(800, 600);
    }
    #endregion

    #region GUI Methods
    private void OnGUI()
    {
        Event events = Event.current;
        mousePosition = events.mousePosition;
        UserInput(events);
        DrawWindows();
    }

    void DrawWindows()
    {
        BeginWindows();
        foreach (BaseEditorNodes node in windows)
        {
            node.DrawCurve();
        }
        for (int i = 0; i < windows.Count; i++)
        {
            windows[i].windowRect = GUI.Window(i, windows[i].windowRect, DrawNodeWindow, windows[i].windowTitle);
        }
        EndWindows();
    }

    void DrawNodeWindow(int id)
    {
        windows[id].DrawWindow();
        GUI.DragWindow();
    }

    void UserInput(Event events)
    {
        if (events.button == 1 && !makeTransition)
        {
            if (events.type == EventType.MouseDown)
            {
                RightClick(events);
            }
        }

        if (events.button == 0 && !makeTransition)
        {
            if (events.type == EventType.MouseDown)
            {

            }
        }
    }

    void RightClick(Event events)
    {
        selectedNode = null;
        for (int i = 0; i < windows.Count; i++)
        {
            if (windows[i].windowRect.Contains(events.mousePosition))
            {
                clickedOnWindow = true;
                selectedNode = windows[i];
                break;
            }
        }
        if (!clickedOnWindow)
        {
            AddNewNode(events);
        }
        else
        {
            ModifyNode(events);
        }
    }

    void AddNewNode(Event events)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddSeparator("");
        menu.AddItem(new GUIContent("add State"), false, CallBack, UserActions.AddNode);
        menu.AddItem(new GUIContent("add Comment"), false, CallBack, UserActions.AddCommentNode);
        menu.ShowAsContext();
        events.Use();
    }

    void ModifyNode(Event events)
    {

    }

    void CallBack(object objects)
    {
        UserActions actions = (UserActions)objects;
        switch (actions)
        {
            case UserActions.AddNode:
                StateEditorNode stateNode = ScriptableObject.CreateInstance<StateEditorNode>();
                
                stateNode.windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 300);
                stateNode.windowTitle = "State";
               
                windows.Add(stateNode);
                break;
            case UserActions.AddTransition:
                break;
            case UserActions.DeleteNode:
                break;
            case UserActions.AddCommentNode:
                break;
        }

        #endregion
    }
}