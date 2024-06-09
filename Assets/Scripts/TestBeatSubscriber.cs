using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBeatSubscriber : MonoBehaviour
{
    private void Start()
    {
        BeatMaker.Instance.OnBeat += OnBeat;
        BeatMaker.Instance.PlayBeats(60);
    }

    private void OnDestroy()
    {
        BeatMaker.Instance.OnBeat -= OnBeat;
    }

    private void OnBeat()
    {
        Debug.Log("Beat!");
    }
}
