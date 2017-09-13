﻿using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using Firebase.Auth;


//개쩌는 클라이언트가 시작하는 부분, 유니티 메인 쓰레드에서 통신을 담당한다
namespace ClientSide
{
    public class KingGodClient : MonoBehaviour {

        public static KingGodClient Instance;
        public string serverIP;
        private NetworkTranslator networkTranslator;
        private int freeId = -1;
	    public static KingGodClient instance;
        public int Seed;
        public int playerNum;
	    void Awake(){
            if (Instance == null)           //Static 변수를 지정하고 이것이 없을경우 - PlayManage 스크립트를 저장하고 이것이 전 범위적인 싱글톤 오브젝트가 된다.
            {
                DontDestroyOnLoad(this.gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);   //싱글톤 오브젝트가 있을경우 다른 오브젝트를 제거.
            }
            networkTranslator = GetComponent<NetworkTranslator>();

	    }

	    void Start () {
			FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://moayang-6693860.firebaseio.com/");
			//FirebaseApp.DefaultInstance.SetEditorP12FileName("Sheeplanet-6a78abd166b8.p12");
			//FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("sheeplanet-76895731@appspot.gserviceaccount.com");
			//FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
			networkTranslator.SetMsgHandler(gameObject.AddComponent<ClientMsgHandler>());

		    Network_Client.Begin (serverIP);
	    }		

	    void OnApplicationQuit(){
		    Network_Client.ShutDown();
	    }

        public void Matching()
        {
            Network_Client.Send(freeId.ToString() + "/" + "Matching");
        }

        public void SetFreeId(int freeId)
        {
            this.freeId = freeId;
        }
		public void SetSeed(string Seed)
		{
			this.Seed = int.Parse(Seed);

		}
		public void SetPlayerNum(string playerNum)
		{
			this.playerNum = int.Parse(playerNum);
		}
    }

}
