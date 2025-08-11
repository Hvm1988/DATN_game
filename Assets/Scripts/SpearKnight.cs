using System;
using System.Collections;
using UnityEngine;

public class SpearKnight : Enemies
{
	private void attack1()
	{
		this._animations.playAnimation(this._animations.attack, false);
		this._animations.boxFake = this.boxNormal;
		base.playAudio(this.audioAttack);
	}

	private void attack2()
	{
		this._animations.playAnimation(this._animations.skill, false);
		this._animations.boxFake = this.boxRotation;
		if (this._animations.transform.localEulerAngles.y == 0f)
		{
			iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
			{
				"position",
				new Vector3(this.hero.transform.position.x - 4f, this.posYBefore, 0f),
				"time",
				1,
				"easetype",
				iTween.EaseType.linear
			}));
		}
		else
		{
			iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
			{
				"position",
				new Vector3(this.hero.transform.position.x + 4f, this.posYBefore, 0f),
				"time",
				1,
				"easetype",
				iTween.EaseType.linear
			}));
		}
		base.playAudio(this._audioRotation);
		base.StartCoroutine(this.delay());
	}

	private IEnumerator delay()
	{
		yield return new WaitForSeconds(1f);
		if (this._animations.transform.localEulerAngles.y == 0f)
		{
			this._animations.transform.localEulerAngles = this.vectorMoveRotion;
		}
		else
		{
			this._animations.transform.localEulerAngles = Vector3.zero;
		}
		yield break;
	}

	private void attack3()
	{
		this._animations.playAnimation(this._animations.skill2, false);
		this._animations.boxFake = this.boxNormal;
		base.playAudio(this._audioLighting);
	}

	public override void skill()
	{
		base.skill();
		this._animations.playAnimation(this._animations.skill3, false);
		base.playAudio(this._audioSkill);
	}

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
				this.rdA = UnityEngine.Random.Range(0, 3);
				if (this.rdA == 0)
				{
					this.attack1();
				}
				else if (this.rdA == 1)
				{
					this.attack2();
				}
				else
				{
					this.attack3();
				}
			}
			else if (this.distanceWithHero >= this.distanceMax && this.distanceWithHero < 8f)
			{
				this.rdA = UnityEngine.Random.Range(0, 6);
				if (this.rdA < 4)
				{
					this.skill();
				}
				else
				{
					this.followHero();
				}
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

	private int rdA;

	public GameObject boxNormal;

	public GameObject boxRotation;

	public AudioClip _audioRotation;

	public AudioClip _audioLighting;

	public AudioClip _audioSkill;
}
