using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{

    private ItemData itemData;

    private PocketStatus pocketStatus;

    private Player player;

    private GameObject Inve;

    private Image panelImage;

    private bool itemCheck;

    public int pocketNum;

    //private PocketStatus.Pocket pocket;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        pocketStatus = FindObjectOfType<PocketStatus>();
        panelImage = transform.GetChild(0).GetComponent<Image>();
        itemCheck = false;
    }

    public void SetItemSwitch(ItemData itemData)                    //pocketpanelにドラッグされたアイテムデータを取得する
    {
        this.itemData = itemData;
        itemCheck = true;
        //Debug.Log(itemCheck);
    }

    public void OnPush()                                            //アイテム使用処理
    {
        if(!itemCheck)
        {
            return;
        } 

        switch (itemData.GetItemSet())                              
        {
            case PocketStatus.Pocket.ball_p:

                Debug.Log("もぉんスタァボォる");

                panelImage.sprite = null;

                itemData = null;

                itemCheck = false;

                break;

            case PocketStatus.Pocket.mon_p:

                Debug.Log("めたももおんものお");

                panelImage.sprite = null;

                itemData = null;

                itemCheck = false;

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
        pocketStatus.usePocketItem(pocketNum);      //アイテムを使用したらPocketStatusにアイテムを使用したパネル情報を飛ばす。      
    }

    public void PanelDelete()
    {
        panelImage.sprite = null;

        itemData = null;

        itemCheck = false;
    }
}
