using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Facebook.Unity;

public class Share : MonoBehaviour {

	public GameObject ShareScreen;
	public GameObject HelpScreen;
	public GameObject PlayScreen;
	public GameObject HomeScreen;
	public GameObject GameOverScreen;

	public Button PlayBtn;
	public Button ShareBtn;
	public Button HelpBtn;
	public Button FbBtn;
	public Button TwitBtn;

	public Button BackBtn;
	public Button CloseBtn;

	public Button RetryBtn;
	public Button HomeBtn;

	public Toggle ToggleBtn;

	// Use this for initialization
	void Start () {

		Sprite[] sp = Resources.LoadAll<Sprite>("");

		Debug.Log ("loaded sprite :: "+sp.Length);

		PlayBtn = PlayBtn.GetComponent<Button> ();
		ShareBtn = ShareBtn.GetComponent<Button> ();
		HelpBtn = HelpBtn.GetComponent<Button> ();
		FbBtn = FbBtn.GetComponent<Button> ();
		TwitBtn = TwitBtn.GetComponent<Button> ();

		CloseBtn = CloseBtn.GetComponent<Button> ();
		BackBtn = BackBtn.GetComponent<Button> ();

		RetryBtn = RetryBtn.GetComponent<Button> ();
		HomeBtn = HomeBtn.GetComponent<Button> ();

		ToggleBtn = ToggleBtn.GetComponent<Toggle> ();
	}

	public void startHome() {
		
		ShareScreen.SetActive(false);
		HelpScreen.SetActive (false);
		PlayScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		HomeScreen.SetActive (true);

		if (ToggleBtn.isOn) {
			GameModel.GodsMode = true;
		}

		Debug.Log ("in the start home");
	}

	public void startPlay(){

		ShareScreen.SetActive(false);
		HelpScreen.SetActive (false);
		HomeScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		PlayScreen.SetActive (true);

		GameModel.StartPlay = true;

		Debug.Log ("in the start play");
	}

	public void startShare(){

		HelpScreen.SetActive (false);
		PlayScreen.SetActive (false);
		HomeScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		ShareScreen.SetActive(true);

		Debug.Log ("in the start share");
	}

	public void startHelp(){
		
		ShareScreen.SetActive(false);
		PlayScreen.SetActive (false);
		HomeScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		HelpScreen.SetActive (true);

		Debug.Log ("in the start help");
	}

	public void startGameOver() {
		ShareScreen.SetActive(false);
		PlayScreen.SetActive (false);
		HomeScreen.SetActive (false);
		HelpScreen.SetActive (false);
		GameOverScreen.SetActive (true);

		Debug.Log ("in the start game over");
	}
	
	void Awake ()
	{
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
	}
	
	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}
	
	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}
		} else {
			Debug.Log("User cancelled login");
		}
	}

	private void ShareCallback (IShareResult result) {
		if (result.Cancelled || !String.IsNullOrEmpty(result.Error)) {
			Debug.Log("ShareLink Error: "+result.Error);
		} else if (!String.IsNullOrEmpty(result.PostId)) {
			// Print post identifier of the shared content
			Debug.Log(result.PostId);
		} else {
			// Share succeeded without postID
			Debug.Log("ShareLink success!");
		}
	}

	public void fbBtnPress(){
		Debug.Log ("Share on facebook");
		var perms = new List<string>(){"public_profile", "email"};
		FB.LogInWithReadPermissions(perms, AuthCallback);

		FB.ShareLink(
			new Uri("https://google.com/"),
			callback: ShareCallback
			);
	}

	public void twitBtnPress(){
		Debug.Log ("Share on twitter");
		
		Application.OpenURL ("https://twitter.com/intent/tweet?");
	}

	public void closeBtnPress(){

		ShareScreen.SetActive(false);
		HelpScreen.SetActive (false);
		PlayScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		HomeScreen.SetActive (true);

		Debug.Log ("in the close btn");
	}

	public void backBtnPress() {

		startHome ();

		GameModel.StopPlay = true;

		Debug.Log ("in the back btn");
	}

	public void retryBtnPress() {

		startPlay ();

		Debug.Log ("in the retry btn");
	}

	public void homeBtnPress() {

		startHome ();

		Debug.Log ("in the retry btn");
	}

	public void toggleBtnPress() {
		if (ToggleBtn.isOn) {
			GameModel.GodsMode = true;
		} else {
			GameModel.GodsMode = false;
		}

		Debug.Log ("in the toggle btn");
	}

	// Update is called once per frame
	void Update () {

		if (GameModel.GameOver) {
			startGameOver();
		}
	
	}

}
