using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
    Jobs
    0 - thief
    1 - thief's thief
    2 - begger
    3~5 = citizen

    0~9 : cat
    10~11 : apple
    12~13 : pencil
    14~17 : shit
    18~21 : bear
    22~25 : tree
    26~33 : candy
    34~35 : stone
    36~41 : mouse
    42~49 : book
    50~57 : ant
    58~65 : bag
    66~73 : dog
    74~75 : persimmon
    76~79 : peppar
    80~87 : ghost
    88~95 : butterfly
*/

public class GameManager : MonoBehaviour
{
    public GameObject my_stuff_window;

    public float start_Timer = 15;
    public int[] user_job=new int[6];
    string[] user_name = new string[6];
    public int[,] user_stuff= new int[6,3];
    public int[] user_my_stuff = new int[6];
    int[] game_stuff = new int[18];
    public int[] user_question = new int[3];


    public int user_question_choice = 0;
    
    public int swap_target_stuff, swap_my_stuff;
    public int swap_target_x, swap_my_x;

    int randomi;
    bool[] random_check;

    bool changeani=true;
    bool isblind = true;
  
    int turn_count = 0;
    int turn_phase = 0;
    GameObject player,stuff_img;
    GameObject player_turn_display;
    GameObject background;
    GameObject change_window;
    GameObject myturn;
    GameObject blind_window;
    GameObject win_anim;
    GameObject lose_anim;
    GameObject result_window;
    AudioSFXManager SFX;

    FlowerEvent flowerevent;
    PlayerController PC;

    AIscript[] Ai=new AIscript[5];

    Text result1, result2, result3;
    Sprite spr;

    public int player_inputvalue=-1;
    public int sceneNum = 0;
    public int game_phase ;
    public int player_order;
    public int question_target = 0;
    public int swap_target = 0;
    public bool question_confirm = false;
    public bool question_change = false;
    public bool question_change_complete = false;
    public float Timer;
    public bool[] user_result = new bool[6];

    // 마지막에 물건을 체크하기 위한
    public bool[] stuff_check = new bool[6];
    public bool[] gift_open = new bool[3];
    public int[] gift = new int[3];
    public RuntimeAnimatorController losegift;

    string message;

    
    /* 
      0 초기 상태
      1 유저 차례 안내
      2 유저 질문 선택
      3 물건 교환 선택 시간
      4 물건 교환 확인
      5
    */
    private Text logText = null;
    private ScrollRect log_scroll_Rect = null;
    private Text chatText = null;
    private ScrollRect chat_scroll_Rect = null;
    private InputField chatInput = null;


    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);

        // 오브젝트 초기화
        
        logText = GameObject.Find("Log_Text").GetComponent<Text>();
        log_scroll_Rect = GameObject.Find("Scroll_View").GetComponent<ScrollRect>();
        chatText = GameObject.Find("Chat_Text").GetComponent<Text>();
        chat_scroll_Rect = GameObject.Find("Chat_View").GetComponent<ScrollRect>();
        chatInput = GameObject.Find("ChatInput").GetComponent<InputField>();
        player_turn_display = GameObject.Find("player_turn");
        change_window = GameObject.Find("Change");
        myturn = GameObject.Find("Myturn");
        blind_window = GameObject.Find("Blind_window");
        win_anim = GameObject.Find("Win_anim");
        lose_anim = GameObject.Find("Lose_anim");
        result_window = GameObject.Find("Result_window");
        flowerevent = GameObject.Find("GameManager").GetComponent<FlowerEvent>();
        PC = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        SFX = GameObject.Find("AudioSFXManager").GetComponent<AudioSFXManager>();


        result1 = GameObject.Find("Result_text1").GetComponent<Text>();
        result2 = GameObject.Find("Result_text2").GetComponent<Text>();
        result3 = GameObject.Find("Result_text3").GetComponent<Text>();

        Ai[0] = GameObject.Find("AI1").GetComponent<AIscript>();
        Ai[1] = GameObject.Find("AI2").GetComponent<AIscript>();
        Ai[2] = GameObject.Find("AI3").GetComponent<AIscript>();
        Ai[3] = GameObject.Find("AI4").GetComponent<AIscript>();
        Ai[4] = GameObject.Find("AI5").GetComponent<AIscript>();


        // 승리 패배 화면 비활성화
        win_anim.SetActive(false);
        lose_anim.SetActive(false);
        result_window.SetActive(false);

        // 배경색 설정
        background = GameObject.Find("Background");
        randomi = Random.Range(0, 5);
        spr = (Sprite)Resources.Load("UI/background" + ((int)randomi+1), typeof(Sprite));
        background.GetComponent<Image>().sprite = spr;
        

        my_stuff_window.SetActive(true);

        // 랜덤으로 직업 설정
         random_check = new bool[6];
         for(int i=0;i<6;i++)
         {
            while(true)
            {               
                randomi = Random.Range(0, 6);
                if (!random_check[randomi])
                {
                    random_check[randomi] = true;
                    user_job[randomi] = i;
                    break;
                }
            }
            
         }
         
        // 랜덤으로 게임의 18개의 물건 선정
        random_check = new bool[96];
        for (int i = 0; i < 18; i++)
        {
            while (true)
            {
                randomi = Random.Range(0, 96);
                if (!random_check[(int)randomi])
                {
                    random_check[(int)randomi] = true;
                    game_stuff[i] = (int)randomi;
                    break;
                }
            }
        }
        
        // 랜덤으로 물건 배정
        Random_Stuff(false);
        
        // 랜덤으로 자신의 물건 정하기
        for(int i=0;i<6;i++)
        {
            randomi = Random.Range(0, 3);
            user_my_stuff[i] = user_stuff[i, (int)randomi];

            Debug.Log(i+"번 직업 : " + GetKoreanJob(user_job[i]) + " 물건 : " + GetKoreanStuff(user_my_stuff[i]));
        }

        // 처음 확인을 위한 타이머
        Timer = 10;

        // Player 순서 정하기
        randomi = Random.Range(0, 6);
        player_order = randomi;

        for(int i=0;i<5;i++)
        {
            Ai[i].job = user_job[i + 1];
            Ai[i].order = i + 1;
        }

        for (int i = 0; i < 6; i++)
            stuff_check[i] = false;

        for(int i=0;i<3;i++)
        {
            gift[i] = -1;
            gift_open[i] = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        switch(game_phase)
        {
            case 0:
                {
                    if (Timer < 0)
                    {
                        
                        Random_Stuff(true);

                        // Player's picture change
                        player = GameObject.FindWithTag("User_Picture1");
                        spr = (Sprite)Resources.Load("Jobs/" + GetJob(user_job[0]), typeof(Sprite));

                        player.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
                        message = "<color=#ea68a2>" + "사회자 : 당신의 역할은 \'" + GetKoreanJob(user_job[0]) + "\'입니다." + "</color>";
                        Send_Log(message);
                        message = "<color=#ea68a2>" + "사회자 : 당신의 물건은 \'" + GetKoreanStuff(user_my_stuff[0]) + "\'입니다." + "</color>";
                        Send_Log(message);

                        sceneNum = 1;
                        turn_phase = 1;
                        game_phase = 1;
                     
                    }
                    break;
                }
            case 1:
                {
                    question_change_complete = false;
                    question_change = false;
                    turn_count++;
                    // 차례에 따른 로그 출력
                    message = "<color=#ea68a2>" + "사회자 : " +(player_order + 1) + "번 유저의 차례입니다." + "</color>";
                    Send_Log(message);
                    GameObject now_player = GameObject.Find("User_Picture" + (player_order + 1));

                    // 화살표 위치 수정
                    if (player_order < 3)
                    {
                        player_turn_display.transform.localScale = new Vector3(0.4f, 0.3f);
                        player_turn_display.transform.localPosition = new Vector3(now_player.transform.localPosition.x - 1.65f, now_player.transform.localPosition.y);
                    }
                    else
                    {
                        player_turn_display.transform.localScale = new Vector3(-0.4f, 0.3f);
                        player_turn_display.transform.localPosition = new Vector3(now_player.transform.localPosition.x + 1.65f, now_player.transform.localPosition.y);
                    }

                    // 턴에 따른 질문의 종류 선정 1턴 : 개인 2턴부터 전체 가능
                    if (turn_phase == 1)
                    {
                        random_check = new bool[20];
                        for (int i = 0; i < 3; i++)
                        {
                            while (true)
                            {
                                randomi = Random.Range(0, 20);
                                if (!random_check[randomi])
                                {
                                    random_check[randomi] = true;
                                    user_question[i] = randomi;
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        random_check = new bool[42];
                        for (int i = 0; i < 3; i++)
                        {
                            while (true)
                            {
                                randomi = Random.Range(0, 42);
                                if (randomi >= 27 && randomi <= 29)
                                    continue;
                                if (!random_check[randomi])
                                {
                                    random_check[randomi] = true;
                                    user_question[i] = randomi;
                                    break;
                                }
                            }

                        }
                    }
                    
                    
                    question_confirm = false;

                    game_phase = 21;
                    Timer = 3;
                    if (player_order == 0)
                    {
                        myturn.transform.GetChild(0).gameObject.SetActive(true);
                        spr = (Sprite)Resources.Load("UI/turn_" + GetJob(user_job[player_order]), typeof(Sprite));
                        myturn.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spr;
                    }
                    break;
                }
            case 21:
                {   // 만약 내차례일 경우 내차례라는 애니메이션 출력하기위한 case
                    if(Timer < 0)
                    {
                        game_phase = 22;
                        if (turn_count == 1 && turn_phase == 1)
                            Timer = 5;
                        else
                            Timer = 0;
                        if(turn_phase==1)
                            Send_Log("<color=#ea68a2>" + "사회자 : " + (player_order + 1) + "번 유저는 각자의 역할을 추리할 수 있는 질문 카드를 선택하세요." + "</color>");
                        else
                            Send_Log("<color=#ea68a2>" + "사회자 : " + (player_order + 1) + "번 유저는 질문 카드를 선택하세요." + "</color>");
                        myturn.transform.GetChild(0).gameObject.SetActive(false);

                    }
                    break;
                }
            case 22:
                {
                    log_scroll_Rect.verticalNormalizedPosition = 0f;
                    if (Timer <0)
                    {
                        game_phase = 2;
                        Timer = 15;
                        log_scroll_Rect.verticalNormalizedPosition = 0f;
                    }
                    break;
                }
            case 2:
                {
                    // 선택하거나 60초 지날경우 3페이즈로 이동
                    if (question_confirm)
                    {
                        game_phase = 3;
                        question_confirm = false;
                        user_question_choice = user_question[player_inputvalue];
                        player_inputvalue = -1;
                        Selectoff();
                        SFX.countOn = true;
                        break;
                    }
                    if(question_change)
                    {
                        if (turn_phase == 1)
                        {
                            random_check = new bool[24];
                            for (int i = 0; i < 3; i++)
                            {
                                while (true)
                                {
                                    randomi = Random.Range(0, 24);
                                    if (!random_check[randomi])
                                    {
                                        random_check[randomi] = true;
                                        user_question[i] = randomi;
                                        break;
                                    }
                                }

                            }
                        }
                        else
                        {
                            random_check = new bool[42];
                            for (int i = 0; i < 3; i++)
                            {
                                while (true)
                                {
                                    randomi = Random.Range(0, 42);
                                    if (randomi >= 24 && randomi <= 29)
                                        continue;
                                    if (!random_check[randomi])
                                    {
                                        random_check[randomi] = true;
                                        user_question[i] = randomi;
                                        break;
                                    }
                                }

                            }
                        }
                        question_change_complete = true;
                    }
                    if(Timer < 0 && !question_confirm) // 시간 초가 지나면 3개의 선택지중 한개 랜덤 선택
                    {
                        randomi = Random.Range(0, 3);
                        if(player_inputvalue == -1)
                            player_inputvalue = randomi;
                        question_confirm = true;
                        Selectoff();
                    }
                    break;
                }
            case 3:
                {
                    //질문의 종류에 따라 질문 대상을 선택해야 하는지 확인
                    string[] question_split = GetQuestion(user_question_choice).Split('_');

                    if (question_split[1].Equals("a"))
                        game_phase = 6;
                    else
                    {
                        game_phase = 4;
                        Timer = 10;
                    }
                    break;
                }
            case 4:
                {
                    // 질문 대상을 선택하고 나면 넘김
                    if (question_confirm)
                    {
                        game_phase = 5;
                        question_confirm = false;
                        question_target = player_inputvalue;
                        player_inputvalue = -1;
                        Selectoff();
                        SFX.countOn = true;
                        break;
                    }
                    if (Timer < 0 && !question_confirm)
                    {   if (player_inputvalue == -1)
                        {
                            while (true)
                            {
                                randomi = Random.Range(0, 6);
                                if (player_order == randomi)
                                    continue;
                                player_inputvalue = randomi;

                                break;
                            }
                        }
                        question_confirm = true;
                        Selectoff();
                    }              
                    break;
                }
            case 5:
                {
                    //개인 질문에 따른 대답
                    bool answer = false;

                    if (user_job[question_target] < 3)
                        answer = !answer;
                    switch (user_question_choice)
                    {
                        case 0:
                            {
                                if (IsBlue(user_my_stuff[question_target]))
                                    answer = !answer;
                                
                                Send_Log((player_order+1)+"번 유저 : "+(question_target+1) + "번 유저의 물건은 배경이 하늘색인가요?");
                                                                
                                if(answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 1:
                            {
                                if (IsGray(user_my_stuff[question_target]))
                                    answer = !answer;
                                
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 배경이 연회색인가요?");
                                
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 2:
                            {
                                if (IsGreen(user_my_stuff[question_target]))
                                    answer = !answer;
                                
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 배경이 연두색인가요?");
                                
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 3:
                            {
                                if (IsPink(user_my_stuff[question_target]))
                                    answer = !answer;
                                
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 배경이 분홍색인가요?");
                                
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 4:
                            {
                                if (IsYellow(user_my_stuff[question_target]))
                                    answer = !answer;
                                
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 배경이 노란색인가요?");
                                
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 5:
                            {
                                if (IsApple(user_my_stuff[question_target]))
                                    answer = !answer;
                                
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 사과인가요?");
                                
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 6:
                            {
                                if (IsBear(user_my_stuff[question_target]))
                                    answer = !answer;
                                
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 곰인가요?");
                                
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 7:
                            {
                                if (IsCat(user_my_stuff[question_target]))
                                    answer = !answer;
                                
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 고양이인가요?");
                                
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 8:
                            {
                                if (IsPencil(user_my_stuff[question_target]))
                                    answer = !answer;
                                
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 연필인가요?");
                                
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 9:
                            {
                                if (IsShit(user_my_stuff[question_target]))
                                    answer = !answer;
                                
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 똥인가요?");
                                
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 10:
                            { 
                                if (IsTree(user_my_stuff[question_target]))
                                    answer = !answer;
                                
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 나무인가요?");
                                
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 11:
                            {
                                if (IsCandy(user_my_stuff[question_target]))
                                    answer = !answer;

                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 사탕인가요?");

                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 12:
                            {
                                if (IsStone(user_my_stuff[question_target]))
                                    answer = !answer;

                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 돌인가요?");

                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 13:
                            {
                                if (IsMouse(user_my_stuff[question_target]))
                                    answer = !answer;

                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 쥐인가요?");

                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 14:
                            {
                                if (IsBook(user_my_stuff[question_target]))
                                    answer = !answer;

                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 책인가요?");

                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 15:
                            {
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저가 가지고 있는 물건 중 배경이 하늘색인 물건이 있나요?");
                                int have = 0;
                                for (int j = 0; j < 3; j++)
                                {
                                    if (IsBlue(user_stuff[question_target, j]))
                                    {
                                        have++;
                                        break;
                                    }
                                }
                                if (have >0)
                                    answer = !answer;
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 16:
                            {
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저가 가지고 있는 물건 중 배경이 연회색인 물건이 있나요?");
                                int have = 0;
                                for (int j = 0; j < 3; j++)
                                {
                                    if (IsGray(user_stuff[question_target, j]))
                                    {
                                        have++;
                                        break;
                                    }
                                }
                                if (have > 0)
                                    answer = !answer;
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 17:
                            {
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저가 가지고 있는 물건 중 배경이 연두색인 물건이 있나요?");

                                int have = 0;
                                for (int j = 0; j < 3; j++)
                                {
                                    if (IsGreen(user_stuff[question_target, j]))
                                    {
                                        have++;
                                        break;
                                    }
                                }
                                if (have > 0)
                                    answer = !answer;
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 18:
                            {
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저가 가지고 있는 물건 중 배경이 분홍색인 물건이 있나요?");

                                int have = 0;
                                for (int j = 0; j < 3; j++)
                                {
                                    if (IsPink(user_stuff[question_target, j]))
                                    {
                                        have++;
                                        break;
                                    }
                                }
                                if (have > 0)
                                    answer = !answer;
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 19:
                            {
                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저가 가지고 있는 물건 중 배경이 노란색인 물건이 있나요?");

                                int have = 0;
                                for (int j = 0; j < 3; j++)
                                {
                                    if (IsYellow(user_stuff[question_target, j]))
                                    {
                                        have++;
                                        break;
                                    }
                                }
                                if (have > 0)
                                    answer = !answer;
                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 20:
                            {
                                if (IsAnt(user_my_stuff[question_target]))
                                    answer = !answer;

                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 개미인가요?");

                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 21:
                            {
                                if (IsBag(user_my_stuff[question_target]))
                                    answer = !answer;

                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 가방인가요?");

                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 22:
                            {
                                if (IsDog(user_my_stuff[question_target]))
                                    answer = !answer;

                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 개인가요?");

                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 23:
                            {
                                if (IsPersimmon(user_my_stuff[question_target]))
                                    answer = !answer;

                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 감인가요?");

                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 24:
                            {
                                if (IsPeppar(user_my_stuff[question_target]))
                                    answer = !answer;

                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 고추인가요?");

                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 25:
                            {
                                if (IsGhost(user_my_stuff[question_target]))
                                    answer = !answer;

                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 귀신인가요?");

                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                        case 26:
                            {
                                if (IsButterfly(user_my_stuff[question_target]))
                                    answer = !answer;

                                Send_Log((player_order + 1) + "번 유저 : " + (question_target + 1) + "번 유저의 물건은 나비인가요?");

                                if (answer)
                                    Send_Log((question_target + 1) + "번 유저 : 네.");
                                else
                                    Send_Log((question_target + 1) + "번 유저 : 아니요.");
                                break;
                            }
                    }
                    game_phase = 61;
                    Timer = 0;
                    question_confirm = false;
                    for (int i = 0; i < 5; i++)
                        Ai[i].GetInfo(user_question_choice, answer, question_target);

                    break;
                }
            case 6:
                {
                    // 전체 질문 대답 처리
                    switch (user_question_choice)
                    {
                        case 30:
                            {
                                Send_Log((player_order + 1) + "번 유저 : 당신이 가지고 있는 물건 중 배경이 하늘색인 물건이 있나요?");
                                bool answer = false;
                                for(int i = 0;i<6;i++)
                                {
                                    if (player_order == i)
                                        continue;
                                    answer = false;
                                    if (user_job[i] < 3)
                                        answer = !answer;
                                    int have = 0;
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (IsBlue(user_stuff[i, j]))
                                        {
                                            have++;
                                            break;
                                        }
                                    }
                                    if (have > 0)
                                        answer = !answer;
                                    if (answer)
                                        Send_Log((i + 1) + "번 유저 : 저는 가지고 있어요.");
                                    else
                                        Send_Log((i + 1) + "번 유저 : 저는 가지고 있지 않아요.");
                                }
                                break;
                            }
                        case 31:
                            {
                                Send_Log((player_order + 1) + "번 유저 : 당신이 가지고 있는 물건 중 배경이 연회색인 물건이 있나요?");
                                bool answer = false;
                                for (int i = 0; i < 6; i++)
                                {
                                    if (player_order == i)
                                        continue;
                                    answer = false;
                                    if (user_job[i] < 3)
                                        answer = !answer;
                                    int have = 0;
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (IsGray(user_stuff[i, j]))
                                        {
                                            have++;
                                            break;
                                        }
                                    }
                                    if (have > 0)
                                        answer = !answer;
                                    if (answer)
                                        Send_Log((i + 1) + "번 유저 : 저는 가지고 있어요.");
                                    else
                                        Send_Log((i + 1) + "번 유저 : 저는 가지고 있지 않아요.");
                                }
                                break;
                            }
                        case 32:
                            {
                                Send_Log((player_order + 1) + "번 유저 : 당신이 가지고 있는 물건 중 배경이 연두색인 물건이 있나요?");
                                bool answer = false;

                                for (int i = 0; i < 6; i++)
                                {
                                    if (player_order == i)
                                        continue;
                                    answer = false;
                                    if (user_job[i] < 3)
                                        answer = !answer;
                                    int have = 0;
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (IsGreen(user_stuff[i, j]))
                                        {
                                            have++;
                                            break;
                                        }
                                    }
                                    if (have > 0)
                                        answer = !answer;
                                    if (answer)
                                        Send_Log((i + 1) + "번 유저 : 저는 가지고 있어요.");
                                    else
                                        Send_Log((i + 1) + "번 유저 : 저는 가지고 있지 않아요.");
                                }
                                break;
                            }
                        case 33:
                            {
                                Send_Log((player_order + 1) + "번 유저 : 당신이 가지고 있는 물건 중 배경이 분홍색인 물건이 있나요?");
                                bool answer = false;
                                for (int i = 0; i < 6; i++)
                                {
                                    if (player_order == i)
                                        continue;
                                    answer = false;
                                    if (user_job[i] < 3)
                                        answer = !answer;
                                    int have = 0;
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (IsPink(user_stuff[i, j]))
                                        {
                                            have++;
                                            break;
                                        }
                                    }
                                    if (have > 0)
                                        answer = !answer;
                                    if (answer)
                                        Send_Log((i + 1) + "번 유저 : 저는 가지고 있어요.");
                                    else
                                        Send_Log((i + 1) + "번 유저 : 저는 가지고 있지 않아요.");
                                }
                                break;
                            }
                        case 34:
                            {
                                Send_Log((player_order + 1) + "번 유저 : 당신이 가지고 있는 물건 중 배경이 노란색인 물건이 있나요?");
                                bool answer = false;
                                for (int i = 0; i < 6; i++)
                                {
                                    if (player_order == i)
                                        continue;
                                    answer = false;
                                    if (user_job[i] < 3)
                                        answer = !answer;
                                    int have = 0;
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (IsYellow(user_stuff[i, j]))
                                        {
                                            have++;
                                            break;
                                        }
                                    }
                                    if (have > 0)
                                        answer = !answer;
                                    if (answer)
                                        Send_Log((i + 1) + "번 유저 : 저는 가지고 있어요.");
                                    else
                                        Send_Log((i + 1) + "번 유저 : 저는 가지고 있지 않아요.");
                                }
                                break;
                            }
                        case 35:
                            {
                                Send_Log((player_order + 1) + "번 유저 : 당신은 본인의 물건을 가지고 있나요?");
                                bool answer = false;
                                for (int i = 0; i < 6; i++)
                                {
                                    if (player_order == i)
                                        continue;
                                    answer = false;
                                    if (user_job[i] < 3)
                                        answer = !answer;
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (user_my_stuff[i]==user_stuff[i, j])
                                        {
                                            answer = !answer;
                                        }
                                    }
                                    if (answer)
                                        Send_Log((i + 1) + "번 유저 : 저는 가지고 있어요.");
                                    else
                                        Send_Log((i + 1) + "번 유저 : 저는 가지고 있지 않아요.");
                                }
                                break;
                            }
                        case 36:
                            {
                                Send_Log((player_order + 1) + "번 유저 : 당신의 물건은 생물인가요?");
                                bool answer = false;
                                for (int i = 0; i < 6; i++)
                                {
                                    if (player_order == i)
                                        continue;
                                    answer = false;
                                    if (user_job[i] < 3)
                                        answer = !answer;
                                    if (IsLive(user_my_stuff[i]))
                                    {
                                            answer = !answer;
                                    }
                                    
                                    if (answer)
                                        Send_Log((i + 1) + "번 유저 : 네.");
                                    else
                                        Send_Log((i + 1) + "번 유저 : 아니요.");
                                }
                                break;
                            }
                        case 37:
                            {
                                Send_Log((player_order + 1) + "번 유저 : 당신의 물건은 배경이 하늘색인가요?");
                                bool answer = false;
                                for (int i = 0; i < 6; i++)
                                {
                                    if (player_order == i)
                                        continue;
                                    answer = false;
                                    if (user_job[i] < 3)
                                        answer = !answer;
                                    if (IsBlue(user_my_stuff[i]))
                                        answer = !answer;                                
                                    if (answer)
                                        Send_Log((i + 1) + "번 유저 : 네.");
                                    else
                                        Send_Log((i + 1) + "번 유저 : 아니요.");
                                }
                                break;
                            }
                        case 38:
                            {
                                Send_Log((player_order + 1) + "번 유저 : 당신의 물건은 배경이 연회색인가요?");
                                bool answer = false;
                                for (int i = 0; i < 6; i++)
                                {
                                    if (player_order == i)
                                        continue;
                                    answer = false;
                                    if (user_job[i] < 3)
                                        answer = !answer;
                                    if (IsGray(user_my_stuff[i]))
                                        answer = !answer;
                                    if (answer)
                                        Send_Log((i + 1) + "번 유저 : 네.");
                                    else
                                        Send_Log((i + 1) + "번 유저 : 아니요.");
                                }
                                break;
                            }
                        case 39:
                            {
                                Send_Log((player_order + 1) + "번 유저 : 당신의 물건은 배경이 연두색인가요?");
                                bool answer = false;
                                for (int i = 0; i < 6; i++)
                                {
                                    if (player_order == i)
                                        continue;
                                    answer = false;
                                    if (user_job[i] < 3)
                                        answer = !answer;
                                    if (IsGreen(user_my_stuff[i]))
                                        answer = !answer;
                                    if (answer)
                                        Send_Log((i + 1) + "번 유저 : 네.");
                                    else
                                        Send_Log((i + 1) + "번 유저 : 아니요.");
                                }
                                break;
                            }
                        case 40:
                            {
                                Send_Log((player_order + 1) + "번 유저 : 당신의 물건은 배경이 분홍색인가요?");
                                bool answer = false;
                                for (int i = 0; i < 6; i++)
                                {
                                    if (player_order == i)
                                        continue;
                                    answer = false;
                                    if (user_job[i] < 3)
                                        answer = !answer;
                                    if (IsPink(user_my_stuff[i]))
                                        answer = !answer;
                                    if (answer)
                                        Send_Log((i + 1) + "번 유저 : 네.");
                                    else
                                        Send_Log((i + 1) + "번 유저 : 아니요.");
                                }
                                break;
                            }
                        case 41:
                            {
                                Send_Log((player_order + 1) + "번 유저 : 당신의 물건은 배경이 노란색인가요?");
                                bool answer = false;
                                for (int i = 0; i < 6; i++)
                                {
                                    if (player_order == i)
                                        continue;
                                    answer = false;
                                    if (user_job[i] < 3)
                                        answer = !answer;
                                    if (IsYellow(user_my_stuff[i]))
                                        answer = !answer;
                                    if (answer)
                                        Send_Log((i + 1) + "번 유저 : 네.");
                                    else
                                        Send_Log((i + 1) + "번 유저 : 아니요.");
                                }
                                break;
                            }
                    }
                    game_phase = 61;
                    Timer = 0;
                    
                    question_confirm = false;

                    
                    break;
                }
            case 61:
                {
                    Selectoff();
                    // 물건 바꾸기전 질문에 대한 답 확인시간
                    if(turn_phase==1)
                        Send_Log("<color=#ea68a2>" + "사회자 : " + (player_order + 1) + "번 유저의 첫 번째 물건 교환입니다." + "</color>");
                    else if (turn_phase==2)
                        Send_Log("<color=#ea68a2>" + "사회자 : " + (player_order + 1) + "번 유저의 두 번째 물건 교환입니다." + "</color>");
                    else if (turn_phase == 3)
                        Send_Log("<color=#ea68a2>" + "사회자 : " + (player_order + 1) + "번 유저의 마지막 물건 교환입니다." + "</color>");
                    Timer = 10;
                    game_phase = 62;
                    break;
                }
            case 62:
                {
                    if (Timer < 0)
                    {
                        Timer = 10;
                        game_phase = 7;
                    }
                    break;
                }
            case 7:
                {
                    // 물건 교환 대상 지정
                    if (question_confirm)
                    {
                        game_phase = 8;                        
                        swap_target = player_inputvalue;
                        player_inputvalue = -1;
                        question_confirm = false;
                        Selectoff();
                        Timer = 10;
                        SFX.countOn = true;
                        break;
                    }
                    if (Timer < 0 && !question_confirm)
                    {
                        if (player_inputvalue == -1)
                        {
                            while (true)
                            {
                                randomi = Random.Range(0, 6);
                                if (player_order == randomi)
                                    continue;
                                player_inputvalue = randomi;

                                break;
                            }
                        }
                        question_confirm = true;
                    }
                    break;
                }
            case 8:
                {
                    // 내가 상대방의 물건 선택
                    if (question_confirm)
                    {
                        game_phase = 9;
                        question_confirm = false;
                        swap_target_stuff = user_stuff[swap_target,player_inputvalue];
                        swap_target_x = player_inputvalue;
                        player_inputvalue = -1;
                        Selectoff();
                        Timer = 20;
                        SFX.countOn = true;
                        break;
                    }
                    if (Timer < 0 && !question_confirm)
                    {
                        randomi = Random.Range(0, 3);
                        player_inputvalue = randomi;
                        question_confirm = true;
                    }
                    break;
                }
            case 9:
                {
                    // 상대방의 나의 물건 선택
                    if (question_confirm)
                    {
                        game_phase = 10;
                        question_confirm = false;
                        swap_my_stuff = user_stuff[player_order, player_inputvalue];
                        swap_my_x = player_inputvalue;
                        player_inputvalue = -1;
                        Selectoff();
                        Timer = 5;
                        SFX.countOn = true;
                        break;
                    }
                    if (Timer < 0 && !question_confirm)
                    {
                        randomi = Random.Range(0, 3);
                        player_inputvalue = randomi;
                        question_confirm = true;
                    }
                    break;
                }
            case 10:
                {
                    // 바꾸는 애니메이션 실행
                    if(changeani)
                    {
                        change_window.transform.localPosition = new Vector3(0, 0);
                        
                        change_window.transform.GetChild(0).gameObject.SetActive(true);
                        spr = (Sprite)Resources.Load("Cards/" + GetStuff(swap_my_stuff) + "_card", typeof(Sprite));
                        change_window.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spr;

                        change_window.transform.GetChild(1).gameObject.SetActive(true);
                        spr = (Sprite)Resources.Load("Cards/" + GetStuff(swap_target_stuff) + "_card", typeof(Sprite));
                        change_window.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;

                        changeani = false;
                    }
                    if(Timer<0)
                    {
                        game_phase = 11;
                        change_window.transform.localPosition = new Vector3(100, 0);
                        change_window.transform.GetChild(0).gameObject.SetActive(false);
                        change_window.transform.GetChild(1).gameObject.SetActive(false);
                        changeani = true;
                    }
                    break;
                }
            case 11:
                {
                    int tmp;

                    tmp = swap_target_stuff;
                    swap_target_stuff = swap_my_stuff;
                    swap_my_stuff = tmp;

                    if (turn_phase == 3)
                    {
                        if(player_order==0)
                            Change_Stuff_img(player_order, swap_my_x, swap_my_stuff);
                        else if(swap_target==0)
                            Change_Stuff_img(swap_target, swap_target_x, swap_target_stuff);
                    }                        
                    else
                    {
                        Change_Stuff_img(player_order, swap_my_x, swap_my_stuff);
                        Change_Stuff_img(swap_target, swap_target_x, swap_target_stuff);
                    }
                    user_stuff[player_order, swap_my_x] = swap_my_stuff;
                    stuff_img = GameObject.FindWithTag("User_Picture" + (player_order + 1));
                    stuff_img.transform.GetChild(4).GetChild(swap_my_x).gameObject.SetActive(true);

                    user_stuff[swap_target, swap_target_x] = swap_target_stuff;
                    stuff_img = GameObject.FindWithTag("User_Picture" + (swap_target + 1));
                    stuff_img.transform.GetChild(4).GetChild(swap_target_x).gameObject.SetActive(true);

                    Send_Log("<color=#00b7ee>" + "도우미 : " + (player_order+1) + "번 유저의 " + GetKoreanStuff(swap_target_stuff) + "과 " + (swap_target+1) + "번 유저의 " + GetKoreanStuff(swap_my_stuff) + "를 교환하였습니다." + "</color>");

                    if (player_order == 5)
                        player_order = 0;
                    else
                        player_order++;

                    game_phase = 1;
                    
                    if(turn_count==6)
                    {
                        turn_count = 0;
                        turn_phase++;

                        //2페이즈 때부터 물건을 안보이게 하기
                        if (turn_phase == 3  && isblind)
                        {
                            game_phase = 12;
                            isblind = false;
                            Timer = 3;
                            blind_window.transform.localPosition = new Vector3(0, 0);
                            blind_window.transform.GetChild(1).gameObject.SetActive(true);

                        }
                        else
                            game_phase = 1;
                    }
                    
                                        
                    if(turn_phase == 4)
                    {
                        game_phase = 30;
                        Timer = 5;
                    }
                    break;
                }
            case 12:
                {
                    if (Timer < 0)
                    {
                        Timer = 10;
                        game_phase = 13;
                        blind_window.transform.GetChild(1).gameObject.SetActive(false);
                        blind_window.transform.GetChild(2).gameObject.SetActive(true);
                    }
                    break;
                }
            case 13:
                {
                    if(Timer<0)
                    {
                        game_phase = 14;
                        blind_window.SetActive(false);
                    }
                    break;
                }
            case 14:
                {
                    for (int i = 1; i < 6; i++)
                    {
                        //if(player) 유저 순서 랜덤일시 적용
                        for (int j = 0; j < 3; j++)
                        {
                            stuff_img = GameObject.FindWithTag("User_Picture" + (i + 1));
                            stuff_img.transform.GetChild(0).GetChild(j).GetComponent<SpriteRenderer>().sprite = null;

                            spr = (Sprite)Resources.Load("UI/stuff_dontknow", typeof(Sprite));
                            stuff_img.transform.GetChild(0).GetChild(j).GetChild(0).GetComponent<SpriteRenderer>().sprite = spr;
                            stuff_img.transform.GetChild(0).GetChild(j).transform.localScale = new Vector3(0.4f, 0.4f);
                            stuff_img.transform.GetChild(0).GetChild(j).GetChild(0).transform.localScale = new Vector3(2.27f, 2.3f);
                        }
                    }
                    game_phase = 1;
                    break;
                }
            case 30:
                {
                    if (Timer < 0)
                    {
                        // 승리 패배 유저마다 판별
                        for (int i = 0; i < 6; i++)
                        {
                            // 도둑의 승리 조건 : 시민의 물건을 가지고 있어야 승리
                            if (user_job[i] == 0)
                            {
                                for (int j = 0; j < 6; j++)
                                {
                                    if (user_job[j] >= 3 && user_job[j] <= 5)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            if (user_my_stuff[j] == user_stuff[i, k])
                                                user_result[i] = true;
                                        }
                                    }
                                }
                            }
                            // 도둑의 도둑 승리 조건 : 도둑의 물건을 가지고 있어야 승리
                            else if (user_job[i] == 1)
                            {
                                for (int j = 0; j < 6; j++)
                                {
                                    if (user_job[j] == 0)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            if (user_my_stuff[j] == user_stuff[i, k])
                                                user_result[i] = true;
                                        }
                                    }
                                }
                            }
                            // 거지 일때 승리 조건 : 시민이 자신의 물건을 가지고있을때
                            else if (user_job[i] == 2)
                            {
                                for (int j = 0; j < 6; j++)
                                {
                                    if (user_job[j] >= 3 && user_job[j] <= 5)
                                    {
                                        for (int k = 0; k < 3; k++)
                                        {
                                            if (user_my_stuff[i] == user_stuff[j, k])
                                                user_result[i] = true;
                                        }
                                    }
                                }
                            }
                            // 시민의 승리 조건 : 자신의 물건을 가지고 있어야 승리, 거지의 물건을 가지고 있으면 패배
                            else
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    if (user_my_stuff[i] == user_stuff[i, j])
                                        user_result[i] = true;
                                }
                                for (int j = 0; j < 6; j++)
                                {
                                    if (user_job[j] == 2)
                                    {
                                        for (int k = 0; k < 3; k++)
                                            if (user_my_stuff[j] == user_stuff[i, k])
                                                user_result[i] = false;
                                    }
                                }
                            }
                        }
                        game_phase = 31;
                        Timer = 10;

                        if (user_result[0])
                        {
                            win_anim.transform.localPosition = new Vector3(0, 0);
                            spr = (Sprite)Resources.Load("UI/" + GetJob(user_job[0]) + "_win", typeof(Sprite));
                            win_anim.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
                            win_anim.SetActive(true);
                        }
                        else
                        {
                            lose_anim.transform.localPosition = new Vector3(0, 0);
                            spr = (Sprite)Resources.Load("UI/" + GetJob(user_job[0]) + "_lose", typeof(Sprite));
                            lose_anim.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
                            lose_anim.SetActive(true);
                        }
                        SFX.LastSFXPlay();
                        GameObject menu = GameObject.Find("Right_Menu");
                        menu.SetActive(false);
                    }
                    break;
                }
            case 31:
                {
                    if(Timer<0)
                    {
                        lose_anim.SetActive(false);
                        win_anim.SetActive(false);
                        result_window.SetActive(true);
                        for(int i=0;i<6;i++)
                        {
                            if (i == 0)
                                result1.text += (i + 1) + "번 현이하다\n";
                            else
                                result1.text += (i + 1) + "번 로봇" + i + "\n";
                            if (user_result[i])
                            {
                                if(i>=0 && i<=1)
                                    result2.text += "<color=#00b7ee>" + "▲ 12" + "</color>" + "\n";
                                else if(i>=2 && i<=3)
                                    result2.text += "<color=#00b7ee>" + "▲ 11" + "</color>" + "\n";
                                else
                                    result2.text += "<color=#00b7ee>" + "▲ 10" + "</color>" + "\n";

                                result3.text += "<color=#00b7ee>" + "승리" + "</color>" + "\n";
                            }
                            else
                            {
                                if (i >= 0 && i <= 1)
                                    result2.text += "<color=#ea68a2>" + "▼ 4" + "</color>" + "\n";
                                else if (i >= 2 && i <= 3)
                                    result2.text += "<color=#ea68a2>" + "▼ 5" + "</color>" + "\n";
                                else
                                    result2.text += "<color=#ea68a2>" + "▼ 6" + "</color>" + "\n";

                                result3.text += "<color=#ea68a2>" + "패배" + "</color>" + "\n";
                            }
                            GameObject user_picture = GameObject.Find("User_Picture" + (i+1));

                           

                            user_picture.transform.GetChild(2).gameObject.SetActive(true);

                            //자신의 물건 가운데 보이게 하기
                            user_picture.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "Player_window";
                            spr = (Sprite)Resources.Load("Stuffs/" + GetStuff(user_my_stuff[i]), typeof(Sprite));
                            user_picture.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;

                            spr = (Sprite)Resources.Load("Jobs/" + GetJob(user_job[i]), typeof(Sprite));
                            user_picture.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;

                            if (user_my_stuff[i] >= 26 && user_my_stuff[i] <= 41 && user_my_stuff[i] != 34 && user_my_stuff[i] != 35)
                            {
                                user_picture.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                user_picture.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                            }
                            else if (user_my_stuff[i] >= 58 && user_my_stuff[i] <= 65)
                            {
                                user_picture.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                user_picture.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                            }
                            else if (user_my_stuff[i] >= 88 && user_my_stuff[i] <= 95)
                            {
                                user_picture.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.3f, 0.3f);
                                user_picture.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
                            }
                            else if (user_my_stuff[i] >= 22 && user_my_stuff[i] <= 25)
                            {
                                user_picture.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.35f, 0.35f);
                                user_picture.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.55f, 2.6f);
                            }
                            else
                            {
                                user_picture.transform.GetChild(0).GetChild(1).transform.localScale = new Vector3(0.4f, 0.4f);
                                user_picture.transform.GetChild(0).GetChild(1).GetChild(0).transform.localScale = new Vector3(2.27f, 2.3f);
                            }


                        }

                        game_phase = 32;
                        Timer = 1;
                    }
                    break;
                }
            case 32:
                {
                    if (Timer < 0)
                    {
                        result_window.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);  
                        game_phase = 33;
                        Timer = 1;
                    }
                    break;
                }
            case 33:
                {
                    if(Timer<0)
                    {
                        result_window.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                        if (flowerevent.hitflower != 3)
                        {
                            spr = (Sprite)Resources.Load("UI/lose_bg2", typeof(Sprite));
                            result_window.transform.GetChild(2).GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
                            result_window.transform.GetChild(2).GetChild(1).transform.GetComponent<Animator>().runtimeAnimatorController = losegift;
                            gift_open[1] = true;
                        }
                        game_phase = 34;
                        Timer = 1;
                    }
                    break;
                }
            case 34:
                {
                    if (Timer < 0)
                    {
                        result_window.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                        if (!user_result[PC.order])
                        {
                            spr = (Sprite)Resources.Load("UI/lose_bg3", typeof(Sprite));
                            result_window.transform.GetChild(2).GetChild(2).GetComponent<SpriteRenderer>().sprite = spr;
                            result_window.transform.GetChild(2).GetChild(2).transform.GetComponent<Animator>().runtimeAnimatorController = losegift;
                            gift_open[2] = true;
                            Timer = 1;
                        }
                        game_phase = 35;
                    }
                    break;
                }
            case 35:
                {
                    if (Timer < 0)
                    {
                        bool giftcheck = true;
                        for (int i = 0; i < 3; i++)
                        {
                            if (gift_open[i] == false)
                                giftcheck = false;
                        }

                        if(giftcheck)
                            result_window.transform.GetChild(3).gameObject.SetActive(true);

                    }
                    break;
                }
        }
    }
    public void Change_Stuff_img(int i, int j, float stuff)
    {
        stuff_img = GameObject.FindWithTag("User_Picture"+(i+1));
        spr = (Sprite)Resources.Load("Stuffs/" + GetStuff(stuff), typeof(Sprite));
        stuff_img.transform.GetChild(0).GetChild(j).GetComponent<SpriteRenderer>().sprite = spr;

        if(IsBlue((int)stuff))
            spr = (Sprite)Resources.Load("UI/stuff_bg_blue", typeof(Sprite));
        else if (IsGray((int)stuff))
            spr = (Sprite)Resources.Load("UI/stuff_bg_gray", typeof(Sprite));
        else if (IsGreen((int)stuff))
            spr = (Sprite)Resources.Load("UI/stuff_bg_green", typeof(Sprite));
        else if (IsPink((int)stuff))
            spr = (Sprite)Resources.Load("UI/stuff_bg_pink", typeof(Sprite));
        else if (IsYellow((int)stuff))
            spr = (Sprite)Resources.Load("UI/stuff_bg_yellow", typeof(Sprite));

        stuff_img.transform.GetChild(0).GetChild(j).GetChild(0).GetComponent<SpriteRenderer>().sprite = spr;
        if (stuff >= 26 && stuff <= 41 && stuff != 34 && stuff != 35)
        {
            stuff_img.transform.GetChild(0).GetChild(j).transform.localScale = new Vector3(0.3f, 0.3f);
            stuff_img.transform.GetChild(0).GetChild(j).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
        }
        else if (stuff >= 12 && stuff <= 13)
        {
            stuff_img.transform.GetChild(0).GetChild(j).transform.localScale = new Vector3(0.3f, 0.3f);
            stuff_img.transform.GetChild(0).GetChild(j).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
        }
        else if(stuff >=58 && stuff <=65)
        {
            stuff_img.transform.GetChild(0).GetChild(j).transform.localScale = new Vector3(0.3f, 0.3f);
            stuff_img.transform.GetChild(0).GetChild(j).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
        }
        else if (stuff >= 88 && stuff <= 95)
        {
            stuff_img.transform.GetChild(0).GetChild(j).transform.localScale = new Vector3(0.3f, 0.3f);
            stuff_img.transform.GetChild(0).GetChild(j).GetChild(0).transform.localScale = new Vector3(3.05f, 3f);
        }
        else if (stuff >= 22 && stuff <= 25)
        {
            stuff_img.transform.GetChild(0).GetChild(j).transform.localScale = new Vector3(0.35f, 0.35f);
            stuff_img.transform.GetChild(0).GetChild(j).GetChild(0).transform.localScale = new Vector3(2.55f, 2.6f);
        }
        else
        {
            stuff_img.transform.GetChild(0).GetChild(j).transform.localScale = new Vector3(0.4f, 0.4f);
            stuff_img.transform.GetChild(0).GetChild(j).GetChild(0).transform.localScale = new Vector3(2.27f, 2.3f);
        }
    }

    void Send_Log(string message)
    {
        int max_line = 40;
        if (!message.Equals(""))
        {
            if (!(message[0] == '<'))
            {
                if (message.Length > max_line)
                {
                    for (int i = 0; i < (message.Length / max_line); i++)
                    {
                        message = message.Insert((max_line * (i + 1)) + i, "\n");
                    }
                }
            }
            else
            {
                if (message.Length > 64)
                {
                    message = message.Insert((max_line + 15), "\n");
                }
            }
            string[] result = message.Split(new char[] { '\n' });
            for (int i = 0; i < result.Length; i++)
            {
                logText.text += (result[i] + "\n");
            }
        }
        log_scroll_Rect.verticalNormalizedPosition = 0f;
    }

    public void Send_Chat()
    {
        int max_line = 40; 
        string msg = (PC.order+1) + "번 유저 : ";
        msg += chatInput.text;
        if (!msg.Equals(""))
        {
            if (msg.Length > max_line)
            {
                for (int i = 0; i < (msg.Length / max_line); i++)
                {
                    msg = msg.Insert((max_line * (i + 1)) + i, "\n");
                }
            }
            string[] result = msg.Split(new char[] { '\n' });
            for (int i = 0; i < result.Length; i++)
            {
                chatText.text += (result[i] + "\n");
            }
            
        }
        chat_scroll_Rect.verticalNormalizedPosition = 0f;
        chatInput.text = null;
    }
    public string GetStuff(float stuff)
    {
        switch ((int)stuff)
        {
            case 0:
                return "cat1";
            case 1:
                return "cat2";
            case 2:
                return "cat3";
            case 3:
                return "cat4";
            case 4:
                return "cat5";
            case 5:
                return "cat6";
            case 6:
                return "cat7";
            case 7:
                return "cat8";
            case 8:
                return "cat9";
            case 9:
                return "cat10";
            case 10:
                return "apple1";
            case 11:
                return "apple2";
            case 12:
                return "pencil1";
            case 13:
                return "pencil2";
            case 14:
                return "shit1";
            case 15:
                return "shit2";
            case 16:
                return "shit3";
            case 17:
                return "shit4";
            case 18:
                return "bear1";
            case 19:
                return "bear2";
            case 20:
                return "bear3";
            case 21:
                return "bear4";
            case 22:
                return "tree1";
            case 23:
                return "tree2";
            case 24:
                return "tree3";
            case 25:
                return "tree4";
            case 26:
                return "candy1";
            case 27:
                return "candy2";
            case 28:
                return "candy3";
            case 29:
                return "candy4";
            case 30:
                return "candy5";
            case 31:
                return "candy6";
            case 32:
                return "candy7";
            case 33:
                return "candy8";
            case 34:
                return "stone1";
            case 35:
                return "stone2";
            case 36:
                return "mouse1";
            case 37:
                return "mouse2";
            case 38:
                return "mouse3";
            case 39:
                return "mouse4";
            case 40:
                return "mouse5";
            case 41:
                return "mouse6";
            case 42:
                return "book1";
            case 43:
                return "book2";
            case 44:
                return "book3";
            case 45:
                return "book4";
            case 46:
                return "book5";
            case 47:
                return "book6";
            case 48:
                return "book7";
            case 49:
                return "book8";
            case 50:
                return "ant1";
            case 51:
                return "ant2";
            case 52:
                return "ant3";
            case 53:
                return "ant4";
            case 54:
                return "ant5";
            case 55:
                return "ant6";
            case 56:
                return "ant7";
            case 57:
                return "ant8";
            case 58:
                return "bag1";
            case 59:
                return "bag2";
            case 60:
                return "bag3";
            case 61:
                return "bag4";
            case 62:
                return "bag5";
            case 63:
                return "bag6";
            case 64:
                return "bag7";
            case 65:
                return "bag8";
            case 66:
                return "dog1";
            case 67:
                return "dog2";
            case 68:
                return "dog3";
            case 69:
                return "dog4";
            case 70:
                return "dog5";
            case 71:
                return "dog6";
            case 72:
                return "dog7";
            case 73:
                return "dog8";
            case 74:
                return "persimmon1";
            case 75:
                return "persimmon2";
            case 76:
                return "peppar1";
            case 77:
                return "peppar2";
            case 78:
                return "peppar3";
            case 79:
                return "peppar4";
            case 80:
                return "ghost1";
            case 81:
                return "ghost2";
            case 82:
                return "ghost3";
            case 83:
                return "ghost4";
            case 84:
                return "ghost5";
            case 85:
                return "ghost6";
            case 86:
                return "ghost7";
            case 87:
                return "ghost8";
            case 88:
                return "butterfly1";
            case 89:
                return "butterfly2";
            case 90:
                return "butterfly3";
            case 91:
                return "butterfly4";
            case 92:
                return "butterfly5";
            case 93:
                return "butterfly6";
            case 94:
                return "butterfly7";
            case 95:
                return "butterfly8";

        }
        return "";
    }
    string GetKoreanStuff(float stuff)
    {
        switch ((int)stuff)
        {
            case 0:
                return "공주의 고양이";
            case 1:
                return "도둑의 고양이";           
            case 2:
                return "싸움꾼 고양이";
            case 3:
                return "분노의 고양이";                
            case 4:
                return "귤 고양이";                
            case 5:
                return "사랑의 고양이";                
            case 6:
                return "거지의 고양이";                
            case 7:
                return "평범한 고양이";                
            case 8:
                return "배고픈 고양이";                
            case 9:
                return "앞머리 고양이";                
            case 10:
                return "좋은 사과";                
            case 11:
                return "황금 사과";                
            case 12:
                return "아빠가 준 연필";                
            case 13:
                return "엄마가 준 연필";                
            case 14:
                return "거지의 똥";                
            case 15:
                return "동 똥";                
            case 16:
                return "은 똥";                
            case 17:
                return "금 똥";                
            case 18:
                return "차가운 곰";                
            case 19:
                return "공주의 곰";                
            case 20:
                return "현이의 곰";                
            case 21:
                return "행복의 곰";                
            case 22:
                return "나쁜 나무";                
            case 23:
                return "사랑 나무";                
            case 24:
                return "소원 나무";                
            case 25:
                return "하늘 나무";
            case 26:
                return "첫눈 사탕";
            case 27:
                return "딸기 사탕";
            case 28:
                return "단풍 사탕";
            case 29:
                return "멜론 사탕";
            case 30:
                return "바다 사탕";
            case 31:
                return "벚꽃 사탕";
            case 32:
                return "망고 사탕";
            case 33:
                return "포도 사탕";
            case 34:
                return "수상한 돌";
            case 35:
                return "튼튼한 돌";
            case 36:
                return "싸움꾼 쥐";
            case 37:
                return "귤 쥐";
            case 38:
                return "번개 쥐";
            case 39:
                return "포도 쥐";
            case 40:
                return "앞머리 쥐";
            case 41:
                return "거지의 쥐";
            case 42:
                return "국어 책";
            case 43:
                return "과학 책";
            case 44:
                return "음악 책";
            case 45:
                return "수학 책";
            case 46:
                return "영어 책";
            case 47:
                return "미술 책";
            case 48:
                return "사회 책";
            case 49:
                return "체육 책";
            case 50:
                return "여왕 개미";
            case 51:
                return "싸움꾼 개미";
            case 52:
                return "나쁜 개미";
            case 53:
                return "번개 개미";
            case 54:
                return "멜론 개미";
            case 55:
                return "하늘 개미";
            case 56:
                return "공주 개미";
            case 57:
                return "거지 개미";
            case 58:
                return "백곰 가방";
            case 59:
                return "단풍 가방";
            case 60:
                return "학생 가방";
            case 61:
                return "식물 가방";
            case 62:
                return "멋진 가방";
            case 63:
                return "포도 가방";
            case 64:
                return "예쁜 가방";
            case 65:
                return "평범한 가방";
            case 66:
                return "평범한 개";
            case 67:
                return "도둑의 개";
            case 68:
                return "번개 개";
            case 69:
                return "차가운 개";
            case 70:
                return "공주의 개";
            case 71:
                return "포도 개";
            case 72:
                return "분노의 개";
            case 73:
                return "배고픈 개";
            case 74:
                return "좋은 감";
            case 75:
                return "안 좋은 감";
            case 76:
                return "황금 고추";
            case 77:
                return "멜론 고추";
            case 78:
                return "좋은 고추";
            case 79:
                return "안 좋은 고추";
            case 80:
                return "나쁜 귀신";
            case 81:
                return "도둑 귀신";
            case 82:
                return "차가운 귀신";
            case 83:
                return "사랑에 빠진 귀신";
            case 84:
                return "수상한 귀신";
            case 85:
                return "공주 귀신";
            case 86:
                return "배고픈 귀신";
            case 87:
                return "거지 귀신";
            case 88:
                return "여왕 나비";
            case 89:
                return "나쁜 나비";
            case 90:
                return "귤 나비";
            case 91:
                return "행복한 나비";
            case 92:
                return "하늘 나비";
            case 93:
                return "사랑에 빠진 나비";
            case 94:
                return "번개 나비";
            case 95:
                return "우정 나비";
        }
        return "";
    }

    public string GetJob(int job)
    {
        switch(job)
        {
            case 0:
                return "thief";                
            case 1:
                return "thief_s";                
            case 2:
                return "beggar";                
            case 3: case 4: case 5:
                return "citizen";                
        }
        return "";
    }
    string GetKoreanJob(int job)
    {
        switch (job)
        {
            case 0:
                return "도둑";                
            case 1:
                return "도둑의 도둑";                
            case 2:
                return "거지";                
            case 3:
            case 4:
            case 5:
                return "시민";                
        }
        return "";
    }

    public string GetQuestion(int question)
    {
        switch(question)
        {
            case 0:
                return "question_p_bg_blue";
            case 1:
                return "question_p_bg_gray";
            case 2:
                return "question_p_bg_green";
            case 3:
                return "question_p_bg_pink";
            case 4:
                return "question_p_bg_yellow";
            case 5:
                return "question_p_apple";
            case 6:
                return "question_p_bear";
            case 7:
                return "question_p_cat";
            case 8:
                return "question_p_pencil";
            case 9:
                return "question_p_shit";
            case 10:
                return "question_p_tree";
            case 11:
                return "question_p_candy";
            case 12:
                return "question_p_stone";
            case 13:
                return "question_p_mouse";
            case 14:
                return "question_p_book";
            case 15:
                return "question_p_have_blue";
            case 16:
                return "question_p_have_gray";
            case 17:
                return "question_p_have_green";
            case 18:
                return "question_p_have_pink";
            case 19:
                return "question_p_have_yellow";
            case 20:
                return "question_p_ant";
            case 21:
                return "question_p_bag";
            case 22:
                return "question_p_dog";
            case 23:
                return "question_p_persimmon";
            case 24:
                return "question_p_peppar";
            case 25:
                return "question_p_ghost";
            case 26:
                return "question_p_butterfly";

            case 30:
                return "question_a_have_blue";
            case 31:
                return "question_a_have_gray";
            case 32:
                return "question_a_have_green";
            case 33:
                return "question_a_have_pink";
            case 34:
                return "question_a_have_yellow";
            case 35:
                return "question_a_have";
            case 36:
                return "question_a_live";
            case 37:
                return "question_a_bg_blue";
            case 38:
                return "question_a_bg_gray";
            case 39:
                return "question_a_bg_green";
            case 40:
                return "question_a_bg_pink";
            case 41:
                return "question_a_bg_yellow";
        }
        return "";
    }


    


    void Random_Stuff(bool img_change)
    {
        random_check = new bool[18];
        int thief_user = -1, thief_stuff = -1;
        int beggar_user = -1, beggar_stuff = -1;

        if (img_change)
        {
            for (int i = 0; i < 6; i++)
            {
                if (user_job[i] == 0 || user_job[i] == 2)
                {
                    randomi = Random.Range(0, 3);
                    user_stuff[i, (int)randomi] = user_my_stuff[i];
                    for (int j = 0; j < 18; j++)
                        if (game_stuff[j] == user_my_stuff[i])
                        {
                            random_check[j] = true;
                            break;
                        }

                    if (user_job[i] == 0)
                    {
                        thief_user = i;
                        thief_stuff = (int)randomi;
                    }
                    else
                    {
                        beggar_user = i;
                        beggar_stuff = (int)randomi;
                    }
                }
            }
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!((beggar_stuff == j && beggar_user == i) || (thief_stuff == j && thief_user == i)))
                {
                    while (true)
                    {
                        randomi = Random.Range(0, 18);
                        if (!random_check[(int)randomi])
                        {
                            random_check[(int)randomi] = true;
                            user_stuff[i, j] = game_stuff[(int)randomi];
                            break;
                        }
                    }
                }
                if (img_change)
                {
                    if (beggar_stuff == j && beggar_user == i)
                        Change_Stuff_img(i, j, user_stuff[beggar_user, beggar_stuff]);
                    else if (thief_stuff == j && thief_user == i)
                        Change_Stuff_img(i, j, user_stuff[thief_user, thief_stuff]);
                    else
                        Change_Stuff_img(i, j, game_stuff[(int)randomi]);
                }
            }
        }

        if (img_change)
        {
            for (int i = 0; i < 6; i++)
            {
                if (user_job[i] >= 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (user_stuff[i, j] == user_my_stuff[i])
                        {
                            while (true)
                            {
                                randomi = Random.Range(0, 5);
                                if (i == (int)randomi)
                                    continue;
                                float randomj = Random.Range(0f, 2f);
                                if (user_stuff[(int)randomi, (int)randomj] == user_my_stuff[beggar_user] || user_stuff[(int)randomi, (int)randomj] == user_my_stuff[thief_user])
                                    continue;
                                int tmp = user_stuff[(int)randomi, (int)randomj];
                                user_stuff[(int)randomi, (int)randomj] = user_stuff[i, j];
                                user_stuff[i, j] = tmp;

                                Change_Stuff_img(i, j, user_stuff[i, j]);
                                Change_Stuff_img((int)randomi, (int)randomj, user_stuff[(int)randomi, (int)randomj]);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }


    // 각자 물건에 따른 확인을 위한 함수
    public bool IsLive(int stuff)
    {
        switch(stuff)
        {
            case 0: case 1: case 2: case 3: case 4: case 5: case 6: case 7: case 8: case 9: // cat
            case 18: case 19: case 20: case 21: // bear
            case 22: case 23: case 24: case 25: // tree
            case 36: case 37: case 38: case 39: case 40: case 41: //mouse
            case 50: case 51: case 52: case 53: case 54: case 55: case 56: case 57://ant
            case 66: case 67: case 68: case 69: case 70: case 71: case 72: case 73://dog
            case 88: case 89: case 90: case 91: case 92: case 93: case 94: case 95://butterfly
                return true;
            default:
                return false;
        }
    }
    public bool IsCat(int stuff)
    {
        switch(stuff)
        {
            case 0: case 1: case 2: case 3: case 4: case 5: case 6: case 7: case 8: case 9:
                return true;
            default:
                return false;
        }
    }
    public  bool IsApple(int stuff)
    {
        switch(stuff)
        {
            case 10: case 11:
                return true;
            default:
                return false;
        }
    }
    public bool IsPencil(int stuff)
    {
        switch(stuff)
        {
            case 12: case 13: 
                return true;
            default:
                return false;
        }
    }
    public bool IsShit(int stuff)
    {
        switch(stuff)
        {
            case 14: case 15: case 16: case 17: 
                return true;
            default:
                return false;
        }
    }
    public bool IsBear(int stuff)
    {
       switch(stuff)
        {
            case 18: case 19: case 20: case 21: 
                return true;
            default:
                return false;
        }
    }
    public bool IsTree(int stuff)
    {
        switch(stuff)
        {
            case 22: case 23: case 24: case 25: 
                return true;
            default:
                return false;
        }
    }
    public bool IsCandy(int stuff)
    {
        switch (stuff)
        {
            case 26:
            case 27:
            case 28:
            case 29:
            case 30:
            case 31:
            case 32:
            case 33:
                return true;
            default:
                return false;
        }
    }
    public bool IsStone(int stuff)
    {
        switch (stuff)
        {
            case 34:
            case 35:
                return true;
            default:
                return false;
        }
    }
    public bool IsMouse(int stuff)
    {
        switch (stuff)
        {
            case 36:
            case 37:
            case 38:
            case 39:
            case 40:
            case 41:
                return true;
            default:
                return false;
        }
    }
    public bool IsBook(int stuff)
    {
        switch (stuff)
        {
            case 42:
            case 43:
            case 44:
            case 45:
            case 46:
            case 47:
            case 48:
            case 49:
                return true;
            default:
                return false;
        }
    }
    public bool IsAnt(int stuff)
    {
        switch (stuff)
        {
            case 50:
            case 51:
            case 52:
            case 53:
            case 54:
            case 55:
            case 56:
            case 57:
                return true;
            default:
                return false;
        }
    }
    public bool IsBag(int stuff)
    {
        switch (stuff)
        {
            case 58:
            case 59:
            case 60:
            case 61:
            case 62:
            case 63:
            case 64:
            case 65:
                return true;
            default:
                return false;
        }
    }
    public bool IsDog(int stuff)
    {
        switch (stuff)
        {
            case 66:
            case 67:
            case 68:
            case 69:
            case 70:
            case 71:
            case 72:
            case 73:
                return true;
            default:
                return false;
        }
    }
    public bool IsPersimmon(int stuff)
    {
        switch (stuff)
        {
            case 74:
            case 75:
                return true;
            default:
                return false;
        }
    }
    public bool IsPeppar(int stuff)
    {
        switch (stuff)
        {
            case 76:
            case 77:
            case 78:
            case 79:
                return true;
            default:
                return false;
        }
    }
    public bool IsGhost(int stuff)
    {
        switch (stuff)
        {
            case 80:
            case 81:
            case 82:
            case 83:
            case 84:
            case 85:
            case 86:
            case 87:
                return true;
            default:
                return false;
        }
    }
    public bool IsButterfly(int stuff)
    {
        switch (stuff)
        {
            case 88:
            case 89:
            case 90:
            case 91:
            case 92:
            case 93:
            case 94:
            case 95:
                return true;
            default:
                return false;
        }
    }

       

    public bool IsBlue(int stuff)
    {
        switch(stuff)
        {
            case 1: case 2: case 3: case 20: case 25:
            case 30:
            case 40:
            case 43:
            case 46:
            case 51:
            case 55:
            case 62:
            case 66:
            case 69:
            case 82:
            case 84:
            case 92:
                return true;
            default:
                return false;
        }
    }
    public bool IsGray(int stuff)
    {
        switch(stuff)
        {
            case 7: case 8: case 13: case 15: case 16: case 18: case 22:
            case 26:
            case 33:
            case 35:
            case 36:
            case 42:
            case 52:
            case 57:
            case 58:
            case 65:
            case 67:
            case 72:
            case 75:
            case 79:
            case 85:
            case 87:
            case 95:
                return true;
            default:
                return false;
        }
    }
    public bool IsPink(int stuff)
    {
        switch(stuff)
        {
            case 0: case 4: case 5: case 19: case 23:
            case 31:
            case 27:
            case 39:
            case 47:
            case 50:
            case 56:
            case 63:
            case 64:
            case 70:
            case 71:
            case 81:
            case 88:
            case 93:
                return true;
            default:
                return false;
        }
    }
    public bool IsYellow(int stuff)
    {
        switch(stuff)
        {
            case 9: case 11: case 14: case 17:
            case 32:
            case 34:
            case 38:
            case 41:
            case 44:
            case 49:
            case 53:
            case 60:
            case 68:
            case 76:
            case 83:
            case 89:
            case 94:
                return true;
            default:
                return false;
        }
    }
    public bool IsGreen(int stuff)
    {
        switch(stuff)
        {
            case 6: case 10:
            case 12:
            case 21: case 24:
            case 28:
            case 29:
            case 37:
            case 45:
            case 48:
            case 54:
            case 59:
            case 61:
            case 73:
            case 74:
            case 77:
            case 78:
            case 80:
            case 86:
            case 90:
            case 91:
                return true;
            default:
                return false;
        }
    }
    bool HaveMyStuff(int player)
    {
        int mystuff = user_my_stuff[player];
        bool ihave = false;

        for(int i=0;i<3;i++)
        {
            if (user_stuff[player, i] == mystuff)
                ihave = true;
        }

        return ihave;
    }

    // 선택한 것들을 모두 표시 안되게 하는 함수
    void Selectoff()
    {
        Transform p1 = GameObject.Find("User_Picture1").transform.GetChild(1);
        Transform p2 = GameObject.Find("User_Picture2").transform.GetChild(1);
        Transform p3 = GameObject.Find("User_Picture3").transform.GetChild(1);
        Transform p4 = GameObject.Find("User_Picture4").transform.GetChild(1);
        Transform p5 = GameObject.Find("User_Picture5").transform.GetChild(1);
        Transform p6 = GameObject.Find("User_Picture6").transform.GetChild(1);
        Transform q1 = GameObject.Find("Question_choice1").transform.GetChild(0);
        Transform q2 = GameObject.Find("Question_choice2").transform.GetChild(0);
        Transform q3 = GameObject.Find("Question_choice3").transform.GetChild(0);

        p1.GetChild(0).gameObject.SetActive(false);
        p2.GetChild(0).gameObject.SetActive(false);
        p3.GetChild(0).gameObject.SetActive(false);
        p4.GetChild(0).gameObject.SetActive(false);
        p5.GetChild(0).gameObject.SetActive(false);
        p6.GetChild(0).gameObject.SetActive(false);
        q1.gameObject.SetActive(false);
        q2.gameObject.SetActive(false);
        q3.gameObject.SetActive(false);
    }
}