using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButtonEvent : MonoBehaviour
{
    Sprite spr;

    private void OnMouseEnter()
    {
        spr = (Sprite)Resources.Load("UI/change_button_push", typeof(Sprite));
        GetComponent<SpriteRenderer>().sprite = spr;
    }

    private void OnMouseExit()
    {
        spr = (Sprite)Resources.Load("UI/change_button", typeof(Sprite));
        GetComponent<SpriteRenderer>().sprite = spr;
    }
}
