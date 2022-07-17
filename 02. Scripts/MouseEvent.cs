using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent  : MonoBehaviour
{ 

    private void OnMouseEnter()
    {
        transform.localScale += new Vector3(0.1f, 0.1f);
    }

    private void OnMouseExit()
    {
        transform.localScale -= new Vector3(0.1f, 0.1f);
    }
}
