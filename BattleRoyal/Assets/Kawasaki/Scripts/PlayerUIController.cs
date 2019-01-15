using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerUIController : MonoBehaviour
{
    private PlayerController playerController;
    private MyItemStatus myItemStatus;

    [SerializeField] private CreateSlotScript createSlot;

    [SerializeField] public MiyaguniButton attackMiyaguniButton;
    [SerializeField] public MiyaguniButton jumpMiyaguniButton;
    [SerializeField] public MiyaguniButton inventoryMiyaguniButton;
    [SerializeField] public MiyaguniButton parryMiyaguniButton;

    [SerializeField] public MiyaguniButton getMiyaguniButton;

    [SerializeField] public MiyaguniButton openMiyaguniButton;

    [SerializeField] Slider hpSlider;

    public Slider GetHPSlider()
    {
        return hpSlider;
    }

    [SerializeField] public GameObject[] usePocketItem;

    [SerializeField] public GameObject inventory;

    [SerializeField] public GameObject[] customSlot;
    [SerializeField] public GameObject[] pocketItem;

    [SerializeField] GameObject deletePanel;

    // プレイヤーとアイテム状態を定義する
    public void SetPlayerController(PlayerController player)
    {
        this.playerController = player.gameObject.GetComponent<PlayerController>();
        this.myItemStatus = player.gameObject.gameObject.GetComponent<MyItemStatus>();
        SetButtons();
    }

    // アイテム状態ゲッター
    public MyItemStatus GetMyItemStatus()
    {
        return myItemStatus;
    }

    public void SetButtons()
    {

        // 攻撃ボタンに攻撃処理をもたせる
        attackMiyaguniButton.gameObject.GetComponent<MiyaguniButton>();
        attackMiyaguniButton.onDown.AddListener(playerController.OnClickAttack);

        // ジャンプボタンにジャンプ処理をもたせる
        jumpMiyaguniButton.gameObject.GetComponent<MiyaguniButton>();
        jumpMiyaguniButton.onDown.AddListener(playerController.Jump);

        // インベントリーボタンにインベントリーを開く処理をもたせる
        inventoryMiyaguniButton.GetComponent<MiyaguniButton>();
        inventoryMiyaguniButton.onDown.AddListener(this.OpenInventory);

        // ゲットボタンにアイテムを取得する処理をもたせる
        getMiyaguniButton.gameObject.GetComponent<MiyaguniButton>();
        getMiyaguniButton.onDown.AddListener(myItemStatus.OnGetButton);

        // パリィボタンにパリィを取得する処理をもたせる
        parryMiyaguniButton.gameObject.GetComponent<MiyaguniButton>();
        parryMiyaguniButton.onDown.AddListener(playerController.ParryClick);

        // オープンボタンに宝箱を開く処理をもたせる
        openMiyaguniButton.gameObject.GetComponent<MiyaguniButton>();
        openMiyaguniButton.onDown.AddListener(playerController.OnClickOpenButton);

        // オープンボタンを非表示にする
        openMiyaguniButton.gameObject.SetActive(false);

        // アイテム使用ボタンにプレイヤーの情報を渡す
        for(int i = 0; i < usePocketItem.Length; i++)
        {
            usePocketItem[i].GetComponent<UseItem>().SetMyPlayer(playerController);
        }

        // カスタムスロットにプレイヤーの情報を渡す
        for(int i = 0; i < customSlot.Length; i++)
        {
            customSlot[i].GetComponent<CustomSlot>().InitMyItemStatus(playerController);
        }

        // ポケットスロットにプレイヤーの情報を渡す
        for (int i = 0; i < pocketItem.Length; i++)
        {
            pocketItem[i].GetComponent<PocketItem>().InitMyItemStatus(playerController);
        }

        // ドロップボタンにプレイヤーの情報を渡す
        DragDelete dd = deletePanel.GetComponent<DragDelete>();
        dd.SetMyPlayer(playerController.gameObject);

        // スロット作成にプレイヤーのアイテム状態の情報を渡す
        createSlot.SetMyItemStatus(playerController.GetMyItemStatus());

        // インベントリーを非表示にする
        inventory.SetActive(false);
    }

    // インベントリを開く
    public void OpenInventory()
    {
        if (!inventory.activeSelf)
        {
            inventory.SetActive(true);
        }
    }
}
