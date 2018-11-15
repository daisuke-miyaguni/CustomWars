using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parry : MonoBehaviour
{
	private PlayerController playerController;
	
	private SphereCollider sphereCollider;

	[SerializeField] private float parryTime = 0.3f;

	private Button parryButton;

	private PhotonView photonView;

	void Awake()
	{
        photonView = gameObject.transform.root.GetComponent<PhotonView>();
    }

	void Start()
	{
		if(photonView.isMine)
		{
			playerController = gameObject.transform.root.GetComponent<PlayerController>();
			sphereCollider = gameObject.transform.GetComponent<SphereCollider>();
        	sphereCollider.enabled = false;
        	parryButton = GameObject.Find("ParryButton").GetComponent<Button>();
			parryButton.onClick.AddListener(this.ParryClick);
		}
	}

	public void ParryClick()
	{
		if(photonView.isMine)
		{
			StartCoroutine(Parrying());
		}
	}

	IEnumerator Parrying()
	{
		sphereCollider.enabled = true;
		yield return new WaitForSeconds(parryTime);
		sphereCollider.enabled = false;
	}

	public void SuccessPatty()
	{
		playerController.ParrySuccess();
	}
}
