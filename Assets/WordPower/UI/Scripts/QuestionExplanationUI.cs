using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionExplanationUI : MonoBehaviour {
	public Text qNoTxt;
	public Text quesTxt;
	public Text[] opt;

	public Text explation;
	public int currAns;
	public int urAns;

	public Image[] optBgImg;
	public Toggle[] optCheckBox;

	public void SetDataInUI(QuestionExplanation pQuesData)
	{
		Reset ();
		qNoTxt.text = pQuesData.qNo;
		quesTxt.text = pQuesData.ques;
		opt[0].text = pQuesData.opt1;
		opt[1].text = pQuesData.opt2;
		opt[2].text = pQuesData.opt3;
		opt[3].text = pQuesData.opt4;
		explation.text = pQuesData.explation;
		currAns = pQuesData.currAns;
		urAns = pQuesData.urAns;
		if (currAns == urAns) {
			optBgImg [currAns - 1].color = Color.green;
			optCheckBox [currAns - 1].isOn = true;
		} else {
			optBgImg [currAns - 1].color = Color.green;
			optCheckBox [currAns - 1].isOn = true;
			if (urAns != 5) {
				optBgImg [urAns - 1].color = Color.red;
				optCheckBox [urAns - 1].isOn = true;

			}
		}
			
	}
	void Reset()
	{
		qNoTxt.text = "";
		quesTxt.text = "";
		explation.text = "";
		for (int i = 0; i < 4; i++) {
			optBgImg [i].color = Color.white;
			optCheckBox [i].isOn = false;
			opt[i].text = "";

		}
	}
}
