using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmButtonEvent : MonoBehaviour
{
    Sprite spr;

    private void OnMouseEnter()
    {
        spr = (Sprite)Resources.Load("UI/confirm_button_push", typeof(Sprite));
        GetComponent<SpriteRenderer>().sprite = spr;
    }

    private void OnMouseExit()
    {
        spr = (Sprite)Resources.Load("UI/confirm_button", typeof(Sprite));
        GetComponent<SpriteRenderer>().sprite = spr;
    }
}
