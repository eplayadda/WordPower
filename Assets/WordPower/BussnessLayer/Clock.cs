using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{

	public Image nidle;

	public enum eClockState
	{
		none,
		play,
		pause
	}

	public eClockState currentClockStart;

	float minimum = 1.0F;
	float maximum = 0.0F;
	float t = 0.0f;
	public float speed = .1f;
	public float fillValue;
	UIManager uiManager;

	void Start ()
	{
		uiManager = UIManager.instance;
	}

	void Update ()
	{
		ClockData ();
	}


	public void PlayClock ()
	{
		currentClockStart = eClockState.play;
	}


	public void PauseClock ()
	{
		currentClockStart = eClockState.pause;
	}


	public void ResetClock ()
	{
		currentClockStart = eClockState.pause;
		float minimum = 1.0F;
		float maximum = 0.0F;
		t = 0f;
		fillValue = 1f;
		nidle.fillAmount = fillValue;
	}


	void ClockData ()
	{
		switch (currentClockStart) {
		case eClockState.play:
			{
				fillValue = Mathf.Lerp (minimum, maximum, t);
				t += speed * Time.deltaTime;
				nidle.fillAmount = fillValue;
				if (t >= 1)
					StartCoroutine ("OnTimeUp");
			}
			break;
		case eClockState.pause:
			{

			}
			break;
		}
	}

	IEnumerator OnTimeUp()
	{
		ResetClock ();
		uiManager.roomPanel.GetComponent<RoomUI> ().TimeUp ();
		yield return null;
	}
}
