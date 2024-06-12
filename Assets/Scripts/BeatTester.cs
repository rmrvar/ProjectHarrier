using UnityEngine;

public class BeatTester : MonoBehaviour
{   
    [field: SerializeField]
    public float Leeway { get; set; }
  
    public event System.Action OnSuccess;
    public event System.Action OnFailure;

    private bool _isThisFrameABeat;
    private bool _didInteractHappen;
    private float _beatTime;
    private float _interactTime;

    private void Update()
    {
        float timeSinceBeat = Time.time - _beatTime; 
        float timeSinceInteract = Time.time - _interactTime;
        // Checks if time since last beat fits in window

        if (_didInteractHappen && timeSinceInteract > Leeway)
        {
            OnFailure?.Invoke();
            _didInteractHappen = false;
        } else 
        if (_didInteractHappen && timeSinceBeat <= Leeway)
        {
            OnSuccess?.Invoke();
            _didInteractHappen = false;
        }
    }

    public void Interact()
    {
       _interactTime = Time.time; 
       _didInteractHappen = true;
    }

    private void Start()
    {
        BeatMaker.Instance.OnBeat += OnBeat;
    }

    private void OnDestroy()
    {
        BeatMaker.Instance.OnBeat -= OnBeat;
    }

    private void OnBeat()
    {
        if (_didInteractHappen)
        {
            OnSuccess?.Invoke();
            _didInteractHappen = false;
        }

        _isThisFrameABeat = true;
        _beatTime = Time.time; 
    }

    private void LateUpdate()
    {
        _isThisFrameABeat = false;
    }
}
