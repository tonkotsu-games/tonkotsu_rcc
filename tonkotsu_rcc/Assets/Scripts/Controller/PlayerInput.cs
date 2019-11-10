using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IInputProvider
{
    public InputPackage GetPackage()
    {
        InputPackage input = new InputPackage();

        input.LeftStick = GetVectorFromAxis("Horizontal", "Vertical");
        input.RightStick = GetVectorFromAxis("CameraHorizontal", "CameraVertical");
        input.DPad = GetVectorFromAxis("CrossHorizontal", "CrossVertical");

        input.LT = GetAxis("TriggerLeft");
        input.RT = GetAxis("TriggerRight");

        input.LB = GetButton("LeftBumper");
        input.RB = GetButton("RightBumper");

        input.A = GetButton("A");
        input.B = GetButton("B");
        input.X = GetButton("X");
        input.Y = GetButton("Y");

        input.Start = GetButton("Start");
        input.Back = GetButton("Select");

        return input;
    }

    private Vector2 GetVectorFromAxis(string a1, string a2)
    {
        return new Vector2(GetAxis(a1), GetAxis(a2));
    }

    private float GetAxis(string inputStr)
    {
        return Input.GetAxisRaw(inputStr);
    }

    private bool GetButton(string inputStr)
    {
        return Input.GetButton(inputStr);
    }

}