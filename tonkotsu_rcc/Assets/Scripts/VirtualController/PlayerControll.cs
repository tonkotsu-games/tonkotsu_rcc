using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private InputPackage inputPackage;
    private Rigidbody rigi;

    public InputPackage InputPackage { get => inputPackage; set => inputPackage = value; }

    [SerializeField] private float movementSpeed = 0;

    private Vector3 moveVector;

    private void Start()
    {        
        rigi = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (inputPackage != null)
        {
            MovementCalculation();
            Move();
        }
    }

    private void MovementCalculation()
    {
        float move = new Vector2(inputPackage.MoveHorizontal, inputPackage.MoveVertical).magnitude;
        if(move > 1)
        {
            move = 1;
        }
        moveVector = new Vector3(inputPackage.MoveHorizontal, 
                                 0f, 
                                 inputPackage.MoveVertical);

        moveVector = moveVector.normalized * move * movementSpeed;
    }

    private void Move()
    {
        rigi.velocity = new Vector3(moveVector.x, 
                                    0f, 
                                    moveVector.z);
    }
}