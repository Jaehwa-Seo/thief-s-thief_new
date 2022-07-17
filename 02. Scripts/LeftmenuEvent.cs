using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeftmenuEvent : MonoBehaviour
{
    //왼쪽 메뉴에 관한 클릭 관련 스크립트
    float MaxDistance = 15f;
    Camera cam;
    Vector3 MousePosition;

    GameObject Leftmenu;
    MainGameManager maingameManager;

    RightmenuEvent quitcheck;

    Sprite spr;

    float Timer;

    bool Leftmenu_click = false;
    bool Leftmenu_open = true;
    bool Leftbook_click = false;
    bool Lefthouse_click = false;
    bool Leftvilige_click = false;
    bool Leftworld_click = false;
    bool Leftfriend_click = false;
    bool Start_click = false;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        Leftmenu = GameObject.Find("Left_Menu");
        quitcheck = GetComponent<RightmenuEvent>();
        maingameManager = GameObject.Find("Main Manager").GetComponent<MainGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime * 2;
        if (!quitcheck.quitwindow_open && !quitcheck.guidemode_open)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MousePosition = Input.mousePosition;
                MousePosition = cam.ScreenToWorldPoint(MousePosition);


                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);

                if (hit)
                {
                    Debug.Log(hit.transform.name);
                    if (hit.transform.CompareTag("Left_Menu"))
                    {
                        Leftmenu_click = true;
                        spr = (Sprite)Resources.Load("UI/Menu/menu_icon2", typeof(Sprite));
                        hit.transform.GetComponent<SpriteRenderer>().sprite = spr;

                        if (Leftmenu_open == false)
                        {
                            Timer = 1;
                            Leftmenu_open = true;
                        }
                        else
                        {
                            Timer = 1;
                            Leftmenu_open = false;
                        }
                    }
                    if (hit.transform.CompareTag("Left_book"))
                    {
                        Leftbook_click = true;
                        hit.transform.GetChild(0).localScale = new Vector3(1.1f, 1.1f);
                    }
                    if (hit.transform.CompareTag("Left_house"))
                    {
                        Lefthouse_click = true;
                        hit.transform.GetChild(0).localScale = new Vector3(1.1f, 1.1f);
                    }
                    if (hit.transform.CompareTag("Left_vilige"))
                    {
                        Leftvilige_click = true;
                        hit.transform.GetChild(0).localScale = new Vector3(1.1f, 1.1f);
                    }
                    if (hit.transform.CompareTag("Left_world"))
                    {
                        Leftworld_click = true;
                        hit.transform.GetChild(0).localScale = new Vector3(1.1f, 1.1f);
                    }
                    if (hit.transform.CompareTag("Left_friend"))
                    {
                        Debug.Log("sfasd");
                        Leftfriend_click = true;
                        hit.transform.GetChild(0).localScale = new Vector3(1.1f, 1.1f);
                    }
                    if (hit.transform.CompareTag("Start"))
                    {
                        Start_click = true;
                        hit.transform.localScale = new Vector3(1.1f, 1.1f);
                    }
                }
            }
        }

        //클릭을 땠을 때
        if (Input.GetMouseButtonUp(0))
        {
            if (Leftmenu_click)
            {
                Leftmenu_click = false;
                spr = (Sprite)Resources.Load("UI/Menu/menu_icon1", typeof(Sprite));
                Leftmenu.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spr;
            }
            if (Leftbook_click)
            {
                Leftbook_click = false;
                Leftmenu.transform.GetChild(1).GetChild(0).localScale = new Vector3(1.0f, 1.0f);
                maingameManager.scenestat = 2;
                maingameManager.Bookfirst();
            }
            if (Lefthouse_click)
            {
                Lefthouse_click = false;
                Leftmenu.transform.GetChild(2).GetChild(0).localScale = new Vector3(1.0f, 1.0f);

                maingameManager.scenestat = 1;
                maingameManager.housemizing = 0;
                maingameManager.Housingfirst();
            }
            if (Leftvilige_click)
            {
                Leftvilige_click = false;
                Leftmenu.transform.GetChild(3).GetChild(0).localScale = new Vector3(1.0f, 1.0f);
                
                maingameManager.Timer = 3;
                maingameManager.Guide.SetActive(true);

                Sprite spr = (Sprite)Resources.Load("UI/notready_guide", typeof(Sprite));
                maingameManager.Guide.transform.GetComponent<SpriteRenderer>().sprite = spr;
                maingameManager.Guide.transform.localPosition = new Vector3(0, 0);
            }
            if (Leftworld_click)
            {
                Leftworld_click = false;
                Leftmenu.transform.GetChild(4).GetChild(0).localScale = new Vector3(1.0f, 1.0f);

                maingameManager.Timer = 3;
                maingameManager.Guide.SetActive(true);

                Sprite spr = (Sprite)Resources.Load("UI/notready_guide", typeof(Sprite));
                maingameManager.Guide.transform.GetComponent<SpriteRenderer>().sprite = spr;
                maingameManager.Guide.transform.localPosition = new Vector3(0, 0);
            }
            if (Leftfriend_click)
            {
                Leftfriend_click = false;
                Leftmenu.transform.GetChild(5).GetChild(0).localScale = new Vector3(1.0f, 1.0f);

                maingameManager.Timer = 3;
                maingameManager.Guide.SetActive(true);

                Sprite spr = (Sprite)Resources.Load("UI/notready_guide", typeof(Sprite));
                maingameManager.Guide.transform.GetComponent<SpriteRenderer>().sprite = spr;
                maingameManager.Guide.transform.localPosition = new Vector3(0, 0);
            }
            if (Start_click)
            {
                // 게임 시작 버튼 부분
                Start_click = false;
                SceneManager.LoadScene("Ingame");
            }
        }
        
        //메뉴가 열릴때 애니메이션 관련
        if (Leftmenu_open)
        {
            if (Timer > 0)
            {
                Leftmenu.transform.localPosition = new Vector3(1.3f - (9.7f * Timer), -3.94f);
            }
            else
            {
                Leftmenu.transform.localPosition = new Vector3(1.3f, -3.94f);
                Leftmenu.transform.GetChild(0).localScale = new Vector3(0.8f, 0.8f);
            }
        }
        else
        {
            if (Timer > 0)
            {
                Leftmenu.transform.localPosition = new Vector3(-8.4f + (9.7f * Timer), -3.94f);
            }
            else
            {
                Leftmenu.transform.localPosition = new Vector3(-8.4f, -3.94f);
                Leftmenu.transform.GetChild(0).localScale = new Vector3(-0.8f, 0.8f);
            }
        }
    }
}
