using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : Photon.MonoBehaviour
{
    WeaponManager wm;
    private int weaponPower = 10;
    Text powerText;

    CustomSlot customSlot;
    [SerializeField] GameObject[] itemCustomSlots = new GameObject[3];
    string customSlotString = "customs_slot";

    PhotonView weaponPV;


    public int GetWeaponPower()
    {
        return weaponPower;
    }

    public void SetWeaponPower(int partsPower)
    {
        this.weaponPower = weaponPower + partsPower;
        powerText.text = "Power: " + weaponPower.ToString();
    }

    public void AttachParts(int partsPower)
    {
        weaponPV.RPC("AttachmentParts", PhotonTargets.AllViaServer, partsPower);
    }

    [PunRPC]
    void AttachmentParts(int wp)
    {
        SetWeaponPower(wp);
    }

    void Awake()
    {
        weaponPV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (weaponPV.isMine)
        {
            wm = GetComponent<WeaponManager>();
            for (int i = 0; i < itemCustomSlots.Length; i++)
            {
                itemCustomSlots[i] = GameObject.Find("PlayerControllerUI").gameObject.transform.Find("Inventory").gameObject.transform.Find("base_panel").gameObject.transform.Find("custom_panel").gameObject.transform.Find(customSlotString + (i + 1).ToString()).gameObject;
                // itemCustomSlots[i] = GameObject.Find(customSlotString + (i + 1).ToString());
                customSlot = itemCustomSlots[i].GetComponent<CustomSlot>();
                customSlot.SetWeaponManager(wm);
            }

            powerText = GameObject.Find("WeaponPower").GetComponent<Text>();
            powerText.text = "Power: " + weaponPower.ToString();
        }

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(weaponPower);
        }
        else
        {
            weaponPower = (int)stream.ReceiveNext();
        }
    }
}
