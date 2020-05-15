using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UILevelScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject levelsGameObject;
    [SerializeField]
    private int levelDisplayCountRow;
    [SerializeField]
    private int levelDisplayCountColumn;
    [SerializeField]
    private GameObject levelPanel;
    [SerializeField]
    private GameObject LevelButton;
    private UIManager uIManager;
    private GameObject gameObject;
    private int totalLevels;
    private GameObject levelContentDisplay;
    private int remainingNumberOfLevelsButton;
    private static List<LevelProperty> listOfLevelProperty;
    private LevelProperty currentLevelProperty;

    public static UILevelScreenManager uILevelScreenManager;

    public static UILevelScreenManager Instance()
    {
        if (uILevelScreenManager == null)
        {
            uILevelScreenManager = new UILevelScreenManager();
        }
        return uILevelScreenManager;
    }



    public void Awake()
    {
        if (uILevelScreenManager != null && uILevelScreenManager != this)
        {
            uILevelScreenManager = null;
        }
        else
        {
            uILevelScreenManager = this;
        }
    }

    public void Initiate()
    {
        uIManager = UIManager.Instance();
        if (levelsGameObject == null)
            Debug.LogWarning("LevelGameObject not assigned");
        levelContentDisplay = UtilFunctions.GetChildGameObjectWithTag(gameObject,TagHolder.LEVEL_CONTENT);
        totalLevels = levelsGameObject.transform.childCount;
        remainingNumberOfLevelsButton = totalLevels;
        listOfLevelProperty = new List<LevelProperty>();
        ResizeLevelContentDisplay();
        
    }

    internal void AssignGameObject(GameObject tmp)
    {
        gameObject = tmp;
    }

    private void ResizeLevelContentDisplay()
    {
        float totalLevelsShownPerPage = levelDisplayCountRow * levelDisplayCountColumn;
        int totalPageCount = Mathf.CeilToInt(totalLevels / totalLevelsShownPerPage);
        for (int i = 0; i < totalPageCount; i++)
        {
            GameObject clonePanel = CreatePanel();
            CreateLevelButtons(clonePanel,totalLevelsShownPerPage);
        }
        
    }

    private void CreateLevelButtons(GameObject clonePanel, float levelsPerPanel)
    {
        for(int i=0;i<levelsPerPanel;i++)
        {
            if (remainingNumberOfLevelsButton > 0)
            {
                GameObject button = Instantiate(LevelButton);
                button.transform.SetParent(clonePanel.transform);
                button.SetActive(true);
                LevelProperty prop = button.AddComponent<LevelProperty>();
                prop.levelNumber = totalLevels - remainingNumberOfLevelsButton+1;
                prop.isCompleted = true;
                prop.isLocked = GameStateManager.GetLevelLockedState(prop.levelNumber) == "Locked";
                button.GetComponent<Button>().onClick.AddListener(prop.OnLevelButtonPressed);
                UtilFunctions.GetChildGameObjectWithTag(button, "LockButtonImage").SetActive(prop.isLocked);
                UtilFunctions.GetChildGameObjectWithTag(button, "LevelText").GetComponent<TextMeshProUGUI>().text = (prop.levelNumber).ToString();
                UtilFunctions.GetChildGameObjectWithTag(button, "LevelText").SetActive(true);
                listOfLevelProperty.Add(prop);
                remainingNumberOfLevelsButton--;
            }
        }
    }

    internal void LoadNextLevel(LevelProperty levelProperty)
    {
        LoadLevel(listOfLevelProperty[levelProperty.levelNumber]);
    }

    private GameObject CreatePanel()
    {
        GameObject tmp = Instantiate(levelPanel);
        tmp.transform.SetParent(gameObject.transform,false);
        return tmp;
    }

    public void LoadLevel (LevelProperty prop)
    {
        foreach(Transform tmp in levelsGameObject.transform)
        {
            if(tmp.tag == "Level_" + prop.levelNumber.ToString())
            {
                tmp.gameObject.SetActive(true);
                tmp.GetComponent<CameraPropertyRequirement>().Init();
                continue;
            }
            tmp.gameObject.SetActive(false);
        }
        uIManager.OnLevelLoaded(prop);
        
    }

    
}
