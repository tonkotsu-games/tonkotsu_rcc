using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] VirtualController virtualController;
    [SerializeField] float movementSpeed = 0;

    private new Rigidbody rigidbody;
    private Vector3 moveVector;

    private void Start()
    {        
        rigidbody = GetComponent<Rigidbody>();

        
    }

    private void FixedUpdate()
    {
        InputPackage input;
        if(virtualController != null)
        {
            input = virtualController.GetPackage();
        }
        else
        {
            input = InputPackage.Empty;
        }

        MovementCalculation(input);
        Move();
    }

    private void MovementCalculation(InputPackage input)
    {
        float move = Mathf.Clamp01(input.LeftStick.magnitude);
        Vector3 inputDir = new Vector3(input.LeftStick.x, 0f, input.LeftStick.y);
        //this still produces wrong results with stick values close to 0
        moveVector = inputDir.normalized * move * movementSpeed;
    }

    private void Move()
    {
        rigidbody.velocity = new Vector3(moveVector.x, 0f, moveVector.z);
    }
}