using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInviteUI : MonoBehaviour
{
	public Image frndPic;
	public Text nameTxt;
	public Text msgTxt;
	UIManager uiManager;
	GameManager gameManager;

	void OnEnable ()
	{
		uiManager = UIManager.instance;
		gameManager = GameManager.instace;
		msgTxt.text = "wants to play "+gameManager.allSubjectType[gameManager.currSubjectType] ;
	}

	public void PlayNow (bool isPlay)
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
