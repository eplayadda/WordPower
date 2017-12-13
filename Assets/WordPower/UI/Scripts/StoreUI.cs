using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI : MonoBehaviour {
	UIManager uiManager;
	void Start()
	{
		uiManager = UIManager.instance;
	}

	public void OnClosed()
	{
		uiManager.storePanel.SetActive (false);
	}

}
