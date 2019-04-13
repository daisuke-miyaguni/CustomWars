using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] float interval = 0.5f;
    Image imageComponent;
	
    void Start ()
	{
		imageComponent = GetComponent<Image>();
		StartCoroutine("Flashing");
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			StopCoroutine(Flashing());
			StartCoroutine(GoToLobby());
			//SceneManager.LoadScene(1);
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
		for(int i = 0; i < 8; i ++)
		{
			imageComponent.enabled = !imageComponent.enabled;
			yield return new WaitForSeconds(interval / 4);
		}

        GameObject.Find("TitleManager").GetComponent<TitleManager>().MoveLobby();
	}
}
