using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : Photon.MonoBehaviour
{
    WeaponManager wm;
    PhotonView weaponPV;

    Text powerText;
    Text speedText;
    Text defenseText;

    private int weaponPower = 15;

    public float GetWeaponPower()
    {
        return weaponPower;
    }    
    
    public void SetWeaponPower(int partsPower)
    {
        this.weaponPower = weaponPower + partsPower;
        powerText.text = "Power: " + weaponPower.ToString();
    }

    private float weaponSpeed = 1.0f;
    private int speedPartsCounts = 0;

    public float GetWeaponSpeed()
    {
        return weaponSpeed;
    }

    public void SetWeaponSpeed(int partsSpeed)
    {
        speedPartsCounts += partsSpeed;
        switch(speedPartsCounts)
        {
            case 1:
                weaponSpeed = 1.25f;
                break;
            case 2:
                weaponSpeed = 1.50f;
                break;
            case 3:
                weaponSpeed = 2.00f;
                break;
            default:
                weaponSpeed = 1.00f;
                break;
        }
        speedText.text = "Speed: " + weaponSpeed.ToString();
    }

    private float weaponDefense = 1.0f;
    private int defensePartsCounts = 0;

    public float GetWeaponDefense()
    {
        return weaponDefense;
    }

    public void SetWeaponDefense(int partsDefense)
    {
        defensePartsCounts += partsDefense;
        switch (defensePartsCounts)
        {
            case 1:
                weaponDefense = 0.700f;
                break;
            case 2:
                weaponDefense = 0.625f;
                break;
            case 3:
                weaponDefense = 0.500f;
                break;
            default:
                weaponDefense = 1.000f;
                break;
        }
        defenseText.text = "Defense: " + weaponDefense.ToString();
    }

    CustomSlot customSlot;
    [SerializeField] GameObject[] itemCustomSlots = new GameObject[3];
    string customSlotString = "customs_slot";

    public CapsuleCollider weaponCollider;

    void Awake()
    {
        weaponPV = GetComponent<PhotonView>();
        weaponCollider = GetComponent<CapsuleCollider>();
        weaponCollider.enabled = false;
    }

    void Start()
    {
        if (weaponPV.isMine)
        {
            for (int i = 0; i < itemCustomSlots.Length; i++)
            {
                itemCustomSlots[i] = GameObject.FindWithTag("PlayerControllerUI").gameObject.transform.Find("Inventory").gameObject.transform.Find("base_panel").gameObject.transform.Find("custom_panel").gameObject.transform.Find(customSlotString + (i + 1).ToString()).gameObject;
                // itemCustomSlots[i] = GameObject.Find(customSlotString + (i + 1).ToString());
                customSlot = itemCustomSlots[i].GetComponent<CustomSlot>();
                customSlot.SetWeaponManager(this);
            }

            powerText = GameObject.Find("WeaponPower").GetComponent<Text>();
            powerText.text = "Power: " + weaponPower.ToString();
            speedText = GameObject.Find("WeaponSpeed").GetComponent<Text>();
            speedText.text = "Speed: " + weaponSpeed.ToString();
            defenseText = GameObject.Find("WeaponDefense").GetComponent<Text>();
            defenseText.text = "Defense: " + weaponDefense.ToString();
        }

    }

    public void AttachParts(float partsPower, int ID)
    {
        weaponPV.RPC("AttachmentParts", PhotonTargets.AllViaServer, partsPower, ID);
    }

    [PunRPC]
    void AttachmentParts(float wp, int itemID)
    {
        switch (itemID)
        {
            default:
            case 0:
                SetWeaponPower(Mathf.CeilToInt(wp));
                break;
            case 1:
                SetWeaponSpeed(Mathf.RoundToInt(wp));
                break;
            case 2:
                SetWeaponDefense(Mathf.RoundToInt(wp));
                break;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    //    }
    //}

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
