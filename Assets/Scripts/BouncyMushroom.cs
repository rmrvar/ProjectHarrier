using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BouncyMushroom : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onSuccess;

    BeatTester _beatTester;

    private void Awake()
    {
        _beatTester = GetComponent<BeatTester>();
    }
    
    private void Start()
    {
        BeatMaker.Instance.OnBeat += OnBeat;
        _beatTester.OnSuccess += OnSuccess; 
        _beatTester.OnFailure += OnFailure; 
    }

    private void OnDestroy()
    {
        BeatMaker.Instance.OnBeat -= OnBeat;
        _beatTester.OnSuccess -= OnSuccess;
        _beatTester.OnFailure -= OnFailure;
    }

    private void OnBeat()
    {
        // Play beat animation (just change color for now).
    }

    private void OnSuccess()
    {
        Debug.Log("success");
        _onSuccess?.Invoke();
    }

    private void OnFailure()
    {
        Debug.Log("failure");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        _beatTester.Interact();
    }
}
