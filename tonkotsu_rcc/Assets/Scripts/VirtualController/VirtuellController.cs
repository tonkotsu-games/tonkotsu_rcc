using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class VirtuellController : MonoBehaviour
{
    IProvider inputProvider;

    InputPackage inputPackage;

    [SerializeField] List<Texture2D> controller;

    private static bool drawController = false;

    private void Awake()
    {
        inputProvider = gameObject.GetComponent<PlayerInput>();
    }

    public static void DrawController()
    {
            drawController = !drawController;
    }

    private void OnGUI()
    {
        if (drawController)
        {
            DrawGUI();
        }
    }

    public void DrawGUI()
    {
        inputPackage = inputProvider.GetPackage();

        GUI.DrawTexture(new Rect(35, 800, 455, 275), controller[0]);
        GUI.DrawTexture(new Rect(245, 820, 30, 30), controller[10]);

        if (inputPackage.MoveButton)
        {
            ShowAction(115, 850, 70, 70, controller[1]);
        }
        else
        {
            GUI.DrawTexture(new Rect(115, 850, 70, 70), controller[1]);
        }
        if (inputPackage.MoveHorizontal >= 0.1f ||
           inputPackage.MoveHorizontal <= -0.1f ||
           inputPackage.MoveVertical >= 0.1f ||
           inputPackage.MoveVertical <= -0.1f)
        {
            ShowAction(130 + (inputPackage.MoveHorizontal * 15), 865 - (inputPackage.MoveVertical * 15), 40, 40, controller[2]);
        }
        else
        {
            GUI.DrawTexture(new Rect(130, 865, 40, 40), controller[2]);
        }
        if (inputPackage.CameraButton)
        {
            ShowAction(280, 915, 70, 70, controller[1]);
        }
        else
        {
            GUI.DrawTexture(new Rect(280, 915, 70, 70), controller[1]);
        }
        if (inputPackage.CameraHorizontal >= 0.1f ||
            inputPackage.CameraHorizontal <= -0.1f ||
            inputPackage.CameraVertical >= 0.1f ||
            inputPackage.CameraVertical <= -0.1f)
        {
            ShowAction(295 + (inputPackage.CameraHorizontal * 15), 930 + (inputPackage.CameraVertical * 15), 40, 40, controller[2]);
        }
        else
        {
            GUI.DrawTexture(new Rect(295, 930, 40, 40), controller[2]);
        }
        if (inputPackage.CrossHorizontal >= 0.1f ||
            inputPackage.CrossHorizontal <= -0.1f ||
            inputPackage.CrossVertical >= 0.1f ||
            inputPackage.CrossVertical <= -0.1f)
        {
            if (inputPackage.CrossHorizontal >= 0.1f)
            {
                ShowAction(170, 910, 70, 70, controller[6]);
            }
            if (inputPackage.CrossHorizontal <= -0.1f)
            {
                ShowAction(170, 910, 70, 70, controller[5]);
            }
            if (inputPackage.CrossVertical >= 0.1f)
            {
                ShowAction(170, 910, 70, 70, controller[7]);
            }
            if (inputPackage.CrossVertical <= -0.1f)
            {
                ShowAction(170, 910, 70, 70, controller[4]);
            }
        }
        else
        {
            GUI.DrawTexture(new Rect(170, 910, 70, 70), controller[3]);
        }
        if (inputPackage.InputX)
        {
            ShowAction(335, 870, 30, 30, controller[8]);
        }
        else
        {
            GUI.DrawTexture(new Rect(335, 870, 30, 30), controller[8]);
        }
        if (inputPackage.InputA)
        {
            ShowAction(365, 895, 30, 30, controller[8]);
        }
        else
        {
            GUI.DrawTexture(new Rect(365, 895, 30, 30), controller[8]);
        }
        if (inputPackage.InputB)
        {
            ShowAction(395, 870, 30, 30, controller[8]);
        }
        else
        {
            GUI.DrawTexture(new Rect(395, 870, 30, 30), controller[8]);
        }
        if (inputPackage.InputY)
        {
            ShowAction(365, 840, 30, 30, controller[8]);
        }
        else
        {
            GUI.DrawTexture(new Rect(365, 840, 30, 30), controller[8]);
        }
        if (inputPackage.SelectButton)
        {
            ShowAction(220, 875, 20, 20, controller[9]);
        }
        else
        {
            GUI.DrawTexture(new Rect(220, 875, 20, 20), controller[9]);
        }
        if (inputPackage.StartButton)
        {
            ShowAction(280, 875, 20, 20, controller[9]);
        }
        else
        {
            GUI.DrawTexture(new Rect(280, 875, 20, 20), controller[9]);
        }
        if (inputPackage.BumberLeft)
        {
            ShowAction(110, 765, 75, 40, controller[11]);
        }
        else
        {
            GUI.DrawTexture(new Rect(110, 765, 75, 40), controller[11]);
        }
        if (inputPackage.BumberRight)
        {
            ShowAction(335, 765, 75, 40, controller[12]);
        }
        else
        {
            GUI.DrawTexture(new Rect(335, 765, 75, 40), controller[12]);
        }
        if (inputPackage.TriggerLeft >= 0.4f)
        {
            ShowAction(130, 710, 50, 65, controller[13]);
        }
        else
        {
            GUI.DrawTexture(new Rect(130, 710, 50, 65), controller[13]);
        }
        if (inputPackage.TriggerRight >= 0.4f)
        {
            ShowAction(340, 710, 50, 65, controller[14]);
        }
        else
        {
            GUI.DrawTexture(new Rect(340, 710, 50, 65), controller[14]);
        }
    }

    private void ShowAction(float leftBound, float upBound, float width, float hight, Texture2D image)
    {
        GUI.DrawTexture(new Rect(leftBound, upBound, width, hight), image, ScaleMode.ScaleToFit, true, 0, Color.red, 0, 0);
    }
}