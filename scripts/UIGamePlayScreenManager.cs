using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIGamePlayScreenManager : MonoBehaviour
{
    public static UIGamePlayScreenManager uIGamePlayScreenController;
    private UILevelScreenManager uILevelScreenManager;
    private UIManager uIManager;
    public GameObject gameObject;
    private GameObject pauseButtonGameObject;
    private GameObject invisibleSliderGameobject;
    private GameObject homeButtonOject;
    private GameObject textField;
    private GameObject gameOverImage;
    private GameObject nextLevelButtonObject;
    private bool isInitialized;
    private bool paused;
    private LevelProperty levelProperty;
    public static UIGamePlayScreenManager Instance()
    {
        if (uIGamePlayScreenController == null)
        {
            uIGamePlayScreenController = new UIGamePlayScreenManager();
        }
        return uIGamePlayScreenController;
    }

    public void Awake()
    {
        if (uIGamePlayScreenController != null && uIGamePlayScreenController != this)
        {
            uIGamePlayScreenController = null;
        }
        else
        {
            uIGamePlayScreenController = this;
        }
        
    }

    internal void SetText(string v)
    {
        Time.timeScale = 0f;
        UtilFunctions.GetChildGameObjectWithTag(gameObject, TagHolder.GAME_OVER_IMAGE).SetActive(true);
        textField.GetComponent<TextMeshProUGUI>().text = v;
    }

    internal void Initiate(LevelProperty prop)
    {
        paused = false;
        levelProperty = prop;
        uIManager = UIManager.Instance();
        isInitialized = true;
        uILevelScreenManager = UILevelScreenManager.Instance();
        pauseButtonGameObject = UtilFunctions.GetChildGameObjectWithTag(gameObject, TagHolder.PAUSE_BUTTON);
        pauseButtonGameObject.GetComponent<Button>().onClick.AddListener(OnPauseClicked);
        homeButtonOject = UtilFunctions.GetGameObjectWithTagRecursive(gameObject, TagHolder.HOME_BUTTON);
        homeButtonOject.GetComponent<Button>().onClick.AddListener(GoHome);
        textField = UtilFunctions.GetGameObjectWithTagRecursive(gameObject, TagHolder.TEXT_FIELD);
        nextLevelButtonObject = UtilFunctions.GetGameObjectWithTagRecursive(gameObject, TagHolder.NEXT_LEVEL_BUTTON);
        nextLevelButtonObject.GetComponent<Button>().onClick.AddListener(LoadNextLevel);
        gameOverImage = UtilFunctions.GetChildGameObjectWithTag(gameObject, TagHolder.GAME_OVER_IMAGE);
        gameOverImage.SetActive(false);
    }

    private void GoHome()
    {
        SceneManager.LoadScene(0);
    }

    internal void AssignGameObject(GameObject tmp)
    {
        gameObject = tmp;
    }

    private void OnPauseClicked()
    {
        paused = !paused;
        if (paused) Time.timeScale = 0; else Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
      
    }

    
    public void LevelDone()
    {
        if (isInitialized)
        {
            GameStateManager.SetLevelAsUnlocked(levelProperty.levelNumber);
            gameOverImage.SetActive(true);
        }
            
        
    }
    public void LoadNextLevel()
    {
        if(levelProperty.levelNumber == 3)
        {
            UtilFunctions.GetGameObjectWithTagRecursive(gameObject,"text").GetComponent<TextMeshProUGUI>().text = "Game Completed, Please provide your comments";
            return;
        }
        if (isInitialized)
        {
            uILevelScreenManager.LoadNextLevel(levelProperty);
        }
    }
   
}
