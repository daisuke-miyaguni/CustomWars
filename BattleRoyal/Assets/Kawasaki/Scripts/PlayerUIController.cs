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

    [SerializeField] Slider hpSlider;

    public Slider GetHPSlider()
    {
        return hpSlider;
    }

    [SerializeField] public GameObject inventory;

    [SerializeField] GameObject deletePanel;

    public void SetPlayerController(PlayerController player)
    {
        this.playerController = player.GetComponent<PlayerController>();
        this.myItemStatus = player.gameObject.GetComponent<MyItemStatus>();
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
