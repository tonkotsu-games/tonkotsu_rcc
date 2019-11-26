﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BehaviorEditor : EditorWindow
{
    #region Variables
    Vector2 mousePosition;
    bool clickedOnWindow;
    int selectedIndex;
    int transit;
    BaseEditorNodes selectedNode;
    static Texture2D texture;
    Rect mouseRect = new Rect(0,0,1,1);

    public static EditorSettings editorSettings;
    

    public enum UserActions
    {
        AddNode,
        AddTransition,
        DeleteNode,
        AddCommentNode,
        MakeTransition,
        MakePortal
    }
    #endregion

    #region Init
    [MenuItem("Behavior Editor/Editor")]
    static void ShowEditor()
    {
        BehaviorEditor editor = EditorWindow.GetWindow<BehaviorEditor>();
        editor.minSize = new Vector2(800, 600);

        texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        texture.SetPixel(0, 0, new Color(.149f, .1843f, .2314f));
        texture.Apply();
    }

    private void OnEnable()
    {
        editorSettings = Resources.Load("EditorSettings") as EditorSettings;
    }

    #endregion

    #region GUI Methods
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), texture, ScaleMode.StretchToFill);
        Event events = Event.current;
        mousePosition = events.mousePosition;
        UserInput(events);
        DrawWindows();

        if (events.type == EventType.MouseDrag)
        {
            editorSettings.currentGraph.DeleteWindowIfNeeded();
            Repaint();
        }
        if(GUI.changed)
        {
            editorSettings.currentGraph.DeleteWindowIfNeeded();
            Repaint();
        }

        if(editorSettings.makeTransition)
        {
            mouseRect.x = mousePosition.x;
            mouseRect.y = mousePosition.y;
            Rect startNode = editorSettings.currentGraph.GetNodeWithIndex(transit).windowRect;
            DrawNodeCurve(startNode, mouseRect, true, Color.red);
            Repaint();
        }

        if (GUI.Button(new Rect(10, 10, 100, 20), "Clear Me"))
        {
            ClearMe();
        }
    }

    void DrawWindows()
    {
        BeginWindows();
        EditorGUILayout.LabelField(" ", GUILayout.Width(100));
        EditorGUILayout.LabelField("Assign Graph ", GUILayout.Width(100));
        editorSettings.currentGraph = (BehaviorGraph)EditorGUILayout.ObjectField(editorSettings.currentGraph, typeof(BehaviorGraph), false, GUILayout.Width(200));

        if (editorSettings.currentGraph != null)
        {
            foreach (BaseEditorNodes node in editorSettings.currentGraph.windows)
            {
                node.DrawCurve();
            }
            for (int i = 0; i < editorSettings.currentGraph.windows.Count; i++)
            {
                editorSettings.currentGraph.windows[i].windowRect = GUI.Window(i, editorSettings.currentGraph.windows[i].windowRect,
                                                                               DrawNodeWindow, editorSettings.currentGraph.windows[i].windowTitle);
            }
        }
        EndWindows();
    }

    void DrawNodeWindow(int id)
    {
        editorSettings.currentGraph.windows[id].DrawWindow();
        GUI.DragWindow();
    }

    void UserInput(Event events)
    {
        if (editorSettings.currentGraph == null)
        {
            return;
        }
        if (events.button == 1 && !editorSettings.makeTransition)
        {
            if (events.type == EventType.MouseDown)
            {
                RightClick(events);
            }
        }

        if (events.button == 0 && !editorSettings.makeTransition)
        {
            if (events.type == EventType.MouseDown)
            {

            }
        }

        if(events.button ==  0 && editorSettings.makeTransition)
        {
            if(events.type == EventType.MouseDown)
            {
                MakeTransition();
            }
        }
    }

    void RightClick(Event events)
    {

        selectedIndex = -1;
        for (int i = 0; i < editorSettings.currentGraph.windows.Count; i++)
        {
            if (editorSettings.currentGraph.windows[i].windowRect.Contains(events.mousePosition))
            {
                clickedOnWindow = true;
                selectedNode = editorSettings.currentGraph.windows[i];
                selectedIndex = i;
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

    void MakeTransition()
    {
        editorSettings.makeTransition = false;
        clickedOnWindow = false;
        for (int i = 0; i < editorSettings.currentGraph.windows.Count; i++)
        {
            if (editorSettings.currentGraph.windows[i].windowRect.Contains(mousePosition))
            {
                clickedOnWindow = true;
                selectedNode = editorSettings.currentGraph.windows[i];
                selectedIndex = i;
                break;
            }
        }
        if(clickedOnWindow)
        {
            if(selectedNode.drawNode is StateEditorNode || selectedNode.drawNode is PortalEditorNode)
            {
                if(selectedNode.id != transit)
                {
                    BaseEditorNodes transNode = editorSettings.currentGraph.GetNodeWithIndex(transit);
                    transNode.targetNode = selectedNode.id;

                    BaseEditorNodes enterNode = editorSettings.currentGraph.GetNodeWithIndex(transNode.enterNode);
                    Transition transition = enterNode.stateReferences.currentState.GetTransition(transNode.transitionReference.transitionId);
                    transition.targetState = selectedNode.stateReferences.currentState;
                }
            }
        }
    }

    #endregion

    #region Context Menu

    void AddNewNode(Event events)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddSeparator("");
        if (editorSettings.currentGraph != null)
        {
            menu.AddItem(new GUIContent("Add State"), false, CallBack, UserActions.AddNode);
            menu.AddItem(new GUIContent("Add Portal"), false, CallBack, UserActions.MakePortal);
            menu.AddItem(new GUIContent("Add Comment"), false, CallBack, UserActions.AddCommentNode);
        }
        else
        {
            menu.AddDisabledItem(new GUIContent("Add State"));
            menu.AddDisabledItem(new GUIContent("Add Comment"));
        }
        menu.ShowAsContext();
        events.Use();
    }

    void ModifyNode(Event events)
    {
        GenericMenu menu = new GenericMenu();
        if (selectedNode.drawNode is StateEditorNode)
        {
            if (selectedNode.stateReferences.currentState != null)
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

        if (selectedNode.drawNode is PortalEditorNode)
        {
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Delete Node"), false, CallBack, UserActions.DeleteNode);
        }


        if (selectedNode.drawNode is CommentEditorNode)
        {
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Delete Node"), false, CallBack, UserActions.DeleteNode);
        }

        if (selectedNode.drawNode is TransitionEditorNode)
        {
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Make Transition"), false, CallBack, UserActions.MakeTransition);

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
                editorSettings.AddNodeOnGraph(editorSettings.stateNode, 200, 100, "State", mousePosition);
                break;
            case UserActions.AddTransition:
                AddTransitionNode(selectedNode, mousePosition);
                break;
            case UserActions.MakeTransition:
                transit = selectedNode.id;
                editorSettings.makeTransition = true;
                break;
            case UserActions.MakePortal:
                editorSettings.AddNodeOnGraph(editorSettings.portalNode, 200, 100, "Portal", mousePosition);
                break;
            case UserActions.AddCommentNode:
                BaseEditorNodes commentNode = editorSettings.AddNodeOnGraph(editorSettings.commentNode, 200, 100, "Comment", mousePosition);
                commentNode.comment = "This is a Comment";
                break;
            case UserActions.DeleteNode:
                if(selectedNode.drawNode is TransitionEditorNode)
                {
                    BaseEditorNodes enterNode = editorSettings.currentGraph.GetNodeWithIndex(selectedNode.enterNode);
                    enterNode.stateReferences.currentState.RemoveTransition(selectedNode.transitionReference.transitionId);
                }
                editorSettings.currentGraph.DeleteNode(selectedNode.id);
                break;
        }
        EditorUtility.SetDirty(editorSettings);
    }

    public static BaseEditorNodes AddTransitionNode(BaseEditorNodes enterNode, Vector2 position)
    {
        BaseEditorNodes transNode = editorSettings.AddNodeOnGraph(editorSettings.transitionNode, 200, 100, "Condition", position);
        transNode.enterNode = enterNode.id;
        Transition transition = editorSettings.stateNode.AddTransition(enterNode);
        transNode.transitionReference.transitionId = transition.id;
        return transNode;
    }

    public static BaseEditorNodes AddTransitionNodeFromTransition(Transition transition, BaseEditorNodes enterNode, Vector2 position)
    {
        BaseEditorNodes transNode = editorSettings.AddNodeOnGraph(editorSettings.transitionNode, 200, 100, "Condition", position);
        transNode.enterNode = enterNode.id;
        transNode.transitionReference.transitionId = transition.id;
        return transNode;
    }

    public void ClearMe()
    {
        editorSettings.currentGraph.windows.Clear();
    }

    public static void ClearList(List<BaseEditorNodes> stateList)
    {
        for (int i = 0; i < stateList.Count; i++)
        {
            if (editorSettings.currentGraph.windows.Contains(stateList[i]))
            {
                editorSettings.currentGraph.windows.Remove(stateList[i]);
            }
        }
    }
    #endregion

    #region Helper Methods

    public static void DrawNodeCurve(Rect start, Rect end, bool left, Color curveColor)
    {
        Vector2 startPosition = new Vector2(
            (left) ? start.x + start.width : start.x,
            start.y + 50);
        Vector2 endPosition;
        if (left)
        {
            endPosition = new Vector2(end.x + (end.width * 0.5f), end.y + (end.height * 0.5f));
        }
        else
        {
            endPosition = new Vector2(end.x + (end.width * 0.5f), end.y + 50);
        }
        Vector2 startTransition = startPosition + Vector2.right * 50;
        Vector2 endTransition = endPosition + Vector2.left * 50;

        Color shadow = Color.black;

        Handles.DrawBezier(startPosition, endPosition, startTransition, endTransition, curveColor, null, 2.5f);
    }

    //public static StateEditorNode AddStateEditorNode(Vector2 position)
    //{
    //    StateEditorNode stateNode = ScriptableObject.CreateInstance<StateEditorNode>();
    //    stateNode.windowRect = new Rect(position.x, position.y, 200, 300);
    //    stateNode.windowTitle = "State";
    //    editorSettings.currentGraph.windows.Add(stateNode);
    //    //currentGraph.SetStateNode(stateNode);
    //    return stateNode;
    //}
    //
    //public static CommentEditorNode AddComment(Vector2 position)
    //{
    //    CommentEditorNode commentNode = ScriptableObject.CreateInstance<CommentEditorNode>();
    //    commentNode.windowRect = new Rect(position.x, position.y, 200, 100);
    //    commentNode.windowTitle = "Comment";
    //    editorSettings.currentGraph.windows.Add(commentNode);
    //    return commentNode;
    //}
    //
    //public static TransitionEditorNode AddTransitionNode(int index, Transition transition, StateEditorNode stateNode)
    //{
    //    Rect stateNodeRect = stateNode.windowRect;
    //    stateNodeRect.y += 50;
    //    float targetY = stateNodeRect.y - stateNodeRect.height;
    //    if (stateNode.currentState != null)
    //    {
    //        targetY += (index * 100);
    //    }
    //
    //    stateNodeRect.y = targetY;
    //    stateNodeRect.x += 200 + 100;
    //    stateNodeRect.y += (stateNodeRect.height * 0.7f);
    //
    //    Vector2 position = new Vector2(stateNodeRect.x, stateNodeRect.y);
    //
    //    return AddTransition(position, transition, stateNode);
    //
    //}
    //
    //public static TransitionEditorNode AddTransition(Vector2 position, Transition transition, StateEditorNode stateNode)
    //{
    //    TransitionEditorNode transitionNode = ScriptableObject.CreateInstance<TransitionEditorNode>();
    //    transitionNode.Init(stateNode, transition);
    //    transitionNode.windowRect = new Rect(position.x, position.y, 200, 80);
    //    transitionNode.windowTitle = "Condition Check";
    //    editorSettings.currentGraph.windows.Add(transitionNode);
    //    stateNode.dependecies.Add(transitionNode);
    //    return transitionNode;
    //
    //}


    #endregion
}