using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///<summary>
///BGMとSEを管理するシングルトン
///</summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
	private string nextBGMName;
	private string nextSEName;
	private bool isFadeOut = false;
	private float bgmFadeSpeedRate;
	private AudioSource bgmSource;
	private List<AudioSource> seSourceList;
	private const int seSourceNum = 10;

	private Dictionary<string,AudioClip>bgmDic,seDic;

	private void Awake()
	{
		if(this != Instance)
		{
			Destroy(this);
			return;
		}

		DontDestroyOnLoad(this.gameObject);

		for(int i = 0; i<seSourceNum + 1; i++)
		{
			gameObject.AddComponent<AudioSource>();
		}

		AudioSource[] audioSourceArray = GetComponents<AudioSource>();
		seSourceList = new List<AudioSource>();
		for (int i = 0; i<audioSourceArray.Length; i++)
		{
			if(i==0)
			{
				audioSourceArray[i].loop = true;
				bgmSource = audioSourceArray[i];
			}
			else
			{
				seSourceList.Add(audioSourceArray[i]); 
			}
		}

		bgmDic = new Dictionary<string, AudioClip>();
		seDic = new Dictionary<string, AudioClip>();

		object[] bgmList = Resources.LoadAll("Audio/BGM");
		object[] seList = Resources.LoadAll("Audio/SE");

		foreach(AudioClip bgm in bgmList)
		{
			bgmDic[bgm.name] = bgm;
		}
		foreach(AudioClip se in seList)
		{
			seDic[se.name] = se;
		}
	}

	///<summary>
	///指定したファイルのSEを再生
	///</summary>
	public void PlaySE(string seName)
	{
		if(!seDic.ContainsKey(seName))
		{
			Debug.Log(seName + "という名前のファイルはありませぬ");
			return;
		}

		nextSEName = seName;
		foreach(AudioSource seSource in seSourceList)
		{
			if(!seSource.isPlaying)
			{
				seSource.PlayOneShot(seDic[nextSEName] as AudioClip);
				return;
			}

		}
	}

	///<summary>
	///指定したファイルのBGMを再生
	///</summary>
	public void PlayBGM(string bgmName,float fadeSpeed = 0.9f)
	{
		if(!bgmDic.ContainsKey(bgmName))
		{
			Debug.Log(bgmName + "という名前のファイルはありません");
			return;
		}

		if (!bgmSource.isPlaying) {
			nextBGMName = "";
			bgmSource.clip = bgmDic [bgmName] as AudioClip;
			bgmSource.Play ();
		}
		//違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
		else if (bgmSource.clip.name != bgmName) {
			nextBGMName = bgmName;
			FadeOutBGM (fadeSpeed);
		}
	}

	//BGMフェードアウト
	public void FadeOutBGM(float fadeSpeed)
	{
		bgmFadeSpeedRate = fadeSpeed;
		isFadeOut = true;
	}

	private void Update()
	{
		if(!isFadeOut)
		{
			return;
		}

		bgmSource.volume -= Time.deltaTime * bgmFadeSpeedRate;
		if(bgmSource.volume <= 0)
		{
			bgmSource.Stop();
			bgmSource.volume = PlayerPrefs.GetFloat("BGM_VOLUME_KEY",1.0f);
			isFadeOut = false;

			if(!string.IsNullOrEmpty(nextBGMName))
			{
				PlayBGM(nextBGMName);
			}
		}
	}
	
}