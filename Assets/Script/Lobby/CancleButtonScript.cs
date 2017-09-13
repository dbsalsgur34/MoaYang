using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CancleButtonScript : MonoBehaviour {
	public Button cancleButton;

	public GameObject LoadingScreen;
	// Use this for initialization
	void Start () {
		cancleButton.onClick.AddListener(CancleMatching);
	}


	public void CancleMatching()
	{
		if (LobbyState.currentLobbyState.Equals(LobbyState.state.WAITFORPLAYING))
		{
			LobbyState.currentLobbyState = LobbyState.state.IDLE;
			LoadingScreen.SetActive(false);
			AudioManager.Instance.InitEffectAudio();
			AudioManager.Instance.BackGroundClipRestore();
			AudioManager.Instance.PlayOneShotEffectClipByName("Button_Lobby");
			//Network_Client.StopThread();
		}
	}
}
