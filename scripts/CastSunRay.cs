using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSunRay : MonoBehaviour
{
    public int numberOFReflection;
    private UIGamePlayScreenManager uIGamePlayScreenManager;
    private LineRenderer line;
    private bool nextLevelCalled;
    private Ray2D ray;
    private RaycastHit2D hit;
    int counter;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        uIGamePlayScreenManager = UIGamePlayScreenManager.Instance();
        nextLevelCalled = false;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray2D(transform.position,transform.up);
        line.positionCount = 1;
        line.SetPosition(0,transform.position);
        for(int i = 0; i < numberOFReflection; i++)
        {
            hit  = Physics2D.Raycast(ray.origin, ray.direction);
            ray = new Ray2D(hit.point, Vector2.Reflect(ray.direction, hit.normal));
           //// line.startWidth = 0.1f;
           //// line.endWidth = 0.1f;
            Debug.DrawRay(hit.point, Vector2.Reflect(ray.direction, hit.normal));
            if(hit.transform == null)
            {
                line.positionCount += 1;
                line.SetPosition(line.positionCount-1,ray.direction*10f);
                return;
            }
            if (!(hit.transform.tag.Equals(TagHolder.MIRROR) || hit.transform.tag.Equals(TagHolder.PLANT)))
            {
                line.positionCount += 1;
                line.SetPosition(line.positionCount - 1, hit.point);
                counter = 0;
                return;
            }else if (hit.transform.tag.Equals(TagHolder.PLANT))
            {
                line.positionCount += 1;
                counter++;
                line.SetPosition(line.positionCount - 1, hit.point);
                if(nextLevelCalled && counter > 60)
                    StartCoroutine(CallNextLevel());
                nextLevelCalled = true;
                return;
            }
            else if(hit.transform.tag.Equals(TagHolder.MIRROR))
            {
                line.positionCount += 1;
                line.SetPosition(line.positionCount - 1, hit.point);
            }
        }
    }

    IEnumerator CallNextLevel()
    {
        yield return new WaitForSeconds(3f);
        uIGamePlayScreenManager.LevelDone();
    }
}
