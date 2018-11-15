using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private ItemDataBase itemDataBase;

    public GameObject Inv;

    private CreateSlotScript createSlotScript;

	// Use this for initialization
	void Start ()
    {
        //Base_P = transform.Find("base_panel").gameObject;
        Inv.SetActive(false);
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
        Inv.SetActive(!Inv.activeSelf);
       // createSlotScript.CreateSlot(itemDataBase.GetItemData());
    }
}
