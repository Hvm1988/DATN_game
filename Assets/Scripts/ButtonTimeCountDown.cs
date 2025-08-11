using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTimeCountDown : MonoBehaviour
{
	private void Start()
	{
		this.timeCount = this.time;
		this.img.fillAmount = 0f;
		this.txt.text = string.Empty;
	}

	private void Update()
	{
		if (this.timeCount < this.time)
		{
			this.timeCount += Time.deltaTime;
			this.ratio = (this.time - this.timeCount) / this.time;
			if (this.timeShow != this.time - this.timeCount)
			{
				this.timeShow = this.time - this.timeCount;
				this.txt.text = string.Format("{0:F1}", this.timeShow);
			}
			if (this.ratio > 1f)
			{
				this.ratio = 1f;
			}
			if (this.ratio < 0f)
			{
				this.ratio = 0f;
				this.txt.text = string.Empty;
			}
			this.img.fillAmount = this.ratio;
		}
	}

	[HideInInspector]
	public float timeCount;

	public float time;

	public Image img;

	public float ratio;

	public Text txt;

	private float timeShow;

	public bool isActive;
}
