using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BouncyMushroom : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onSuccess;

    private BeatTester _beatTester;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _beatTester = GetComponent<BeatTester>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        StartCoroutine(IE_PlayBeatAnimation());
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

    private IEnumerator IE_PlayBeatAnimation()
    {
        var oldColor = _spriteRenderer.color;
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.07F);
        _spriteRenderer.color = oldColor;
    }
}
