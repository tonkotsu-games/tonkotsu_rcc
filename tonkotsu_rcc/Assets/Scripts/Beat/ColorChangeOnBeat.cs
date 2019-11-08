using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeOnBeat : BeatBehaviour
{

    MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    protected override void OnBeat()
    {
        meshRenderer.material.color = Color.HSVToRGB(Random.value, 1, 1);
    }
}
