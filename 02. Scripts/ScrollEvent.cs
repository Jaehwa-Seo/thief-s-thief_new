using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollEvent : MonoBehaviour
{

    private Text logText = null;
    private ScrollRect log_scroll_Rect = null;

    float scroll = 0;
    float Timer = 0;

    string message;

    int startadvice = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        logText = GameObject.Find("Advice_Text").GetComponent<Text>();
        log_scroll_Rect = GameObject.Find("Advice_View").GetComponent<ScrollRect>();
        startadvice = Random.Range(0, 6);
    }

    // Update is called once per frame
    void Update()
    {
        log_scroll_Rect.horizontalNormalizedPosition = scroll;
        scroll += Time.deltaTime / 20;
        Timer -= Time.deltaTime;

        if(Timer <0)
        {
            Timer = 60;
            logText.text = null;
            message = GetAdvice(startadvice);
            SetAdvice(message);
            scroll = 0;
            startadvice++;

            if (startadvice == 6)
                startadvice = 0;
        }
    }

    string GetAdvice(int num)
    {
        switch(num)
        {
            case 0:
                return "게임 화면에서 움직이는 꽃을 터치하면 꽃을 획득할 수 있답니다.";
            case 1:
                return "게임 화면에서 모은 꽃은 '나의 도감'에서 조합할 수 있어요.";
            case 2:
                return "앞머리 생물은 앞머리에 자부심을 가지고 있어요.";
            case 3:
                return "시민은 '나의 물건'을 들지 않고 게임을 시작합니다.";
            case 4:
                return "꽃 나라의 모든 물건에는 꽃이 달려있답니다.";
            case 5:
                return "거지와 도둑은 '나의 물건'을 들고 게임을 시작합니다.";
            case 6:
                return "평범한 생물은 꽃 나라의 어디서든 항상 볼 수 있답니다.";
            case 7:
                return "시민은 진실만을 말하지만 거지와 도둑, 도둑의 도둑은 거짓만을 말해요.";
            case 8:
                return "왕의 폭정으로 일부 시민들은 거지가 되었답니다.";
            case 9:
                return "교환한 물건은 체크가 생겨요.";
            case 10:
                return "도둑의 도둑은 부자예요.";
            case 11:
                return "안 좋은 음식은 정말 맛없어요.";
            case 12:
                return "꽃 나라에는 악마교라는 종교가 있어요.";

            default:
                return "";
        }
    }

    void SetAdvice(string message)
    {
        for (int i = 0; i < 97; i++)
        {
            logText.text += " ";
        }

        logText.text += message;

        for (int i = 0; i < 97; i++)
        {
            logText.text += " ";
        }
    }
}
