using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{

    private ItemData itemData;
    private PocketStatus pocketStatus;
    private Player player;
    private Image panelImage;
    private bool itemCheck;

    //private PocketStatus.Pocket pocket;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        panelImage = transform.GetChild(0).GetComponent<Image>();
        itemCheck = false;
    }

    public void SetItemSwitch(ItemData itemData)
    {
        this.itemData = itemData;
        itemCheck = true;
        Debug.Log(itemCheck);
    }

    public void OnPush()
    {
        /* string imageName = panelImage.sprite.name; */

        if(!itemCheck)
        {
            return;
        } 

        switch (itemData.GetItemSet())
        {
            case PocketStatus.Pocket.ball_p:

                Debug.Log("もぉんスタァボォる");

                break;

            case PocketStatus.Pocket.mon_p:

                Debug.Log("めたももおんものお");

                break;

            case PocketStatus.Pocket.riyo_p:

                var recover = itemData.GetItemPower();

                player.Recovery(recover);

                panelImage.sprite = null;

                itemData = null;

                itemCheck = false;

                //PlayerController.CallRecover(recover);
                



                break;

            default:

                Debug.Log("なし");

                break;
        }            
    }
}
