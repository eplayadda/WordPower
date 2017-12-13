using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInviteUI : MonoBehaviour {
	UIManager uiManager;
	GameManager gameManager;
	void Start()
	{
		uiManager = UIManager.instance;
		gameManager = GameManager.instace;
	}
	public void PlayNow(bool isPlay)
	{
		gameObject.SetActive (false);
		if (isPlay) {
			if (gameManager.availableCoin > gameManager.tablePrice) {
				uiManager.DesableAllPanels ();
				uiManager.roomPanel.SetActive (true);
				uiManager.roomPanel.GetComponent<RoomUI> ().CreateRoomBySerevr ();
				ConnectionManager.Instance.IacceptChallage ();
			}
		} else {
			uiManager.storePanel.SetActive (true);
		}
	}

}
