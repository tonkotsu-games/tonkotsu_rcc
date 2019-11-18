using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BeatBehaviour
{

    Rigidbody rigidbody;

    [SerializeField] float velocity;
    [SerializeField] VirtualController virtualController;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
       var input =  virtualController.GetPackage();


        UpdateMovement(input.LeftStick);



    }


    private void UpdateMovement(Vector2 input)
    {



    }


}
