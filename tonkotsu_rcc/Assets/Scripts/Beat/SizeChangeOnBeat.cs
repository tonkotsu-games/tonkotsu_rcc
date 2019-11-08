using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SizeChangeOnBeat : BeatBehaviour
{
    [BoxGroup("SizeChangeOnBeat")]
    [SerializeField] Vector3 normalScale, onBeatScale;

    protected override void OnBeatRangeStay()
    {
        transform.localScale = Vector3.Lerp(normalScale, onBeatScale, beatRangeCloseness);
    }

    protected override void OnBeatRangeExit()
    {
        transform.localScale = normalScale;
    }

}
