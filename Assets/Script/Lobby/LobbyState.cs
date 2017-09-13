using System.Collections;
using System.Collections.Generic;

public static class LobbyState {
	
	public enum state
	{
		DEFAULT,
		IDLE,
		WAITFORPLAYING
	}

	private static state lobbyState = state.IDLE;
	
	public static state currentLobbyState { get { return lobbyState; } set { lobbyState = value; } }

}
