using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffMouseEvent : MonoBehaviour
{

    GameManager gameManager;
    int z = 1;
    bool mover = false;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void Update()
    {
        if(mover)
        {
            if (transform.localRotation.z > 0.1)
                z *= -1;
            else if (transform.localRotation.z < -0.1)
                z *= -1;

            if (gameManager.sceneNum == 1)
            {
                
                transform.Rotate(0, 0, z);
                transform.GetChild(0).transform.Rotate(0, 0, -z);
            }
        }
    }
    private void OnMouseEnter()
    {
        if(!(transform.GetChild(0).GetComponent<SpriteRenderer>().name.Equals("stuff_dontknow")))
            mover = true;
           
           
    }

    private void OnMouseExit()
    {
        mover = false;
            transform.rotation =Quaternion.identity;
            transform.GetChild(0).transform.rotation =Quaternion.identity;
    }
}
