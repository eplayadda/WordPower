using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;
	public GameObject loading;
	public GameObject loginPanel;
	public GameObject gameModePanel;
	public GameObject antoSynoPanel;
	public GameObject wordDetailsPanel;
	public GameObject lobbyPanel;
	public GameObject roomPanel;
	public GameObject resultPanel;
	public GameObject storePanel;
	public GameObject gameInvitePanel;
	public GameObject friendsListPanel;
	public GameObject explationPanel;
	public Image playerProfilePic;

	private Sprite profilePic;

	void Awake ()
	{
		if (instance == null)
			instance = this;
	}

	public void OnSignalRConnected ()
	{
		loginPanel.GetComponent<LoginUI> ().LoginDone ();
	}

	public void OnSendRequest (int pTablePrice)
	{
		Debug.Log ("Invte Panel");
		GameManager.instace.tablePrice = pTablePrice;
		gameInvitePanel.SetActive (true);
	}

	public void OnChallangeAccepted ()
	{
		roomPanel.GetComponent<RoomUI> ().MakeInteractable ();
	}

	public void OnGameStartOnServer ()
	{
		roomPanel.GetComponent<RoomUI> ().StartTest (false);
	}

	public void FriendAnswer (string pdata)
	{
		roomPanel.GetComponent<RoomUI> ().FriendAnwer (pdata);
	}

	public void FriendGameOver ()
	{
		roomPanel.GetComponent<RoomUI> ().OnGameOver ();
	}

	public void DesableAllPanels ()
	{
		loginPanel.SetActive (false);
		gameModePanel.SetActive (false);
		antoSynoPanel.SetActive (false);
		wordDetailsPanel.SetActive (false);
		lobbyPanel.SetActive (false);
		roomPanel.SetActive (false);
		resultPanel.SetActive (false);
		storePanel.SetActive (false);
		gameInvitePanel.SetActive (false);
	}

	public void UpdateFrindProfilePic (Sprite frindPic)
	{
		lobbyPanel.GetComponent<LobbyPanelUI> ().UpdateProfilePic (frindPic);

	}

	public void UpdateProfilePic (string playeUrl)
	{
		StartCoroutine (DownloadImage (playeUrl));
	}

	IEnumerator DownloadImage (string url)
	{
		WWW www = new WWW (url);
		yield return www;
		Texture2D tex = www.texture;
		profilePic = Sprite.Create (tex, new Rect (0, 0, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
		playerProfilePic.sprite = profilePic;
		Debug.Log ("Done");
	}

	public Sprite GetUserProfilePic ()
	{
		return profilePic;
	}


	public void OnCloseFrindsList ()
	{
		friendsListPanel.SetActive (false);
	}
}
