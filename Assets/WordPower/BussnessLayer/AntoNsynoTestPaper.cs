using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntoNsynoTestPaper : MonoBehaviour {

	public static AntoNsynoTestPaper instace;
	public List <AntoQuestion> questionList;
	void Awake()
	{
		if (instace == null)
			instace = this;
	}


	public List <AntoQuestion> GetQuestionFromDB()
	{
		TextAsset asset = Resources.Load ("Test") as TextAsset;
		questionList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AntoQuestion>>(asset.ToString());
		return questionList;
	}

}
[System.Serializable]
public class AntoQuestion
{
	public string Question;
	public string O_1;
	public string O_2;
	public string O_3;
	public string O_4;
	public string A;
	public string Explain;
}
