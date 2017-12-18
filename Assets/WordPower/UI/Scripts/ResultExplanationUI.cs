using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultExplanationUI : MonoBehaviour {
	public GameObject questionPrefab;
	public Transform content;
	List <AntoQuestion> questionList;
	List<GameObject> allQuestGo = new List<GameObject>();
	UIManager uiManager;
	void OnEnable()
	{
		uiManager = UIManager.instance;
	}

	public void Explaion(List<int> pMyAns)
	{
		gameObject.SetActive (true);
		QuestionExplanation currQues = new QuestionExplanation ();
		questionList = AntoNsynoTestPaper.instace.GetQuestionFromDB ();
		for (int i = 0; i < questionList.Count; i++) {
			GameObject go = Instantiate(questionPrefab) as GameObject;
			go.transform.SetParent(content);
			go.SetActive (true);
			go.transform.localScale = Vector3.one;
			go.transform.localPosition= Vector3.zero;
			allQuestGo.Add (go);
			QuestionExplanationUI qUI = go.GetComponent<QuestionExplanationUI> ();
			currQues.qNo = (i + 1) + "";
			currQues.ques = questionList [i].Question;
			currQues.opt1 = questionList [i].O_1;
			currQues.opt2 = questionList [i].O_2;
			currQues.opt3 = questionList [i].O_3;
			currQues.opt4 = questionList [i].O_4;
			currQues.explation = questionList [i].Explain;
			currQues.currAns = System.Convert.ToInt32( questionList [i].A);
			if (i < pMyAns.Count) {
				currQues.urAns = pMyAns [i];
			} else {
				currQues.urAns = 5;
			}
			qUI.SetDataInUI (currQues);
		}

	}
	public void OnBackBtn()
	{
		gameObject.SetActive (false);
		foreach (GameObject item in allQuestGo) {
			Destroy (item);
		}
	}
}

public class QuestionExplanation
{
	public string qNo;
	public string ques;
	public string opt1;
	public string opt2;
	public string opt3;
	public string opt4;
	public string explation;
	public int currAns;
	public int urAns;
}
