using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private ItemDataBase itemDataBase;

    public GameObject inv;

    public GameObject pocket;

    private CreateSlotScript createSlotScript;
	
	// Update is called once per frame
	void Update ()                                              //Eキーでインベントリ開閉
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Inventory();
        }
	}

    void Inventory()
    {
        inv.SetActive(!inv.activeSelf);
        pocket.SetActive(!pocket.activeSelf);
    }

    public void OnClick()                                       //インベントリをタッチで閉じるための処理
    {
        inv.SetActive(!inv.activeSelf);
        pocket.SetActive(!pocket.activeSelf);
    }
}
