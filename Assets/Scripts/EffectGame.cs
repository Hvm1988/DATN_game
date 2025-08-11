using System;
using UnityEngine;

public class EffectGame : MonoBehaviour
{
	public void OnEnableObj()
	{
		if (!GameSave.endGame)
		{
			base.transform.position = new Vector3(this.target.transform.position.x + 5f, 8f, 0f);
			this.vt = (this.target.transform.position - base.transform.position).normalized;
			this.eff.SetActive(true);
			this.effImpact.SetActive(false);
			this.effImpactGround.SetActive(false);
			this.isMove = true;
			this.canPhysics = true;
			if (GameConfig.soundVolume > 0f)
			{
				this._audioEff.Play();
			}
		}
	}

	private void FixedUpdate()
	{
		if (this.isMove)
		{
			base.transform.Translate(this.vt * 20f * Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (this.canPhysics)
		{
			if (coll.gameObject.tag.Equals("hero"))
			{
				this.canPhysics = false;
				this.isMove = false;
				this.eff.SetActive(false);
				this.effImpact.SetActive(true);
				this.target.hit(100);
				if (GameConfig.soundVolume > 0f)
				{
					if (this._audioEffImpact.volume != GameConfig.soundVolume)
					{
						this._audioEffImpact.volume = GameConfig.soundVolume;
					}
					this._audioEffImpact.Play();
				}
			}
			else if (coll.gameObject.tag.Equals("platform"))
			{
				this.canPhysics = false;
				this.isMove = false;
				this.eff.SetActive(false);
				this.effImpactGround.SetActive(true);
				if (GameConfig.soundVolume > 0f)
				{
					if (this._audioEffImpact.volume != GameConfig.soundVolume)
					{
						this._audioEffImpact.volume = GameConfig.soundVolume;
					}
					this._audioImpactGround.Play();
				}
			}
		}
	}

	[HideInInspector]
	public Hero target;

	public GameObject eff;

	public GameObject effImpact;

	public GameObject effImpactGround;

	public AudioSource _audioEff;

	public AudioSource _audioEffImpact;

	public AudioSource _audioImpactGround;

	private bool isMove;

	private bool canPhysics;

	private Vector3 vt;
}
