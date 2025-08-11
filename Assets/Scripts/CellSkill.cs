using System;
using UnityEngine;

public class CellSkill : MonoBehaviour
{
	private void OnEnable()
	{
		if (this.parrent._animations.transform.localEulerAngles.y == 0f)
		{
			base.transform.localPosition = this.posLeft;
		}
		else
		{
			base.transform.localPosition = this.posRight;
		}
		this.obj.SetActive(true);
		this.impact.SetActive(false);
		base.Invoke("setAt", 1.5f);
		this.playAudio(this.audio_loop, true);
	}

	private void Update()
	{
		if (this.isMove)
		{
			base.transform.Translate(this.vecMove * 18f * Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (this.isBox && (col.gameObject.tag.Equals("hero") || col.gameObject.tag.Equals("platform")))
		{
			this.isBox = false;
			this.isMove = false;
			this.obj.SetActive(false);
			this.impact.SetActive(true);
			base.Invoke("disable", 1f);
			if (col.gameObject.tag.Equals("hero"))
			{
				this.parrent.hero.hit(this.parrent.damage);
			}
			this.playAudio(this.audio_impact, false);
		}
	}

	private void disable()
	{
		base.gameObject.SetActive(false);
	}

	private void setAt()
	{
		this.vecMove = this.parrent.hero.transform.position + Vector3.up - base.transform.position;
		this.vecMove = this.vecMove.normalized;
		this.isMove = true;
		this.isBox = true;
	}

	private void playAudio(AudioClip audioClip, bool isLoop)
	{
		if (GameConfig.soundVolume > 0f)
		{
			if (this._audio.volume != GameConfig.soundVolume)
			{
				this._audio.volume = GameConfig.soundVolume;
			}
			this._audio.clip = audioClip;
			this._audio.loop = isLoop;
			this._audio.Play();
		}
	}

	public Enemies parrent;

	public GameObject obj;

	public GameObject impact;

	private bool isMove;

	public Vector3 posLeft;

	public Vector3 posRight;

	private Vector3 vecMove;

	private bool isBox;

	public AudioSource _audio;

	public AudioClip audio_loop;

	public AudioClip audio_impact;
}
