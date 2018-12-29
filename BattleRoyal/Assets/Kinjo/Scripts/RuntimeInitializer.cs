using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeInitializer : MonoBehaviour {

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void InitializeBeforeSceneLoad()
	{
		var manager = GameObject.Instantiate(Resources.Load("AudioManager"));
		GameObject.DontDestroyOnLoad( manager );
	}
	// Update is called once per frame
	void Update () {
		
	}
}
