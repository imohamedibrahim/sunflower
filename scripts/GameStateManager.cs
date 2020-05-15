using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GameStateManager : MonoBehaviour
{
    // Start is called before the first frame update
    private const string LEVEL_TAG = "LEVEL_STATE_";
    public void Start()
    {
        if (GetLevelLockedState(1) == "Locked") {
            SetLevelAsUnlocked(1);
        }
    }

    public static string GetLevelLockedState(int level)
    {
        string tmp = "Locked";
        string level_tmp = LEVEL_TAG + level.ToString();
        if (PlayerPrefs.HasKey(level_tmp))
        {
           tmp = PlayerPrefs.GetString(level_tmp);
        }
        return tmp;
    }

    public static void SetLevelAsUnlocked(int level)
    {
        PlayerPrefs.SetString(LEVEL_TAG+level.ToString(),"Unlocked");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
