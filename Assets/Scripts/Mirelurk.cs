using System;
using UnityEngine;

public class Mirelurk : Enemies
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
				this.attack();
			}
			else if (this.distanceWithHero >= this.distanceMax && this.distanceWithHero < 6f)
			{
				this.skill();
			}
			else
			{
				this.followHero();
			}
		}
	}

	private void followHero()
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

	public override void skill()
	{
		base.skill();
		this._animations.playAnimation(this._animations.skill, false);
		base.playAudio(this._audioSkill);
	}

	public AudioClip _audioSkill;
}
