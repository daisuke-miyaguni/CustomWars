using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonAnimation : MonoBehaviour {
[SerializeField] float interval = 0.5f;
Image imageComponent;
	void Start ()
	{
		imageComponent = GetComponent<Image>();
		StartCoroutine("Flashing");
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(1))
		{
			StopCoroutine("Flashing");
			StartCoroutine("GoToLobby");
		}
	}

	IEnumerator Flashing ()
	{
		while(true)
		{
			imageComponent.enabled = !imageComponent.enabled;
			yield return new WaitForSeconds(interval);
		}
	}

	IEnumerator GoToLobby()
	{
		while(true)
		{
			imageComponent.enabled = !imageComponent.enabled;
			yield return new WaitForSeconds(interval / 4);
		}
	}

}
