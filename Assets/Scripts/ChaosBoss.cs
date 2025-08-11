using System;
using UnityEngine;

public class ChaosBoss : Enemies
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
			else if (this.distanceWithHero >= this.distanceMax && this.distanceWithHero < 7f)
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
		this._animations.playAnimation(this._animations.skill, false);
		base.playAudio(this.audioSkill);
	}

	public AudioClip audioSkill;

	private int rdSkill;
}
