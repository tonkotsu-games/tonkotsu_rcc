using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class VirtualController : MonoBehaviour
{
    [SerializeField] VirtualControllerInputType startingInputType;

    IInputProvider currentProvider;
    InputPackage currentPackage;

    [ReadOnly]
    [SerializeField] VirtualControllerInputType currentType;

    private void Start()
    {
        if(currentType == VirtualControllerInputType.None)
        {
            ChangeInputType(startingInputType);
        }
    }

    private void Update()
    {
        currentPackage = currentProvider.GetPackage();
    }

    public void ChangeInputType( VirtualControllerInputType newType)
    {
        currentType = newType;
        currentProvider = newType.GetProvider();
    }

    public InputPackage GetPackage()
    {
        return currentPackage;
    }

    public void DrawGUI(Texture2D[] controllerTextures)
    {

        GUI.DrawTexture(new Rect(35, 800, 455, 275), controllerTextures[0]);
        GUI.DrawTexture(new Rect(245, 820, 30, 30), controllerTextures[10]);

        if (currentPackage.LeftStickMoved())
        {
            ShowAction(115, 850, 70, 70, controllerTextures[1]);
        }
        else
        {
            GUI.DrawTexture(new Rect(115, 850, 70, 70), controllerTextures[1]);
        }

        if (currentPackage.LeftStickMoved())
        {
            ShowAction(130 + (currentPackage.LeftStick.x * 15), 865 - (currentPackage.LeftStick.y * 15), 40, 40, controllerTextures[2]);
        }
        else
        {
            GUI.DrawTexture(new Rect(130, 865, 40, 40), controllerTextures[2]);
        }

        if (currentPackage.RightStickMoved())
        {
            ShowAction(280, 915, 70, 70, controllerTextures[1]);
        }
        else
        {
            GUI.DrawTexture(new Rect(280, 915, 70, 70), controllerTextures[1]);
        }

        if (currentPackage.RightStickMoved())
        {
            ShowAction(295 + (currentPackage.RightStick.x * 15), 930 + (currentPackage.RightStick.y * 15), 40, 40, controllerTextures[2]);
        }
        else
        {
            GUI.DrawTexture(new Rect(295, 930, 40, 40), controllerTextures[2]);
        }

        if (currentPackage.DPadMoved())
        {
            if (currentPackage.DPad.x >= 0.1f)
            {
                ShowAction(170, 910, 70, 70, controllerTextures[6]);
            }
            if (currentPackage.DPad.y <= -0.1f)
            {
                ShowAction(170, 910, 70, 70, controllerTextures[5]);
            }
            if (currentPackage.DPad.y >= 0.1f)
            {
                ShowAction(170, 910, 70, 70, controllerTextures[7]);
            }
            if (currentPackage.DPad.y <= -0.1f)
            {
                ShowAction(170, 910, 70, 70, controllerTextures[4]);
            }
        }
        else
        {
            GUI.DrawTexture(new Rect(170, 910, 70, 70), controllerTextures[3]);
        }

        if (currentPackage.X)
        {
            ShowAction(335, 870, 30, 30, controllerTextures[8]);
        }
        else
        {
            GUI.DrawTexture(new Rect(335, 870, 30, 30), controllerTextures[8]);
        }

        if (currentPackage.A)
        {
            ShowAction(365, 895, 30, 30, controllerTextures[8]);
        }
        else
        {
            GUI.DrawTexture(new Rect(365, 895, 30, 30), controllerTextures[8]);
        }

        if (currentPackage.B)
        {
            ShowAction(395, 870, 30, 30, controllerTextures[8]);
        }
        else
        {
            GUI.DrawTexture(new Rect(395, 870, 30, 30), controllerTextures[8]);
        }

        if (currentPackage.Y)
        {
            ShowAction(365, 840, 30, 30, controllerTextures[8]);
        }
        else
        {
            GUI.DrawTexture(new Rect(365, 840, 30, 30), controllerTextures[8]);
        }

        if (currentPackage.Back)
        {
            ShowAction(220, 875, 20, 20, controllerTextures[9]);
        }
        else
        {
            GUI.DrawTexture(new Rect(220, 875, 20, 20), controllerTextures[9]);
        }

        if (currentPackage.Start)
        {
            ShowAction(280, 875, 20, 20, controllerTextures[9]);
        }
        else
        {
            GUI.DrawTexture(new Rect(280, 875, 20, 20), controllerTextures[9]);
        }

        if (currentPackage.LB)
        {
            ShowAction(110, 765, 75, 40, controllerTextures[11]);
        }
        else
        {
            GUI.DrawTexture(new Rect(110, 765, 75, 40), controllerTextures[11]);
        }

        if (currentPackage.RB)
        {
            ShowAction(335, 765, 75, 40, controllerTextures[12]);
        }
        else
        {
            GUI.DrawTexture(new Rect(335, 765, 75, 40), controllerTextures[12]);
        }

        if (currentPackage.LT >= 0.4f)
        {
            ShowAction(130, 710, 50, 65, controllerTextures[13]);
        }
        else
        {
            GUI.DrawTexture(new Rect(130, 710, 50, 65), controllerTextures[13]);
        }

        if (currentPackage.RT >= 0.4f)
        {
            ShowAction(340, 710, 50, 65, controllerTextures[14]);
        }
        else
        {
            GUI.DrawTexture(new Rect(340, 710, 50, 65), controllerTextures[14]);
        }
    }

    private void ShowAction(float leftBound, float upBound, float width, float hight, Texture2D image)
    {
        GUI.DrawTexture(new Rect(leftBound, upBound, width, hight), image, ScaleMode.ScaleToFit, true, 0, Color.red, 0, 0);
    }

}

public enum VirtualControllerInputType
{
    None,
    Player
}

public static class VirtualControllerInputTypeMethods
{
    public static IInputProvider GetProvider(this VirtualControllerInputType type)
    {
        switch (type)
        {
            case VirtualControllerInputType.Player:
                return new PlayerInput();

            default:
                return new EmptyInput();
        }
    }
}

