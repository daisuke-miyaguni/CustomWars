using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDelete : MonoBehaviour
{
    private ItemData myItemData;

    private GameObject slotName;

    GameObject myPlayer;

    [SerializeField] GameObject[] items;

    public void SetMyPlayer(GameObject player)
    {
        this.myPlayer = player;
    }

    /* private GameObject panel;

    CreateSlotScript create;

    public void OnEnable()
    {
        panel = GameObject.Find("item_panel");

        create = panel.GetComponent<CreateSlotScript>();
    } */


    public void DropDragItem()                                                          //捨てるアイテムのアイテムデータ取得
    {
        if (FindObjectOfType<DragSlot>() == null)
        {
            return;
        }

        var dragSlot = FindObjectOfType<DragSlot>();
        myItemData = dragSlot.GetItem();

        slotName = ProcessingSlot.itemSlot;
        // Vector3 p_pos = GameObject.Find("Sphere").transform.position;
        Vector3 p_pos = new Vector3
        (
            myPlayer.transform.position.x,
            myPlayer.transform.position.y + 1.0f,
            myPlayer.transform.position.z + 0.8f
        );

        switch (myItemData.GetItemType())
        {
            case MyItemStatus.Item.parts1:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts1] = false;

                GameObject parts1 = (GameObject)Resources.Load(items[0].name);                  //捨てたアイテムをプレイヤーポジションに生成する
                PhotonNetwork.Instantiate(parts1.name, p_pos, Quaternion.identity,0);

                Destroy(slotName);
                break;

            case MyItemStatus.Item.parts2:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts2] = false;

                GameObject parts2 = (GameObject)Resources.Load(items[1].name);
                PhotonNetwork.Instantiate(parts2.name, p_pos, Quaternion.identity,0);

                Destroy(slotName);

                break;

            case MyItemStatus.Item.parts3:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.parts3] = false;

                GameObject parts3 = (GameObject)Resources.Load(items[2].name);
                PhotonNetwork.Instantiate(parts3.name, p_pos, Quaternion.identity,0);

                Destroy(slotName);

                break;

            case MyItemStatus.Item.mon:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.mon] = false;

                GameObject mon = (GameObject)Resources.Load(items[3].name);
                PhotonNetwork.Instantiate(mon.name, p_pos, Quaternion.identity,0);

                Destroy(slotName);

                break;

            case MyItemStatus.Item.ball:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.ball] = false;

                GameObject show = (GameObject)Resources.Load("show");
                PhotonNetwork.Instantiate(show.name, p_pos, Quaternion.identity,0);

                Destroy(slotName);

                break;

            case MyItemStatus.Item.riyo:

                MyItemStatus.itemFlags[(int)MyItemStatus.Item.riyo] = false;

                GameObject riyo = (GameObject)Resources.Load("riyo");
                PhotonNetwork.Instantiate(riyo.name, p_pos, Quaternion.identity,0);

                Destroy(slotName);

                break;

            default:
                break;
        }

    }

}
