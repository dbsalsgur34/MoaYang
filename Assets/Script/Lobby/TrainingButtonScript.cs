using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingButtonScript : MonoBehaviour {
	public Button traningButton;

	void Start()
	{
		traningButton.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		LoadTrainingScene();
		SavePref();
	}

	private void LoadTrainingScene()
	{
		if (LobbyState.currentLobbyState.Equals(LobbyState.state.IDLE))
		{
			StartCoroutine(PlayManage.Instance.LoadScene("Training"));
		}
	}

	private void SavePref()
	{
		PlayManage.Instance.SaveData();
		PlayerPrefs.Save();
	}
}
