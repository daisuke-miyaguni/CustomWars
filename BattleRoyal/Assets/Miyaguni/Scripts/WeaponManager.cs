using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private const int minWeaponPower = 10;
    private int weaponPower = minWeaponPower;

	CustomSlot customSlot;
	GameObject[] itemCustomSlots = new GameObject[3];
	string customSlotString = "customs_slot";

    public int GetWeaponPower()
    {
        return weaponPower;
    }

    public void SetWeaponPower(int partsPower)
    {
        this.weaponPower = weaponPower + partsPower;
    }

	void Start()
	{
		for(int i = 0; i < itemCustomSlots.Length; i++)
		{
			itemCustomSlots[i] = GameObject.Find(customSlotString + (i + 1).ToString());
			customSlot = itemCustomSlots[i].GetComponent<CustomSlot>();
			customSlot.SetWeaponManager(this);
		}
	}
}
