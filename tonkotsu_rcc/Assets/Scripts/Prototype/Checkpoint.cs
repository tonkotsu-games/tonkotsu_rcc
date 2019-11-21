using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Checkpoint : MonoBehaviour
{
    [Button]
    [CheatMethod]
    public void TeleportPlayer()
    {
        PlayerHandler.Player.position = transform.position;
    }
}
