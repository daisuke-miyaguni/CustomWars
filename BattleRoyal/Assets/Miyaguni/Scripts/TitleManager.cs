using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayBGM ("bgm_maoudamashii_neorock79");
    }

    public void MoveLobby()
	{
        SceneManager.LoadScene(1);
	}
}
