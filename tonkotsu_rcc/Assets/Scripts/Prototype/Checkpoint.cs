using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    [CheatMethod]
    public void TeleportPlayer()
    {
        PlayerHandler.Player.position = transform.position;
    }
}
