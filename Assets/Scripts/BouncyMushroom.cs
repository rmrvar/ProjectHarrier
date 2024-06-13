using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class BouncyMushroom : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onSuccess;

    private BeatTester _beatTester;
    private SpriteShapeRenderer _spriteRenderer;

    private bool _isResponsive = true;

    private void Awake()
    {
        _beatTester = GetComponent<BeatTester>();
        _spriteRenderer = GetComponentInChildren<SpriteShapeRenderer>();
    }
    
    private void Start()
    {
        BeatMaker.Instance.OnBeat += OnBeat;
        _beatTester.OnSuccess += OnSuccess; 
        _beatTester.OnFailure += OnFailure; 

        GameManager.Instance.OnPlayerRespawned += Reset;
    }

    private void OnDestroy()
    {
        BeatMaker.Instance.OnBeat -= OnBeat;
        _beatTester.OnSuccess -= OnSuccess;
        _beatTester.OnFailure -= OnFailure;

        GameManager.Instance.OnPlayerRespawned -= Reset;
    }

    private void OnBeat()
    {
        if (!_isResponsive)
        {
            return;
        }
        StartCoroutine(IE_PlayBeatAnimation());
    }

    private void OnSuccess()
    {
        Debug.Log("success");
        _onSuccess?.Invoke();
        _isResponsive = false;
        _spriteRenderer.color = Color.blue;
    }

    private void OnFailure()
    {
        Debug.Log("failure");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isResponsive)
        {
            return;
        }
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

    private void Reset()
    {
        _isResponsive = true;
        _spriteRenderer.color = Color.white;
    }
}
