using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSFXManager : MonoBehaviour
{
    float MaxDistance = 15f;
    Camera cam;
    Vector3 MousePosition;

    public AudioSource click;
    public AudioSource win;
    public AudioSource lose;
    public AudioSource count;

    bool lastSFX = true;

    GameManager GM;
    PlayerController PC;
    public bool countOn;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        PC = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        countOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MousePosition = Input.mousePosition;
            MousePosition = cam.ScreenToWorldPoint(MousePosition);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);
            if(hit)
            {
                click.Play();
            }
        }


        if(GM.player_order==PC.order)
        {
            switch(GM.game_phase)
            {
                case 2:
                case 4:
                case 7:
                case 8:
                    {
                        if(GM.Timer<3)
                        {
                            if (!count.isPlaying && countOn)
                            {
                                count.Play();
                                countOn = false;
                            }
                        }
                        break;
                    }
            }
        }
        if(GM.swap_target==PC.order)
        {
            if(GM.game_phase==9)
                if (GM.Timer < 3)
                {
                    if (!count.isPlaying && countOn)
                    {
                        count.Play();
                        countOn = false;
                    }
                }
        }
    }
    public void LastSFXPlay()
    {
        if (lastSFX)
        {
            if (GM.user_result[PC.order])
            {
                if (!win.isPlaying)
                    win.Play();
            }
            else
            {
                if (!lose.isPlaying)
                    lose.Play();
            }
            lastSFX = false;
        }
    }
}
