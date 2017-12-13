using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUI : MonoBehaviour 
{
	UIManager uiManager;
	public GameObject questionPanal; 
	public Text qNoTxt;
	public Text quesTxt;
	public Text opt1;
	public Text opt2;
	public Text opt3;
	public Text opt4;
	public Button startGameBtn;
	public GameObject startGamePanel;
	public Image myPic;
	public Image frndPic;
	public Button addFriendBtn;
	public Clock timmer;
	public Toggle[] allToggle;
	public ToggleGroup toggleGrp;
	int currQuestion;
	int currFrndQuestion;
	List <AntoQuestion> questionList;
	public int rightAns;
	public int wrongAns;
	public int unAns;
	public Text myAnsCount;
	public Text frndAnsCount;
	char lastAns;
	int frindCrrAns;
	int frindAllAns;
	void Start()
	{
		uiManager = UIManager.instance;
	}
	public void CreateRoom()
	{
		Reset ();
		questionList = AntoNsynoTestPaper.instace.GetQuestionFromDB ();
		currQuestion = 0;
		ConnectionManager.Instance.OnSendRequest ("0");
		questionPanal.SetActive (false);
		startGamePanel.SetActive (true);
		startGameBtn.interactable = false;
		startGameBtn.gameObject.SetActive (true);

	}

	public void CreateRoomBySerevr()
	{
		Reset ();
		DownFrindImage ();
		questionList = AntoNsynoTestPaper.instace.GetQuestionFromDB ();
		currQuestion = 0;
		questionPanal.SetActive (false);
		startGamePanel.SetActive (true);
		startGameBtn.gameObject.SetActive(false);

	}

	void DownFrindImage()
	{
		addFriendBtn.gameObject.SetActive (false);
		frndPic.transform.parent.gameObject.SetActive (true);
		myPic.transform.parent.gameObject.SetActive (true);
	}

	public void MakeInteractable()
	{
		DownFrindImage ();
		startGameBtn.interactable = true;
	}

	public void StartTest(bool isOwner){
		if (isOwner) {
			ConnectionManager.Instance.OnServerGameStart ();
			questionPanal.SetActive (true);
			startGamePanel.SetActive (false);
		} else {
			questionPanal.SetActive (true);
			startGamePanel.SetActive (false);
		}
		GameManager.instace.currRoomStatus = GameManager.eRoomStatus.play;
		SetQuestionInUI (currQuestion);

	}

	public void TimeUp()
	{
		Debug.Log ("Time UP");
		CheckAnswer (lastAns);
		int tAns = wrongAns + rightAns;
		ConnectionManager.Instance.OnSendMeAnswer (lastAns+"");
		myAnsCount.text = tAns+ "";
		currQuestion++;
		SetQuestionInUI (currQuestion);
	}
	// 
	public void OnGameOver()
	{
		timmer.ResetClock ();
		UIManager.instance.resultPanel.SetActive (true);
		UIManager.instance.resultPanel.GetComponent<ResultUI> ().SetReportCart (rightAns,frindCrrAns);

	}


	public void FriendAnwer(string pData)
	{
		CheckFriendAnswer (pData[0]);
		currFrndQuestion++;
		frndAnsCount.text = frindAllAns + "";
		Debug.Log ("TimeUp Rec");
		if (currFrndQuestion >= questionList.Count) {
			timmer.ResetClock ();
			UIManager.instance.resultPanel.SetActive (true);
			GameManager.instace.currRoomStatus = GameManager.eRoomStatus.gameOver;
			UIManager.instance.resultPanel.GetComponent<ResultUI> ().SetReportCart (rightAns,frindCrrAns);
			return;
		}
	}

	public void OnAnswerSelected(Toggle a)
	{
		if (a.isOn) 
		{
			lastAns = a.name [0];
		}
	}

	void SetQuestionInUI(int pQ)
	{
		if (pQ >= questionList.Count) {
			timmer.ResetClock ();
			ConnectionManager.Instance.OnGameOverSendData ();
			return;
		}
		lastAns = 'E';
		toggleGrp.allowSwitchOff = true;
		for (int i = 0; i < allToggle.Length; i++) {
			allToggle [i].isOn = false;
		}
		toggleGrp.allowSwitchOff = false;

		qNoTxt.text = (pQ+1)+"";
		quesTxt.text = questionList [pQ].Question;
		opt1.text = questionList [pQ].O_1;
		opt2.text = questionList [pQ].O_2;
		opt3.text = questionList [pQ].O_3;
		opt4.text = questionList [pQ].O_4;
	 	timmer.PlayClock ();
	}

	void CheckAnswer(char str)
	{
		int ans = System.Convert.ToInt32 (str) - 64;
		if (questionList [currQuestion ].A == ans+"") {
			rightAns++;
		} 
		else if("5" == ans+"")
		{
			unAns++;
		}
		else {
			wrongAns++;
		}
	}

	void CheckFriendAnswer(char str)
	{
		int ans = System.Convert.ToInt32 (str) - 64;
		if (questionList [currFrndQuestion ].A == ans+"") {
			frindCrrAns++;
			frindAllAns++;
		} 
		else if("5" == ans+"")
		{
			//unAns++;
		}
		else {
			frindAllAns++;
			//wrongAns++;
		}
	}
	public void OnBackSelected()
	{
		GameManager.instace.currRoomStatus = GameManager.eRoomStatus.gameOver;
		ConnectionManager.Instance.OnGameOverSendData ();
		uiManager.resultPanel.SetActive (true);
		UIManager.instance.resultPanel.GetComponent<ResultUI> ().SetReportCart (rightAns,frindCrrAns);
	}

	void Reset()
	{
		currFrndQuestion = 0;
		currQuestion = 0;
		rightAns = 0;
		wrongAns = 0;
		unAns = 0;
		frindAllAns = 0;
		frindCrrAns = 0;
		myAnsCount.text = "0";
		frndAnsCount.text = "0";
	}
}
