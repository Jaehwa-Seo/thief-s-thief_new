using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffClick : MonoBehaviour
{
    float MaxDistance = 15f;
    Camera cam;
    Vector3 MousePosition;

    bool stuff_click = false;

    Transform click;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePosition = Input.mousePosition;
            MousePosition = cam.ScreenToWorldPoint(MousePosition);


            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, MaxDistance);

            if (hit)
            {
                if (hit.transform.CompareTag("Main_stuff"))
                {
                    stuff_click = true;
                    hit.transform.localScale = new Vector3(0.8f, 0.8f);
                    click = hit.transform;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (stuff_click)
            {
                stuff_click = false;
                click.transform.localScale = new Vector3(0.7f, 0.7f);
            }
        }
    }
}
