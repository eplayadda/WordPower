using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class AntoNsynoDataBase : MonoBehaviour {
	public static AntoNsynoDataBase instace;
	public WordDetailsUI wordPanel;

	void Awake()
	{
		if (instace == null)
			instace = this;
		
	}

	public void GetDataFromDB()
	{
		TextAsset asset = Resources.Load ("One") as TextAsset;
		List <WordModel> data = JsonConvert.DeserializeObject<List<WordModel>>(asset.ToString());
		wordPanel.CreateWords (data);
	}
}
