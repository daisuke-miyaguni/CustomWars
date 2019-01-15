using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    MiyaguniButton miyaguniButton;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10000.0f, Color.red);
    }
    public void ButtonClick()
    {
        Debug.Log("click button");
    }

    public void ButtonDown()
    {
        Debug.Log("down button");
    }
}
