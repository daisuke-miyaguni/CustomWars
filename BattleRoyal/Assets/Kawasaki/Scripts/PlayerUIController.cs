using System.Collections;
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
    // [SerializeField] public Button itemButton;
    [SerializeField] public Button inventoryButton;
    // [SerializeField] public Button avoidButton;
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

    public void SetPlayerController(PlayerController player)
    {
        this.playerController = player.gameObject.GetComponent<PlayerController>();
        this.myItemStatus = player.gameObject.gameObject.GetComponent<MyItemStatus>();
        SetButtons();
    }

    public void SetButtons()
    {
        attackButton.GetComponent<Button>();
        attackButton.onClick.AddListener(playerController.OnClickAttack);

        jumpButton.GetComponent<Button>();
        jumpButton.onClick.AddListener(playerController.Jump);

        inventoryButton.GetComponent<Button>();
        inventoryButton.onClick.AddListener(this.OpenInventory);

        getButton.GetComponent<Button>();
        getButton.onClick.AddListener(myItemStatus.OnGetButton);        

        // avoidButton.GetComponent<Button>();
        // avoidButton.onClick.AddListener(playerController.Avoid);

        parryButton.GetComponent<Button>();
        parryButton.onClick.AddListener(playerController.ParryClick);

        openButton.GetComponent<Button>();
        openButton.onClick.AddListener(playerController.OnClickOpenButton);
        openButton.gameObject.SetActive(false);

        for(int i = 0; i < usePocketItem.Length; i++)
        {
            usePocketItem[i].GetComponent<UseItem>().SetMyPlayer(playerController);
        }

        for(int i = 0; i < customSlot.Length; i++)
        {
            customSlot[i].GetComponent<CustomSlot>().InitMyItemStatus(playerController);
        }

        for (int i = 0; i < pocketItem.Length; i++)
        {
            pocketItem[i].GetComponent<PocketItem>().InitMyItemStatus(playerController);
        }

        DragDelete dd = deletePanel.GetComponent<DragDelete>();
        dd.SetMyPlayer(playerController.gameObject);

        createSlot.SetMyItemStatus(playerController.GetMyItemStatus());

        inventory.SetActive(false);
    }

    // カバンを開く
    public void OpenInventory()
    {
        if (!inventory.activeSelf)
        {
            inventory.SetActive(true);
        }
    }
}
