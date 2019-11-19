using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "ManualBeatAnalysis", menuName = "Analysis/ManualBeat")]
public class ManualBeatAnalysisSO : AnalysisSO
{
    [SerializeField]
    int samplePerBeat, initialSampleOffset;


    protected override List<int> AnalyseClip()
    {
        var results = new List<int>();
        int amount = Clip.samples;
        spectrum = new float[amount];

        Clip.GetData(spectrum, 0);

        for (int i = 0; i < spectrum.Length; i++)
        {
            spectrum[i] = Mathf.Abs(spectrum[i]);
        }

        for (int i = initialSampleOffset; i < Clip.samples; i+= samplePerBeat)
        {
            results.Add(i);
        }

        analysed = true;
        lastClipName = Clip.name;

        Debug.Log("Result: " + results.Count);

        return results as List<int>;
    }
}
