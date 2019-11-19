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
        if (GUI.Button(new Rect(10, 10, 100, 20), "Clear Me"))
        {
            ClearMe();
        }

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
        clickedOnWindow = false;
    }

    void AddNewNode(Event events)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddSeparator("");
        menu.AddItem(new GUIContent("Add State"), false, CallBack, UserActions.AddNode);
        menu.AddItem(new GUIContent("Add Comment"), false, CallBack, UserActions.AddCommentNode);
        menu.ShowAsContext();
        events.Use();
    }

    void ModifyNode(Event events)
    {
        GenericMenu menu = new GenericMenu();
        if (selectedNode is StateEditorNode)
        {
            StateEditorNode stateNode = (StateEditorNode)selectedNode;
            if (stateNode.currentState != null)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Add Transition"), false, CallBack, UserActions.AddTransition);
            }
            else
            {
                menu.AddSeparator("");
                menu.AddDisabledItem(new GUIContent("Add Transition"));
            }
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Delete Node"), false, CallBack, UserActions.DeleteNode);
        }

        if (selectedNode is CommentEditorNode)
        {
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Delete Node"), false, CallBack, UserActions.DeleteNode);
        }

        if (selectedNode is TransitionEditorNode)
        {
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Delete Node"), false, CallBack, UserActions.DeleteNode);
        }
        menu.ShowAsContext();
        events.Use();
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
                if (selectedNode is StateEditorNode)
                {
                    StateEditorNode stateTransitionNode = (StateEditorNode)selectedNode;
                    Transition transition = stateTransitionNode.AddTransition();
                    AddTransitionNode(stateTransitionNode.currentState.transitions.Count, transition, stateTransitionNode);
                }
                break;
            case UserActions.DeleteNode:
                if (selectedNode != null)
                {
                    if (selectedNode is StateEditorNode)
                    {
                        StateEditorNode stateTransitionNode = (StateEditorNode)selectedNode;
                        stateTransitionNode.ClearReference();
                        windows.Remove(stateTransitionNode);
                    }
                    if (selectedNode is TransitionEditorNode)
                    {
                        TransitionEditorNode target = (TransitionEditorNode)selectedNode;
                        windows.Remove(target);

                        if (target.enterState.currentState.transitions.Contains(target.targetTransition))
                        {
                            target.enterState.currentState.transitions.Remove(target.targetTransition);
                        }
                    }
                    if (selectedNode is CommentEditorNode)
                    {
                        windows.Remove(selectedNode);
                    }

                }
                break;
            case UserActions.AddCommentNode:
                CommentEditorNode commentNode = ScriptableObject.CreateInstance<CommentEditorNode>();
                commentNode.windowRect = new Rect(mousePosition.x, mousePosition.y, 200, 100);
                commentNode.windowTitle = "Comment";
                windows.Add(commentNode);
                break;
        }
    }

    public void ClearMe()
    {
        windows.Clear();

    }

    public static void ClearList(List<BaseEditorNodes> stateList)
    {
        for (int i = 0; i < stateList.Count; i++)
        {
            if (windows.Contains(stateList[i]))
            {
                windows.Remove(stateList[i]);
            }
        }
    }
    #endregion

    #region Helper Methods
    public static TransitionEditorNode AddTransitionNode(int index, Transition transition, StateEditorNode stateNode)
    {
        Rect stateNodeRect = stateNode.windowRect;
        stateNodeRect.y += 50;
        float targetY = stateNodeRect.y - stateNodeRect.height;
        if (stateNode.currentState != null)
        {
            targetY += (index * 100);
        }

        stateNodeRect.y = targetY;

        TransitionEditorNode transitionNode = ScriptableObject.CreateInstance<TransitionEditorNode>();
        transitionNode.Init(stateNode, transition);
        transitionNode.windowRect = new Rect(stateNodeRect.x + 200 + 100, stateNodeRect.y + (stateNodeRect.height * 0.7f), 200, 80);
        transitionNode.windowTitle = "Condition Check";
        windows.Add(transitionNode);
        stateNode.dependecies.Add(transitionNode);
        return transitionNode;
    }

    public static void DrawNodeCurve(Rect start, Rect end, bool left, Color curveColor)
    {
        Vector2 startPosition = new Vector2(
            (left) ? start.x + start.width : start.x,
            start.y + (start.height * 0.5f));

        Vector2 endPosition = new Vector2(end.x + (end.width * 0.5f), end.y + (end.height * 0.5f));
        Vector2 startTransition = startPosition + Vector2.right * 50;
        Vector2 endTransition = endPosition + Vector2.left * 50;

        Color shadow = Color.black;

        for (int i = 0; i < 3; i++)
        {
            Handles.DrawBezier(startPosition, endPosition, startTransition, endTransition, shadow, null, 1);
        }
        Handles.DrawBezier(startPosition, endPosition, startTransition, endTransition, curveColor, null, 1);
    }
    #endregion
}