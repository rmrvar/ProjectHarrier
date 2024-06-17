using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Levels to Load")]
    public string newGameLevel;
    private string _levelToLoad;
    [SerializeField] private GameObject _noSavedGameDialog = null;
    public void NewGameDialog_Yes()
    {
        SceneManager.LoadScene(newGameLevel);
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

    public void ExitButton()
    {
        Application.Quit();
    }

}
