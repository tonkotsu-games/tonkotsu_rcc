using UnityEngine;

[System.Serializable]
public struct InputPackage
{
    public Vector2 LeftStick;
    public Vector2 RightStick;
    public Vector2 DPad;

    public float LT;
    public float RT;

    public bool LB;
    public bool RB;

    public bool A;
    public bool B;
    public bool X;
    public bool Y;

    public bool LeftStickButton;
    public bool RightStickButton;

    public bool Start;
    public bool Back;

    public static InputPackage Empty { get => new InputPackage(); }

    public bool LeftStickMoved()
    {
        return LeftStick.magnitude > 0.1f;
    }

    public bool RightStickMoved()
    {
        return RightStick.magnitude > 0.1f;
    }

    public bool DPadMoved()
    {
        return DPad.magnitude > 0.1f;
    }

    public bool LTPressed()
    {
        return LT > 0.3f;
    }

    public bool RTPressed()
    {
        return RT > 0.3f;
    }
}