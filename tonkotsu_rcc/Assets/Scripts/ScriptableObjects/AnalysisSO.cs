using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public abstract class AnalysisSO : ScriptableObject
{
    [Header("Clip to Analyse-------")]
    [Tooltip("Clip that should be analysed.")]
    public AudioClip Clip;

    protected List<int> resultList;

    [ShowNonSerializedField] 
    protected bool analysed;

    protected string lastClipName = null;

    protected float[] spectrum = null;
    public float[] Spectrum { get => spectrum; }

    public List<int> ResultList { get => resultList; }

    protected abstract void AnalyseClip();
    
    protected virtual void OnValidate()
    {
        if(Clip == null)
        {
            analysed = false;
        }
        else if(lastClipName != Clip.name)
        {
            analysed = false;
            lastClipName = Clip.name;
        }
    }
}