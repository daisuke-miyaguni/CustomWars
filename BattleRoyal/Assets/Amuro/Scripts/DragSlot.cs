using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    private ItemData dragItemData;

    private GameObject slotData;

    private int deleteNum;                  //どのスロットからドロップされたかを判断するための数字　　0 = default, 1 = itemslot, 2 = customslot, itempocket

    public void OnEnable()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public void SetDragItem(ItemData dragItemData, GameObject slot, int num)                  //ドラッグ時のアイテム画像を取得
    {
        this.dragItemData = dragItemData;
        slotData = slot;
        deleteNum = num;
        transform.GetComponent<Image>().sprite = dragItemData.GetItemSprite();          //ProcessingSlot, CustomSlot, CreateSlotScript
    }

    public GameObject GetSlotData()
    {
        return slotData;
    }
    public ItemData GetItem()
    {
        return dragItemData;
    }

    public int GetDeleteNum()
    {
        return deleteNum;
    }

    public void DeleteDragItem()
    {
        dragItemData = null;
        slotData = null;
        deleteNum = 0;
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;               //アイテム画像をうごかす
    }
}
