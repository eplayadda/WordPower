﻿using UnityEngine;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;
using System;
using System.Text;
using BestHTTP.SignalR;
using BestHTTP;
using BestHTTP.SignalR.Hubs;
using BestHTTP.SignalR.Messages;
using BestHTTP.SignalR.JsonEncoders;
using System.Collections.Generic;

public class ConnectionManager : MonoBehaviour
{
	public static ConnectionManager Instance;
	string HUB_NAME = "eLarning";
	string CLIENTID = "ClientId";
	string GETREQUEST = "GetRequest";
	string ACK_CONNECTED = "receiveAcknowledgement";
	string CHALLENGEACCEPTED = "ChallengeAccepted";
	string INPUTRECIVEC = "OnInputRecived";
    //	string baseUrl = "http://52.11.67.198/SignalRDemo/";
    // "http://localhost:1921/SignalRDemo";// "http://52.33.40.224/SignalRDemo";//"http://localhost:1921/SignalRDemo";
    //string baseUrl = "http://localhost:1921/SignalRDemo2/";//"http://52.11.67.198/SignalRDemo";// "http://52.33.40.224/SignalRDemo";
    string baseUrl = "http://www.eplayadda.com/SignalR/eLarningHub/hubs";//"http://52.11.67.198/eLarningHub/";
	//string baseUrl = "http://localhost:30359/eLarningHub/eLarningHub/";
	public string myID = "1";
	public string friedID = "1";
    public List<string> onlineFriends = new List<string>();
    bool isLatestOnline; 
	public enum SignalRConectionStatus
	{
		None = 0,
		DisConnected,
		Connected,
	}

	private SignalRConectionStatus curSignalRConectionStatus;
	public static Connection signalRConnection;
	public static Hub _newHub;
	public Coroutine signalRCoroutine;

	void Awake ()
	{
		PlayerPrefs.DeleteAll ();
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (this.gameObject);
		} else {
			DestroyImmediate (this.gameObject);
			return;
		}
		//MakeConnection ();
	}

	public void MakeConnection ()
	{
		signalRConnection = null;

		if (signalRConnection == null) {
			try {
				Uri uri = new Uri (baseUrl);
				_newHub = new Hub (HUB_NAME);
				signalRConnection = new Connection (uri, _newHub);
				signalRConnection.JsonEncoder = new LitJsonEncoder ();

				signalRConnection.OnStateChanged += OnSignalRStatusChange;
				signalRConnection.OnError += OnSignalRErrorOccur;
				signalRConnection.OnConnected += OnSignalRConnected;
				signalRConnection.OnClosed += (con) => OnSignalRClosed ();
				signalRConnection.OnReconnected += onSignalRReconnected;

				Dictionary<string, string> dict = new Dictionary<string, string> ();
				dict.Add (CLIENTID, myID);
				signalRConnection.AdditionalQueryParams = dict;
				signalRCoroutine = StartCoroutine ("OpenSignalRConnection");
				AllOperations (); 

			} catch (Exception e) {
			}
		}      
	}

	void OnSignalRStatusChange (Connection conection, ConnectionStates oldState, ConnectionStates newState)
	{

	}

	void OnSignalRErrorOccur (Connection connection, string error)
	{
	}

	void  OnSignalRConnected (Connection connection)
	{
		//curSignalRConectionStatus = SignalRConectionStatus.Connected;
	}

	public void OnSignalRClosed ()
	{

	}

	void OnApplicationQuit ()
	{
		if (signalRConnection != null) {
			signalRConnection.Close ();
			signalRConnection = null;
		}
		Debug.Log ("Application Quit");
	}


	void onSignalRReconnected (Connection connection)
	{
		Debug.Log ("Signal R connection Reconnected");
	}

	public IEnumerator OpenSignalRConnection ()
	{
		signalRConnection.Open ();
		while (true) {
			yield return new WaitForSeconds (10f);
			try {
				if (signalRConnection.State == ConnectionStates.Closed) {
					signalRConnection.Open ();
				}
			} catch (Exception e) {
				Debug.LogError ("Exception SignalR Open " + e.Message);
			}
		}
	}

	public void AllOperations ()
	{
		signalRConnection [HUB_NAME].On (GETREQUEST, OnReceiveMatchDetails);
		signalRConnection [HUB_NAME].On (ACK_CONNECTED, Ack);
		signalRConnection [HUB_NAME].On (CHALLENGEACCEPTED, ChallengeAccepted);
		signalRConnection [HUB_NAME].On (INPUTRECIVEC, OnInputRecived);
	}

	List <string> usersID = new List<string> ();

	// Sending Request
	public void OnSendRequest (string pTablePrice,string pCurrSubjectType)
	{
		usersID.Clear ();
		usersID.Add (myID);
		usersID.Add (friedID);
		usersID.Add (pTablePrice);
		usersID.Add (pCurrSubjectType);
		Debug.Log (myID + "Send Request" + friedID);
		signalRConnection [HUB_NAME].Call ("SendRequest", usersID);
	}

	// Request Came
	public void OnReceiveMatchDetails (Hub hub, MethodCallMessage msg)
	{
		Debug.Log ("Request came");
		var str = msg.Arguments [0] as object[];
		friedID = str [0].ToString ();
		int tablePrice = Convert.ToInt32 (str [2].ToString ());
		int subjectType = Convert.ToInt32 (str [3].ToString ());
		UIManager.instance.OnSendRequest (tablePrice,subjectType);

	}

	public void IacceptChallage ()
	{
		usersID.Clear ();
		usersID.Add (myID);
		usersID.Add (friedID);
		signalRConnection [HUB_NAME].Call ("IacceptedChallenge", usersID);


	}

	public void ChallengeAccepted (Hub hub, MethodCallMessage msg)
	{
		UIManager.instance.OnChallangeAccepted ();
	}

	List <string> inputData = new List<string> ();

    public void GetOnlineFriend()
    {
        isLatestOnline = true;
        signalRConnection[HUB_NAME].Call("SendOnlineFriend", myID);
    }

    public void OnServerGameStart ()
	{
		inputData.Clear ();
		inputData.Add (friedID);
		inputData.Add ("");
		inputData.Add (3 + "");
		signalRConnection [HUB_NAME].Call ("InPutTaken", inputData);
	}

	public void OnSendMeAnswer (string ansCount)
	{
		inputData.Clear ();
		inputData.Add (friedID);
		inputData.Add (ansCount);
		inputData.Add (0 + "");
		signalRConnection [HUB_NAME].Call ("InPutTaken", inputData);
	}

	public void OnGameOverSendData ()
	{
		//Game Over
		inputData.Clear ();
		inputData.Add (friedID);
		inputData.Add ("");
		inputData.Add (1 + "");
		signalRConnection [HUB_NAME].Call ("InPutTaken", inputData);
	}

	public void OnInputRecived (Hub hub, MethodCallMessage msg)
	{
		var str = msg.Arguments [0] as object[];
		Debug.Log (str [2].ToString ());


		if (str [2].ToString () == "0") {
			if (GameManager.instace.currRoomStatus == GameManager.eRoomStatus.play) {
				string a = str [1].ToString ();
				UIManager.instance.FriendAnswer (a);
			}

		} else if (str [2].ToString () == "1") {
			//int a = Convert.ToInt32(str[1]);
			UIManager.instance.FriendGameOver ();
		} else if (str [2].ToString () == "2") {
		} else if (str [2].ToString () == "3") {
			if (GameManager.instace.currRoomStatus != GameManager.eRoomStatus.play) {
				UIManager.instance.OnGameStartOnServer ();
				Debug.Log ("3333");
			}
		}
	}

	public void Ack (Hub hub, MethodCallMessage msg)
	{
        onlineFriends.Clear();
        UIManager.instance.OnSignalRConnected ();
        var str = msg.Arguments[0] as object[];
        for (int i = 0; i < str.Length; i++)
        {
            if(myID != str[i].ToString())
               onlineFriends.Add(str[i].ToString());
        }
        Debug.Log(str[0].ToString()+""+ str.Length);
        if (isLatestOnline)
        {
            SocialManager.Instance.facebookManager.GetFriends();
            isLatestOnline = false;
        }

    }


}