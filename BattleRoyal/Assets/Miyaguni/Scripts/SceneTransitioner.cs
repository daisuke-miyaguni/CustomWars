using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
	[SerializeField] string sceneName;

	public void MoveScene()
	{
		SceneManager.LoadScene(sceneName);
	}
}
