using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SizeChangeOnBeat : BeatBehaviour
{
    [BoxGroup("SizeChangeOnBeat")]
    [SerializeField] Vector3 normalScale, onBeatScale;

    protected override void OnBeatRangeEnter()
    {
        transform.localScale = onBeatScale;
    }

    protected override void OnBeatRangeExit()
    {
        transform.localScale = normalScale;
    }

}
