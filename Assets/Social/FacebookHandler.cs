using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Facebook.MiniJSON;
using System;
using UnityEngine.UI;


public class FacebookHandler : MonoBehaviour
{
	public GameObject FriendPrefab;
	public Transform parentObject;

	//public GameObject FriendPrefabRoom;
	//public Transform parentRoom;

	private string userId;
	//private Texture profilePic;
	//string appStoreLink = "https://play.google.com/store/apps/details?id=com.eplayadda.mindssmash";
	//string inviteAppLinkUrl = "https://fb.me/350820032015040";
	private bool IsInternetAvailabe = false;
	//public Text testText;

	void Start ()
	{
		StartCoroutine (checkInternetConnection ((isConnected) => {
			// handle connection status here
			if (isConnected) {
				IsInternetAvailabe = true;
				if (!FB.IsInitialized) {

					FB.Init (OnInitComplete, OnHideUnity);
				} else {
					FB.ActivateApp ();
				}
			} else {
				IsInternetAvailabe = false;
			}
		}));

	}

	IEnumerator checkInternetConnection (Action<bool> action)
	{
		while (true) {
			WWW www = new WWW ("http://google.com");
			yield return www;
			if (www.error != null) {
				action (false);
			} else {
				action (true);
			}
			yield return new WaitForSeconds (5);
		}
	}

	private void OnInitComplete ()
	{
		if (FB.IsInitialized) {
			FB.ActivateApp ();
		}
		Debug.Log ("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
	}

	//Facebook Login
	public void OnFacebookLogin ()
	{
		if (!FB.IsLoggedIn) { 
			CallFBLogin (); 
		} else {
			ConnectionManager.Instance.MakeConnection ();
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		Debug.Log ("Is Fb Hide");
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	private void CallFBLogin ()
	{
		FB.LogInWithReadPermissions (new List<string> (){ "public_profile", "email", "user_friends" }, this.FBLoginCallBack);
		FB.LogInWithPublishPermissions (new List<string> () { "publish_actions" }, this.FBLoginCallBack);
	}

	private void FBLoginCallBack (ILoginResult result)
	{
		if (result.Error == null) {
			Debug.Log ("LoginSuccess" + result.RawResult);
			var token = Facebook.Unity.AccessToken.CurrentAccessToken;
			userId = token.UserId.ToString ();
			ConnectionManager.Instance.myID = userId;
			ConnectionManager.Instance.MakeConnection ();
			//OnFacebookShare ();
		} else if (result.Error != null) {
			Debug.Log ("Error in Login");
		}

	}

	public void GetFriends ()
	{
		if (FB.IsLoggedIn) {
			FB.API ("me?fields=id,name,friends.limit(20){first_name,picture}", HttpMethod.GET, this.GetFreindCallback);
		} else {
			LoginForFriendsList ();
		}

	}

	private void LoginForFriendsList ()
	{
		FB.LogInWithReadPermissions (new List<string> (){ "public_profile", "email", "user_friends" }, this.FBLoginGetFriendCallBack);
		FB.LogInWithPublishPermissions (new List<string> () { "publish_actions" }, this.FBLoginGetFriendCallBack);
	}

	void FBLoginGetFriendCallBack (ILoginResult result)
	{
		if (string.IsNullOrEmpty (result.Error)) {
			GetFriends ();
		}	
	}

	bool isFrndsAvials = false;

	void GetFreindCallback (IResult result)
	{
		if (isFrndsAvials)
			return;
		string resposne = result.RawResult;
		Debug.Log (resposne);
		var data = (Dictionary<string, object>)result.ResultDictionary;
		var tagData = data ["friends"] as Dictionary<string,object>;
		var resultData = tagData ["data"] as List<object>;
		//Debug.Log (tagData ["first_name"].ToString ());
		for (int i = 0; i < resultData.Count; i++) {
			var resultValue = resultData [i] as Dictionary<string, object>;
			var picture = resultValue ["picture"] as Dictionary<string ,object>;
			var picData = picture ["data"] as Dictionary<string,object>;
			string url = picData ["url"].ToString ();
			Debug.Log ("url : " + url);
			GameObject g = Instantiate (FriendPrefab) as GameObject;
			g.SetActive (true);
			g.transform.SetParent (parentObject);
			g.transform.localScale = Vector3.one;
			g.transform.position = Vector3.zero;
			g.GetComponent<FriendsDetails> ().Name.text = resultValue ["first_name"].ToString ();
			Button btn = g.GetComponentInChildren<Button> ();
			Debug.Log (resultValue ["first_name"].ToString () + "  , " + resultValue ["id"].ToString ());
			string id = resultValue ["id"].ToString ();
			g.GetComponent<FriendsDetails> ().ID = System.Convert.ToInt64 (id);
			AddListener (btn, id);
			if (!string.IsNullOrEmpty (id)) {
				FB.API ("https" + "://graph.facebook.com/" + id + "/picture?width=128&height=128", HttpMethod.GET, delegate(IGraphResult avatarResult) {
					if (avatarResult.Error != null) {
						Debug.Log (avatarResult.Error);
					} else {

						g.GetComponent<FriendsDetails> ().ProfilePic.sprite = Sprite.Create (avatarResult.Texture, new Rect (0, 0, 128, 128), new Vector2 (0.5f, 0.5f));
						;
					}
				});
			}
			isFrndsAvials = true;
		}
	}

	private void AddListener (Button btn, string fbID)
	{
		btn.onClick.AddListener (() => SetFriendsId (fbID));
	}
	//	private void OnGUI ()
	//	{
	//		if (GUI.Button (new Rect (100, 100, 100, 50), "Login")) {
	//			Login ();
	//		}
	//
	//		if (GUI.Button (new Rect (100, 200, 100, 50), "GetFriends")) {
	//			GetFriends ();
	//		}
	//
	//		if (GUI.Button (new Rect (100, 300, 100, 50), "Invite")) {
	//			AppRequest ();
	//		}
	//	}

	public void SetFriendsId (string id)
	{
		ConnectionManager.Instance.friedID = id;
		Debug.Log ("SetFriendsId : " + id);
		UIManager.instance.friendsListPanel.SetActive (false);
		FB.API ("https" + "://graph.facebook.com/" + id + "/picture?width=128&height=128", HttpMethod.GET, delegate(IGraphResult avatarResult) {
			if (avatarResult.Error != null) {
				Debug.Log (avatarResult.Error);
			} else {

				UIManager.instance.UpdateFrindProfilePic (Sprite.Create (avatarResult.Texture, new Rect (0, 0, 128, 128), new Vector2 (0.5f, 0.5f)));

			}
		});

	}


	public void DownloadImageByID (string id)
	{
		Debug.Log ("ID " + id);
		FB.API ("https" + "://graph.facebook.com/" + id + "/picture?width=128&height=128", HttpMethod.GET, delegate(IGraphResult avatarResult) {
			if (avatarResult.Error != null) {
				Debug.Log (avatarResult.Error);
			} else {

				UIManager.instance.roomPanel.GetComponent<RoomUI> ().frndPic.sprite = Sprite.Create (avatarResult.Texture, new Rect (0, 0, 128, 128), new Vector2 (0.5f, 0.5f));

			}
		});
	}


	//Share On Facebook.
	public void OnFacebookShare ()
	{
		if (!IsInternetAvailabe)
			return;
		Debug.Log ("OnFacebookShare");
		if (FB.IsLoggedIn) {
			//FB.ShareLink (new System.Uri (appStoreLink), "MindSsmash", "want to bit me ? Download and play the Game", null, callback: ShareCallBck);

		} else {
			Debug.Log ("Please Login");
			CallFBLogin ();
		}
	}

	private void ShareCallBck (IShareResult result)
	{
		if (result.Cancelled || !string.IsNullOrEmpty (result.RawResult)) {
			Debug.Log ("Share Error : " + result.Error);
		} else if (!string.IsNullOrEmpty (result.PostId)) {
			Debug.Log (result.PostId);
		} else {
			Debug.Log ("Share success");
		}
	}

	public void PlayerInfo ()
	{
		Debug.Log ("GetPlayerInfo");
		string queryString = "/me?fields=id,first_name,picture.width(128).height(128)";
		FB.API (queryString, HttpMethod.GET, GetPlayerInfoCallback);
	}

	//	string firstName = "";
	//
	private void GetPlayerInfoCallback (IGraphResult result)
	{
		Debug.Log ("GetPlayerInfoCallback");
		if (result.Error != null) {
			Debug.LogError (result.Error);
			return;
		}
		Debug.Log (result.RawResult);
	
		Dictionary<string,object> resultData = (Dictionary<string,object>)result.ResultDictionary;
		//firstName = resultData ["first_name"].ToString ();
		string playerImgUrl = DeserializePictureURL (result.ResultDictionary);
		Debug.Log ("playerImgUrl " + playerImgUrl);
		UIManager.instance.UpdateProfilePic (playerImgUrl);
		//GetScoreFB ();
	}



	public static string DeserializePictureURL (object userObject)
	{
		var user = userObject as Dictionary<string, object>;
	
		object pictureObj;
		if (user.TryGetValue ("picture", out pictureObj)) {
			var pictureData = (Dictionary<string, object>)(((Dictionary<string, object>)pictureObj) ["data"]);
			return (string)pictureData ["url"];
		}
		return null;
	}

	public bool IsFbLogedin ()
	{
		return FB.IsLoggedIn;
	}

	public void GetScoreFB ()
	{
		foreach (GameObject g in friendsList) {
			Destroy (g);
		}

		if (FB.IsLoggedIn) {
			
			//UIAnimationController.Instance.loading = true;
			//UIController.Instance.ActiveUI (UIAnimationController.Instance.loadingImage);
			//UIController.Instance.DeactiveUI (SocialManager.Instance.facebookPanel);
			//SetScoreToFB (ScoreHandler.instance.GetCurrentScore ().ToString ());
			FB.API ("/app/scores?fields=score,user.limit(30)", HttpMethod.GET, GetScoreCallBack);
		} else {
			//UIAnimationController.Instance.loading = false;
			//UIController.Instance.ActiveUI (SocialManager.Instance.facebookPanel);

			//CreateLeaderBoard ("Me", ScoreHandler.instance.GetCurrentScore ().ToString (), "");
		}
	}

	public void LoginWithFacebook ()
	{
		if (!IsInternetAvailabe)
			return;
		FB.LogInWithReadPermissions (new List<string> (){ "public_profile", "email", "user_friends" }, this.APICallback);
		FB.LogInWithPublishPermissions (new List<string> () { "publish_actions" }, this.APICallback);
	}

	void APICallback (ILoginResult result)
	{
		if (result.Error != null) {
			Debug.Log ("Error in login ");
			//UIController.Instance.ActiveUI (SocialManager.Instance.facebookPanel);
		} else {
			GetScoreFB ();
		}

	}

	private void GetScoreCallBack (IResult result)
	{
		foreach (GameObject g in friendsList) {
			Destroy (g);
		}
		friendsList.Clear ();
		Debug.Log (result.RawResult);
		if (FB.IsLoggedIn) {
			var dict = Json.Deserialize (result.RawResult) as Dictionary<string,object>;
			List<object> scores = dict ["data"] as List<object>;
			for (int i = 0; i < scores.Count; i++) {
				Dictionary<string , object> scoreData = scores [i] as Dictionary<string , object>;
				object score = scoreData ["score"];
				var user = scoreData ["user"] as Dictionary<string , object>;
				var userName = user ["name"];
				//debugText.text = score.ToString () + "-" + userName.ToString ();
				Debug.Log (score.ToString () + "-" + userName.ToString ());
				//		UIAnimationController.Instance.loading = false;
				//		UIController.Instance.DeactiveUI (UIAnimationController.Instance.loadingImage);
				CreateLeaderBoard (userName.ToString (), score.ToString (), user ["id"].ToString ());

			}
		}
	}

	public void SetScoreToFB (string score)
	{
		if (!IsInternetAvailabe)
			return;
		if (FB.IsLoggedIn) {
			var scoreData = new Dictionary<string,string> ();
			scoreData ["score"] = score;
			FB.API ("/me/scores", HttpMethod.POST, delegate(IGraphResult result) {
				Debug.Log ("result Submit + " + result.RawResult);	
				//debugText.text = "result Submit + " + result.RawResult;
			}, scoreData);
		} 
	}

	public GameObject playerInfoPrefab;
	public Transform parentProfile;
	// info;
	List<GameObject> friendsList = new List<GameObject> ();

	public void CreateLeaderBoard (string userName, string score, string id)
	{
		GameObject profile = Instantiate (playerInfoPrefab) as GameObject;
		profile.SetActive (true);
		friendsList.Add (profile);
		profile.transform.SetParent (parentProfile);
		profile.transform.localScale = Vector3.one;
		Image avatar = profile.transform.Find ("ProfilePic").GetComponent<Image> ();
		//info = profile.GetComponent<UserInfo> ();
	
		//info.UpdateUserInfo (userName, score);
		if (!string.IsNullOrEmpty (id)) {
			FB.API ("https" + "://graph.facebook.com/" + id + "/picture?width=128&height=128", HttpMethod.GET, delegate(IGraphResult avatarResult) {
				if (avatarResult.Error != null) {
					Debug.Log (avatarResult.Error);
				} else {
				
					avatar.sprite = Sprite.Create (avatarResult.Texture, new Rect (0, 0, 128, 128), new Vector2 (0.5f, 0.5f));
					;
				}
			});
		}

	}

	public void DeleteAllScoreFromFB ()
	{
		if (FB.IsLoggedIn) {
			FB.API ("/me/scores", HttpMethod.DELETE, delegate(IGraphResult result) {
				if (result.Error != null)
					Debug.Log (result.Error);
				else
					Debug.Log (result.RawResult);
			});
		}
	}

	public void UpdateTopScorrer ()
	{
		if (FB.IsLoggedIn) {
			//UIAnimationController.Instance.gameoverLoadning = true;
			//UIController.Instance.ActiveUI (UIAnimationController.Instance.loadingGameOverImage);
			FB.API ("/app/scores?fields=score,user.limit(1)", HttpMethod.GET, GetTopScorrer);
		} else {
			//ScoreHandler.instance.ScoreAI ();
		}
	}

	private void GetTopScorrer (IResult result)
	{
		var dict = Json.Deserialize (result.RawResult) as Dictionary<string,object>;
		List<object> scores = dict ["data"] as List<object>;
		Dictionary<string , object> scoreData = scores [0] as Dictionary<string , object>;
		object score = scoreData ["score"];
		var user = scoreData ["user"] as Dictionary<string , object>;
		var userName = user ["name"];
		//testText.text = userName.ToString () + " | " + score.ToString ();
		Debug.Log ("UserName " + userName.ToString ());
		PlayerPrefs.SetString ("FriendName", userName.ToString ());
		PlayerPrefs.SetInt ("FriendScore", Convert.ToInt32 (score.ToString ()));
		//UIAnimationController.Instance.gameoverLoadning = false;
		//UIController.Instance.DeactiveUI (UIAnimationController.Instance.loadingGameOverImage);
		//ScoreHandler.instance.ScoreAI ();

		Debug.Log (PlayerPrefs.GetString ("FriendName") + " " + PlayerPrefs.GetInt ("FriendScore"));
	}


	public void InviteFriends ()
	{
		if (!IsInternetAvailabe)
			return;
		if (FB.IsLoggedIn) {
			//FB.Mobile.AppInvite (new Uri (inviteAppLinkUrl), null, this.InviteCallback);
		} else {
			FB.LogInWithReadPermissions (new List<string> (){ "public_profile", "email", "user_friends" }, this.InviteFreindLoginCallback);
			FB.LogInWithPublishPermissions (new List<string> () { "publish_actions" }, this.InviteFreindLoginCallback);
		}
	}

	private void InviteCallback (IAppInviteResult result)
	{
		if (string.IsNullOrEmpty (result.Error)) {
			Debug.Log (result.Error);
		} else {
			Debug.Log ("Successfully Invited");
		}
	}

	private void InviteFreindLoginCallback (ILoginResult result)
	{
		if (!string.IsNullOrEmpty (result.Error)) {
			InviteFriends ();
		}
	}
}



