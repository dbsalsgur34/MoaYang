using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayButtonScript : MonoBehaviour {
	public Button playButton;
	public GameObject LoadingScreen;

	// Use this for initialization
	void Start () {
		playButton.onClick.AddListener(StartMatching);
	}

	public void StartMatching()	{
		if (LobbyState.currentLobbyState.Equals(LobbyState.state.IDLE)) 	{
			LobbyState.currentLobbyState  = LobbyState.state.WAITFORPLAYING;
			LoadingScreen.SetActive(true);
			AudioManager.Instance.PlayEffectClipByName("Wait", 2f);
			AudioManager.Instance.PlayOneShotEffectClipByName("Button_Lobby");
			AudioManager.Instance.BackGroundClipAttenuation();
			
		}
	}

}
