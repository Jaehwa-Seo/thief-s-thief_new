using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBGMManager : MonoBehaviour
{
    public AudioSource[] bgm = new AudioSource[5];
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, 5);

        for(int i=0;i<5;i++)
        {
            if (rand == i)
                bgm[i].Play();
            else
                bgm[i].Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
