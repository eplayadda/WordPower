using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInviteUI : MonoBehaviour {
	UIManager uiManager;
	void Start()
	{
		uiManager = UIManager.instance;
	}
	public void PlayNow(bool isPlay)
	{
		gameObject.SetActive (false);
		if (isPlay) {
			uiManager.DesableAllPanels ();
			uiManager.roomPanel.SetActive (true);
			uiManager.roomPanel.GetComponent<RoomUI> ().CreateRoomBySerevr ();
			ConnectionManager.Instance.IacceptChallage ();
		} else {
			
		}
	}

}
