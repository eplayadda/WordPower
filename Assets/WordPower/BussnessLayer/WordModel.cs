using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordModel : MonoBehaviour {

	public string questionNo;
	public string questionInEng;
	public string questionInHin;
	public string syno;
	public string anto;

	public  WordModel(string pQuestionNo,string pQuestionInEng,string pQuestionInHin,string pSyno,string pAnto)
	{
		questionNo = pQuestionNo;
		questionInEng = pQuestionInEng;
		questionInHin = pQuestionInHin;
		syno = pSyno;
		anto = pAnto;
	}
}
