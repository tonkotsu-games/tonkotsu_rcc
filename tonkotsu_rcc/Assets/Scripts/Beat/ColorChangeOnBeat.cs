using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ColorChangeOnBeat : BeatBehaviour
{
    [SerializeField] Gradient colorGradiant;

    MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    protected override void OnBeatRangeStay()
    {
         Color c = colorGradiant.Evaluate(beatRangeCloseness);
        meshRenderer.material.SetColor("_BaseColor", c);
    }

}
