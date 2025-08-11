using System;
using System.Collections;
using UnityEngine;

public class Frieza : Enemies
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
					this.skill_2();
				}
			}
			else
			{
				this.rdSkill = UnityEngine.Random.Range(0, 3);
				if (this.rdSkill < 2)
				{
					this.skill_1();
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
	}

	public override void skill()
	{
		base.skill();
		this._meshRenderer.sortingOrder = 100;
	}

	private void skill_1()
	{
		this.damage = (int)((float)this.damageBefore * 0.8f);
		this.skill();
		if (this.hero.transform.position.x < base.transform.position.x)
		{
			this._animations.transform.localEulerAngles = Vector3.zero;
		}
		else
		{
			this._animations.transform.localEulerAngles = this.vectorMoveRotion;
		}
		this._animations.playAnimation(this._animations.skill, false);
		base.playAudio(this.audioSkill_1, 0f);
		this._gamemanager.playCamAnimation(0.8f, "cameraVibrate2");
	}

	private void skill_2()
	{
		this.damage = (int)((float)this.damageBefore * 0.8f);
		this.skill();
		this.rdSkill = UnityEngine.Random.Range(0, 2);
		if (this.rdSkill == 0)
		{
			this.posFllow = this.hero.transform.position.x + UnityEngine.Random.Range(1f, 2f);
		}
		else
		{
			this.posFllow = this.hero.transform.position.x - UnityEngine.Random.Range(1f, 2f);
		}
		if (this.posFllow < base.transform.position.x)
		{
			this._animations.transform.localEulerAngles = Vector3.zero;
			this.timeDistance = base.transform.position.x - this.posFllow;
		}
		else
		{
			this._animations.transform.localEulerAngles = this.vectorMoveRotion;
			this.timeDistance = this.posFllow - base.transform.position.x;
		}
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			new Vector3(this.posFllow, base.transform.position.y, 0f),
			"time",
			this.timeDistance * this.speed,
			"easetype",
			iTween.EaseType.linear
		}));
		this._animations.playAnimation(this._animations.move, true);
		this.playAnimation(this.timeDistance * this.speed, this._animations.skill2, false);
		base.playAudio(this.audioSkill_2, this.timeDistance * this.speed);
		this._gamemanager.playCamAnimation(1f + this.timeDistance * this.speed, "cameraVibrate2");
	}

	public override void updatePosition()
	{
		base.updatePosition();
		this._meshRenderer.sortingOrder = -10;
	}

	public override void attack()
	{
		this.damage = this.damageBefore;
		base.attack();
		this._meshRenderer.sortingOrder = -10;
	}

	public override void hit(int _damage)
	{
		base.hit(_damage);
		base.playAudio(this.hit1);
		this._meshRenderer.sortingOrder = -10;
	}

	private void playAnimation(float timeDelay, string nameAnimation, bool loop)
	{
		base.StartCoroutine(this.STplayAnimation(timeDelay, nameAnimation, loop));
	}

	private IEnumerator STplayAnimation(float timeDelay, string nameAnimation, bool loop)
	{
		yield return new WaitForSeconds(timeDelay);
		if (this.hero.transform.position.x < base.transform.position.x)
		{
			this._animations.transform.localEulerAngles = Vector3.zero;
		}
		else
		{
			this._animations.transform.localEulerAngles = this.vectorMoveRotion;
		}
		this._animations.playAnimation(nameAnimation, loop);
		yield break;
	}

	private int rdSkill;

	public MeshRenderer _meshRenderer;

	public AudioClip hit1;

	public AudioClip hit2;

	public AudioClip audioSkill_1;

	public AudioClip audioSkill_2;

	public AudioClip audioStart;
}
