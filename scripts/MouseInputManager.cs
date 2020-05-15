using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputManager : MonoBehaviour
{
    private bool isPressed;
    private GameObject currentSelectedObject;
    private Vector2 initialPosition;
    private Vector2 centerPosition;
    [SerializeField]
    private float rotationSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D[] col = Physics2D.OverlapPointAll(v);
            if (col.Length != 0 && col[0].transform != null && col[0].transform.tag.Equals(TagHolder.MIRROR))
            {
                currentSelectedObject = col[0].transform.gameObject;
                centerPosition = Camera.main.WorldToScreenPoint(col[0].transform.position);
                isPressed = true;
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
        }
        if (isPressed)
        {
            Vector2 tmp = new Vector2(Input.mousePosition.x,Input.mousePosition.y) - centerPosition;
            float angle = Mathf.Atan2(tmp.y, tmp.x) * Mathf.Rad2Deg;
            
            Debug.Log("Mouse" + Input.mousePosition + angle + " tmp " + tmp);
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            currentSelectedObject.transform.rotation = q;
            
        }
         
    }
}
