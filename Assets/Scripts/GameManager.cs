using System.Collections;
using Platformer.Mechanics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance { get; private set; }

    private Health _playerHealth;
    private Transform _playerTransform;
    private Vector2 _startPosition;
    private Quaternion _startRotation;

    private void Awake()
    {
        Debug.Assert(
            Instance == null,
            $"Attempted to create multiple instances of {typeof(GameManager).Name}!"
          );
        Instance = this;

        var player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = player.GetComponent<Health>();
        _playerTransform = player.GetComponent<Transform>();
        _startPosition = _playerTransform.position;
        _startRotation = _playerTransform.rotation;
    }

    private void Start()
    {
        _playerHealth.OnKilled += OnPlayerKilled;
        BeatMaker.Instance.OnBeat += OnBeat;
        BeatMaker.Instance.PlayBeats(60);
    }

    private void OnDestroy()
    {
        BeatMaker.Instance.OnBeat -= OnBeat;
        _playerHealth.OnKilled -= OnPlayerKilled;
    }

    private void OnBeat()
    {
        Debug.Log("Beat!");
    }

    private void OnPlayerKilled()
    {
        StartCoroutine(IE_RespawnPlayer());
    }

    private IEnumerator IE_RespawnPlayer()
    {
        var controller = _playerTransform.GetComponent<PlayerController>();
        controller.controlEnabled = false;
        // TODO: Add fade out.
        yield return new WaitForSeconds(1);
        // TODO: Reset the state of the level. Perhaps easiest to just do a scene reload?
        controller.controlEnabled = true;
        _playerHealth.TopOff();
        _playerTransform.SetPositionAndRotation(_startPosition, _startRotation);
        // TODO: Add fade in.
    }
}