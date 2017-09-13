using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class MatchingManager : MonoBehaviour {

    //Network에서 받은 메세지를 저장하는 Queue.
    private Queue<string> messageQueue;
    private int playerNumber;
    private int seed;
    

    DatabaseReference matchRef;
    

    private void Start()
    {
        InitManager();
    }

    private void InitManager()
    {
        messageQueue = new Queue<string>();
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://moayang-6693860.firebaseio.com/");
		//FirebaseApp.DefaultInstance.SetEditorP12FileName("Sheeplanet-6a78abd166b8.p12");
		//FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("sheeplanet-76895731@appspot.gserviceaccount.com");
		//FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
		matchRef = FirebaseDatabase.DefaultInstance.RootReference.Child("Match");
    }

    public void PushMatchingQue(string uid)
    {

    }
    
}
