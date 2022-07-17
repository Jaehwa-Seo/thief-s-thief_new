using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainGameManager : MonoBehaviour
{
    float MaxDistance = 15f;
    Camera cam;
    Vector3 MousePosition;

    bool[] have_stuff = new bool[96];
    int[,] have_stuff_house = new int[14, 7];
    int[,] have_stuff_book = new int[7, 15];
    int[,] have_stuff_profile = new int[7, 15];
    int[] show_stuff = new int[7];

    bool[] have_bg = new bool[11];
    int[,] have_bg_house = new int[3, 5];
    int[] show_bg = new int[5];

    public int scenestat = 0;
    public int housemizing = 0;

    GameObject main_window;
    GameObject house_window;
    GameObject book_window;
    GameObject profile_window;

    GameObject Stuffs;
    GameObject Backgrounds;
    GameObject DragStuff;
    GameObject HouseStuff;
    GameObject Background;
    public GameObject Guide;
    GameObject Return;
    GameObject Books;
    GameObject Profiles;

   // PlayFabManager playfabmanager;
    RightmenuEvent rightmenu;

    Text Tier;

    public RuntimeAnimatorController UI_Up, UI_Down;

    int stuff_total_page = 0;
    int stuff_current_page = 0;
    int total_stuff = 0;
    int total_setting_stuff = 0;
    int[] setting_stuff = new int[20];
    


    int bg_total_page = 0;
    int bg_current_page = 0;
    int total_bg = 0;
    int setting_bg = 0;

    int book_total_page = 0;
    int book_current_page = 0;

    int profile_total_page = 0;
    int profile_current_page = 0;
    int setting_profile=0;


    public bool drag_on = false;
    int drag_stuff = -1;

    bool drag_on_mainstuff = false;
    int drag_mainstuff = -1;
    int drag_index;

    bool Housingmenu = true;
    bool return_click = false;

    bool bookcard_open = false;

    public float Timer=0;

    public int exp;

    string ID;

    Sprite spr1;


    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        main_window = GameObject.Find("Main Window");
        house_window = GameObject.Find("House Window");
        Stuffs = GameObject.Find("Stuffs");
        Backgrounds = GameObject.Find("Backgrounds");
        DragStuff = GameObject.Find("Drag Stuff");
        HouseStuff = GameObject.Find("House_stuffs");
        Background = GameObject.Find("Background");
        Guide = GameObject.Find("Guide");
        Return = GameObject.Find("Return_icon");
        Books = GameObject.Find("Books");
        book_window = GameObject.Find("Book Window");
        profile_window = GameObject.Find("Profile Window");
        Profiles = GameObject.Find("Profiles");
        Tier = GameObject.Find("Tier").GetComponent<Text>();
        rightmenu = GameObject.Find("Main Camera").GetComponent<RightmenuEvent>();
        //playfabmanager = GameObject.Find("PlayFabManager").GetComponent<PlayFabManager>();

        Stuffs.GetComponent<Animator>().speed = 0;

        Backgrounds.SetActive(false);
        Guide.SetActive(false);
        scenestat = 0;


        ID = "현이사랑하다";

        //tier reset
        exp = 0;

        if (exp >= 0 && exp <= 99)
        {
            spr1 = (Sprite)Resources.Load("UI/bronze_frame", typeof(Sprite));
            Tier.text = "Bronze";
        }
        if (exp >= 100 && exp <= 199)
        {
            spr1 = (Sprite)Resources.Load("UI/silver_frame", typeof(Sprite));
            Tier.text = "Silver";
        }
        if (exp >= 200 && exp <= 299)
        {
            spr1 = (Sprite)Resources.Load("UI/gold_frame", typeof(Sprite));
            Tier.text = "Gold";
        }
        if (exp >= 300 && exp <= 399)
        {
            spr1 = (Sprite)Resources.Load("UI/platinum_frame", typeof(Sprite));
            Tier.text = "Platinum";
        }
        if (exp >= 400)
        {
            spr1 = (Sprite)Resources.Load("UI/diamond_frame", typeof(Sprite));
            Tier.text = "Diamond";
        }
        main_window.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = spr1;
        ///

        

        //stuff reset
        for (int i = 0; i < 96; i++)
        {
            /*if (playfabmanager.book[i] == 0)*/
            if (false)
                have_stuff[i] = false;
            else
                have_stuff[i] = true;
        }

        //background reset
        for (int i = 0; i < 11; i++)
        {
/*            if (playfabmanager.background[i] == 0)*/
            if(false)
                have_bg[i] = false;
            else
                have_bg[i] = true;
        }

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 5; j++)
                have_bg_house[i, j] = -1;
        for (int i = 0; i < 14; i++)
            for (int j = 0; j < 7; j++)
                have_stuff_house[i, j] = -1;
        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 15; j++)
                have_stuff_book[i, j] = -1;
        for (int i = 0; i < 7; i++)
            for (int j = 0; j < 15; j++)
                have_stuff_profile[i, j] = -1;

        //setting_stuff reset
        /*for (int i = 0; i < 20; i++)
        {
            setting_stuff[i] = playfabmanager.settingstuff[i];
        }
*/
        for (int i = 0; i < 20; i++)
        {
            setting_stuff[i] = 0;
        }
        int x = 0, y = 0;

        for (int i = 0; i < 96; i++)
        {
            if (have_stuff[i])
            {
                total_stuff++;
                have_stuff_house[x, y] = i;

                y++;
                if (y == 7)
                {
                    y = 0;
                    x++;
                }
                stuff_total_page = x;
            }
        }

        x = 0; y = 0;

        for (int i = 0; i < 11; i++)
        {
            if (have_bg[i])
            {
                total_bg++;
                have_bg_house[x, y] = i;

                y++;
                if (y == 5)
                {
                    y = 0;
                    x++;
                }
                bg_total_page = x;
            }
        }
        x = 0; y = 0;

        for (int i = 0; i < 96; i++)
        {
            have_stuff_book[x, y] = i;
            y++;
            if (y == 15)
            {
                y = 0;
                x++;
            }
            book_total_page = x;
        }

        x = 0; y = 0;
        for (int i=0;i<6;i++)
        {
            have_stuff_profile[x, y] = i;
            y++;
            if (y == 15)
            {
                y = 0;
                x++;
            }
            profile_total_page = x;
        }

        for (int i = 6; i < 102; i++)
        {
            if (have_stuff[i - 6])
            {
                have_stuff_profile[x, y] = i;
                y++;
                if (y == 15)
                {
                    y = 0;
                    x++;
                }
                profile_total_page = x;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rightmenu.guidemode_open)
        {
            main_window.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = null;
            main_window.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = null;
        }
        else
        {
            if (exp >= 0 && exp <= 99)
            {
                spr1 = (Sprite)Resources.Load("UI/bronze_frame", typeof(Sprite));
                Tier.text = "Bronze";
            }
            if (exp >= 100 && exp <= 199)
            {
                spr1 = (Sprite)Resources.Load("UI/silver_frame", typeof(Sprite));
                Tier.text = "Silver";
            }
            if (exp >= 200 && exp <= 299)
            {
                spr1 = (Sprite)Resources.Load("UI/gold_frame", typeof(Sprite));
                Tier.text = "Gold";
            }
            if (exp >= 300 && exp <= 399)
            {
                spr1 = (Sprite)Resources.Load("UI/platinum_frame", typeof(Sprite));
                Tier.text = "Platinum";
            }
            if (exp >= 400)
            {
                spr1 = (Sprite)Resources.Load("UI/diamond_frame", typeof(Sprite));
                Tier.text = "Diamond";
            }
            main_window.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = ID;
        }
        Timer -= Time.deltaTime;
        switch (scenestat)
        {
            case 0:
                {
                    if (Timer < 0)
                    {
                        Guide.SetActive(false);
                        Guide.transform.localPosition = new Vector3(0, 3.95f);
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        MousePosition = Input.mousePosition;
                        MousePosition = cam.ScreenToWorldPoint(MousePosition);

                        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                        RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);
                        if (hit)
                        {
                            if(hit.transform.CompareTag("Profile_icon"))
                            {
                                scenestat = 3;
                                Profilefirst();
                            }
                        }
                    }
                    main_window.SetActive(true);
                    house_window.SetActive(false);
                    book_window.SetActive(false);
                    profile_window.SetActive(false);
                    break;
                }                
            case 1:
                {
                    if(Timer<0)
                    {
                        Guide.SetActive(false);
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        MousePosition = Input.mousePosition;
                        MousePosition = cam.ScreenToWorldPoint(MousePosition);

                        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                        RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);

                        if (hit)
                        {
                            if(hit.transform.CompareTag("Return_icon"))
                            {
                                return_click = true;
                                Sprite spr = (Sprite)Resources.Load("UI/House/return_icon2", typeof(Sprite));
                                Return.transform.GetComponent<SpriteRenderer>().sprite = spr;
                            }
                            if (hit.transform.CompareTag("Stuff_button"))
                            {
                                Stuffs.GetComponent<Animator>().speed = 0;
                                Stuffs.GetComponent<Animator>().runtimeAnimatorController = UI_Down;
                                if (housemizing == 1)
                                {
                                    stuff_current_page = 0;
                                    for (int i = 0; i < 7; i++)
                                    {
                                        Sprite spr;
                                        Stuffs.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                                        Stuffs.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);

                                        if (have_stuff_house[stuff_current_page, i] == -1)
                                        {
                                            spr = null;
                                        }
                                        else
                                        {
                                            spr = (Sprite)Resources.Load("StuffFrames/" + GetStuff(have_stuff_house[stuff_current_page, i]), typeof(Sprite));
                                            for (int j = 0; j < 20; j++)
                                            {
                                                if (show_stuff[i] == setting_stuff[j])
                                                {
                                                    Stuffs.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                                                    Stuffs.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                                                }
                                            }
                                        }
                                        Stuffs.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spr;
                                        show_stuff[i] = have_stuff_house[stuff_current_page, i];
                                    }
                                    Housingmenu = true;
                                    Stuffs.transform.GetChild(10).rotation = Quaternion.identity;
                                    Stuffs.transform.GetChild(10).Rotate(0, 0, 90);
                                    if (stuff_current_page == 0)
                                        Stuffs.transform.GetChild(8).gameObject.SetActive(false);
                                    else
                                    {
                                        Stuffs.transform.GetChild(8).gameObject.SetActive(true);
                                        Stuffs.transform.GetChild(9).gameObject.SetActive(true);
                                    }
                                }
                                
                                housemizing = 0;


                                Stuffs.SetActive(true);
                                Backgrounds.SetActive(false);
                            }
                            if (hit.transform.CompareTag("Bg_button"))
                            {
                                if (housemizing == 0)
                                {
                                    bg_current_page = 0;
                                    for (int i = 0; i < 5; i++)
                                    {
                                        Sprite spr = (Sprite)Resources.Load("UI/House/" + GetBg(have_bg_house[bg_current_page, i]), typeof(Sprite));
                                        Backgrounds.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spr;
                                        show_bg[i] = have_bg_house[bg_current_page, i];

                                        if(show_bg[i]==setting_bg)
                                            Backgrounds.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                                        else
                                            Backgrounds.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                                    }
                                    if (bg_current_page == 0)
                                        Backgrounds.transform.GetChild(5).gameObject.SetActive(false);
                                    else
                                    {
                                        Backgrounds.transform.GetChild(5).gameObject.SetActive(true);
                                        Backgrounds.transform.GetChild(6).gameObject.SetActive(true);
                                    }
                                }
                                housemizing = 1;
                                Stuffs.SetActive(false);
                                Backgrounds.SetActive(true);
                            }

                           

                            // 물건창을 켜놓았을때
                            if (housemizing == 0)
                            {
                                if (hit.transform.CompareTag("Right_icon"))
                                    StuffPagePlus();
                                if (hit.transform.CompareTag("Left_icon"))
                                    StuffPageMinus();


                                if (hit.transform.CompareTag("Stuffs"))
                                {
                                    if (hit.transform.GetComponent<SpriteRenderer>().sprite != null)
                                    {
                                        if (hit.transform.GetChild(0).gameObject.active == false)
                                        {
                                            drag_on = true;
                                            drag_stuff = show_stuff[hit.transform.name[5] - 49];

                                            Sprite spr = (Sprite)Resources.Load("Stuffs/" + GetStuff(drag_stuff), typeof(Sprite));
                                            DragStuff.transform.GetComponent<SpriteRenderer>().sprite = spr;

                                            Stuffs.GetComponent<Animator>().runtimeAnimatorController = UI_Down;
                                            Stuffs.GetComponent<Animator>().speed = 1;
                                        }
                                        else
                                        {
                                            Timer = 3;
                                            Guide.SetActive(true);

                                            Sprite spr = (Sprite)Resources.Load("UI/House/already_guide", typeof(Sprite));
                                            Guide.transform.GetComponent<SpriteRenderer>().sprite = spr;
                                        }
                                    }
                                }

                                if (hit.transform.CompareTag("Updown_icon"))
                                {
                                    if (Housingmenu)
                                    {
                                        Stuffs.GetComponent<Animator>().runtimeAnimatorController = UI_Down;
                                        Stuffs.GetComponent<Animator>().speed = 1;

                                        
                                        Stuffs.transform.GetChild(10).Rotate(0,0,180);
                                        Housingmenu = false;
                                    }
                                    else
                                    {
                                        Stuffs.GetComponent<Animator>().runtimeAnimatorController = UI_Up;
                                        Stuffs.GetComponent<Animator>().speed = 1;

                                        Stuffs.transform.GetChild(10).Rotate(0, 0, 180);
                                        Housingmenu = true;
                                    }
                                }
                                if (hit.transform.CompareTag("Main_stuff"))
                                {
                                    drag_on_mainstuff = true;
                                    drag_index = HouseStuff.transform.Find(hit.transform.name).GetSiblingIndex();
                                    drag_mainstuff = setting_stuff[drag_index];

                                    HouseStuff.transform.GetChild(drag_index).GetComponent<SpriteRenderer>().sprite = null;
                                    Sprite spr = (Sprite)Resources.Load("Stuffs/" + GetStuff(drag_mainstuff), typeof(Sprite));
                                    DragStuff.transform.GetComponent<SpriteRenderer>().sprite = spr;


                                    Stuffs.GetComponent<Animator>().runtimeAnimatorController = UI_Down;
                                    Stuffs.GetComponent<Animator>().speed = 1;
                                }
                                if (hit.transform.CompareTag("Delete_icon"))
                                {
                                    bool find = false;
                                    for (int i = 0; i < total_setting_stuff; i++)
                                    {
                                        if (find)
                                        {
                                            HouseStuff.transform.GetChild(i - 1).localPosition = HouseStuff.transform.GetChild(i).localPosition;
                                            HouseStuff.transform.GetChild(i - 1).GetComponent<SpriteRenderer>().sprite = HouseStuff.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
                                            setting_stuff[i - 1] = setting_stuff[i];

                                            if(i==total_setting_stuff-1)
                                            {
                                                HouseStuff.transform.GetChild(i).localPosition = new Vector3(100, 0);
                                                HouseStuff.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null;
                                                setting_stuff[i] = -1;
                                            }
                                        }
                                        else
                                        {
                                            if (setting_stuff[i] == show_stuff[hit.transform.parent.name[5] - 49])
                                            {
                                                setting_stuff[i] = -1;
                                                HouseStuff.transform.GetChild(i).localPosition = new Vector3(100, 0);
                                                HouseStuff.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null;

                                                hit.transform.parent.GetChild(0).gameObject.SetActive(false);
                                                hit.transform.parent.GetChild(1).gameObject.SetActive(false);
                                                find = true;
                                            }
                                        }
                                    }

                                    total_setting_stuff--;
                                }

                            }
                            else if (housemizing == 1)
                            {
                                if (hit.transform.CompareTag("Right_icon"))
                                    BgPagePlus();
                                if (hit.transform.CompareTag("Left_icon"))
                                    BgPageMinus();

                                if (hit.transform.CompareTag("Backgrounds"))
                                {
                                    if (hit.transform.GetComponent<SpriteRenderer>().sprite != null)
                                    {
                                        for (int i = 0; i < 5; i++)
                                        {
                                            if (show_bg[i] == setting_bg)
                                                Backgrounds.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                                        }
                                        setting_bg = show_bg[hit.transform.name[10] - 49];

                                        Sprite spr = (Sprite)Resources.Load("UI/House/" + GetBg(setting_bg), typeof(Sprite));

                                        Background.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spr;

                                        Backgrounds.transform.GetChild(hit.transform.name[10] - 49).GetChild(0).gameObject.SetActive(true);
                                    }
                                }
                            }                           
                        }
                    }

                    if (drag_on || drag_on_mainstuff)
                    {
                        Vector3 mouseInWorldCoordinates = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z)));

                        DragStuff.transform.position = mouseInWorldCoordinates;
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        if (drag_on)
                        {
                            if (total_setting_stuff != 20)
                            {
                                Debug.Log(total_setting_stuff);
                                HouseStuff.transform.GetChild(total_setting_stuff).position = DragStuff.transform.position;
                                Sprite spr = (Sprite)Resources.Load("Stuffs/" + GetStuff(drag_stuff), typeof(Sprite));
                                HouseStuff.transform.GetChild(total_setting_stuff).GetComponent<SpriteRenderer>().sprite = spr;
                                DragStuff.transform.GetComponent<SpriteRenderer>().sprite = null;

                                setting_stuff[total_setting_stuff] = drag_stuff;

                                for (int i = 0; i < 7; i++)
                                {
                                    if (drag_stuff == show_stuff[i])
                                    {
                                        Stuffs.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                                        Stuffs.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                                    }
                                }

                                drag_on = false;
                                drag_stuff = -1;
                                total_setting_stuff++;
                            }
                            else
                            {
                                DragStuff.transform.GetComponent<SpriteRenderer>().sprite = null;
                                drag_on = false;
                                drag_stuff = -1;

                                Timer = 3;
                                Guide.SetActive(true);

                                Sprite spr = (Sprite)Resources.Load("UI/House/full_guide", typeof(Sprite));
                                Guide.transform.GetComponent<SpriteRenderer>().sprite = spr;
                            }

                            Stuffs.GetComponent<Animator>().runtimeAnimatorController = UI_Up;
                            Stuffs.GetComponent<Animator>().speed = 1;
                        }
                        else if (drag_on_mainstuff)
                        {
                            HouseStuff.transform.GetChild(drag_index).position = DragStuff.transform.position;

                            Sprite spr = (Sprite)Resources.Load("Stuffs/" + GetStuff(drag_mainstuff), typeof(Sprite));
                            HouseStuff.transform.GetChild(drag_index).GetComponent<SpriteRenderer>().sprite = spr;
                            DragStuff.transform.GetComponent<SpriteRenderer>().sprite = null;

                            setting_stuff[total_setting_stuff] = drag_stuff;

                            drag_on_mainstuff = false;
                            drag_mainstuff = -1;

                            Stuffs.GetComponent<Animator>().runtimeAnimatorController = UI_Up;
                            Stuffs.GetComponent<Animator>().speed = 1;
                        }
                        if(return_click)
                        {
                            return_click = false;
                            Sprite spr = (Sprite)Resources.Load("UI/House/return_icon1", typeof(Sprite));
                            Return.transform.GetComponent<SpriteRenderer>().sprite = spr;
                            scenestat = 0;
                        }
                    }

                    main_window.SetActive(false);
                    house_window.SetActive(true);
                    book_window.SetActive(false);
                    profile_window.SetActive(false);
                    break;
                }
            case 2:
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (bookcard_open)
                        {
                            bookcard_open = false;
                            book_window.transform.GetChild(5).gameObject.SetActive(false);
                        }
                        else
                        {

                            MousePosition = Input.mousePosition;
                            MousePosition = cam.ScreenToWorldPoint(MousePosition);

                            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);

                            if (hit)
                            {
                                if (hit.transform.CompareTag("Return_icon"))
                                {
                                    return_click = true;
                                    Sprite spr = (Sprite)Resources.Load("UI/House/return_icon2", typeof(Sprite));
                                    Return.transform.GetComponent<SpriteRenderer>().sprite = spr;
                                }
                                if (hit.transform.CompareTag("Right_icon"))
                                    BookPagePlus();
                                if (hit.transform.CompareTag("Left_icon"))
                                    BookPageMinus();

                                if (hit.transform.CompareTag("Book_icon"))
                                {
                                    if (!hit.transform.GetChild(0).gameObject.active)
                                    {
                                        bookcard_open = true;
                                        book_window.transform.GetChild(5).gameObject.SetActive(true);

                                        int index = Books.transform.Find(hit.transform.name).GetSiblingIndex();

                                        Sprite spr = (Sprite)Resources.Load("Cards/" + GetStuff(have_stuff_book[book_current_page, index]) + "_card", typeof(Sprite));
                                        book_window.transform.GetChild(5).GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
                                    }
                                }
                            }
                        }
                    }

                    if(Input.GetMouseButtonUp(0))
                    {
                        if (return_click)
                        {
                            return_click = false;
                            Sprite spr = (Sprite)Resources.Load("UI/House/return_icon1", typeof(Sprite));
                            Return.transform.GetComponent<SpriteRenderer>().sprite = spr;
                            scenestat = 0;
                        }
                    }

                    main_window.SetActive(false);
                    house_window.SetActive(false);
                    book_window.SetActive(true);
                    profile_window.SetActive(false);
                    break;
                }
            case 3:
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (bookcard_open)
                        {
                            bookcard_open = false;
                            book_window.transform.GetChild(6).gameObject.SetActive(false);
                        }
                        else
                        {

                            MousePosition = Input.mousePosition;
                            MousePosition = cam.ScreenToWorldPoint(MousePosition);

                            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);

                            if (hit)
                            {
                                if (hit.transform.CompareTag("Return_icon"))
                                {
                                    return_click = true;
                                    Sprite spr = (Sprite)Resources.Load("UI/House/return_icon2", typeof(Sprite));
                                    Return.transform.GetComponent<SpriteRenderer>().sprite = spr;
                                }

                                if (hit.transform.CompareTag("Right_icon"))
                                    ProfilePagePlus();
                                if (hit.transform.CompareTag("Left_icon"))
                                    ProfilePageMinus();

                                if (hit.transform.CompareTag("Profile_icon"))
                                {
                                    if (hit.transform.GetComponent<SpriteRenderer>().sprite!=null)
                                    {
                                        int index = Profiles.transform.Find(hit.transform.name).GetSiblingIndex();
                                        Sprite spr;

                                        if (have_stuff_profile[profile_current_page, index] >= 0 && have_stuff_profile[profile_current_page, index] <= 5 )
                                            spr = (Sprite)Resources.Load("StuffFrames/" + GetIcon(have_stuff_profile[profile_current_page, index]), typeof(Sprite));
                                        else
                                            spr = (Sprite)Resources.Load("StuffFrames/" + GetStuff(have_stuff_profile[profile_current_page, index]-6), typeof(Sprite));

                                        main_window.transform.GetChild(2).GetChild(1).GetComponent<SpriteRenderer>().sprite = spr;
                                        setting_profile = have_stuff_profile[profile_current_page, index];

                                        for(int i=0;i<15;i++)
                                            Profiles.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);

                                        Profiles.transform.GetChild(index).GetChild(0).gameObject.SetActive(true);

                                    }
                                }
                            }
                        }
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        if (return_click)
                        {
                            return_click = false;
                            Sprite spr = (Sprite)Resources.Load("UI/House/return_icon1", typeof(Sprite));
                            Return.transform.GetComponent<SpriteRenderer>().sprite = spr;
                            scenestat = 0;
                        }
                    }

                    main_window.SetActive(false);
                    house_window.SetActive(false);
                    book_window.SetActive(false);
                    profile_window.SetActive(true);
                    break;
                }
        }
    }

    void StuffPagePlus()
    {
        if (stuff_current_page != stuff_total_page)
        {
            stuff_current_page++;
            for (int i = 0; i < 7; i++)
            {
                Sprite spr;
                Stuffs.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                Stuffs.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);

                if (have_stuff_house[stuff_current_page, i] == -1)
                {
                    spr = null;
                }
                else
                {
                    spr = (Sprite)Resources.Load("StuffFrames/" + GetStuff(have_stuff_house[stuff_current_page, i]), typeof(Sprite));
                    for (int j = 0; j < 20; j++)
                    {
                        if (show_stuff[i] == setting_stuff[j])
                        {
                            Stuffs.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                            Stuffs.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                        }
                    }
                }
                Stuffs.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spr;
                show_stuff[i] = have_stuff_house[stuff_current_page, i];

                

                
            }
            if (stuff_current_page == stuff_total_page)
                Stuffs.transform.GetChild(9).gameObject.SetActive(false);
            else
            {
                Stuffs.transform.GetChild(8).gameObject.SetActive(true);
                Stuffs.transform.GetChild(9).gameObject.SetActive(true);
            }
        }

    }
    void StuffPageMinus()
    {
        if (stuff_current_page != 0)
        {
            stuff_current_page--;
            for (int i = 0; i < 7; i++)
            {
                Sprite spr;
                Stuffs.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                Stuffs.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
                if (have_stuff_house[stuff_current_page, i] == -1)
                    spr = null;
                else
                {
                    spr = (Sprite)Resources.Load("StuffFrames/" + GetStuff(have_stuff_house[stuff_current_page, i]), typeof(Sprite));
                    for (int j = 0; j < 20; j++)
                    {
                        if (show_stuff[i] == setting_stuff[j])
                        {
                            Stuffs.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                            Stuffs.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                        }
                    }
                }
                Stuffs.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spr;
                show_stuff[i] = have_stuff_house[stuff_current_page, i];

                
               
            }
            if (stuff_current_page == 0)
                Stuffs.transform.GetChild(8).gameObject.SetActive(false);
            else
            {
                Stuffs.transform.GetChild(8).gameObject.SetActive(true);
                Stuffs.transform.GetChild(9).gameObject.SetActive(true);
            }
        }
    }

    void BgPagePlus()
    {
        if (bg_current_page != bg_total_page)
        {
            bg_current_page++;
            for (int i = 0; i < 5; i++)
            {
                Sprite spr;
                if (bg_current_page == bg_total_page && have_bg_house[bg_current_page, i] == 0)
                    spr = null;
                else
                    spr = (Sprite)Resources.Load("UI/House/" + GetBg(have_bg_house[bg_current_page, i]), typeof(Sprite));
                Backgrounds.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spr;
                show_bg[i] = have_bg_house[bg_current_page, i];

                if (show_bg[i] == setting_bg)
                    Backgrounds.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                else
                    Backgrounds.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
            if (bg_current_page == bg_total_page)
                Backgrounds.transform.GetChild(6).gameObject.SetActive(false);
            else
            {
                Backgrounds.transform.GetChild(5).gameObject.SetActive(true);
                Backgrounds.transform.GetChild(6).gameObject.SetActive(true);
            }
        }
       
    }
    void BgPageMinus()
    {
        Debug.Log("asd");
        if (bg_current_page != 0)
        {
            bg_current_page--;
            for (int i = 0; i < 5; i++)
            {
                Sprite spr;
                if (bg_current_page == bg_total_page && have_bg_house[bg_current_page, i] == 0)
                    spr = null;
                else
                    spr = (Sprite)Resources.Load("UI/House/" + GetBg(have_bg_house[bg_current_page, i]), typeof(Sprite));
                Backgrounds.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spr;
                show_bg[i] = have_bg_house[bg_current_page, i];

                if (show_bg[i] == setting_bg)
                    Backgrounds.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                else
                    Backgrounds.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
            if (bg_current_page == 0)
                Backgrounds.transform.GetChild(5).gameObject.SetActive(false);
            else
            {
                Backgrounds.transform.GetChild(5).gameObject.SetActive(true);
                Backgrounds.transform.GetChild(6).gameObject.SetActive(true);
            }
        }
        

    }

    void BookPagePlus()
    {
        if (book_current_page != book_total_page)
        {
            book_current_page++;
            for (int i = 0; i < 15; i++)
            {
                Sprite spr;

                Books.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);

                if (have_stuff_book[book_current_page, i] == -1)
                {
                    spr = null;
                }
                else
                {
                    spr = (Sprite)Resources.Load("StuffFrames/" + GetStuff(have_stuff_book[book_current_page, i]), typeof(Sprite));
                    if (!have_stuff[(book_current_page * 15) + i])
                    {
                        Books.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                    }
                }
                Books.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spr;               
            }
            if (book_current_page == book_total_page)
                book_window.transform.GetChild(3).gameObject.SetActive(false);
            else
            {
                book_window.transform.GetChild(2).gameObject.SetActive(true);
                book_window.transform.GetChild(3).gameObject.SetActive(true);
            }
        }

    }
    void BookPageMinus()
    {
        if (book_current_page != 0)
        {
            book_current_page--;
            for (int i = 0; i < 15; i++)
            {
                Sprite spr;

                Books.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);

                if (have_stuff_book[book_current_page, i] == -1)
                {
                    spr = null;
                    Books.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    spr = (Sprite)Resources.Load("StuffFrames/" + GetStuff(have_stuff_book[book_current_page, i]), typeof(Sprite));
                    if (!have_stuff[(book_current_page * 15) + i])
                    {
                        Books.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                    }
                }
                Books.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spr;
            }
            if (book_current_page == 0)
                book_window.transform.GetChild(2).gameObject.SetActive(false);
            else
            {
                book_window.transform.GetChild(2).gameObject.SetActive(true);
                book_window.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }

    void ProfilePagePlus()
    {
        if (profile_current_page != profile_total_page)
        {
            profile_current_page++;
            for (int i = 0; i < 15; i++)
            {
                Sprite spr;

                Profiles.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);

                if (have_stuff_profile[profile_current_page, i] == -1)
                {
                    spr = null;
                }
                else
                {
                    if (have_stuff_profile[profile_current_page, i] >= 0 && have_stuff_profile[profile_current_page, i] <= 5)
                        spr = (Sprite)Resources.Load("StuffFrames/" + GetIcon(have_stuff_profile[profile_current_page, i]), typeof(Sprite));
                    else
                        spr = (Sprite)Resources.Load("StuffFrames/" + GetStuff(have_stuff_profile[profile_current_page, i] - 6), typeof(Sprite));

                    if (have_stuff_profile[profile_current_page, i] == setting_profile)
                        Profiles.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);

                }
                Profiles.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spr;
            }
            if (profile_current_page == profile_total_page)
                profile_window.transform.GetChild(3).gameObject.SetActive(false);
            else
            {
                profile_window.transform.GetChild(2).gameObject.SetActive(true);
                profile_window.transform.GetChild(3).gameObject.SetActive(true);
            }
        }

    }
    void ProfilePageMinus()
    {
        if (profile_current_page != 0)
        {
            profile_current_page--;
            for (int i = 0; i < 15; i++)
            {
                Sprite spr;

                Profiles.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);

                if (have_stuff_profile[profile_current_page, i] == -1)
                {
                    spr = null;
                }
                else
                {
                    if (have_stuff_profile[profile_current_page, i] >= 0 && have_stuff_profile[profile_current_page, i] <= 5)
                        spr = (Sprite)Resources.Load("StuffFrames/" + GetIcon(have_stuff_profile[profile_current_page, i]), typeof(Sprite));
                    else
                        spr = (Sprite)Resources.Load("StuffFrames/" + GetStuff(have_stuff_profile[profile_current_page, i] - 6), typeof(Sprite));

                    if (have_stuff_profile[profile_current_page, i] == setting_profile)
                        Profiles.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);

                }
                Profiles.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spr;
            }
            if (profile_current_page == 0)
                profile_window.transform.GetChild(3).gameObject.SetActive(false);
            else
            {
                profile_window.transform.GetChild(2).gameObject.SetActive(true);
                profile_window.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }

    public void Housingfirst()
    {
        Stuffs.GetComponent<Animator>().speed = 0;
        Stuffs.GetComponent<Animator>().runtimeAnimatorController = UI_Down;

        stuff_current_page = 0;
        for (int i = 0; i < 7; i++)
        {
            Sprite spr;
            Stuffs.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            Stuffs.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);

            if (have_stuff_house[stuff_current_page, i] == -1)
            {
                spr = null;
            }
            else
            {
                spr = (Sprite)Resources.Load("StuffFrames/" + GetStuff(have_stuff_house[stuff_current_page, i]), typeof(Sprite));
                for (int j = 0; j < 20; j++)
                {
                    if (show_stuff[i] == setting_stuff[j])
                    {
                        Stuffs.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                        Stuffs.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                    }
                }
            }
            Stuffs.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spr;
            show_stuff[i] = have_stuff_house[stuff_current_page, i];
        }
        Housingmenu = true;
        Stuffs.transform.GetChild(10).rotation = Quaternion.identity;
        Stuffs.transform.GetChild(10).Rotate(0, 0, 90);


        housemizing = 0;

        if(stuff_total_page==0 && stuff_current_page==0)
        {
            Stuffs.transform.GetChild(8).gameObject.SetActive(false);
            Stuffs.transform.GetChild(9).gameObject.SetActive(false);
        }
        else if (stuff_current_page == 0)
            Stuffs.transform.GetChild(8).gameObject.SetActive(false);
        else
        {
            Stuffs.transform.GetChild(8).gameObject.SetActive(true);
            Stuffs.transform.GetChild(9).gameObject.SetActive(true);
        }

        Stuffs.SetActive(true);
        Backgrounds.SetActive(false);
    }

    public void Bookfirst()
    {
        book_current_page = 0;
        for (int i = 0; i < 15; i++)
        {
            Sprite spr = (Sprite)Resources.Load("StuffFrames/" + GetStuff(have_stuff_book[book_current_page, i]), typeof(Sprite));
            Books.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spr;

            if (!have_stuff[(book_current_page * 15) + i])
                Books.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);

            if (book_current_page == 0)
                book_window.transform.GetChild(2).gameObject.SetActive(false);
            else
            {
                book_window.transform.GetChild(2).gameObject.SetActive(false);
                book_window.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }

    public void Profilefirst()
    {
        profile_current_page = 0;
        for (int i = 0; i < 15; i++)
        {
            Sprite spr;
            if(have_stuff_profile[profile_current_page, i] >= 0 && have_stuff_profile[profile_current_page, i] <= 5)
                spr = (Sprite)Resources.Load("StuffFrames/" + GetIcon(have_stuff_profile[profile_current_page, i]), typeof(Sprite));
            else
                spr = (Sprite)Resources.Load("StuffFrames/" + GetStuff(have_stuff_profile[profile_current_page, i]-6), typeof(Sprite));

            Profiles.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spr;

            Profiles.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            if(have_stuff_profile[profile_current_page,i]==setting_profile)
                Profiles.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);

            if (profile_total_page == 0 && profile_current_page == 0)
            {
                profile_window.transform.GetChild(2).gameObject.SetActive(false);
                profile_window.transform.GetChild(3).gameObject.SetActive(false);
            }
            else if (profile_current_page == 0)
                profile_window.transform.GetChild(2).gameObject.SetActive(false);
            else
            {
                profile_window.transform.GetChild(2).gameObject.SetActive(false);
                profile_window.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
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

    public string GetIcon(float stuff)
    {
        switch ((int)stuff)
        {
            case 0:
                return "icon1";
            case 1:
                return "icon2";
            case 2:
                return "icon3";
            case 3:
                return "icon4";
            case 4:
                return "icon5";
            case 5:
                return "icon6";
        }
        return "";
    }


    public string GetBg(float stuff)
    {
        switch ((int)stuff)
        {
            case 0:
                return "house_bg1";
            case 1:
                return "house_bg2";
            case 2:
                return "house_bg3";
            case 3:
                return "house_bg4";
            case 4:
                return "house_bg5";
            case 5:
                return "house_bg6";
            case 6:
                return "house_bg7";
            case 7:
                return "house_bg8";
            case 8:
                return "house_bg9";
            case 9:
                return "house_bg10";
            case 10:
                return "house_bg11";
        }
        return "";
    }
}
