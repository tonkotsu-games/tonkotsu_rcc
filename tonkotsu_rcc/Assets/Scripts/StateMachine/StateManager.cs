using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;

    [HideInInspector]
    public float delta;
    [HideInInspector]
    public Transform transform;

    private void Start()
    {
        transform = gameObject.transform;
    }

    private void Update()
    {
        if(currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
}