
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperty : MonoBehaviour
{
    public int levelNumber;
    public bool isCompleted;
    public bool isLocked;
    private UILevelScreenManager uILevelScreenManager;

    public void OnLevelButtonPressed()
    {
        uILevelScreenManager = UILevelScreenManager.Instance();
        uILevelScreenManager.LoadLevel(this);
    }
}
