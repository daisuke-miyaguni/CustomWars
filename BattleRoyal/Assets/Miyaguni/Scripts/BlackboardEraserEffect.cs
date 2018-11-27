﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardEraserEffect : MonoBehaviour
{
	[SerializeField] GameObject eraserEffect;

	PhotonView bbePV;

	void Start()
	{
		bbePV = GetComponent<PhotonView>();
	}

	void OnTriggerEnter(Collider other)
	{
		bbePV.RPC("EffectStart", PhotonTargets.AllViaServer);
	}

	[PunRPC]
	void EffectStart()
	{
        eraserEffect.SetActive(true);
    }

	
}