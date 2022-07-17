using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameObject player_window;
    GameObject myinfo_window;
    GameObject question_window;
    GameObject question_target_window;
    GameObject swap_target_window;
    GameObject swap_userstuff_window;
    GameObject timer_bomb;
    GameObject waiting_anim;

    Transform timer_bomb_wick;
    TouchManager touchManager;

    GameManager gameManager;
    
    Sprite spr;

    
    public Text timer;

    int question_anim = 0;
    int swap_anim = 0;

    public int order;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player_window = GameObject.Find("Player_window");
        myinfo_window = GameObject.Find("Myinfo_window");
        question_window = GameObject.Find("Question_window");
        question_target_window = GameObject.Find("Question_target_window");
        swap_target_window = GameObject.Find("Swap_target_window");
        swap_userstuff_window = GameObject.Find("Swap_userstuff_window");
        timer_bomb = GameObject.Find("Timer_bomb");
        timer_bomb_wick = GameObject.Find("Timer_bomb_wick").transform;
        touchManager = GameObject.Find("Main Camera").GetComponent<TouchManager>();
        waiting_anim = GameObject.Find("Waiting_anim");

        waiting_anim.SetActive(false);

        order = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameManager.game_phase)
        {
            case 0:
                {
                    // 처음 자신의 직업과 물건 확인        
                    spr = (Sprite)Resources.Load("Cards/" + gameManager.GetJob(gameManager.user_job[0]) + "_card", typeof(Sprite));
                    player_window.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
                    myinfo_window.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spr;

                    spr = (Sprite)Resources.Load("Cards/" + gameManager.GetStuff(gameManager.user_my_stuff[0]) + "_card", typeof(Sprite));
                    player_window.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = spr;
                    myinfo_window.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
                                        
                    break;
                }
            case 1:
                {  
                    if(player_window.activeSelf)
                        player_window.SetActive(false);
                    question_anim = 0;
                    touchManager.card_window.SetActive(false);
                    touchManager.job_window.SetActive(false);
                    touchManager.myinfo_window.SetActive(false);
                    break;
                }
            case 2:
                {
                    if (gameManager.player_order == order)
                    {

                        touchManager.card_window.SetActive(false);
                        touchManager.job_window.SetActive(false);
                        touchManager.myinfo_window.SetActive(false);
                        

                        gameManager.sceneNum = 2;
                        question_window.transform.localPosition = new Vector3(0, 0);
                                             
                        for (int i = 0; i < 3; i++)
                        {
                            spr = (Sprite)Resources.Load("Questions/" + gameManager.GetQuestion(gameManager.user_question[i]), typeof(Sprite));
                            question_window.transform.GetChild(i + 1).GetComponent<SpriteRenderer>().sprite = spr;

                            if (question_anim<3)
                            {
                                question_window.transform.GetChild(i + 1).gameObject.SetActive(false);
                                question_window.transform.GetChild(i + 1).gameObject.SetActive(true);
                                question_anim ++;
                            }
                        }
                        
                        if(gameManager.question_change_complete)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                spr = (Sprite)Resources.Load("Questions/" + gameManager.GetQuestion(gameManager.user_question[i]), typeof(Sprite));
                                question_window.transform.GetChild(i + 1).GetComponent<SpriteRenderer>().sprite = spr;
                                question_window.transform.GetChild(i + 1).gameObject.SetActive(false);
                                question_window.transform.GetChild(i + 1).gameObject.SetActive(true);
                            }
                            gameManager.question_change = false;
                            gameManager.question_change_complete = false;
                            question_window.transform.GetChild(5).transform.localPosition = new Vector3(100.0f, 0.0f);
                            
                        }

                       if(gameManager.Timer<=20)
                        {
                            int second = (int)gameManager.Timer;
                            timer.text = second.ToString();
                            timer_bomb.transform.localPosition = new Vector3(0, -0.27f);
                            timer_bomb_wick.localPosition = new Vector3(0, -2.15f - ((0.45f / 20f) * (20 - second)));
                        }
                    }
                    break;
                }
            case 3:
                {
                    if (gameManager.player_order == order)
                    {
                        question_window.transform.localPosition = new Vector3(100, 0);
                        timer.text = null;
                        timer_bomb.transform.localPosition = new Vector3(100, 0);
                        question_window.transform.GetChild(5).transform.localPosition = new Vector3(-4.4f, -4.1f);
                    }
                    break;
                }
            case 4:
                {
                    if (gameManager.player_order == order)
                    {
                        question_target_window.transform.localPosition = new Vector3(0, 0);
                        gameManager.sceneNum = 3;
                        if (gameManager.Timer <= 20)
                        {
                            int second = (int)gameManager.Timer;
                            timer.text = second.ToString();
                            timer_bomb.transform.localPosition = new Vector3(0, -0.27f);
                            timer_bomb_wick.localPosition = new Vector3(0, -2.15f - ((0.45f / 20f)*(20-second)));
                        }
                        
                    }
                    
                    break;
                }
            case 5:
                {
                    if (gameManager.player_order == order)
                    {
                        question_target_window.transform.localPosition = new Vector3(100, 0);
                        timer.GetComponent<Text>().text = null;
                        timer_bomb.transform.localPosition = new Vector3(100, 0);
                    }

                    break;
                }
            case 61:
                {
                    if(gameManager.player_order == order)
                    {
                        gameManager.sceneNum = 1;
                    }
                    break;
                }
            case 7:
                {
                    if (gameManager.player_order == order)
                    {
                        swap_target_window.transform.localPosition = new Vector3(0, 0);
                        gameManager.sceneNum = 4;
                        if (gameManager.Timer <= 20)
                        {
                            int second = (int)gameManager.Timer;
                            timer.GetComponent<Text>().text = second.ToString();
                            timer_bomb.transform.localPosition = new Vector3(0, -0.27f);
                            timer_bomb_wick.localPosition = new Vector3(0, -2.15f - ((0.45f / 20f) * (20 - second)));
                        }
                        swap_anim = 0;
                    }
                    break;
                }
            case 8:
                {
                    if (gameManager.player_order == order)
                    {
                        swap_target_window.transform.localPosition = new Vector3(100, 0);
                        swap_userstuff_window.transform.localPosition = new Vector3(0, 0);
                        gameManager.sceneNum = 5;
                        for (int i = 0; i < 3; i++)
                        {
                            spr = (Sprite)Resources.Load("Cards/" + gameManager.GetStuff(gameManager.user_stuff[gameManager.swap_target,i])+"_card", typeof(Sprite));
                            swap_userstuff_window.transform.GetChild(i + 1).GetComponent<SpriteRenderer>().sprite = spr;
                            if (swap_anim < 3)
                            {
                                swap_userstuff_window.transform.GetChild(i + 1).gameObject.SetActive(false);
                                swap_userstuff_window.transform.GetChild(i + 1).gameObject.SetActive(true);
                                swap_anim++;
                            }
                        }
                        if (gameManager.Timer <= 20)
                        {
                            int second = (int)gameManager.Timer;
                            timer.GetComponent<Text>().text = second.ToString();
                            timer_bomb.transform.localPosition = new Vector3(0, -0.27f);
                            timer_bomb_wick.localPosition = new Vector3(0, -2.15f - ((0.45f / 20f) * (20 - second)));
                        }
                    }
                    break;
                }
            case 9:
                {
                    if (gameManager.player_order == order)
                    {
                        gameManager.sceneNum = 1;
                        swap_userstuff_window.transform.localPosition = new Vector3(100, 0);
                        timer.GetComponent<Text>().text = null;
                        timer_bomb.transform.localPosition = new Vector3(100, 0);
                        waiting_anim.SetActive(true);

                    }
                    else if(gameManager.swap_target == order)
                    {
                        swap_target_window.transform.localPosition = new Vector3(100, 0);
                        swap_userstuff_window.transform.localPosition = new Vector3(0, 0);
                        gameManager.sceneNum = 5;
                        for (int i = 0; i < 3; i++)
                        {
                            spr = (Sprite)Resources.Load("Cards/" + gameManager.GetStuff(gameManager.user_stuff[gameManager.player_order, i]) + "_card", typeof(Sprite));
                            swap_userstuff_window.transform.GetChild(i + 1).GetComponent<SpriteRenderer>().sprite = spr;
                            if (swap_anim < 3)
                            {
                                swap_userstuff_window.transform.GetChild(i + 1).gameObject.SetActive(false);
                                swap_userstuff_window.transform.GetChild(i + 1).gameObject.SetActive(true);
                                swap_anim++;
                            }
                        }
                        if (gameManager.Timer <= 20)
                        {
                            int second = (int)gameManager.Timer;
                            timer.GetComponent<Text>().text = second.ToString();
                            timer_bomb.transform.localPosition = new Vector3(0, -0.27f);
                            timer_bomb_wick.localPosition = new Vector3(0, -2.15f - ((0.45f / 20f) * (20 - second)));
                        }
                    }
                    break;
                }
            case 10:
                {

                    
                    gameManager.sceneNum = 1;
                    swap_userstuff_window.transform.localPosition = new Vector3(100, 0);
                    timer.GetComponent<Text>().text = null;
                    timer_bomb.transform.localPosition = new Vector3(100, 0);
                    waiting_anim.SetActive(false);
                    touchManager.card_window.SetActive(false);
                    touchManager.job_window.SetActive(false);
                    touchManager.myinfo_window.SetActive(false);
                    break;
                }
            case 30:
                {
                    gameManager.sceneNum = 10;
                    break;
                }
            case 31:
                {
                    gameManager.sceneNum = 6;
                    break;
                }
                
        }
    }
}
