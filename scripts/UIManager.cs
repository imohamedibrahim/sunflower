using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject MainCanvas;
    public static UIManager uIManager;
    private UIGamePlayScreenManager uIGamePlayScreenController;
    private UIMainScreenManager uIMainScreenController;
    private UILevelScreenManager uILevelScreenManager;
    private GameObject uILevelScreenGameObject;
    private GameObject uIGamePlayGameObject;
    private GameObject uIMainScreenGameObject;
    
    private TouchInputManager touchInputManager;
    public static UIManager Instance()
    {
        if (uIManager == null)
        {
            uIManager = new UIManager();
        }
        return uIManager;
    }

    

    public void Awake()
    {
        if (uIManager != null && uIManager != this)
        {
            uIManager = null;
        }
        else
        {
            uIManager = this;
        }
    }
    private void Start()
    {
        Initiate();
        //touchInputManager.OnSpacePressed += OnSpacePressed;
    }

    // Start is called before the first frame update
    public void Initiate()
    {
        uIGamePlayScreenController = UIGamePlayScreenManager.Instance();
        uIMainScreenController = UIMainScreenManager.Instance();
        uILevelScreenManager = UILevelScreenManager.Instance();
        uIGamePlayGameObject = UtilFunctions.GetChildGameObjectWithTag(MainCanvas, TagHolder.UI_GAME_PLAY_SCREEN);
        uIMainScreenGameObject = UtilFunctions.GetChildGameObjectWithTag(MainCanvas, TagHolder.UI_MAIN_SCREEN);
        uILevelScreenGameObject = UtilFunctions.GetChildGameObjectWithTag(MainCanvas, TagHolder.UI_LEVEL_SCREEN);
        uIGamePlayGameObject.SetActive(false);
        uILevelScreenGameObject.SetActive(false);
        uIMainScreenGameObject.SetActive(true);
        Time.timeScale = 1f;
        uIMainScreenController.AssignGameObject(uIMainScreenGameObject);
        uIMainScreenController.Initiate();
    }

    internal void LoadLevelScreen()
    {
        uILevelScreenManager.AssignGameObject(uILevelScreenGameObject);
        uILevelScreenGameObject.SetActive(true);
        uIGamePlayGameObject.SetActive(false);
        uIMainScreenGameObject.SetActive(false);
        uILevelScreenManager.Initiate();
    }


    public void OnLevelLoaded(LevelProperty prop)
    {
        uIGamePlayScreenController.AssignGameObject(uIGamePlayGameObject);
        uIGamePlayGameObject.SetActive(true);
        uIMainScreenGameObject.SetActive(false);
        uILevelScreenGameObject.SetActive(false);
        uIGamePlayScreenController.Initiate(prop);
    }

    
}
