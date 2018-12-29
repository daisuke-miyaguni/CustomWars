using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour {
[SerializeField]private AudioSource source;
[SerializeField]AudioClip titleBGM;
[SerializeField]AudioClip mainBGM;
private string beforeScene;
	// Use this for initialization
	void Start ()
	{
		source = GetComponent<AudioSource>();
		beforeScene = SceneManager.GetActiveScene().name;
		if(beforeScene == "Main")
		{
			source.clip = mainBGM;
		} else {
			source.clip = titleBGM;
		}
		source.Play();
		SceneManager.activeSceneChanged += OnActiveSceneChanged;
	}
	
	void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
	{
		if (beforeScene == "Lobby")
		{
			print("aaa");
			source.Stop();
			source.clip = mainBGM;
			source.Play();
		} else if (beforeScene == "Main")
		{
			source.Stop();
			source.clip = titleBGM;
			source.Play();
		}

		beforeScene = nextScene.name;
	}
}
