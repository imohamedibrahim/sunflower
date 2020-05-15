using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPropertyRequirement : MonoBehaviour
{
    [SerializeField]
    private float cameraSize;
    [SerializeField]
    private Vector3 cameraPosition;

    public void Init()
    {
        Camera.main.orthographicSize = cameraSize;
        Camera.main.transform.position = cameraPosition;
    }
}
