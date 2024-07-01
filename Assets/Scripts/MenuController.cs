using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuController : MonoBehaviour
{
    // FUNCTION: the menu controller controls all menu settings
    [Header("Volume Settings")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Slider sfxSlider = null;
    private float _musicVolume;
    private float _sfxVolume;
    const string MIXER_MUSIC = "musicMixer";
    const string MIXER_SFX = "sfxMixer";
   
    [Header("Confirmation Prompt")]
    [SerializeField] private GameObject _confirmationPrompt = null;

    [Header("Levels to Load")]
    public string newGameLevel;
    private string _levelToLoad;
    [SerializeField] private string _mainMenu;
    [SerializeField] private GameObject _noSavedGameDialog = null;
    [SerializeField] private Animator _curtainAnimator;

    [Header("Pause Settings")]
    public static bool _isPaused = false;

    

    [Header("Active Panels")]
    [SerializeField] private GameObject _pausedPanel;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _audioSettingsPanel;
    [SerializeField] private GameObject _newDialogPanel;
    [SerializeField] private GameObject _pausedExit;
    [SerializeField] private GameObject _mainExit;  
    [SerializeField] private GameObject _gameOverPanel;

    public static MenuController Instance;
    private void Awake()
    {   
        if (Instance == null)
        {
        Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        DontDestroyOnLoad(gameObject);
        
    }
  
    private void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= GameOverPanel;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            { 
            Resume();
            }
            else
            {
            Pause();
            }
        }
    }
    public void NewGameDialog_Yes()
    {
        StartCoroutine(IE_NewGameDialog_Yes());
    }
    private IEnumerator IE_NewGameDialog_Yes()
    {
        _curtainAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1 + 0.05F);

        SceneManager.LoadScene(newGameLevel);
        _newDialogPanel.SetActive(false);
        foreach (Transform child in transform)
        {
            if (child.gameObject.name != "Black Curtain" && child.gameObject.name != "EventSystem")
            {
                child.gameObject.SetActive(false);
            }
            if (child.gameObject.name == "Game HUD")
            {
                child.gameObject.SetActive(true);
            }
        }
        _curtainAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1 + 0.05F);
        GameManager.Instance.OnGameOver += GameOverPanel;
    }

    public void LoadGameDialog_Yes()
    {
       if (PlayerPrefs.HasKey("SavedLevel"))
       {
        _levelToLoad = PlayerPrefs.GetString("SavedLevel");
        SceneManager.LoadScene(_levelToLoad);
       }
       else 
       {
            _noSavedGameDialog.SetActive(true);
       }
    }
    public void MainMenu()
    {
        StartCoroutine(IE_MainMenu());
    }

    private IEnumerator IE_MainMenu()
    {
         _curtainAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1 + 0.05F);

        
        SceneManager.LoadScene(_mainMenu);
        foreach (Transform child in transform)
        {
            if (child.gameObject.name != "Black Curtain" && child.gameObject.name != "EventSystem")
            {
                child.gameObject.SetActive(false);
            }
            if (child.gameObject.name == "Main Background")
            {
                child.gameObject.SetActive(true);
            }
        }
        UILoader();
        _curtainAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1 + 0.05F);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    // UI PANEL FUNCTIONS

    public void UILoader()
    {   
        /* Scene currentScene = SceneManager.GetActiveScene();
        
        if (currentScene.name == "MainMenu")
        {
            
        }
        else if (currentScene.name == null)
        {
            print(currentScene.name);
            
        }
        else
        {
            print("other");
        }*/
        OpenOptionsPanel();
        OpenStartPanel();
        OpenMainPanel();
    }

    private void Pause()
    {  
        Scene currentScene = SceneManager.GetActiveScene();
        
        if (currentScene.name != "MainMenu")
        { 
        _isPaused = true;
        Time.timeScale = 0;
        _pausedPanel.SetActive(true);
        _pausedExit.SetActive(true);
        _mainExit.SetActive(false);
        print("Paused"); 
        }  
    }

    public void Resume()
    {
        _isPaused = false;
        Time.timeScale = 1;
        _pausedPanel.SetActive(false);
        _pausedExit.SetActive(false);
        _mainExit.SetActive(true);
        print("Unpaused");
    }

    private void OpenStartPanel()
    {
        _startPanel.SetActive(true);
        print("is working");
    }

    private void OpenMainPanel()
    {
        _mainMenuPanel.SetActive(true);
    }

    private void OpenOptionsPanel()
    {
        _optionsPanel.SetActive(true);
    }

    private void GameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }
    // VOLUME SETTING FUNCTIONS
    private void OpenSettingsPanel()
    {
        _audioSettingsPanel.SetActive(true);
    }

    private void SetMusicVolume(float volume)
    {
        mixer.SetFloat(MIXER_MUSIC, MathF.Log10(volume) * 20);
        _musicVolume = volume;
    }

    private void SetSFXVolume(float volume)
    {
        mixer.SetFloat(MIXER_SFX, MathF.Log10(volume) * 20);
        _sfxVolume = volume;
    }
    
    public void ApplyVolume()
    {
        PlayerPrefs.SetFloat(MIXER_MUSIC, _musicVolume);
        PlayerPrefs.SetFloat(MIXER_SFX, _sfxVolume);
    }
    public void LoadVolume(float music)
    {
       musicSlider.value = PlayerPrefs.GetFloat(MIXER_MUSIC, 1f);
       sfxSlider.value = PlayerPrefs.GetFloat(MIXER_SFX, 1f);
       
    }
    
  
}
