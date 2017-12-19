using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectSelectionForTestUI : MonoBehaviour {
	UIManager uiManager;
	GameManager gameManager;

	void OnEnable ()
	{
		uiManager = UIManager.instance;
		gameManager = GameManager.instace;
	}

	public void OnBackSelected ()
	{
		uiManager.subjectSelectionForTestPanel.SetActive (false);
		uiManager.gameModePanel.SetActive (true);

	}

	public void OnTestSelected(int pTestType)
	{
		gameManager.currSubjectType = pTestType;
		uiManager.subjectSelectionForTestPanel.SetActive (false);
		uiManager.lobbyPanel.SetActive (true);
	}

}
