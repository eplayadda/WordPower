using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordDetailsUI : MonoBehaviour {
	public Transform parent; 
	public GameObject wordOb; 
	public RectTransform viewPort;
	UIManager uiManager;
	void Start()
	{
		uiManager = UIManager.instance;
	}
	public void CreateWords(List<WordModel> pAllWords)
	{
		//instace
		//make chield
		viewPort.sizeDelta = new Vector2(800,500*pAllWords.Count);
		foreach (WordModel item in pAllWords) {
			GameObject go = Instantiate(wordOb) as GameObject;
			go.transform.SetParent (parent);
			go.GetComponent<RectTransform> ().localScale = Vector3.one;
			go.GetComponent<WordUI> ().SetDataInUI (item);
			go.SetActive (true);
		}
	}

	public void OnBackSelected()
	{
		uiManager.wordDetailsPanel.SetActive (false);
		uiManager.antoSynoPanel.SetActive (true);

	}

}
