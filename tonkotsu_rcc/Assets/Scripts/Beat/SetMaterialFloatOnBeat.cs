using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterialFloatOnBeat : BeatBehaviour
{
    [SerializeField] Material materialToEffect;
    [SerializeField] string variableReferenceName;


    protected override void OnBeatRangeStay()
    {
        materialToEffect.SetFloat(variableReferenceName, beatRangeCloseness);
    }

    protected override void OnBeatRangeExit()
    {
        materialToEffect.SetFloat(variableReferenceName, 0);
    }
}
