using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntoSynoUI : MonoBehaviour {
	UIManager uiManager;
	void Start()
	{
		uiManager = UIManager.instance;
	}

	public void OnAlbabateSelected(int a)
	{
		uiManager.wordDetailsPanel.SetActive (true);
		uiManager.antoSynoPanel.SetActive (false);
		AntoNsynoDataBase.instace.GetDataFromDB ();
	}

	public void OnBackSelected()
	{
		uiManager.antoSynoPanel.SetActive (false);
		uiManager.gameModePanel.SetActive (true);

	}
}
