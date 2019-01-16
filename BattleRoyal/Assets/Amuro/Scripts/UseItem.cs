using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{

    private ItemData itemData;

    private PocketStatus pocketStatus;

    // private Player player;

    private PlayerController myPlayer;

    private GameObject Inve;

    private Image panelImage;

    private bool itemCheck;

    public int pocketNum;

    //private PocketStatus.Pocket pocket;

    private void Start()
    {
        // player = FindObjectOfType<Player>();
        pocketStatus = gameObject.transform.root.GetComponent<PocketStatus>();
        panelImage = transform.GetChild(0).GetComponent<Image>();
        itemCheck = false;
    }

    public void SetMyPlayer(PlayerController Player)
    {
        this.myPlayer = Player;
    }

    public void SetItemSwitch(ItemData itemData)                    //pocketpanelにドラッグされたアイテムデータを取得する
    {
        this.itemData = itemData;
        itemCheck = true;
        //Debug.Log(itemCheck);
    }

    public void OnPush()                                            //アイテム使用処理
    {
        if (!itemCheck)
        {
            return;
        }

        switch (itemData.GetItemSet())
        {
            case PocketStatus.Pocket.ball_p:

                float bigRecover = itemData.GetItemPower();
                
                //HPが100以上なら使用できない
                if (myPlayer.GetPlayerHp() >= 100 )
                {
                    return;
                }

                myPlayer.CallRecover(Mathf.CeilToInt(bigRecover));

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

                float recover = itemData.GetItemPower();
                
                //HPが100以上なら使用できない
                if (myPlayer.GetPlayerHp() >= 100 )
                {
                    return;
                }

                myPlayer.CallRecover(Mathf.CeilToInt(recover));

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
