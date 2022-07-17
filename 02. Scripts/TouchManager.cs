using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchManager : MonoBehaviour
{

    float MaxDistance = 15f;
    Camera cam;
    Vector3 MousePosition;

    public GameObject job_window;
    GameObject opening_picture;
    GameManager gameManager;

    public GameObject card_window;
    public GameObject myinfo_window;
    public GameObject question_window;

    RightmenuEvent quitcheck;

    bool Click_flower = false;
    GameObject flower;
    FlowerEvent flowerevent;
    PlayerController PC;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        flowerevent = GameObject.Find("GameManager").GetComponent<FlowerEvent>();
        PC = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        quitcheck = GetComponent<RightmenuEvent>();
        quitcheck.Rightmenu_open = false;
    }

    // Update is called once per frame
    void Update()
    {

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
                    if (gameManager.sceneNum == 1)
                    {
                        if (hit.transform.CompareTag("Job_window"))
                        {

                            job_window.SetActive(false);
                            Sprite sprt;
                            if (hit.transform.name.Equals("Frame"))
                                sprt = (Sprite)Resources.Load("Jobs/" + hit.transform.parent.name, typeof(Sprite));
                            else
                                sprt = (Sprite)Resources.Load("Jobs/" + hit.transform.name, typeof(Sprite));
                            opening_picture.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprt;
                            opening_picture = null;
                            job_window.transform.position = new Vector3(100, 0, 0);

                        }
                        else if (hit.transform.CompareTag("User_Img"))
                        {
                            if (hit.transform.root.name == "User_Picture1")
                            {
                                myinfo_window.SetActive(true);
                                myinfo_window.transform.position = new Vector3(-2.78f, 3.54f, 0);
                                job_window.SetActive(false);
                                card_window.SetActive(false);
                            }
                            else
                            {
                                if (hit.transform.position.x >= 0)
                                    job_window.transform.position = new Vector3(hit.transform.position.x - 6.7f, hit.transform.position.y, 0);
                                else
                                    job_window.transform.position = new Vector3(hit.transform.position.x + 6.7f, hit.transform.position.y, 0);

                                job_window.SetActive(true);
                                card_window.SetActive(false);
                                myinfo_window.SetActive(false);
                                card_window.transform.position = new Vector3(100, 0, 0);
                                opening_picture = GameObject.FindWithTag(hit.transform.root.name);
                            }
                        }

                        else if (hit.transform.CompareTag("User_Stuff"))
                        {
                            Sprite sprt = (Sprite)Resources.Load("Cards/" + hit.transform.GetComponent<SpriteRenderer>().sprite.name + "_card", typeof(Sprite));
                            card_window.transform.GetComponent<SpriteRenderer>().sprite = sprt;
                            float x, y;

                            if (hit.transform.root.name == "User_Picture3" || hit.transform.root.name == "User_Picture6")
                                y = -2.65f;
                            else if (hit.transform.root.name == "User_Picture1" || hit.transform.root.name == "User_Picture4")
                                y = 2.64f;
                            else
                                y = 0.42f;

                            if (hit.transform.root.name == "User_Picture1" || hit.transform.root.name == "User_Picture2" || hit.transform.root.name == "User_Picture3")
                                x = -2f;
                            else
                                x = 2f;

                            card_window.SetActive(true);
                            card_window.transform.position = new Vector3(x, y, 0);
                            job_window.SetActive(false);
                            myinfo_window.SetActive(false);
                        }

                        if(hit.transform.CompareTag("ChatEnter"))
                        {
                            gameManager.Send_Chat();                     
                        }
                    }
                    else if (gameManager.sceneNum == 2)
                    {
                        if (hit.transform.CompareTag("Question_choice"))
                        {
                            Transform q1 = GameObject.Find("Question_choice1").transform.GetChild(0);
                            Transform q2 = GameObject.Find("Question_choice2").transform.GetChild(0);
                            Transform q3 = GameObject.Find("Question_choice3").transform.GetChild(0);
                            if (hit.transform.name.Equals("Question_choice1"))
                            {
                                gameManager.player_inputvalue = 0;
                                q1.gameObject.SetActive(true);
                                q2.gameObject.SetActive(false);
                                q3.gameObject.SetActive(false);
                            }
                            else if (hit.transform.name.Equals("Question_choice2"))
                            {
                                gameManager.player_inputvalue = 1;
                                q1.gameObject.SetActive(false);
                                q2.gameObject.SetActive(true);
                                q3.gameObject.SetActive(false);
                            }
                            else if (hit.transform.name.Equals("Question_choice3"))
                            {
                                gameManager.player_inputvalue = 2;
                                q1.gameObject.SetActive(false);
                                q2.gameObject.SetActive(false);
                                q3.gameObject.SetActive(true);
                            }
                        }
                        else if (hit.transform.CompareTag("Question_confirm"))
                        {
                            if (gameManager.player_inputvalue != -1)
                            {
                                gameManager.question_confirm = true;
                            }
                        }
                        else if (hit.transform.CompareTag("Question_change"))
                        {
                            gameManager.question_change = true;
                        }
                    }
                    else if (gameManager.sceneNum == 3)
                    {
                        if (hit.transform.CompareTag("User_Img"))
                        {
                            Transform p2 = GameObject.Find("User_Picture2").transform.GetChild(1);
                            Transform p3 = GameObject.Find("User_Picture3").transform.GetChild(1);
                            Transform p4 = GameObject.Find("User_Picture4").transform.GetChild(1);
                            Transform p5 = GameObject.Find("User_Picture5").transform.GetChild(1);
                            Transform p6 = GameObject.Find("User_Picture6").transform.GetChild(1);

                            p2.GetChild(0).gameObject.SetActive(false);
                            p3.GetChild(0).gameObject.SetActive(false);
                            p4.GetChild(0).gameObject.SetActive(false);
                            p5.GetChild(0).gameObject.SetActive(false);
                            p6.GetChild(0).gameObject.SetActive(false);

                            if (!hit.transform.root.name.Equals("User_Picture1"))
                                hit.transform.root.GetChild(1).GetChild(0).gameObject.SetActive(true);

                            if (hit.transform.root.name.Equals("User_Picture2"))
                                gameManager.player_inputvalue = 1;
                            else if (hit.transform.root.name.Equals("User_Picture3"))
                                gameManager.player_inputvalue = 2;
                            else if (hit.transform.root.name.Equals("User_Picture4"))
                                gameManager.player_inputvalue = 3;
                            else if (hit.transform.root.name.Equals("User_Picture5"))
                                gameManager.player_inputvalue = 4;
                            else if (hit.transform.root.name.Equals("User_Picture6"))
                                gameManager.player_inputvalue = 5;

                        }
                        else if (hit.transform.CompareTag("Question_confirm"))
                        {
                            if (gameManager.player_inputvalue > 0)
                            {
                                gameManager.question_confirm = true;
                                Transform p2 = GameObject.Find("User_Picture2").transform.GetChild(1);
                                Transform p3 = GameObject.Find("User_Picture3").transform.GetChild(1);
                                Transform p4 = GameObject.Find("User_Picture4").transform.GetChild(1);
                                Transform p5 = GameObject.Find("User_Picture5").transform.GetChild(1);
                                Transform p6 = GameObject.Find("User_Picture6").transform.GetChild(1);

                                p2.GetChild(0).gameObject.SetActive(false);
                                p3.GetChild(0).gameObject.SetActive(false);
                                p4.GetChild(0).gameObject.SetActive(false);
                                p5.GetChild(0).gameObject.SetActive(false);
                                p6.GetChild(0).gameObject.SetActive(false);

                            }
                        }
                    }
                    else if (gameManager.sceneNum == 4)
                    {
                        if (hit.transform.CompareTag("User_Img"))
                        {
                            Transform p2 = GameObject.Find("User_Picture2").transform.GetChild(1);
                            Transform p3 = GameObject.Find("User_Picture3").transform.GetChild(1);
                            Transform p4 = GameObject.Find("User_Picture4").transform.GetChild(1);
                            Transform p5 = GameObject.Find("User_Picture5").transform.GetChild(1);
                            Transform p6 = GameObject.Find("User_Picture6").transform.GetChild(1);

                            p2.GetChild(0).gameObject.SetActive(false);
                            p3.GetChild(0).gameObject.SetActive(false);
                            p4.GetChild(0).gameObject.SetActive(false);
                            p5.GetChild(0).gameObject.SetActive(false);
                            p6.GetChild(0).gameObject.SetActive(false);

                            if (!hit.transform.root.name.Equals("User_Picture1"))
                                hit.transform.root.GetChild(1).GetChild(0).gameObject.SetActive(true);

                            if (hit.transform.root.name.Equals("User_Picture2"))
                                gameManager.player_inputvalue = 1;
                            else if (hit.transform.root.name.Equals("User_Picture3"))
                                gameManager.player_inputvalue = 2;
                            else if (hit.transform.root.name.Equals("User_Picture4"))
                                gameManager.player_inputvalue = 3;
                            else if (hit.transform.root.name.Equals("User_Picture5"))
                                gameManager.player_inputvalue = 4;
                            else if (hit.transform.root.name.Equals("User_Picture6"))
                                gameManager.player_inputvalue = 5;

                        }
                        else if (hit.transform.CompareTag("Question_confirm"))
                        {
                            if (gameManager.player_inputvalue > 0)
                            {
                                gameManager.question_confirm = true;
                                Transform p2 = GameObject.Find("User_Picture2").transform.GetChild(1);
                                Transform p3 = GameObject.Find("User_Picture3").transform.GetChild(1);
                                Transform p4 = GameObject.Find("User_Picture4").transform.GetChild(1);
                                Transform p5 = GameObject.Find("User_Picture5").transform.GetChild(1);
                                Transform p6 = GameObject.Find("User_Picture6").transform.GetChild(1);

                                p2.GetChild(0).gameObject.SetActive(false);
                                p3.GetChild(0).gameObject.SetActive(false);
                                p4.GetChild(0).gameObject.SetActive(false);
                                p5.GetChild(0).gameObject.SetActive(false);
                                p6.GetChild(0).gameObject.SetActive(false);

                            }
                        }
                    }
                    else if (gameManager.sceneNum == 5)
                    {
                        if (hit.transform.CompareTag("Swap_choice"))
                        {
                            Transform q1 = GameObject.Find("Swap_choice1").transform.GetChild(0);
                            Transform q2 = GameObject.Find("Swap_choice2").transform.GetChild(0);
                            Transform q3 = GameObject.Find("Swap_choice3").transform.GetChild(0);
                            if (hit.transform.name.Equals("Swap_choice1"))
                            {
                                gameManager.player_inputvalue = 0;
                                q1.gameObject.SetActive(true);
                                q2.gameObject.SetActive(false);
                                q3.gameObject.SetActive(false);
                            }
                            else if (hit.transform.name.Equals("Swap_choice2"))
                            {
                                gameManager.player_inputvalue = 1;
                                q1.gameObject.SetActive(false);
                                q2.gameObject.SetActive(true);
                                q3.gameObject.SetActive(false);
                            }
                            else if (hit.transform.name.Equals("Swap_choice3"))
                            {
                                gameManager.player_inputvalue = 2;
                                q1.gameObject.SetActive(false);
                                q2.gameObject.SetActive(false);
                                q3.gameObject.SetActive(true);
                            }
                        }
                        else if (hit.transform.CompareTag("Swap_confirm"))
                        {
                            if (gameManager.player_inputvalue != -1)
                            {
                                gameManager.question_confirm = true;
                            }
                        }
                    }// 게임이 끝났을 때
                    else if (gameManager.sceneNum == 6)
                    {
                        if (hit.transform.CompareTag("User_Stuff"))
                        {
                            if (hit.transform.root.name.Equals("User_Picture1"))
                            {
                                if (gameManager.stuff_check[0])
                                {
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "Player_window";
                                    hit.transform.root.GetChild(2).gameObject.SetActive(true);
                                    
                                    Sprite spr = (Sprite)Resources.Load("Stuffs/" + gameManager.GetStuff(gameManager.user_my_stuff[0]), typeof(Sprite));
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite=spr;
                                    gameManager.stuff_check[0] = false;

                                    if (gameManager.user_my_stuff[0] >= 26 && gameManager.user_my_stuff[0] <= 41 && gameManager.user_my_stuff[0] != 34 && gameManager.user_my_stuff[0] != 35)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[0] >= 58 && gameManager.user_my_stuff[0] <= 65)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[0] >= 88 && gameManager.user_my_stuff[0] <= 95)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[0] >= 22 && gameManager.user_my_stuff[0] <= 25)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.35f, 0.35f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.55f, 2.6f);
                                    }
                                    else
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.4f, 0.4f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.27f, 2.3f);
                                    }
                                }
                                else
                                {
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "stuff_img";
                                    hit.transform.root.GetChild(2).gameObject.SetActive(false);
                                    for (int i = 0; i < 3; i++)
                                    {
                                        gameManager.Change_Stuff_img(0, i, gameManager.user_stuff[0, i]);
                                    }
                                    gameManager.stuff_check[0] = true;
                                }
                            }
                            else if (hit.transform.root.name.Equals("User_Picture2"))
                            {
                                if (gameManager.stuff_check[1])
                                {
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "Player_window";
                                    hit.transform.root.GetChild(2).gameObject.SetActive(true);

                                    Sprite spr = (Sprite)Resources.Load("Stuffs/" + gameManager.GetStuff(gameManager.user_my_stuff[1]), typeof(Sprite));
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
                                    gameManager.stuff_check[1] = false;

                                    if (gameManager.user_my_stuff[1] >= 26 && gameManager.user_my_stuff[1] <= 41 && gameManager.user_my_stuff[1] != 34 && gameManager.user_my_stuff[1] != 35)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[1] >= 58 && gameManager.user_my_stuff[1] <= 65)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[1] >= 88 && gameManager.user_my_stuff[1] <= 95)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[1] >= 22 && gameManager.user_my_stuff[1] <= 25)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.35f, 0.35f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.55f, 2.6f);
                                    }
                                    else
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.4f, 0.4f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.27f, 2.3f);
                                    }
                                }
                                else
                                {
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "stuff_img";
                                    hit.transform.root.GetChild(2).gameObject.SetActive(false);
                                    for (int i = 0; i < 3; i++)
                                    {
                                        gameManager.Change_Stuff_img(1, i, gameManager.user_stuff[1, i]);
                                    }
                                    gameManager.stuff_check[1] = true;
                                }
                            }
                            else if (hit.transform.root.name.Equals("User_Picture3"))
                            {
                                if (gameManager.stuff_check[2])
                                {
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "Player_window";
                                    hit.transform.root.GetChild(2).gameObject.SetActive(true);

                                    Sprite spr = (Sprite)Resources.Load("Stuffs/" + gameManager.GetStuff(gameManager.user_my_stuff[2]), typeof(Sprite));
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
                                    gameManager.stuff_check[2] = false;

                                    if (gameManager.user_my_stuff[2] >= 26 && gameManager.user_my_stuff[2] <= 41 && gameManager.user_my_stuff[2] != 34 && gameManager.user_my_stuff[2] != 35)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[2] >= 58 && gameManager.user_my_stuff[2] <= 65)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[2] >= 88 && gameManager.user_my_stuff[2] <= 95)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[2] >= 22 && gameManager.user_my_stuff[2] <= 25)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.35f, 0.35f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.55f, 2.6f);
                                    }
                                    else
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.4f, 0.4f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.27f, 2.3f);
                                    }
                                }
                                else
                                {
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "stuff_img";
                                    hit.transform.root.GetChild(2).gameObject.SetActive(false);
                                    for (int i = 0; i < 3; i++)
                                    {
                                        gameManager.Change_Stuff_img(2, i, gameManager.user_stuff[2, i]);
                                    }
                                    gameManager.stuff_check[2] = true;
                                }
                            }
                            else if (hit.transform.root.name.Equals("User_Picture4"))
                            {
                                if (gameManager.stuff_check[3])
                                {
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "Player_window";
                                    hit.transform.root.GetChild(2).gameObject.SetActive(true);

                                    Sprite spr = (Sprite)Resources.Load("Stuffs/" + gameManager.GetStuff(gameManager.user_my_stuff[3]), typeof(Sprite));
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
                                    gameManager.stuff_check[3] = false;

                                    if (gameManager.user_my_stuff[3] >= 26 && gameManager.user_my_stuff[3] <= 41 && gameManager.user_my_stuff[3] != 34 && gameManager.user_my_stuff[3] != 35)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[3] >= 58 && gameManager.user_my_stuff[3] <= 65)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[3] >= 88 && gameManager.user_my_stuff[3] <= 95)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[3] >= 22 && gameManager.user_my_stuff[3] <= 25)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.35f, 0.35f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.55f, 2.6f);
                                    }
                                    else
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.4f, 0.4f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.27f, 2.3f);
                                    }
                                }
                                else
                                {
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "stuff_img";
                                    hit.transform.root.GetChild(2).gameObject.SetActive(false);
                                    for (int i = 0; i < 3; i++)
                                    {
                                        gameManager.Change_Stuff_img(3, i, gameManager.user_stuff[3, i]);
                                    }
                                    gameManager.stuff_check[3] = true;
                                }
                            }
                            else if (hit.transform.root.name.Equals("User_Picture5"))
                            {
                                if (gameManager.stuff_check[4])
                                {
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "Player_window";
                                    hit.transform.root.GetChild(2).gameObject.SetActive(true);

                                    Sprite spr = (Sprite)Resources.Load("Stuffs/" + gameManager.GetStuff(gameManager.user_my_stuff[4]), typeof(Sprite));
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
                                    gameManager.stuff_check[4] = false;

                                    if (gameManager.user_my_stuff[4] >= 26 && gameManager.user_my_stuff[4] <= 41 && gameManager.user_my_stuff[4] != 34 && gameManager.user_my_stuff[4] != 35)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[4] >= 58 && gameManager.user_my_stuff[4] <= 65)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[4] >= 88 && gameManager.user_my_stuff[4] <= 95)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[4] >= 22 && gameManager.user_my_stuff[4] <= 25)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.35f, 0.35f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.55f, 2.6f);
                                    }
                                    else
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.4f, 0.4f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.27f, 2.3f);
                                    }
                                }
                                else
                                {
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "stuff_img";
                                    hit.transform.root.GetChild(2).gameObject.SetActive(false);
                                    for (int i = 0; i < 3; i++)
                                    {
                                        gameManager.Change_Stuff_img(4, i, gameManager.user_stuff[4, i]);
                                    }
                                    gameManager.stuff_check[4] = true;
                                }
                            }
                            else if (hit.transform.root.name.Equals("User_Picture6"))
                            {
                                if (gameManager.stuff_check[5])
                                {
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "Player_window";
                                    hit.transform.root.GetChild(2).gameObject.SetActive(true);

                                    Sprite spr = (Sprite)Resources.Load("Stuffs/" + gameManager.GetStuff(gameManager.user_my_stuff[5]), typeof(Sprite));
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
                                    gameManager.stuff_check[5] = false;

                                    if (gameManager.user_my_stuff[5] >= 26 && gameManager.user_my_stuff[5] <= 41 && gameManager.user_my_stuff[5] != 34 && gameManager.user_my_stuff[5] != 35)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[5] >= 58 && gameManager.user_my_stuff[5] <= 65)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[5] >= 88 && gameManager.user_my_stuff[5] <= 95)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                                    }
                                    else if (gameManager.user_my_stuff[5] >= 22 && gameManager.user_my_stuff[5] <= 25)
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.35f, 0.35f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.55f, 2.6f);
                                    }
                                    else
                                    {
                                        hit.transform.root.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.4f, 0.4f);
                                        hit.transform.root.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.27f, 2.3f);
                                    }
                                }
                                else
                                {
                                    hit.transform.root.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "stuff_img";
                                    hit.transform.root.GetChild(2).gameObject.SetActive(false);
                                    for (int i = 0; i < 3; i++)
                                    {
                                        gameManager.Change_Stuff_img(5, i, gameManager.user_stuff[5, i]);
                                    }
                                    gameManager.stuff_check[5] = true;
                                }
                            }
                        }
                        if(hit.transform.CompareTag("Result_Gift"))
                        {
                            if (hit.transform.name.Equals("gift1"))
                            {
                                if (gameManager.gift_open[0] == false)
                                {
                                    int randomi = Random.Range(0, 96);
                                    Sprite spr = (Sprite)Resources.Load("StuffFrames/" + gameManager.GetStuff(randomi), typeof(Sprite));
                                    hit.transform.GetComponent<SpriteRenderer>().sprite = spr;
                                    hit.transform.localScale = new Vector3(0.3f, 0.3f);
                                    hit.transform.GetComponent<Animator>().runtimeAnimatorController = null;

                                    gameManager.gift_open[0] = true;
                                    gameManager.gift[0] = randomi;
                                }
                            }
                            if (hit.transform.name.Equals("gift2"))
                            {
                                if (gameManager.gift_open[1] == false)
                                {
                                    int randomi = Random.Range(0, 96);
                                    Sprite spr = (Sprite)Resources.Load("StuffFrames/" + gameManager.GetStuff(randomi), typeof(Sprite));
                                    hit.transform.GetComponent<SpriteRenderer>().sprite = spr;
                                    hit.transform.localScale = new Vector3(0.3f, 0.3f);
                                    hit.transform.GetComponent<Animator>().runtimeAnimatorController = null;

                                    gameManager.gift_open[1] = true;
                                    gameManager.gift[1] = randomi;
                                }
                            }
                            if (hit.transform.name.Equals("gift3"))
                            {
                                if (gameManager.gift_open[2] == false)
                                {
                                    Sprite spr = (Sprite)Resources.Load("StuffFrames/" + gameManager.GetStuff(gameManager.user_my_stuff[PC.order]), typeof(Sprite));
                                    hit.transform.GetComponent<SpriteRenderer>().sprite = spr;
                                    hit.transform.localScale = new Vector3(0.3f, 0.3f);
                                    hit.transform.GetComponent<Animator>().runtimeAnimatorController = null;

                                    gameManager.gift_open[2] = true;
                                    gameManager.gift[2] = gameManager.user_my_stuff[PC.order];
                                }
                            }                           
                        }
                        if(hit.transform.CompareTag("ToMain"))
                        {
                            SceneManager.LoadScene("main scene");    
                        }
                    }

                    if (hit.transform.CompareTag("Windy_flower"))
                    {
                        hit.transform.localScale = new Vector3(1.1f, 1.1f);
                        Click_flower = true;
                        flower = hit.transform.gameObject;
                    }
                }
                else
                {
                    job_window.SetActive(false);
                    card_window.SetActive(false);
                    myinfo_window.SetActive(false);
                }

            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(Click_flower)
            {
                Click_flower = false;
                flower.transform.localScale = new Vector3(1f, 1f);
                flower.SetActive(false);
                flowerevent.Hitflower();
            }
        }
        
    } 
    
}

