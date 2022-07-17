using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureEvent : MonoBehaviour
{
    GameManager gameManager;
    Sprite spr;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseEnter()
    {
        if (gameManager.sceneNum == 1 || gameManager.sceneNum == 3 || gameManager.sceneNum == 4)
        {
            spr = (Sprite)Resources.Load("UI/bronze_frame_push", typeof(Sprite));
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;            
        }
    }

    private void OnMouseExit()
    {
        if (gameManager.sceneNum == 1 || gameManager.sceneNum == 3 || gameManager.sceneNum == 4)
        {           
            spr = (Sprite)Resources.Load("UI/bronze_frame", typeof(Sprite));
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
        }
    }

}
