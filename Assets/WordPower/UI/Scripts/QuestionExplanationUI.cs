using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionExplanationUI : MonoBehaviour {
	public Text qNoTxt;
	public Text quesTxt;
	public Text opt1;
	public Text opt2;
	public Text opt3;
	public Text opt4;
	public Text explation;
	public int currAns;
	public int urAns;

	public void SetDataInUI(QuestionExplanation pQuesData)
	{
		qNoTxt.text = pQuesData.qNo;
		quesTxt.text = pQuesData.ques;
		opt1.text = pQuesData.opt1;
		opt2.text = pQuesData.opt2;
		opt3.text = pQuesData.opt3;
		opt4.text = pQuesData.opt4;
		explation.text = pQuesData.explation;
		currAns = pQuesData.currAns;
		urAns = pQuesData.urAns;
	}
}
