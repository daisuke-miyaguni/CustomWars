using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField] private CreateSlotScript createSlot;

    [SerializeField] public Button attackButton;
    [SerializeField] public Button jumpButton;
    [SerializeField] public Button itemButton;
    [SerializeField] public Button inventoryButton;
    // [SerializeField] public Button avoidButton;
    [SerializeField] public Button parryButton;
    [SerializeField] public GameObject inventory;

    public void SetPlayerController(PlayerController player)
    {
        this.playerController = player.GetComponent<PlayerController>();
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

        // avoidButton.GetComponent<Button>();
        // avoidButton.onClick.AddListener(playerController.Avoid);

        parryButton.GetComponent<Button>();
        parryButton.onClick.AddListener(playerController.ParryClick);

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
        else
        {
            inventory.SetActive(false);
        }

    }
}
