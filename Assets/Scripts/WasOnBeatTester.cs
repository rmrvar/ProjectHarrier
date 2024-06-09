using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Rename
public class WasOnBeatTester : MonoBehaviour
{
    [SerializeField]
    private float _leeway;

    public event System.Action OnSuccess;
    public event System.Action OnFailure;

    // Did you interact on the beat, yes or no?
    public bool Interact()
    {
        // TODO: This frame perfect success is too hard for players.
        // Add some leeway before or after that is still a success.

        if (_isThisFrameABeat)
        {
            OnSuccess?.Invoke();
        }
        else
        {
            OnFailure?.Invoke();
        }
        return _isThisFrameABeat;
    }

    private void Start()
    {
        BeatMaker.Instance.OnBeat += DoSomethingOnBeat;
    }

    private void OnDestroy()
    {
        BeatMaker.Instance.OnBeat -= DoSomethingOnBeat;
    }

    private void DoSomethingOnBeat()
    {
        _isThisFrameABeat = true;
    }

    private void LateUpdate()
    {
        _isThisFrameABeat = false;
    }

    private bool _isThisFrameABeat;
}
