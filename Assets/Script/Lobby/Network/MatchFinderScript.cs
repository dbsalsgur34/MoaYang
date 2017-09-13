using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;

public class MatchFinderScript : MonoBehaviour {
    //랜덤이 Monobehaviour에서 스태틱으로 지정해놔서 저럴걸ㅇㅇ
    // Use this for initialization
    public Button playButton;
	public Button cancleButton;

	private float matchSeed;
    DatabaseReference matchQueRef;
	DatabaseReference userMatchHIstoryRef;
	FirebaseAuth auth;

	void Start () {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://moayang-6693860.firebaseio.com/");
		//FirebaseApp.DefaultInstance.SetEditorP12FileName("Sheeplanet-6a78abd166b8.p12");
		//FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("sheeplanet-76895731@appspot.gserviceaccount.com");
		//FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
		auth = FirebaseAuth.DefaultInstance;

		cancleButton.onClick.AddListener(CancleFinding);
		playButton.onClick.AddListener(FindNewMatch);
	}

	public void FindNewMatch()
	{
		matchSeed = Random.value;
		RegistToMatchQue();
	}
    void RegistToMatchQue()
    {
		//matchQue에 push하고 value를 현재 로그인 한 유저의 아이디로 저장한다.
		FirebaseDatabase.DefaultInstance.RootReference.Child("matchingQue").Push().SetValueAsync(auth.CurrentUser.UserId).ContinueWith(task =>
        {
			if (task.IsCompleted)
			{
				matchQueRef = FirebaseDatabase.DefaultInstance.RootReference.Child("matchingQue");
				StartCoroutine(Waitting());
			}
			else if (task.IsCanceled)
			{
				Debug.Log("cancled : "+task.Exception.Message.ToString());

			}
			else
			{
				Debug.Log("else : "+task.Exception.Message.ToString());
			}
        });

    }

	public IEnumerator Waitting()
	{
		//User.<자신의id>.History에 child가 추가되는 것을 기다림. 
		//child가 add되면 해당 user의 User.<상대의 id>.History로 push함.
		//제한 시간 안에 자신의 History에 push가 들어오지 않을 경우 이벤트를 비활성화 하고 finding 실행
		userMatchHIstoryRef.Child(auth.CurrentUser.UserId).Child("History").ChildAdded += HistoryChildAdded;
		yield return new WaitForSeconds(1f);
		userMatchHIstoryRef.Child(auth.CurrentUser.UserId).Child("History").ChildAdded += null;
		Finding();
		
	}
	void HistoryChildAdded(object sender, ChildChangedEventArgs args)
	{
		if (args.DatabaseError != null)
		{
			Debug.LogError(args.DatabaseError.Message);
			return;
		}
		else
		{
			// Do something with the data in args.Snapshot
			Debug.Log(args.Snapshot.Value.ToString());

		}
	}

    void Finding()
    {
		//matchQue에 새롭게 push된 아이디를 통해 User.<id>.History에 push하고 자신의id를 value로 저장한다.
		//첫번 째 저장된 id는 find된 user 의 id, 만약 자신의 id가 1번에 저장되었다면 자신의 History에 Pushrk 들어오는 것을 기다림. 2번 이상인 경우, 다른 match를 find
		//자신의 History 에 Push가 들어오면 match 성사
		matchQueRef.ChildAdded += MatchQueChildAdded;
	}
    void MatchQueChildAdded(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            
        }
		else
		{
			// Do something with the data in args.Snapshot
			Debug.Log(args.Snapshot.GetValue(true).ToString());

		}

		
    }

	void CancleFinding()
	{
		//matchQueRef.ChildAdded += null;
		//userMatchHIstoryRef.Child(auth.CurrentUser.UserId).Child("History").ChildAdded += null;

		Debug.Log("Cancle Finding Match");
	}
}
