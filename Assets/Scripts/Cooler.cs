using System;
using System.Collections;
using UnityEngine;

public class Cooler : Enemies
{
	private void Awake()
	{
		this.damageBefore = this.damage;
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
				this.attack();
			}
			else if (this.distanceWithHero >= this.distanceMax && this.distanceWithHero < 4f)
			{
				this.rdAction = UnityEngine.Random.Range(0, 4);
				if (this.rdAction < 2)
				{
					this.skillExplosive();
				}
				else if (this.rdAction == 2)
				{
					this.punchExplosive();
				}
				else
				{
					this.skillCircle();
				}
			}
			else if (this.distanceWithHero >= 4f && this.distanceWithHero < 7f)
			{
				this.rdAction = UnityEngine.Random.Range(0, 3);
				if (this.rdAction < 2)
				{
					this.skillCircle();
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

	public override void attack()
	{
		this.canGetHit = false;
		this.count++;
		if (this.count < 3)
		{
			this.punchBase();
		}
		else
		{
			this.rdAction = UnityEngine.Random.Range(0, 3);
			if (this.rdAction < 2)
			{
				this.punchExplosive();
			}
			else
			{
				this.skillExplosive();
			}
			this.count = 0;
		}
	}

	private void punchBase()
	{
		this.mesh.sortingOrder = -100;
		this.damage = this.damageBefore;
		this._animations.playAnimation(this._animations.attack, false);
		base.playAudio(this.audioAttack);
		base.playAudio(this.audioAttack, 0.3f);
	}

	private void punchExplosive()
	{
		this.mesh.sortingOrder = 100;
		this.damage = this.damageBefore + this.damageBefore / 2;
		this._animations.playAnimation(this._animations.skill, false);
		this.box.size = new Vector2(20f, 3f);
		base.playAudio(this.audioExplosive, 0.5f);
	}

	private void skillExplosive()
	{
		this.canGetHit = false;
		this.mesh.sortingOrder = 100;
		this.damage = 2 * this.damageBefore;
		this._animations.playAnimation(this._animations.skill2, false);
		base.transform.position = new Vector3(this.hero.transform.position.x, this.posYBefore, 0f);
		this.box.size = new Vector2(28f, 3f);
		base.playAudio(this.audioLaser, 0.7f);
		base.playAudio(this.audioLaser, 1f);
		base.playAudio(this.audioLaser, 1.3f);
		base.playAudio(this.audioLaser, 1.6f);
	}

	private void skillCircle()
	{
		this.canGetHit = false;
		this.mesh.sortingOrder = 100;
		this.damage = this.damageBefore;
		this._animations.playAnimation(this._animations.skill3, false);
		base.StartCoroutine(this.boxCircleControl());
		base.playAudio(this.audioCircle, 0.5f);
	}

	private IEnumerator boxCircleControl()
	{
		yield return new WaitForSeconds(0.8f);
		this.boxSkillCircle.SetActive(true);
		yield return new WaitForSeconds(0.6f);
		this.boxSkillCircle.SetActive(false);
		yield break;
	}

	public override void hit(int _damage)
	{
		base.hit(_damage);
		this.rdAction = UnityEngine.Random.Range(0, 2);
		if (this.rdAction == 0)
		{
			base.playAudio(this.hit1);
		}
		else
		{
			base.playAudio(this.hit2);
		}
	}

	private int count;

	private int rdAction;

	private Vector3 pos;

	public MeshRenderer mesh;

	private new int damageBefore;

	public GameObject boxSkillCircle;

	public BoxCollider2D box;

	public AudioClip audioExplosive;

	public AudioClip audioLaser;

	public AudioClip audioCircle;

	public AudioClip hit1;

	public AudioClip hit2;
}
