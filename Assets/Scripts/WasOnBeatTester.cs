using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// TODO: Rename
public class WasOnBeatTester : MonoBehaviour
{
    [SerializeField]
    
    private float _beatTime; // Time of each beat
    public float _leeway; // Window of lenience
  
    public event System.Action OnSuccess;
    public event System.Action OnFailure;

   private bool didInteractHappen;
   private float interactTime;
   private void Update()
   {
     float timeSinceBeat = Time.time - _beatTime; 
     float timeSinceInteract = Time.time - interactTime;
        // Checks if time since last beat fits in window

        if (didInteractHappen && timeSinceInteract > _leeway)
        {
            OnFailure?.Invoke();
            didInteractHappen = false;
        }
        else if (didInteractHappen && timeSinceBeat <= _leeway)
        {

            OnSuccess?.Invoke();
            didInteractHappen = false;
        }

   }
    // Did you interact on the beat, yes or no?
    public void Interact()
    {
        
       interactTime = Time.time; 
       didInteractHappen = true;

       
    }

    private void Start()
    {
        // Beat set to true on each beat
        BeatMaker.Instance.OnBeat += DoSomethingOnBeat;
    }

    private void OnDestroy()
    {
        BeatMaker.Instance.OnBeat -= DoSomethingOnBeat;
    }

    private void DoSomethingOnBeat()
    { // Sets beat to true and gives time
        _isThisFrameABeat = true;
        _beatTime = Time.time; 
    }

    private void LateUpdate()
    {
        _isThisFrameABeat = false;
    }

    private bool _isThisFrameABeat;
}
