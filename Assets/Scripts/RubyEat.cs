using System;
using UnityEngine;

public class RubyEat : MonoBehaviour
{
	private void Awake()
	{
		this._audio = base.GetComponent<AudioSource>();
	}

	public void OnReady(float posX)
	{
		this.canEat = true;
		this.eff.SetActive(true);
		this.effImpact.SetActive(false);
		this._sprite.SetActive(true);
		this.value = 1;
		this.rd = UnityEngine.Random.Range(0, 2);
		base.transform.position = ((this.rd != 0) ? new Vector3(posX, 2.8f, 0f) : new Vector3(posX, -0.3f, 0f));
		base.Invoke("disable", 5f);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (this.canEat && other.gameObject.tag.Equals("hero"))
		{
			this.canEat = false;
			this.eff.SetActive(false);
			this.effImpact.SetActive(true);
			this._sprite.SetActive(false);
			GameSave.getRuby += this.value;
			this.number.set_numPink(this.value);
			this.number.gameObject.SetActive(true);
			if (GameConfig.soundVolume > 0f)
			{
				if (this._audio.volume != GameConfig.soundVolume)
				{
					this._audio.volume = GameConfig.soundVolume;
				}
				this._audio.Play();
			}
			base.CancelInvoke("disable");
		}
	}

	private void disable()
	{
		this.canEat = false;
		this.eff.SetActive(false);
		this.effImpact.SetActive(false);
		this._sprite.SetActive(false);
	}

	public GameObject eff;

	public GameObject effImpact;

	public GameObject _sprite;

	private int value;

	public Set_number_show number;

	private int rd;

	private AudioSource _audio;

	public bool canEat;
}
