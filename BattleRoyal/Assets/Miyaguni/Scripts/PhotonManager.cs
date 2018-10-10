using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : Photon.MonoBehaviour {
	PhotonView pView;
	[SerializeField]
	string usingSettingsVersion;

	void Start(){

	}

	void Connect(){
		PhotonNetwork.ConnectUsingSettings(usingSettingsVersion);
	}
}
