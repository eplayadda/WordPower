using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanelUI : MonoBehaviour
{
	public Text tableCoinTxt;
	public Text totalCoinTxt;
	public Text testTypeTxt;
	public Image friendProfilePic;
	int matchValue;
	int totalCoin = 700;
	int minTableVal = 100;
	UIManager uiManager;
	GameManager gameManager;

	void OnEnable ()
	{
		gameManager = GameManager.instace;
		uiManager = UIManager.instance;
		testTypeTxt.text = "Play "+gameManager.allSubjectType[gameManager.currSubjectType]+" with Friends";
		totalCoin = gameManager.availableCoin;
		OnTablePriceClicked (true);
	}

	public void OnBackSelected ()
	{
		uiManager.lobbyPanel.SetActive (false);
		uiManager.subjectSelectionForTestPanel.SetActive (true);

	}

	public void OnStorePageClicked ()
	{
		uiManager.storePanel.SetActive (true);
	}


	public void OnVideoAddClicked ()
	{

	}

	public void OnCreateRoom ()
	{
		uiManager.lobbyPanel.SetActive (false);
		uiManager.roomPanel.SetActive (true);
		gameManager.tablePrice = matchValue;
		uiManager.roomPanel.GetComponent<RoomUI> ().CreateRoom (matchValue);
	}

	public void OnTablePriceClicked (bool isIncress)
	{
		if (isIncress) {
			matchValue += minTableVal;
			totalCoin -= minTableVal;
		} else {
			matchValue -= minTableVal;
			totalCoin += minTableVal;
		}

		if (!CheckCoinLimit ()) {
			if (isIncress) {
				matchValue -= minTableVal;
				totalCoin += minTableVal;

			} else {
				matchValue += minTableVal;
				totalCoin -= minTableVal;
			}
		} else {
			totalCoinTxt.text = totalCoin.ToString ();
			tableCoinTxt.text = matchValue.ToString ();
		}
	}

	bool CheckCoinLimit ()
	{
		if (totalCoin >= 0 && matchValue >= minTableVal) {
			return true;
		} else {
			return false;
		}
			
	}

	public void OnClickInviteFriend ()
	{
		uiManager.friendsListPanel.SetActive (true);
        ConnectionManager.Instance.GetOnlineFriend();
		//SocialManager.Instance.facebookManager.GetFriends ();
	}

	public void UpdateProfilePic (Sprite frndProfile)
	{
		friendProfilePic.sprite = frndProfile;
	}


}
