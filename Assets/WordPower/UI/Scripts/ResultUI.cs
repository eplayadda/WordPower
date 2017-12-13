using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour {
	UIManager uiManager;
	GameManager gameManager;
	void Start()
	{
		gameManager = GameManager.instace;
		uiManager = UIManager.instance;
	}
	public Text meNumber;
	public Text friendNumber;
	public Text msgTxt;
	public void SetReportCart(int pMyNum ,int pFriendNum)
	{
		meNumber.text = pMyNum + "";
		friendNumber.text = pFriendNum + "";
		if (pMyNum > pFriendNum) {
			gameManager.availableCoin += gameManager.tablePrice;
			msgTxt.text = "You Won";
		} else if (pFriendNum > pMyNum) {
			gameManager.availableCoin -= gameManager.tablePrice;
			msgTxt.text = "You Los";
		} else {
			msgTxt.text = "Draw";
		}
	}

	public void OnMenuButtonClicked()
	{
		uiManager.DesableAllPanels ();
		uiManager.gameModePanel.SetActive (true);
	}

	public void OnReplayButtonClicked()
	{
		uiManager.DesableAllPanels ();
		uiManager.lobbyPanel.SetActive (true);
	}

}
