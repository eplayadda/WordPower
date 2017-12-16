using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour {
	UIManager uiManager;
	GameManager gameManager;
	List<int> myAns;
	void Start()
	{
		gameManager = GameManager.instace;
		uiManager = UIManager.instance;
	}
	public Text meNumber;
	public Text friendNumber;
	public Text msgTxt;
	public void SetReportCart(int pMyNum ,int pFriendNum,List<int> pMyAns)
	{
		myAns = pMyAns;
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

	public void OnExplationClicked()
	{
		uiManager.explationPanel.SetActive (true);
		Debug.Log (myAns.Count);
		uiManager.explationPanel.GetComponent<ResultExplanationUI> ().Explaion (myAns);
	}

}
