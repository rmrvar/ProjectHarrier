using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBeatSubscriber : MonoBehaviour
{
    private void Start()
    {
        BeatMaker.Instance.OnBeat += PrintInfo;
        BeatMaker.Instance.PlayBeats(60);
    }

    private void OnDestroy()
    {
        BeatMaker.Instance.OnBeat -= PrintInfo;
    }

    private void PrintInfo()
    {
        Debug.Log("Beat!");
    }
}
