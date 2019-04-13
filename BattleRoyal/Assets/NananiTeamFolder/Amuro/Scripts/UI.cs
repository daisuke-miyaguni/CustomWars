using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private ItemDataBase itemDataBase;

    public GameObject Inv;

    public GameObject Pocket;

    private CreateSlotScript createSlotScript;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Inventory();
        }
    }

    void Inventory()
    {
        Inv.SetActive(!Inv.activeSelf);
        Pocket.SetActive(!Pocket.activeSelf);
    }

    public void OnClick()
    {
        Inv.SetActive(!Inv.activeSelf);
        Pocket.SetActive(!Pocket.activeSelf);
    }

}
