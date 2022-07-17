using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerEvent : MonoBehaviour
{
    public int hitflower = 0;
    int total = 3;

    int currentflower = 0;


    GameObject Flowers;

    float Timer;

    // Start is called before the first frame update
    void Start()
    {
        Flowers = GameObject.Find("Flowers");
        Timer = 180;
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;

        if(Timer<0)
        {
            Timer = 180;

            if(total > currentflower)
            {
                currentflower++;
                int randomi = Random.Range(0, 5);
                Flowers.transform.GetChild(randomi).gameObject.SetActive(true);
                Sprite spr = (Sprite)Resources.Load("UI/win_bg"+Random.Range(1,7), typeof(Sprite));
                Flowers.transform.GetChild(randomi).transform.GetComponent<SpriteRenderer>().sprite = spr;
            }
            
        }
    }

    public void Hitflower()
    {
        hitflower++;
    }
}
