using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private ItemDataBase itemDataBase;

    public GameObject basePanel;

    public GameObject Pocket;

    private CreateSlotScript createSlotScript;


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
        Pocket.SetActive(!Pocket.activeSelf);
    }

    public void OnClick()
    {
<<<<<<< HEAD
=======

>>>>>>> fce8e1650e5b1c2a9406be04f2e403d5cfaa683f
        Pocket.SetActive(!Pocket.activeSelf);
        basePanel.SetActive(!basePanel.activeSelf);
    }

}
