using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AnalysisSO : ScriptableObject
{
    [Header("Clip to Analyse-------")]
    [Tooltip("Clip that should be analysed.")]
    public AudioClip Clip;

    protected List<int> resultList;
    [HideInInspector] 
    public List<int> ResultList { get => resultList; }

    [ShowNonSerializedField] 
    protected bool hasBeenAnalysedOnce;

    protected string lastClipName = null;

    protected float[] spectrum = null;


    [Button]
    protected virtual void AnalyseClip()
    {
        throw new System.NotImplementedException();
    }

    
    protected virtual void OnValidate()
    {
        if(Clip == null)
        {
            hasBeenAnalysedOnce = false;
        }
        else if(lastClipName != Clip.name)
        {
            hasBeenAnalysedOnce = false;
            lastClipName = Clip.name;
        }
    }
}