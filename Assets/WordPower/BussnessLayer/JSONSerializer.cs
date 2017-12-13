//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Newtonsoft.Json;
//
//public class ModelJson
//{
//    public string ID;
//    public string Name;
//    public float Age;
//    public ModelJson(string ID, string Name, float Age)
//    {
//        this.ID = ID;
//        this.Name = Name;
//        this.Age = Age;
//    }
//}
//
//
//
//public class JSONSerializer : MonoBehaviour {
//
//	void Start () {
//        
//        List<ModelJson> Users = new List<ModelJson>();
//        Users.Add(new ModelJson("1","A",21));
//        Users.Add(new ModelJson("2","B",22));
//        Users.Add(new ModelJson("3","C",23));
//        //SaveJSON(Users);
//        ReadJSON();
//
//    }
//
//    public void SaveJSON(List<ModelJson> users)
//    {
//      
//        string jsonString = JsonConvert.SerializeObject(users);
//        Debug.Log(jsonString);
//        MyPrefs.Save<string>(jsonString, "SaveMe", true);
//    }
//
//    public void ReadJSON()
//    {
//        string jsonData = MyPrefs.Load<string>("SaveMe", true);
//        Debug.Log(jsonData);
//        List <ModelJson> data = JsonConvert.DeserializeObject<List<ModelJson>>(jsonData);
//		Debug.Log("Name "+data[0].ID);
//    }
//	
//	
//}
