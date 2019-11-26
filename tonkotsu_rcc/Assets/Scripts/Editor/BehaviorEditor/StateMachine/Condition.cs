using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : ScriptableObject
{
    public string description;

    public abstract bool checkCondition(StateManager state);
}