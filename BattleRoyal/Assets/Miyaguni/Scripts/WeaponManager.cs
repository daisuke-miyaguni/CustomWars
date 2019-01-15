using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : Photon.MonoBehaviour
{
    WeaponManager wm;
    PhotonView weaponPV;
    PlayerController playerController;
    Rigidbody rootRb;

    Text powerText;
    Text speedText;
    Text defenseText;

    [SerializeField] private GameObject cap;
    [SerializeField] private GameObject correction;
    [SerializeField] private GameObject ruler;

    private int weaponPower = 15;
    private int powerPartsCounts = 0;

    public float GetWeaponPower()
    {
        return weaponPower;
    }    
    
    public void SetWeaponPower(int partsPower)
    {
        this.weaponPower = weaponPower + partsPower;
        this.powerPartsCounts = (weaponPower/3) - 5;

        if(powerPartsCounts > 0)
        {
            cap.SetActive(true);
        }
        else
        {
            cap.SetActive(false);
        }

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

        if (speedPartsCounts > 0)
        {
            correction.SetActive(true);
        }
        else
        {
            correction.SetActive(false);
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

        if (defensePartsCounts > 0)
        {
            ruler.SetActive(true);
        }
        else
        {
            ruler.SetActive(false);
        }

        defenseText.text = "Defense: " + weaponDefense.ToString();
    }

    CustomSlot customSlot;
    GameObject[] itemCustomSlots = new GameObject[3];
    string customSlotString = "customs_slot";

    public CapsuleCollider weaponCollider;

    void Awake()
    {
        weaponPV = GetComponent<PhotonView>();
        weaponCollider = GetComponent<CapsuleCollider>();
        weaponCollider.enabled = false;
        playerController = gameObject.transform.root.GetComponent<PlayerController>();
        rootRb = gameObject.transform.root.GetComponent<Rigidbody>();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Parry"
        && other.gameObject.transform.root != this.gameObject.transform.root
        && weaponPV.isMine)
        {
            //other.gameObject.GetComponent<SphereCollider>().enabled = false;
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            playerController.CallWasparryed();
            rootRb.AddForce(gameObject.transform.root.forward * -10f, ForceMode.Impulse);
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
