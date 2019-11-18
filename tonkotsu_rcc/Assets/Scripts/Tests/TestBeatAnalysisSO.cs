using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBeatAnalysisSO : MonoBehaviour
{
    [SerializeField] BeatAnalysisSO beatCalculated;
 
    void Start()
    {
        Debug.LogError("Testing BeatAnalysisSO, Beats found: " + beatCalculated.ResultList.Count);
    }
}
