using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "BeatAnalysis", menuName = "Analysis/Beat")]
public class BeatAnalysisSO : AnalysisSO
{
    [SerializeField]
    private float limit = 0.4f, waitSamples = 5000f;

    [Button]
    protected override void AnalyseClip()
    {
        if(Clip == null)
        {
            analysed = false;
            return;
        }

        Debug.Log("Beat Analysed");

        //Analysis
        resultList = new List<int>();
        int amount = Clip.samples;
        spectrum = new float[amount];
        Clip.GetData(spectrum, 0);

        for (int i = 0; i < spectrum.Length; i++)
        {
            spectrum[i] = Mathf.Abs(spectrum[i]);
        }

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            if (spectrum[i] > limit)
            {
                if (spectrum[i] <= spectrum[i - 1] && spectrum[i] >= spectrum[i + 1])
                {
                    ResultList.Add(i);
                    i += (int)waitSamples;
                }
            }
        }

        analysed = true;
        lastClipName = Clip.name;

        Debug.Log("Result: " + resultList.Count);
    }
}