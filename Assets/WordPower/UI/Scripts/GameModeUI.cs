using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeUI : MonoBehaviour {
	
	UIManager uiManager;
	void Start()
	{
		uiManager = UIManager.instance;
	}

	public void OnReadBtnSelected()
	{
		uiManager.antoSynoPanel.SetActive (true);
		uiManager.gameModePanel.SetActive (false);
	}

	public void OnPlayBtnSelected()
	{
	//	uiManager.gameModePanel.SetActive (false);
	//	uiManager.lobbyPanel.SetActive (true);
		uiManager.gameModePanel.SetActive (false);
		uiManager.subjectSelectionForTestPanel.SetActive (true);
	}



}
