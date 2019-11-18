using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerController : BeatBehaviour
{
    [BoxGroup("Camera")]
    [Required]
    [SerializeField] new Camera camera;

    [BoxGroup("PlayerController")]
    [Required]
    [SerializeField] VirtualController virtualController;

    [BoxGroup("PlayerController")]
    [SerializeField] float force, rayCastMaxDist;

    [BoxGroup("PlayerController")]
    [SerializeField] float walkVelocity;

    [BoxGroup("PlayerController")]
    [ReadOnly]
    [SerializeField] State state;

    new Rigidbody rigidbody;
    Vector3 camStartingOffset;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        camStartingOffset = camera.transform.position - transform.position;
    }

    protected override void Update()
    {
        var input =  virtualController.GetPackage();

        UpdateRotation();

        if (state != State.Attack)
        {
            UpdateMovement(input.LeftStick);
        }



    }

    private void LateUpdate()
    {
        camera.transform.position = rigidbody.position + camStartingOffset;
    }

    protected override void OnBeatRangeStay()
    {
        if(state == State.Idle || state == State.Move)
        {
            if (virtualController.GetPackage().A)
            {
                //attack
            }
        }
    }


    private void UpdateMovement(Vector2 input)
    {
        Vector3 camForward = camera.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = camera.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 inputDir = camForward*input.y + camRight*input.x;
        Ray r = new Ray(transform.position, inputDir);

        bool hit = Physics.Raycast(r, rayCastMaxDist);

        if (hit)
        {
            Debug.DrawLine(r.origin, r.origin + r.direction, Color.red, 1f);
        }
        else
        {
            rigidbody.AddForce(inputDir * force);
        }

        Vector3 xzVel = rigidbody.velocity;
        xzVel.y = 0;

        if(xzVel.magnitude > walkVelocity)
        {
            xzVel = xzVel.normalized * walkVelocity;
            rigidbody.velocity = new Vector3(xzVel.x, rigidbody.velocity.y, xzVel.z);
        }
        
    }

    private void UpdateRotation()
    {
        Vector3 forward = rigidbody.velocity;
        forward.y = 0;

        if(forward.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.identity;
            float angle = Vector3.Angle(Vector3.forward, forward); 
            transform.Rotate(Vector3.up, (forward.x>0 ? angle : 360 - angle));
        }
    }


    public enum State
    {
        Idle,
        Move,
        Attack
    }

}
