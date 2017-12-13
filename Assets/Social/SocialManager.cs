using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using System;

public class SocialManager : MonoBehaviour
{
	private static SocialManager mInstance;

	public static SocialManager Instance {
		get {
			if (mInstance == null)
				mInstance = FindObjectOfType<SocialManager> ();
			return mInstance;
		}
		set {
			mInstance = value;
		}
	}

	public FacebookHandler facebookManager;

	// Use this for initialization
	void Start ()
	{
		
	}



	public void OnClickFacebookIcon ()
	{
		facebookManager.OnFacebookLogin ();
	}

	public void OnClickFacebookShare ()
	{
		facebookManager.OnFacebookShare ();
	}

	public void RateUs ()
	{
		PlayerPrefs.SetInt ("RateUs", 1);
		Debug.Log ("RetUs");
		//GameManager.instance.ShowRateUsPanel (false);
		Application.OpenURL ("market://details?id=com.eplayadda.mindssmash");
	}

}
