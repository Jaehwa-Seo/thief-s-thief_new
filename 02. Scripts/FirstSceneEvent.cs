using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneEvent : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //애니메이션이 끝난뒤 꽃과 가이드 글 표시
            if(Input.GetMouseButtonDown(0))
            {
            SceneManager.LoadScene("main scene");
        }
    }
}
