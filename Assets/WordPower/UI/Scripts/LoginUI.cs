using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUI : MonoBehaviour
{
	
	UIManager uiManager;

	void Start ()
	{
		uiManager = UIManager.instance;
	}


	public void OnFbLoginClicked ()
	{
		uiManager.loading.SetActive (true);
		SocialManager.Instance.facebookManager.OnFacebookLogin ();
		//ConnectionManager.Instance.MakeConnection ();
	}

	public void LoginDone ()
	{
		uiManager.loading.SetActive (false);
		uiManager.loginPanel.SetActive (false);
		uiManager.gameModePanel.SetActive (true);
		SocialManager.Instance.facebookManager.PlayerInfo ();
	}

	public void OnGuestLogin ()
	{
		uiManager.loginPanel.SetActive (false);
		uiManager.gameModePanel.SetActive (true);
	}

	public void SetMyID (int i)
	{
		if (i == 1) {
			ConnectionManager.Instance.myID = "1";
			ConnectionManager.Instance.friedID = "2";
		} else {
			ConnectionManager.Instance.myID = "2";
			ConnectionManager.Instance.friedID = "1";
		}
	}
}
