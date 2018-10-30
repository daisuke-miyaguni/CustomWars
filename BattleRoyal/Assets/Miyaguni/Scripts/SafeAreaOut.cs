using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaOut : MonoBehaviour
{
	[SerializeField] string areaTagName;
	
	void OnColliderStay(Collider other)
	{
		if(other == null)
		{
			Destroy(gameObject);
		}
	}
}