using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMaker : MonoBehaviour
{
    public static BeatMaker Instance { get; private set; }

    public event System.Action OnBeat;

    public bool IsPlaying { get; private set; }
    public int Bpm { get; private set; }

    public void PlayBeats(int bpm)
    {
        IsPlaying = true;
        Bpm = bpm;
        StartNewCoroutine();
    }

    public void StopBeats()
    {
        IsPlaying = false;
        Bpm = 0;
        StopOldCoroutine();
    }

    private void StartNewCoroutine()
    {
        StopOldCoroutine();
        _coroutine = StartCoroutine(IE_PlayBeats());
    }

    private void StopOldCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator IE_PlayBeats()
    {
        var interval = Bpm / 60.0F;
        while (true)
        {
            yield return new WaitForSeconds(interval);
            OnBeat?.Invoke();
        }
    }

    private void Awake()
    {
        Debug.Assert(
            Instance == null,
            $"Attempted to create multiple instances of {typeof(BeatMaker).Name}!"
          );
        Instance = this;
    }

    private Coroutine _coroutine;
}
