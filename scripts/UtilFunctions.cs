using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UtilFunctions : MonoBehaviour
{
    
    public static void ThrowExceptionIfKeyIsNull<T>(T tmp, string name)
    {
        if (tmp == null || tmp.Equals(null))
        {
            Debug.LogError(name + " Object is not assigned!!!");
            throw new KeyNotFoundException(name + " Object is not assigned!!!");
        }
    }

    public static GameObject GetChildGameObjectWithTag(GameObject parentGameObject, string tag)
    {
        foreach(Transform t in parentGameObject.transform)
        {
            if(t.tag.ToLower() == tag.ToLower())
            {
                return t.gameObject;
            }
        }
        Debug.Log("Unable to get child gameobject with tag " + tag + " for gameobject " + parentGameObject.name);
        return null;
    }

    public static GameObject GetGameObjectWithTagRecursive(GameObject parentGameObject, string tag)
    {

        foreach(Transform t in parentGameObject.transform)
        {

            if(t.tag.ToLower() == tag.ToLower())
            {
                return t.gameObject;
            }
            else if (t.childCount > 0)
            {
                GameObject tmp = GetGameObjectWithTagRecursive(t.gameObject,tag);
                if (tmp != null)
                {
                    return tmp;
                }
            }
        }
        return null;
    }

    public static void DisableOrEnableGameObjectFromList(GameObject[] listOfGameObject,bool tmp)
    {
        foreach (GameObject gameObject in listOfGameObject)
        {
            gameObject.SetActive(tmp);
        }
    }
}
