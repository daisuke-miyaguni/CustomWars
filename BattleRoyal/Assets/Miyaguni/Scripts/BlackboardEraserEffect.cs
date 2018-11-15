using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardEraserEffect : MonoBehaviour
{
	[SerializeField] GameObject eraserEffect;

	void OnTriggerEnter(Collider other)
	{
		eraserEffect.SetActive(true);
	}
}