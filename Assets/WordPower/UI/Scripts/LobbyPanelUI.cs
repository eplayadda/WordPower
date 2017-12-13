using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanelUI : MonoBehaviour {
	public Text tableCoinTxt;
	public Text totalCoinTxt;
	int matchValue;
	int totalCoin = 700;
	int minTableVal = 100;
	UIManager uiManager;
	void Start()
	{
		uiManager = UIManager.instance;
		OnTablePriceClicked (true);
	}

	public void OnBackSelected()
	{
		uiManager.lobbyPanel.SetActive (false);
		uiManager.gameModePanel.SetActive (true);

	}

	public void OnStorePageClicked()
	{
		uiManager.storePanel.SetActive (true);
	}


	public void OnVideoAddClicked()
	{

	}

	public void OnCreateRoom()
	{
		uiManager.lobbyPanel.SetActive (false);
		uiManager.roomPanel.SetActive (true);
		uiManager.roomPanel.GetComponent<RoomUI> ().CreateRoom ();
	}

	public void OnTablePriceClicked(bool isIncress)
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

	bool CheckCoinLimit()
	{
		if (totalCoin >= 0 && matchValue >= minTableVal) {
			return true;
		} else {
			return false;
		}
			
	}
}
