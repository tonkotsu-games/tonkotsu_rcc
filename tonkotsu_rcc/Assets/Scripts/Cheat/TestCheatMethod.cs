using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCheatMethod : MonoBehaviour
{
    [CheatMethod] 
    void Cheat()
    {
        Debug.LogError("Cheater!");
    }

    [CheatMethod] 
    public void Cheat2()
    {
        Debug.LogError("Cheater!");
    }
}
