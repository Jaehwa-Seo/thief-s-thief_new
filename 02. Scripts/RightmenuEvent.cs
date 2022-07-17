using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightmenuEvent : MonoBehaviour
{
    //오른쪽 메뉴에 관한 클릭 관련 스크립트

    float MaxDistance = 15f;
    Camera cam;
    Vector3 MousePosition;

    GameObject Rightmenu;
    GameObject QuitWindow;
    GameObject GameGuideWindow;

    Sprite spr;

    float Timer;

    bool Rightmenu_click = false;
    public bool Rightmenu_open = true;
    bool RightHowto_click = false;
    bool RightOption_click = false;
    bool RightQuit_click = false;

    public bool quitwindow_open = false;
    bool quityes_click = false;
    bool quitno_click = false;

    public bool guidemode_open = false;
    int guide_page;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        Rightmenu = GameObject.Find("Right_Menu");
        QuitWindow = GameObject.Find("Quit_window");
        GameGuideWindow = GameObject.Find("GameGuide Window");
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime * 2;
        if (Input.GetMouseButtonDown(0))
        {
            if (guidemode_open)
            {
                guide_page++;
                spr = (Sprite)Resources.Load("UI/Guide/guide" + guide_page, typeof(Sprite));
                GameGuideWindow.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;

                if(guide_page==7)
                {
                    guidemode_open = false;
                    GameGuideWindow.transform.localPosition = new Vector3(100.0f, 0.0f);
                }
            }
            else
            {
                MousePosition = Input.mousePosition;
                MousePosition = cam.ScreenToWorldPoint(MousePosition);


                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);


                if (hit)
                {
                    if (quitwindow_open)
                    {
                        if (hit.transform.CompareTag("Quit_Yes"))
                        {
                            quityes_click = true;
                        }
                        else if (hit.transform.CompareTag("Quit_No"))
                        {
                            quitno_click = true;
                        }

                    }
                    else
                    {
                        if (hit.transform.CompareTag("Right_Menu"))
                        {
                            Rightmenu_click = true;
                            spr = (Sprite)Resources.Load("UI/Menu/menu_icon2", typeof(Sprite));
                            hit.transform.GetComponent<SpriteRenderer>().sprite = spr;

                            if (Rightmenu_open == false)
                            {
                                Timer = 1;
                                Rightmenu_open = true;
                            }
                            else
                            {
                                Timer = 1;
                                Rightmenu_open = false;
                            }
                        }
                        if (hit.transform.CompareTag("Right_Howto"))
                        {

                            RightHowto_click = true;
                            hit.transform.GetChild(0).localScale = new Vector3(1.1f, 1.1f);
                        }
                        if (hit.transform.CompareTag("Right_Option"))
                        {
                            RightOption_click = true;
                            hit.transform.GetChild(0).localScale = new Vector3(1.1f, 1.1f);
                        }
                        if (hit.transform.CompareTag("Right_Quit"))
                        {
                            RightQuit_click = true;
                            hit.transform.GetChild(0).localScale = new Vector3(1.1f, 1.1f);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Rightmenu_click)
            {
                Rightmenu_click = false;
                spr = (Sprite)Resources.Load("UI/Menu/menu_icon1", typeof(Sprite));
                Rightmenu.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spr;
            }
            if (RightHowto_click)
            {
                RightHowto_click = false;
                Rightmenu.transform.GetChild(1).GetChild(0).localScale = new Vector3(1.0f, 1.0f);
                
                guidemode_open = true;
                GameGuideWindow.transform.localPosition = new Vector3(0.0f, 0.0f);
                guide_page = 1;
                spr = (Sprite)Resources.Load("UI/Guide/guide" + guide_page, typeof(Sprite));
                GameGuideWindow.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;

            }
            if (RightOption_click)
            {
                RightOption_click = false;
                Rightmenu.transform.GetChild(2).GetChild(0).localScale = new Vector3(1.0f, 1.0f);
            }
            if (RightQuit_click)
            {
                RightQuit_click = false;
                Rightmenu.transform.GetChild(3).GetChild(0).localScale = new Vector3(1.0f, 1.0f);
                QuitWindow.transform.localPosition = new Vector3(0.0f, 0.0f);
                quitwindow_open = true;
            }
            if(quitno_click)
            {
                quitno_click = false;
                QuitWindow.transform.GetChild(2).localScale = new Vector3(1f, 1f);
                quitwindow_open = false;
                QuitWindow.transform.localPosition = new Vector3(100f, 0);
            }
            if (quityes_click)
            {
                quityes_click = false;
                QuitWindow.transform.GetChild(1).localScale = new Vector3(1f, 1f);
                Application.Quit();
            }
        }

        if (Rightmenu_open)
        {
            if (Timer > 0)
            {
                Rightmenu.transform.localPosition = new Vector3(2.5f + (5.9f * Timer), 3.94f);
            }
            else
            {
                Rightmenu.transform.localPosition = new Vector3(2.5f, 3.94f);
                Rightmenu.transform.GetChild(0).localScale = new Vector3(-0.8f, 0.8f);
            }
        }
        else
        {
            if (Timer > 0)
            {
                Rightmenu.transform.localPosition = new Vector3(8.4f - (5.9f * Timer), 3.94f);
            }
            else
            {
                Rightmenu.transform.localPosition = new Vector3(8.4f, 3.94f);
                Rightmenu.transform.GetChild(0).localScale = new Vector3(0.8f, 0.8f);
            }
        }
    }
}

