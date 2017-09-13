using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButtonScript : MonoBehaviour {
	public Button settingButton;

	// Use this for initialization
	void Start () {
		settingButton.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		LoadSettingScene();
		SavePref();
	}

	private void LoadSettingScene()
	{
		if (LobbyState.currentLobbyState.Equals(LobbyState.state.IDLE))
		{
			StartCoroutine(PlayManage.Instance.LoadScene("Setting"));
		}
	}

	private void SavePref()
	{
		PlayManage.Instance.SaveData();
		PlayerPrefs.Save();
	}
}
