﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    private PlayerController playerController;
    private MyItemStatus myItemStatus;

    [SerializeField] private CreateSlotScript createSlot;

    [SerializeField] public Button attackButton;
    [SerializeField] public Button jumpButton;
    [SerializeField] public Button inventoryButton;
    [SerializeField] public Button parryButton;

    [SerializeField] public Button getButton;

    [SerializeField] public Button openButton;

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
        attackButton.gameObject.GetComponent<Button>();
        attackButton.onClick.AddListener(playerController.OnClickAttack);

        // ジャンプボタンにジャンプ処理をもたせる
        jumpButton.gameObject.GetComponent<Button>();
        jumpButton.onClick.AddListener(playerController.Jump);

        // インベントリーボタンにインベントリーを開く処理をもたせる
        inventoryButton.GetComponent<Button>();
        inventoryButton.onClick.AddListener(this.OpenInventory);

        // ゲットボタンにアイテムを取得する処理をもたせる
        getButton.gameObject.GetComponent<Button>();
        getButton.onClick.AddListener(myItemStatus.OnGetButton);

        // パリィボタンにパリィを取得する処理をもたせる
        parryButton.gameObject.GetComponent<Button>();
        parryButton.onClick.AddListener(playerController.ParryClick);

        // オープンボタンに宝箱を開く処理をもたせる
        openButton.gameObject.GetComponent<Button>();
        openButton.onClick.AddListener(playerController.OnClickOpenButton);
        // オープンボタンを非表示にする
        openButton.gameObject.SetActive(false);

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
