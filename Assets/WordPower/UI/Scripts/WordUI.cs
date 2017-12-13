using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WordUI : MonoBehaviour {
	public Text questionNoTxt;
	public Text questionInEngTxt;
	public Text questionInHinTxt;
	public Text synoTxt;
	public Text antoTxt;

	public void SetDataInUI(WordModel wordData)
	{
		questionNoTxt.text = wordData.questionNo;
		questionInEngTxt.text = wordData.questionInEng;
		questionInEngTxt.text += "  ( "+wordData.questionInHin+" )";
		synoTxt.text = wordData.syno;
		antoTxt.text = wordData.anto;
	}

	public void OnSoundClicked()
	{
		
	}
}
