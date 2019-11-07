using System.Collections.Generic;
using UnityEngine;

public class BeatHandler : MonoBehaviour
{
    [SerializeField]
    private int timeWindow = 0;
    [SerializeField]
    private int reactionTime = 0;

    public List<int> beatList = new List<int>();

    private float timeSample = 0;
    private float[] spectrum = null;
    private float sampleBeat = 0;
    [HideInInspector]
    public float sampleTimeInSec = 0;

    private AudioSource sourceWave = null;

    private bool debugMode = false;
    private bool copy = false;

    public List<int> beatListCopy;

    void Start()
    {
        sourceWave = GetComponent<AudioSource>();
        sampleBeat = Mathf.Abs(beatList[0] - beatList[1]);
        sampleTimeInSec = sampleBeat / 44100;
        beatListCopy = new List<int>(beatList);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            debugMode = !debugMode;
        }
        if ((sourceWave.timeSamples >= beatList[beatList.Count - 1] && copy) || beatListCopy.Count == 0)
        {
            copy = false;
            beatListCopy = new List<int>(beatList);
        }
        if(sourceWave.timeSamples <= beatList[0] && !copy)
        {
            copy = true;
        }
    }

    public bool IsOnBeat(int reactionTime, int timeWindow)
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

    private void OnDrawGizmos()
    {
        if (debugMode)
        {
            if (spectrum == null)
            {
                return;
            }
            Vector3 displacement = Camera.main.ScreenToWorldPoint(new Vector3(100, 100, 5));
            float heightMulti = 1;
            float widthMulti = 0.000005f;
            Gizmos.color = new Color(0.5f, 0, 0.5f, 1);

            for (int i = 0; i < spectrum.Length; i += 100)
            {
                Gizmos.DrawLine(displacement + new Vector3(i * widthMulti, 0, 0),
                                displacement + new Vector3(i * widthMulti, heightMulti * spectrum[i], 0));
            }

            Gizmos.color = Color.green;
            for (int i = 0; i < beatList.Count; i++)
            {
                Gizmos.DrawLine(displacement + new Vector3((beatList[i] - timeWindow + reactionTime) * widthMulti, 0, 0),
                                displacement + new Vector3((beatList[i] + timeWindow + reactionTime) * widthMulti, 0, 0));
            }

            Gizmos.color = Color.red;
            Gizmos.DrawLine(displacement + new Vector3(sourceWave.timeSamples * widthMulti, 0, 0), displacement + new Vector3(sourceWave.timeSamples * widthMulti, heightMulti, 0));
        }
    }
}