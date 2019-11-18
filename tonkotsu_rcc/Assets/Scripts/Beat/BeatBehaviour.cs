using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public abstract class BeatBehaviour : MonoBehaviour
{
    [BoxGroup("BeatBehaviour")]
    [Tooltip("In what sample-range should OnBeatRange be called?")]
    [SerializeField] protected int onBeatRangeWindow;

    [BoxGroup("BeatBehaviour")]
    [Tooltip("How many samples ahed is the OnBeatRange called? Shifts the OnBeatRange window forward in time")]
    [SerializeField] protected int onBeatRangeDelay;

    protected float beatRangeCloseness;
    private bool isInBeatRange = false;

    protected virtual void Awake()
    {
        //subscribe to BeatHandler BeatHit event that doesnt exist right now with OnBeat
        //BeatHandler.BeatHit += OnBeat;
    }

    protected virtual void OnDestroy()
    {
        //unsubscribe to BeatHandler BeatHit event
        ////BeatHandler.BeatHit += OnBeat;
    }

    protected virtual void Update()
    {

        beatRangeCloseness = BeatHandler.BeatRangePercent(onBeatRangeDelay, onBeatRangeWindow);

        if (beatRangeCloseness > 0)
        {
            if (isInBeatRange)
            {
                OnBeatRangeStay();
            }
            else
            {
                isInBeatRange = true;
                OnBeatRangeEnter();
            }

        }
        else
        {
            if (isInBeatRange)
            {
                isInBeatRange = false;
                OnBeatRangeExit();
            }
        }
    }

    protected virtual void OnBeat() { }

    protected virtual void OnBeatRangeEnter() { }

    protected virtual void OnBeatRangeStay() { }

    protected virtual void OnBeatRangeExit() { }


}
