using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : ManagerBase {

    

    private Text playerleveltext;
    private Text playerIDtext;
    private Text playerEXP;
    //private GameObject clientObject;
    private GameObject LoadingScreen;
    private Button MatchingCancleButton;

    private int level;
    private float EXP;
    private float maxEXP;


    protected new void Awake()
    {
        return;
    }

    protected override void Start()
    {
        base.Start();

        CalEXP();
        AudioManager.Instance.PlayBackGroundClipByName("Lobby",14.5f);

    }

    private void LobbyInit()
    {
        LobbyUIInit();
        LobbyObjectInit();        
    }

    private void LobbyUIInit()
    {
        playerleveltext = GameObject.Find("PlayerLevel").GetComponent<Text>();
        playerIDtext = GameObject.Find("PlayerID").GetComponent<Text>();
        playerEXP = GameObject.Find("PlayerExp").GetComponent<Text>();
        level = PlayManage.Instance.GetPlayerLevel();
        EXP = PlayManage.Instance.GetEXP();
        if (level < 10)
        {
            playerleveltext.text = "Level : 0" + level.ToString();
        }
        else
        {
            playerleveltext.text = "Level : " + level.ToString();
        }
        playerIDtext.text = PlayManage.Instance.PlayerID;
        maxEXP = PlayManage.Instance.GetMaxEXP();
        playerEXP.text = "EXP : " + EXP.ToString("N0") + " / " + maxEXP.ToString("N0");
        LobbyState.currentLobbyState = LobbyState.state.IDLE;
    }

    private void LobbyObjectInit()
    {
		LoadingScreen = GameObject.Find("Loading");
        LoadingScreen.SetActive(false);
    }
    private void CalEXP()
    {
        if (EXP >= maxEXP)
        {
            EXP -= maxEXP;
            level += 1;
            PlayManage.Instance.SaveData();
            LobbyInit();
        }
    }

    public bool GetIsGameMatching()
    {
        return (LoadingScreen.activeSelf.Equals(true)) ? true : false;
    }
}
