using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
    public int id;
    public Condition condition;
    public  State targetState;
    public bool disable;
}