using System.Collections.Generic;
using UnityEngine;

public class BeatHandler : MonoBehaviour
{
    [SerializeField]
    private int timeWindow = 0;
    [SerializeField]
    private int reactionTime = 0;

    [SerializeField]
    private BeatAnalysisSO beatAnalysisSO;
    [SerializeField]
    private List<Texture2D> spectrumTexture;

    private static float timeSample = 0;

    private static AudioSource sourceWave = null;

    private bool debugMode = false;
    private bool copy = false;

    private static List<int> beatListCopy;

    void Start()
    {
        sourceWave = GetComponent<AudioSource>();
        beatListCopy = new List<int>(beatAnalysisSO.ResultList);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            debugMode = !debugMode;
        }
        if ((sourceWave.timeSamples >= beatAnalysisSO.ResultList[beatAnalysisSO.ResultList.Count - 1] && copy) || beatListCopy.Count == 0)
        {
            copy = false;
            beatListCopy = new List<int>(beatAnalysisSO.ResultList);
        }
        if (sourceWave.timeSamples <= beatAnalysisSO.ResultList[0] && !copy)
        {
            copy = true;
        }
    }

    private static bool IsOnBeat(int reactionTime, int timeWindow)
    {
        timeSample = sourceWave.timeSamples - reactionTime;
        for (int i = 0; i < beatListCopy.Count; i++)
        {
            if (timeSample >= (beatListCopy[i] - timeWindow) &&
                timeSample <= (beatListCopy[i] + timeWindow))
            {
                return true;

            }
            else if (timeSample > beatListCopy[i] + timeWindow)
            {
                beatListCopy.RemoveAt(i);
                i -= 1;
            }
        }
        return false;
    }

    private void OnGUI()
    {
        if (debugMode)
        {
            float heightMulti = 100;
            float widthMulti = 1;
            int sampleJump = 1800;

            for (int i = 0; i * sampleJump < beatAnalysisSO.Spectrum.Length; i++)
            {
                if (i > Screen.width - 1)
                {
                    break;
                }
                GUI.DrawTexture(new Rect(i * widthMulti, 5, widthMulti, heightMulti * beatAnalysisSO.Spectrum[i * sampleJump]), spectrumTexture[0]);
            }
            for (int j = 0; j < beatAnalysisSO.ResultList.Count; j++)
            {
                GUI.DrawTexture(new Rect(beatAnalysisSO.ResultList[j] / sampleJump, 5, widthMulti, heightMulti), spectrumTexture[1]);
            }
            GUI.DrawTexture(new Rect(sourceWave.timeSamples / sampleJump, 5, widthMulti, heightMulti * 1.1f), spectrumTexture[2]);
        }
    }
}