using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
