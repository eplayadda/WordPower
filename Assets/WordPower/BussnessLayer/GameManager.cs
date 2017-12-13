using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public enum eRoomStatus
	{
		none,
		play,
		gameOver
	}
	public static GameManager instace;
	public eRoomStatus currRoomStatus;

	void Awake()
	{
		if (instace == null)
			instace = this;
	}
}
