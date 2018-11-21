using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    public GameObject basePanel;

	// Use this for initialization
	void Start ()
    {
        // basePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Inventory();
        }
	}

    void Inventory()
    {
        basePanel.SetActive(!basePanel.activeSelf);
    }

    public void OnClick()
    {
        basePanel.SetActive(!basePanel.activeSelf);
    }
}
