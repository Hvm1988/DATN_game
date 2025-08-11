using System;
using UnityEngine;

public class ShieldManBoss : Enemies
{
	public override void control()
	{
		if (!GameSave.gamePasue && !GameSave.endGame && !this.isDie)
		{
			if (this.hero.transform.position.x < base.transform.position.x)
			{
				this._animations.transform.localEulerAngles = Vector3.zero;
				this.distanceWithHero = base.transform.position.x - this.hero.transform.position.x;
			}
			else
			{
				this._animations.transform.localEulerAngles = this.vectorMoveRotion;
				this.distanceWithHero = this.hero.transform.position.x - base.transform.position.x;
			}
			if (this.distanceWithHero < this.distanceMax && this.distanceWithHero > this.distanceMin)
			{
				this.rdSkill = UnityEngine.Random.Range(0, 2);
				if (this.rdSkill == 0)
				{
					this.attack();
				}
				else
				{
					this.skill();
				}
			}
			else if (this.distanceWithHero >= this.distanceMax && this.distanceWithHero < 8f)
			{
				this.skill();
			}
			else
			{
				if (base.transform.position.x >= this.hero.transform.position.x)
				{
					this.posFllow = this.hero.transform.position.x + UnityEngine.Random.Range(this.distanceMin, this.distanceMax);
					if (this.posFllow > this._gamemanager.doorRight.transform.position.x - 2f)
					{
						this.posFllow = this.hero.transform.position.x - UnityEngine.Random.Range(this.distanceMin, this.distanceMax);
					}
				}
				else
				{
					this.posFllow = this.hero.transform.position.x - UnityEngine.Random.Range(this.distanceMin, this.distanceMax);
					if (this.posFllow < this._gamemanager.doorLeft.transform.position.x + 2f)
					{
						this.posFllow = this.hero.transform.position.x + UnityEngine.Random.Range(this.distanceMin, this.distanceMax);
					}
				}
				this.updatePosition();
			}
		}
	}

	public override void skill()
	{
		base.skill();
		if (this.hero.transform.position.x < base.transform.position.x)
		{
			this._animations.transform.localEulerAngles = Vector3.zero;
			this.pos = base.transform.position - new Vector3(5f, 0f, 0f);
		}
		else
		{
			this._animations.transform.localEulerAngles = this.vectorMoveRotion;
			this.pos = base.transform.position + new Vector3(5f, 0f, 0f);
		}
		this._animations.playAnimation(this._animations.skill, false);
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			this.pos,
			"delay",
			0.3f,
			"time",
			0.3f,
			"easeyype",
			iTween.EaseType.linear
		}));
		base.playAudio(this._audioSkill);
	}

	public override void die()
	{
		base.die();
		this.boxSkill.SetActive(false);
	}

	public GameObject boxSkill;

	public AudioClip _audioSkill;

	private Vector3 pos;

	private int rdSkill;
}
