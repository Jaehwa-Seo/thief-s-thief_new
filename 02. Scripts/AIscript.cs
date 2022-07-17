using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIscript : MonoBehaviour
{
    int[,] user_first_stuff = new int[6, 3];
    int[,] user_current_stuff = new int[6, 3];
    int[] user_my_stuff = new int[6];

    int[] user_job = new int[6] { 0,0,0,0,0,0 };

    int[] user_question = new int[3];
    public int job;
    public int order;
    int[] total_stuff = new int[17];
    int[] total_color = new int[5];

    int rand;

    GameManager gm;


    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        for (int i=0;i<6;i++)
        {
            for(int j=0;j<3;j++)
            {
                user_first_stuff[i, j] = gm.user_stuff[i, j];
                if (gm.IsCat(user_first_stuff[i,j]))
                    total_stuff[0]++;
                else if (gm.IsApple(user_first_stuff[i, j]))
                    total_stuff[1]++;
                else if (gm.IsPencil(user_first_stuff[i, j]))
                    total_stuff[2]++;
                else if (gm.IsShit(user_first_stuff[i, j]))
                    total_stuff[3]++;
                else if (gm.IsBear(user_first_stuff[i, j]))
                    total_stuff[4]++;
                else if (gm.IsTree(user_first_stuff[i, j]))
                    total_stuff[5]++;
                else if (gm.IsCandy(user_first_stuff[i, j]))
                    total_stuff[6]++;
                else if (gm.IsStone(user_first_stuff[i, j]))
                    total_stuff[7]++;
                else if (gm.IsMouse(user_first_stuff[i, j]))
                    total_stuff[8]++;
                else if (gm.IsBook(user_first_stuff[i, j]))
                    total_stuff[9]++;
                else if (gm.IsAnt(user_first_stuff[i, j]))
                    total_stuff[10]++;
                else if (gm.IsBag(user_first_stuff[i, j]))
                    total_stuff[11]++;
                else if (gm.IsDog(user_first_stuff[i, j]))
                    total_stuff[12]++;
                else if (gm.IsPersimmon(user_first_stuff[i, j]))
                    total_stuff[13]++;
                else if (gm.IsPeppar(user_first_stuff[i, j]))
                    total_stuff[14]++;
                else if (gm.IsGhost(user_first_stuff[i, j]))
                    total_stuff[15]++;
                else if (gm.IsButterfly(user_first_stuff[i, j]))
                    total_stuff[16]++;

                if (gm.IsBlue(user_first_stuff[i, j]))
                    total_color[0]++;
                else if (gm.IsGray(user_first_stuff[i, j]))
                    total_color[1]++;
                else if (gm.IsPink(user_first_stuff[i, j]))
                    total_color[2]++;
                else if (gm.IsYellow(user_first_stuff[i, j]))
                    total_color[3]++;
                else if (gm.IsGreen(user_first_stuff[i, j]))
                    total_color[4]++;
            }
        }
        user_current_stuff = gm.user_stuff;
        user_question = gm.user_question;
        user_my_stuff = gm.user_my_stuff;
        user_job[order] = job;
    }

    void Update()
    {
        // 직업에 따른 Ai의 선택
        switch(job)
        {
            case 0:
            case 2:
                {

                    switch (gm.game_phase)
                    {
                        case 2:
                            {
                                if (gm.Timer < 12)
                                {
                                    // 내 질문 상태일 때
                                    if (order == gm.player_order)
                                    {

                                        int qvalue = 0;
                                        int max = 0;
                                        int select_q = -1;
                                        for (int i = 0; i < 3; i++)
                                        {
                                            // 질문에 따른 가치 선택
                                            if (user_question[i] >= 30 && user_question[i] <= 34)
                                            {
                                                qvalue = 100;
                                            }
                                            else if (user_question[i] >= 15 && user_question[i] <= 19)
                                            {
                                                qvalue = 90;
                                            }
                                            else if ((user_question[i] >= 5 && user_question[i] <= 14) || (user_question[i] >= 20 && user_question[i] <= 26))
                                            {
                                                switch (user_question[i])
                                                {
                                                    case 5:
                                                        {
                                                            if (total_stuff[1] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 6:
                                                        {
                                                            if (total_stuff[4] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 7:
                                                        {
                                                            if (total_stuff[0] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 8:
                                                        {
                                                            if (total_stuff[2] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 9:
                                                        {
                                                            if (total_stuff[3] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 10:
                                                        {
                                                            if (total_stuff[5] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 11:
                                                        {
                                                            if (total_stuff[6] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 12:
                                                        {
                                                            if (total_stuff[7] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 13:
                                                        {
                                                            if (total_stuff[8] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 14:
                                                        {
                                                            if (total_stuff[9] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 20:
                                                        {
                                                            if (total_stuff[10] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 21:
                                                        {
                                                            if (total_stuff[11] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 22:
                                                        {
                                                            if (total_stuff[12] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 23:
                                                        {
                                                            if (total_stuff[13] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 24:
                                                        {
                                                            if (total_stuff[14] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 25:
                                                        {
                                                            if (total_stuff[15] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 26:
                                                        {
                                                            if (total_stuff[16] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 30:
                                                    case 31:
                                                    case 32:
                                                    case 33:
                                                    case 34:
                                                    case 35:
                                                    case 36:
                                                    case 37:
                                                    case 38:
                                                    case 39:
                                                    case 40:
                                                    case 41:
                                                        {
                                                            qvalue = 110;
                                                            break;
                                                        }
                                                }
                                            }
                                            else
                                                qvalue = 60;
                                            // 질문의 가치중 가장 높은 것을 선택
                                            if (max < qvalue)
                                            {
                                                max = qvalue;
                                                select_q = i;
                                            }
                                        }
                                        gm.player_inputvalue = select_q;
                                        gm.question_confirm = true;
                                    }
                                }
                                break;
                            }
                        case 4:
                            {
                                if (gm.Timer < 7)
                                {
                                    if (order == gm.player_order)
                                    {
                                        //내가 나쁜사람인지 아는 사람이 있는 지 확인
                                        int know = 0;
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (user_job[i] != 0)
                                                know++;
                                        }
                                        if (know <= 5)
                                        {
                                            while (true)
                                            {
                                                rand = Random.Range(0, 6);
                                                if (order == rand)
                                                    continue;
                                                if (user_job[rand] == 0)
                                                    break;
                                            }
                                            gm.player_inputvalue = rand;
                                            gm.question_confirm = true;
                                        }
                                    }
                                }
                                break;
                            }
                        case 7:
                            {
                                if (gm.Timer < 7)
                                {
                                    if (order == gm.player_order)
                                    {
                                        bool select = false;
                                        int target = -1;
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (user_job[i] == 1)
                                            {
                                                target = user_my_stuff[i];
                                            }
                                        }
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (i == order)
                                                continue;
                                            int have = 0;
                                            for (int j = 0; j < 3; j++)
                                            {
                                                if (user_current_stuff[i, j] == target)
                                                    have++;
                                            }

                                            if (have != 0)
                                            {
                                                gm.player_inputvalue = i;
                                                gm.question_confirm = true;
                                                select = true;
                                                break;
                                            }
                                        }

                                        if (!select)
                                        {
                                            while (true)
                                            {
                                                rand = Random.Range(0, 6);
                                                if (rand == order)
                                                    continue;

                                                gm.player_inputvalue = rand;
                                                gm.question_confirm = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case 8:
                            {
                                if (gm.Timer < 7)
                                {
                                    if (order == gm.player_order)
                                    {
                                        bool select = false;
                                        int target = -1;
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (user_job[i] == 1)
                                            {
                                                target = user_my_stuff[i];
                                            }
                                        }
                                        for (int i = 0; i < 3; i++)
                                        {
                                            if (user_current_stuff[gm.swap_target, i] == target)
                                            {
                                                gm.player_inputvalue = i;
                                                gm.question_confirm = true;
                                                select = true;
                                                break;
                                            }
                                        }

                                        if (!select)
                                        {
                                            while (true)
                                            {
                                                rand = Random.Range(0, 3);
                                                gm.player_inputvalue = rand;
                                                gm.question_confirm = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case 9:
                            {
                                if (order == gm.swap_target)
                                {
                                    bool select = false;
                                    int target = -1;
                                    for (int i = 0; i < 6; i++)
                                    {
                                        if (user_job[i] == 1)
                                        {
                                            target = user_my_stuff[i];
                                        }
                                    }
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (user_current_stuff[gm.swap_target, i] == target)
                                        {
                                            gm.player_inputvalue = i;
                                            gm.question_confirm = true;
                                            select = true;
                                            break;
                                        }
                                    }

                                    while (true)
                                    {
                                        rand = Random.Range(0, 3);
                                        gm.player_inputvalue = rand;
                                        gm.question_confirm = true;
                                        break;
                                    }
                                }
                                break;
                            }
                    }

                    break;
                }
            case 1:
                {

                    switch (gm.game_phase)
                    {
                        case 2:
                            {
                                if (gm.Timer < 12)
                                {
                                    // 내 질문 상태일 때
                                    if (order == gm.player_order)
                                    {

                                        int qvalue = 0;
                                        int max = 0;
                                        int select_q = -1;
                                        for (int i = 0; i < 3; i++)
                                        {
                                            // 질문에 따른 가치 선택
                                            if (user_question[i] >= 30 && user_question[i] <= 34)
                                            {
                                                qvalue = 100;
                                            }
                                            else if (user_question[i] >= 15 && user_question[i] <= 19)
                                            {
                                                qvalue = 90;
                                            }
                                            else if ((user_question[i] >= 5 && user_question[i] <= 14) || (user_question[i] >= 20 && user_question[i] <= 26))
                                            {
                                                switch (user_question[i])
                                                {
                                                    case 5:
                                                        {
                                                            if (total_stuff[1] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 6:
                                                        {
                                                            if (total_stuff[4] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 7:
                                                        {
                                                            if (total_stuff[0] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 8:
                                                        {
                                                            if (total_stuff[2] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 9:
                                                        {
                                                            if (total_stuff[3] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 10:
                                                        {
                                                            if (total_stuff[5] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 11:
                                                        {
                                                            if (total_stuff[6] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 12:
                                                        {
                                                            if (total_stuff[7] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 13:
                                                        {
                                                            if (total_stuff[8] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 14:
                                                        {
                                                            if (total_stuff[9] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 20:
                                                        {
                                                            if (total_stuff[10] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 21:
                                                        {
                                                            if (total_stuff[11] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 22:
                                                        {
                                                            if (total_stuff[12] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 23:
                                                        {
                                                            if (total_stuff[13] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 24:
                                                        {
                                                            if (total_stuff[14] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 25:
                                                        {
                                                            if (total_stuff[15] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 26:
                                                        {
                                                            if (total_stuff[16] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 30:
                                                    case 31:
                                                    case 32:
                                                    case 33:
                                                    case 34:
                                                    case 35:
                                                    case 36:
                                                    case 37:
                                                    case 38:
                                                    case 39:
                                                    case 40:
                                                    case 41:
                                                        {
                                                            qvalue = 110;
                                                            break;
                                                        }
                                                }
                                            }
                                            else
                                                qvalue = 60;
                                            // 질문의 가치중 가장 높은 것을 선택
                                            if (max < qvalue)
                                            {
                                                max = qvalue;
                                                select_q = i;
                                            }
                                        }
                                        gm.player_inputvalue = select_q;
                                        gm.question_confirm = true;
                                    }
                                }
                                break;
                            }
                        case 4:
                            {
                                if (gm.Timer < 7)
                                {
                                    if (order == gm.player_order)
                                    {
                                        //내가 나쁜사람인지 아는 사람이 있는 지 확인
                                        int know = 0;
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (user_job[i] != 0)
                                                know++;
                                        }
                                        if (know <= 5)
                                        {
                                            while (true)
                                            {
                                                rand = Random.Range(0, 6);
                                                if (order == rand)
                                                    continue;
                                                if (user_job[rand] == 0)
                                                    break;
                                            }
                                            gm.player_inputvalue = rand;
                                            gm.question_confirm = true;
                                        }
                                    }
                                }
                                break;
                            }
                        case 7:
                            {
                                if (gm.Timer < 7)
                                {
                                    if (order == gm.player_order)
                                    {
                                        bool select = false;
                                        int target = -1;
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (user_job[i] == -1)
                                            {
                                                target = user_my_stuff[i];
                                            }
                                        }
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (i == order)
                                                continue;
                                            int have = 0;
                                            for (int j = 0; j < 3; j++)
                                            {
                                                if (user_current_stuff[i, j] == target)
                                                    have++;
                                            }

                                            if (have != 0)
                                            {
                                                gm.player_inputvalue = i;
                                                gm.question_confirm = true;
                                                select = true;
                                                break;
                                            }
                                        }

                                        if (!select)
                                        {
                                            while (true)
                                            {
                                                rand = Random.Range(0, 6);
                                                if (rand == order)
                                                    continue;

                                                gm.player_inputvalue = rand;
                                                gm.question_confirm = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case 8:
                            {
                                if (gm.Timer < 7)
                                {
                                    if (order == gm.player_order)
                                    {
                                        bool select = false;
                                        int target = -1;
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (user_job[i] == -1)
                                            {
                                                target = user_my_stuff[i];
                                            }
                                        }
                                        for (int i = 0; i < 3; i++)
                                        {
                                            if (user_current_stuff[gm.swap_target, i] == target)
                                            {
                                                gm.player_inputvalue = i;
                                                gm.question_confirm = true;
                                                select = true;
                                                break;
                                            }
                                        }

                                        if (!select)
                                        {
                                            while (true)
                                            {
                                                rand = Random.Range(0, 3);
                                                gm.player_inputvalue = rand;
                                                gm.question_confirm = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case 9:
                            {
                                if (order == gm.swap_target)
                                {
                                    bool select = false;
                                    int target = -1;
                                    for (int i = 0; i < 6; i++)
                                    {
                                        if (user_job[i] == -1)
                                        {
                                            target = user_my_stuff[i];
                                        }
                                    }
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (user_current_stuff[gm.swap_target, i] == target)
                                        {
                                            gm.player_inputvalue = i;
                                            gm.question_confirm = true;
                                            select = true;
                                            break;
                                        }
                                    }

                                    while (true)
                                    {
                                        rand = Random.Range(0, 3);
                                        gm.player_inputvalue = rand;
                                        gm.question_confirm = true;
                                        break;
                                    }
                                }
                                break;
                            }
                    }

                    break;
                }
            
            case 3:
            case 4:
            case 5:
                {

                    switch (gm.game_phase)
                    {
                        case 2:
                            {
                                if (gm.Timer < 12)
                                {
                                    // 내 질문 상태일 때
                                    if (order == gm.player_order)
                                    {

                                        int qvalue = 0;
                                        int max = 0;
                                        int select_q = -1;
                                        for (int i = 0; i < 3; i++)
                                        {
                                            // 질문에 따른 가치 선택
                                            if (user_question[i] >= 30 && user_question[i] <= 34)
                                            {
                                                qvalue = 100;
                                            }
                                            else if (user_question[i] >= 15 && user_question[i] <= 19)
                                            {
                                                qvalue = 90;
                                            }
                                            else if ((user_question[i] >= 5 && user_question[i] <= 14) || (user_question[i] >= 20 && user_question[i] <= 26))
                                            {
                                                switch (user_question[i])
                                                {
                                                    case 5:
                                                        {
                                                            if (total_stuff[1] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 6:
                                                        {
                                                            if (total_stuff[4] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 7:
                                                        {
                                                            if (total_stuff[0] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 8:
                                                        {
                                                            if (total_stuff[2] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 9:
                                                        {
                                                            if (total_stuff[3] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 10:
                                                        {
                                                            if (total_stuff[5] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 11:
                                                        {
                                                            if (total_stuff[6] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 12:
                                                        {
                                                            if (total_stuff[7] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 13:
                                                        {
                                                            if (total_stuff[8] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 14:
                                                        {
                                                            if (total_stuff[9] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 20:
                                                        {
                                                            if (total_stuff[10] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 21:
                                                        {
                                                            if (total_stuff[11] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 22:
                                                        {
                                                            if (total_stuff[12] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 23:
                                                        {
                                                            if (total_stuff[13] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 24:
                                                        {
                                                            if (total_stuff[14] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 25:
                                                        {
                                                            if (total_stuff[15] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 26:
                                                        {
                                                            if (total_stuff[16] == 0)
                                                                qvalue = 80;
                                                            else
                                                                qvalue = 70;
                                                            break;
                                                        }
                                                    case 30:
                                                    case 31:
                                                    case 32:
                                                    case 33:
                                                    case 34:
                                                    case 35:
                                                    case 36:
                                                    case 37:
                                                    case 38:
                                                    case 39:
                                                    case 40:
                                                    case 41:
                                                        {
                                                            qvalue = 110;
                                                            break;
                                                        }
                                                }
                                            }
                                            else
                                                qvalue = 60;
                                            // 질문의 가치중 가장 높은 것을 선택
                                            if (max < qvalue)
                                            {
                                                max = qvalue;
                                                select_q = i;
                                            }
                                        }
                                        gm.player_inputvalue = select_q;
                                        gm.question_confirm = true;
                                    }
                                }
                                break;
                            }
                        case 4:
                            {
                                if (gm.Timer < 7)
                                {
                                    if (order == gm.player_order)
                                    {
                                        //내가 나쁜사람인지 아는 사람이 있는 지 확인
                                        int know = 0;
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (user_job[i] != 0)
                                                know++;
                                        }
                                        if (know <= 5)
                                        {
                                            while (true)
                                            {
                                                rand = Random.Range(0, 6);
                                                if (order == rand)
                                                    continue;
                                                if (user_job[rand] == 0)
                                                    break;
                                            }
                                            gm.player_inputvalue = rand;
                                            gm.question_confirm = true;
                                        }
                                    }
                                }
                                break;
                            }
                        case 7:
                            {
                                if (gm.Timer < 7)
                                {
                                    if (order == gm.player_order)
                                    {
                                        bool select = false;
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (i == order)
                                                continue;
                                            int have = 0;
                                            for (int j = 0; j < 3; j++)
                                            {
                                                if (user_current_stuff[i, j] == user_my_stuff[order])
                                                    have++;
                                            }

                                            if (have > 0)
                                            {
                                                gm.player_inputvalue = i;
                                                gm.question_confirm = true;
                                                select = true;
                                                break;
                                            }
                                        }
                                        if (!select)
                                        {
                                            while (true)
                                            {
                                                rand = Random.Range(0, 6);
                                                if (rand == order)
                                                    continue;

                                                gm.player_inputvalue = rand;
                                                gm.question_confirm = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case 8:
                            {
                                if (gm.Timer < 7)
                                {
                                    if (order == gm.player_order)
                                    {
                                        bool select = false;
                                        for (int i = 0; i < 3; i++)
                                        {
                                            if (user_current_stuff[gm.swap_target, i] == user_my_stuff[order])
                                            {
                                                gm.player_inputvalue = i;
                                                gm.question_confirm = true;
                                                select = true;
                                                break;
                                            }
                                        }
                                        if (!select)
                                        {
                                            while (true)
                                            {
                                                rand = Random.Range(0, 3);
                                                gm.player_inputvalue = rand;
                                                gm.question_confirm = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case 9:
                            {
                                if (order == gm.swap_target)
                                {
                                    bool select = false;
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (user_current_stuff[gm.swap_target, i] == user_my_stuff[order])
                                        {
                                            gm.player_inputvalue = i;
                                            gm.question_confirm = true;
                                            select = true;
                                            break;
                                        }
                                    }
                                    if (!select)
                                    {
                                        while (true)
                                        {
                                            rand = Random.Range(0, 3);
                                            gm.player_inputvalue = rand;
                                            gm.question_confirm = true;
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                    }

                    break;
                }
        }
    }

   public void GetInfo(int question,bool answer,int target)
    {
        switch (question)
        {
            case 0:
                {
                    if (gm.IsBlue(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 1:
                {
                    if (gm.IsGray(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 2:
                {
                    if (gm.IsGreen(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 3:
                {
                    if (gm.IsPink(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 4:
                {
                    if (gm.IsYellow(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 5:
                {
                    if (gm.IsApple(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 6:
                {
                    if (gm.IsBear(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 7:
                {
                    if (gm.IsCat(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 8:
                {
                    if (gm.IsPencil(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 9:
                {
                    if (gm.IsShit(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 10:
                {
                    if (gm.IsTree(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 11:
                {
                    if (gm.IsCandy(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 12:
                {
                    if (gm.IsStone(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 13:
                {
                    if (gm.IsMouse(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 14:
                {
                    if (gm.IsBook(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 15:
                {

                    int have = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (gm.IsBlue(user_current_stuff[target, i]))
                            have++;
                    }

                    if (answer)
                    {
                        if (have == 0)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    else
                    {
                        if (have == 0)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    break;
                }
            case 16:
                {
                    int have = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (gm.IsGray(user_current_stuff[target, i]))
                            have++;
                    }

                    if (answer)
                    {
                        if (have == 0)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    else
                    {
                        if (have == 0)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    break;
                }
            case 17:
                {
                    int have = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (gm.IsGreen(user_current_stuff[target, i]))
                            have++;
                    }

                    if (answer)
                    {
                        if (have == 0)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    else
                    {
                        if (have == 0)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    break;
                }
            case 18:
                {
                    int have = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (gm.IsPink(user_current_stuff[target, i]))
                            have++;
                    }

                    if (answer)
                    {
                        if (have == 0)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    else
                    {
                        if (have == 0)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    break;
                }
            case 19:
                {
                    int have = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (gm.IsYellow(user_current_stuff[target, i]))
                            have++;
                    }

                    if (answer)
                    {
                        if (have == 0)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    else
                    {
                        if (have == 0)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    break;
                }
            case 20:
                {
                    if (gm.IsAnt(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 21:
                {
                    if (gm.IsBag(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 22:
                {
                    if (gm.IsDog(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 23:
                {
                    if (gm.IsPersimmon(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 24:
                {
                    if (gm.IsPeppar(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 25:
                {
                    if (gm.IsGhost(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
            case 26:
                {
                    if (gm.IsButterfly(user_my_stuff[target]))
                    {
                        if (answer)
                            user_job[target] = 1;
                        else
                            user_job[target] = -1;
                    }
                    else
                    {
                        if (answer)
                            user_job[target] = -1;
                        else
                            user_job[target] = 1;
                    }
                    break;
                }
        }
    }

    public void GetInfo(int question,bool answer)
    {
        switch(question)
        {
            case 30:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (gm.player_order == i)
                            continue;

                        int have = 0;
                        for (int j = 0; j < 3; j++)
                        {

                            if (gm.IsBlue(user_current_stuff[i, j]))
                                have++;
                        }

                        if (answer)
                        {
                            if (have == 0)
                                user_job[i] = -1;
                            else
                                user_job[i] = 1;
                        }
                        else
                        {
                            if (have == 0)
                                user_job[i] = 1;
                            else
                                user_job[i] = -1;
                        }
                    }                    
                    break;
                }
            case 31:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (gm.player_order == i)
                            continue;

                        int have = 0;
                        for (int j = 0; j < 3; j++)
                        {

                            if (gm.IsGray(user_current_stuff[i, j]))
                                have++;
                        }

                        if (answer)
                        {
                            if (have == 0)
                                user_job[i] = -1;
                            else
                                user_job[i] = 1;
                        }
                        else
                        {
                            if (have == 0)
                                user_job[i] = 1;
                            else
                                user_job[i] = -1;
                        }
                    }
                    break;
                }
            case 32:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (gm.player_order == i)
                            continue;

                        int have = 0;
                        for (int j = 0; j < 3; j++)
                        {

                            if (gm.IsGreen(user_current_stuff[i, j]))
                                have++;
                        }

                        if (answer)
                        {
                            if (have == 0)
                                user_job[i] = -1;
                            else
                                user_job[i] = 1;
                        }
                        else
                        {
                            if (have == 0)
                                user_job[i] = 1;
                            else
                                user_job[i] = -1;
                        }
                    }
                    break;
                }
            case 33:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (gm.player_order == i)
                            continue;

                        int have = 0;
                        for (int j = 0; j < 3; j++)
                        {

                            if (gm.IsPink(user_current_stuff[i, j]))
                                have++;
                        }

                        if (answer)
                        {
                            if (have == 0)
                                user_job[i] = -1;
                            else
                                user_job[i] = 1;
                        }
                        else
                        {
                            if (have == 0)
                                user_job[i] = 1;
                            else
                                user_job[i] = -1;
                        }
                    }
                    break;
                }
            case 34:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (gm.player_order == i)
                            continue;

                        int have = 0;
                        for (int j = 0; j < 3; j++)
                        {

                            if (gm.IsYellow(user_current_stuff[i, j]))
                                have++;
                        }

                        if (answer)
                        {
                            if (have == 0)
                                user_job[i] = -1;
                            else
                                user_job[i] = 1;
                        }
                        else
                        {
                            if (have == 0)
                                user_job[i] = 1;
                            else
                                user_job[i] = -1;
                        }
                    }
                    break;
                }
            case 35:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (gm.player_order == i)
                            continue;

                        int have = 0;
                        for (int j = 0; j < 3; j++)
                        {
                            if (user_current_stuff[i,j] == user_my_stuff[i])
                                have++;
                        }

                        if (answer)
                        {
                            if (have == 0)
                                user_job[i] = -1;
                            else
                                user_job[i] = 1;
                        }
                        else
                        {
                            if (have == 0)
                                user_job[i] = 1;
                            else
                                user_job[i] = -1;
                        }
                    }
                    break;
                }
            case 36:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (gm.player_order == i)
                            continue;

                        if (gm.IsLive(user_my_stuff[i]))
                        {
                            if (answer)
                                user_job[i] = 1;
                            else
                                user_job[i] = -1;
                        }
                        else
                        {
                            if (answer)
                                user_job[i] = -1;
                            else
                                user_job[i] = 1;
                        }
                    }
                    break;
                }
            case 37:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (gm.player_order == i)
                            continue;

                        if (gm.IsBlue(user_my_stuff[i]))
                        {
                            if (answer)
                                user_job[i] = 1;
                            else
                                user_job[i] = -1;
                        }
                        else
                        {
                            if (answer)
                                user_job[i] = -1;
                            else
                                user_job[i] = 1;
                        }
                    }
                    break;
                }
            case 38:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (gm.player_order == i)
                            continue;

                        if (gm.IsGray(user_my_stuff[i]))
                        {
                            if (answer)
                                user_job[i] = 1;
                            else
                                user_job[i] = -1;
                        }
                        else
                        {
                            if (answer)
                                user_job[i] = -1;
                            else
                                user_job[i] = 1;
                        }
                    }
                    break;
                }
            case 39:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (gm.player_order == i)
                            continue;

                        if (gm.IsGreen(user_my_stuff[i]))
                        {
                            if (answer)
                                user_job[i] = 1;
                            else
                                user_job[i] = -1;
                        }
                        else
                        {
                            if (answer)
                                user_job[i] = -1;
                            else
                                user_job[i] = 1;
                        }
                    }
                    break;
                }
            case 40:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (gm.player_order == i)
                            continue;

                        if (gm.IsPink(user_my_stuff[i]))
                        {
                            if (answer)
                                user_job[i] = 1;
                            else
                                user_job[i] = -1;
                        }
                        else
                        {
                            if (answer)
                                user_job[i] = -1;
                            else
                                user_job[i] = 1;
                        }
                    }
                    break;
                }
            case 41:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (gm.player_order == i)
                            continue;

                        if (gm.IsYellow(user_my_stuff[i]))
                        {
                            if (answer)
                                user_job[i] = 1;
                            else
                                user_job[i] = -1;
                        }
                        else
                        {
                            if (answer)
                                user_job[i] = -1;
                            else
                                user_job[i] = 1;
                        }
                    }
                    break;
                }
        }
    }
}
