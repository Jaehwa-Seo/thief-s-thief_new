using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobwindowEvent : MonoBehaviour
{
    Sprite spr;

    private void OnMouseEnter()
    {
        spr = (Sprite)Resources.Load("UI/bronze_frame_push", typeof(Sprite));
        if(transform.name.Equals("Frame"))
            transform.GetComponent<SpriteRenderer>().sprite = spr;
        else
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spr;        
    }

    private void OnMouseExit()
    {
            spr = (Sprite)Resources.Load("UI/bronze_frame", typeof(Sprite));
        if(transform.name.Equals("Frame"))
            transform.GetComponent<SpriteRenderer>().sprite = spr;
        else
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spr;

    }
}
