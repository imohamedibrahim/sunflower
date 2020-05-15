using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMainScreenManager : MonoBehaviour
{

    public static UIMainScreenManager uIMainScreenController;
    private UIManager uIManager;
    private GameObject gameObject;
    private GameObject startButtonGameobject;
    
    public static UIMainScreenManager Instance()
    {
        if (uIMainScreenController == null)
        {
            uIMainScreenController = new UIMainScreenManager();
        }
        return uIMainScreenController;
    }



    public void Awake()
    {
        if (uIMainScreenController != null && uIMainScreenController != this)
        {
            uIMainScreenController = null;
        }
        else
        {
            uIMainScreenController = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        uIManager = UIManager.Instance();
    }

    internal void AssignGameObject(GameObject tmp)
    {
        gameObject = tmp;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Initiate()
    {
        startButtonGameobject = UtilFunctions.GetChildGameObjectWithTag(gameObject, TagHolder.START_BUTTON);
        startButtonGameobject.GetComponent<Button>().onClick.AddListener(StartClicked);
    }

    private void StartClicked()
    {
        uIManager.LoadLevelScreen();
    }
}

