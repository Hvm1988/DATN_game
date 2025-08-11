using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	private void Awake()
	{
		Timer.ins = this;
		this._animator = this.txtTime.gameObject.GetComponent<Animator>();
	}

	private void timeCountDown()
	{
		this.timeCount--;
		if (this.timeCount <= 30 && this.setColor)
		{
			this.txtTime.color = this.shortTime;
			this._animator.enabled = true;
			this.setColor = false;
			this._gamemanager.soundBgHeroDyingOutControl();
		}
		if (this.timeCount <= 15 && this.imgTimeOutActive)
		{
			this.imgTimeOutActive = false;
			this.timeOut.SetActive(true);
		}
		this.minute = this.timeCount / 60;
		this.seconds = this.timeCount % 60;
		this.txtTime.text = this.minute.ToString() + ": " + this.seconds.ToString();
		if (!this._gamemanager.tutorialDone && this.timeCount <= 15)
		{
			for (int i = 0; i < this._gamemanager.enemiesPlaying.Count; i++)
			{
				this._gamemanager.enemiesPlaying[i].hit(100000);
			}
		}
		if (this.timeCount <= 0)
		{
			this.endTimeCountDown();
			this._gamemanager.gameLose();
		}
	}

	public void startTimeCountDown()
	{
		this.timeCount = 180;
		this.setColor = true;
		this.imgTimeOutActive = true;
		this.timeOut.SetActive(false);
		this.txtTime.color = this.lengthTime;
		this._animator.enabled = false;
		base.InvokeRepeating("timeCountDown", 1f, 1f);
	}

	public void startTimeCountDown(int _isTime)
	{
		this.timeCount = _isTime;
		this.setColor = true;
		this.imgTimeOutActive = true;
		this.timeOut.SetActive(false);
		this.txtTime.color = this.lengthTime;
		this._animator.enabled = false;
		base.InvokeRepeating("timeCountDown", 1f, 1f);
	}

	public void endTimeCountDown()
	{
		this._animator.enabled = false;
		base.CancelInvoke("timeCountDown");
	}

	public int getTimeRemaining()
	{
		return this.timeCount;
	}

	public static Timer ins;

	public const int timeStart = 180;

	public int timeCount;

	public Color lengthTime;

	public Color shortTime;

	private bool setColor;

	private bool imgTimeOutActive;

	public Text txtTime;

	private int minute;

	private int seconds;

	private Animator _animator;

	public GameManager _gamemanager;

	public GameObject timeOut;
}
