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

	public AudioSource AttachBGMSource,AttachSESource;

	private Dictionary<string,AudioClip>bgmDic,seDic;

	private void Awake()
	{
		if(this != Instance)
		{
			Destroy(this);
			return;
		}

		DontDestroyOnLoad(this.gameObject);

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
		AttachSESource.PlayOneShot(seDic[nextSEName] as AudioClip);
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

		if (!AttachBGMSource.isPlaying) {
			nextBGMName = "";
			AttachBGMSource.clip = bgmDic [bgmName] as AudioClip;
			AttachBGMSource.Play ();
		}
		//違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
		else if (AttachBGMSource.clip.name != bgmName) {
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

		AttachBGMSource.volume -= Time.deltaTime * bgmFadeSpeedRate;
		if(AttachBGMSource.volume <= 0)
		{
			AttachBGMSource.Stop();
			AttachBGMSource.volume = PlayerPrefs.GetFloat("BGM_VOLUME_KEY",1.0f);
			isFadeOut = false;

			if(!string.IsNullOrEmpty(nextBGMName))
			{
				PlayBGM(nextBGMName);
			}
		}
	}
	
}