using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    private ItemData dragItemData;


    public void OnEnable()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public void SetDragItem(ItemData dragItemData)                  //ドラッグ時のアイテム画像を取得
    {
        this.dragItemData = dragItemData;
        transform.GetComponent<Image>().sprite = dragItemData.GetItemSprite();          //ProcessingSlot, CustomSlot, CreateSlotScript
    }

    public ItemData GetItem()
    {
        return dragItemData;
    }

    public void DeleteDragItem()
    {
        dragItemData = null;
    }

	// Update is called once per frame
	void Update ()
    {
        transform.position = Input.mousePosition;               //アイテム画像をうごかす
	}
}
