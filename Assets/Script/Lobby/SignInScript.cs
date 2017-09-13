using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class SignInScript : MonoBehaviour {
	FirebaseAuth auth;
	FirebaseUser user;

	DatabaseReference root;
	DatabaseReference history;
	DatabaseReference steate;

	FirebaseDatabase db;
	Credential credential;


	public Text text;
	// Use this for initialization
	void Start () {
		Debug.Log("initstart");
		text.text = "initstart";
		Init();
		Debug.Log("initEnd");
		text.text = "initEnd";
		Debug.Log("SignIn");
		text.text = "SignIn";
		FirebaseSignIn();
	}
	void Init()
	{
		Application.runInBackground = true;
		text.text = "init 1";
		auth = FirebaseAuth.DefaultInstance;
		auth.StateChanged += AuthStateChanged;
		AuthStateChanged(this, null);
		user = null;
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://test-21ff1.firebaseio.com/");
		text.text = "init 2";
		text.text = "init 3";
		root = FirebaseDatabase.DefaultInstance.RootReference;
		text.text = "init 4";
	}
	void FirebaseSignIn() {
		auth.SignInAnonymouslyAsync().ContinueWith(signInTask => {
			if (signInTask.IsCompleted) {
				
			}
			else {
			}
		});
	}
	
	private void InitUserData() {
		root.Child("User").Child(auth.CurrentUser.UserId).Child("History").Push().SetValueAsync("ACTIVATE").ContinueWith(task => {
			if (task.IsCompleted) {
				Debug.Log("Init Completed");
			}
			else {
				Debug.Log("Init Failed");
			}
		});
	}

	private void IsNewUser() {
		root.Child("User").Child(auth.CurrentUser.UserId.ToString()).Child("State").SetValueAsync("OnLine").ContinueWith(stateTask => {
			if (stateTask.IsCompleted)
				text.text = "Exist";
			else
				text.text = "New";
		});
	}

	void AuthStateChanged(object sender, System.EventArgs eventArgs) {
		if (user == null || auth.CurrentUser != user) {
			bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
			if (!signedIn && user != null) {
				text.text = "Signed out " + user.UserId;
			}
			user = auth.CurrentUser;
			if (signedIn) {
				text.text = "Signed in " + user.UserId;
				IsNewUser();
			}
		}
	}

	void Update() {
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
	}
}
