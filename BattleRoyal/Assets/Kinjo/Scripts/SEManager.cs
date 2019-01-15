using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour {

	void PlayAttackSE(int atkNum)
	{
		switch(atkNum)
		{
			case 0:
			AudioManager.Instance.PlaySE("sword-gesture1");
			return;

			case 1:
			AudioManager.Instance.PlaySE("sword-gesture1");
			return;

			case 2:
			AudioManager.Instance.PlaySE("sword-gesture3");
			return;
		}
	}
}
