using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selfdestruct : MonoBehaviour
{
    [SerializeField] float secondsBeforeDestruction;

    private void Start()
    {
        Destroy(gameObject, secondsBeforeDestruction);
    }
}
