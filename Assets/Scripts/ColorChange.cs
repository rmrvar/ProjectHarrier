using UnityEngine;
  
public class ColorChange : MonoBehaviour
{ 
    [SerializeField] 
    private int _colorIndex = 0;
    [SerializeField]
    private Color[] _defaultColor;
    private BeatTester _beatTester;
    private SpriteRenderer _spriteRenderer;

    private bool _colorChanged;

    private void Awake()
    {
        _beatTester = GetComponent<BeatTester>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Start()
    {
        _spriteRenderer.color = _defaultColor[0];

        _colorChanged = false;
        _beatTester.OnSuccess += ModeSwitch; 
        BeatMaker.Instance.OnBeat += ColorShift;
    }

    private void OnDestroy()
    {
        _beatTester.OnSuccess -= ModeSwitch;
        BeatMaker.Instance.OnBeat -= ColorShift;
    }

    private void ModeSwitch()
    {
        _colorChanged = true;
        _spriteRenderer.color = Color.red;
    }
   
    private void ColorShift() 
    {   
        if(_colorChanged)
        {
            _spriteRenderer.color = Color.red;
        }
        else
        {
            _spriteRenderer.color = _defaultColor[_colorIndex];
        }

        _colorIndex++;
        if (_colorIndex >= 2)
        {
            _colorIndex = 0;
        }
    }
}
