using System;
using System.Collections;
using UnityEngine;

public class Enemies : Person
{
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void resetObjStart()
	{
		if (!this._gamemanager.isTestGame && (this._enemyDefine != null || this._enemyDefine._name.Equals(string.Empty)))
		{
			this.HP = this._enemyDefine.hp;
			this.damage = this._enemyDefine.damage;
			this.defend = this._enemyDefine.defense;
			this.speed = this._enemyDefine.speed;
			this.EXP = this._enemyDefine.exp;
			float num = Mathf.Pow(this._enemyDefine.increase, (float)this.level);
			this.HP = (int)((float)this.HP * num);
			this.damage = (int)((float)this.damage * num);
			this.defend = (int)((float)this.defend * num);
			this.EXP = (int)((float)this.EXP * num);
			if (DataHolder.difficult == 1)
			{
				this.HP += (int)((float)this.HP * GameConfig.difficultNormal);
				this.damage += (int)((float)this.damage * GameConfig.difficultNormal);
				this.defend += (int)((float)this.defend * GameConfig.difficultNormal);
				this.EXP += (int)((float)this.EXP * GameConfig.difficultNormal);
			}
			else if (DataHolder.difficult == 2)
			{
				this.HP += (int)((float)this.HP * GameConfig.difficultHard);
				this.damage += (int)((float)this.damage * GameConfig.difficultHard);
				this.defend += (int)((float)this.defend * GameConfig.difficultHard);
				this.EXP += (int)((float)this.EXP * GameConfig.difficultHard);
			}
		}
		this.damageBefore = this.damage;
		this._animations.playAnimation(this._animations.idle, true);
		this.canGetHit = true;
		this.isDie = false;
		this._animatorHit = base.GetComponent<Animator>();
		if (this.isBoss)
		{
			BossInfo.instance.onSetup(this._name, this.HP, this.avatar);
			this._gamemanager.mapTurnObj.SetActive(false);
			this._gamemanager.BossInfoObj.SetActive(true);
		}
		if (base.transform.position.x < this.hero.transform.position.x)
		{
			this.posFllow = this._gamemanager.doorLeft.transform.position.x + 2f;
		}
		else
		{
			this.posFllow = this._gamemanager.doorRight.transform.position.x - 2f;
		}
		this.updatePosition();
	}

	public virtual void control()
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

	public virtual void attack()
	{
		this.canGetHit = false;
		this._animations.playAnimation(this._animations.attack, false);
		this.playAudio(this.audioAttack);
		if (this.attEffect != null)
		{
			base.StartCoroutine(this.showEffect(this.attEffect, this.timeDelayEffect, this.timeDestroyEffect));
		}
	}

	public virtual void skill()
	{
		this.canGetHit = false;
	}

	public virtual void updatePosition()
	{
		this.canGetHit = false;
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
		base.StartCoroutine(this.DelayControl(this.timeDistance * this.speed));
	}

	private IEnumerator DelayControl(float timeDelay)
	{
		yield return new WaitForSeconds(timeDelay);
		this.control();
		yield break;
	}

	public void resetObj()
	{
		this.canGetHit = true;
		iTween.Stop();
		this._animations.playAnimation(this._animations.idle, true);
	}

	public virtual void idle()
	{
		if (!this.isDie)
		{
			this._animations.playAnimation(this._animations.idle, true);
			this.canGetHit = true;
		}
	}

	public override void hit(int _damage)
	{
		if (!GameSave.endGame && !GameSave.gamePasue && !this.isDie)
		{
			this.rdOneHit = UnityEngine.Random.Range(0, 100);
			if (this.rdOneHit < GameSave.heroOneHit)
			{
				this.damageHit = (((int)(1.5f * (float)_damage - (float)this.defend) <= 1) ? 1 : ((int)(1.5f * (float)_damage - (float)this.defend)));
				this.oneHitEffect.showEffectOneHit(this.damageHit);
			}
			else
			{
				this.damageHit = ((_damage - this.defend <= 1) ? 1 : (_damage - this.defend));
				this.hitEffect.showEffect(this.damageHit);
			}
			this.HP -= this.damageHit;
			this.showHitParticle();
			UIHeroInfo.ins.comboCount();
			if (this.isBoss)
			{
				BossInfo.instance.updateUI(this.HP);
			}
			if (this.HP < 0)
			{
				this.die();
			}
			else
			{
				if (this.canGetHit)
				{
					this._animations.playAnimation(this._animations.hit, false);
					if (this._listSkill.avoidSkill)
					{
						base.StartCoroutine(this.avoid_attack_of_hero());
					}
					if (this._listSkill.hadFightsback)
					{
						base.StartCoroutine(this.fightsback());
					}
				}
				if (this._animatorHit != null)
				{
					this._animatorHit.Play("enemyHit");
				}
			}
		}
	}

	public override void die()
	{
		this.isDie = true;
		iTween.Stop(base.gameObject);
		this._animations.playAnimation(this._animations.die, false);
		if (this._animations.boxFake != null)
		{
			this._animations.boxFake.SetActive(false);
		}
		if (this._animations.boxFakeSkill != null)
		{
			this._animations.boxFakeSkill.SetActive(false);
		}
		if (this.addRigibodyWhenDie)
		{
			iTween.MoveTo(base.gameObject, new Vector3(base.transform.position.x, -1f, 0f), 1f);
		}
		this._gamemanager.checkGame(this);
		base.StartCoroutine(this.disableObj());
		if (this._gamemanager.tutorialDone)
		{
			ListCoins.ins.showCoin(base.transform.position);
			GameSave.getExp += this.EXP + this.EXP * GameConfig.addExpDropBase / 100;
			this.hero.addExp(this.EXP + this.EXP * GameConfig.addExpDropBase / 100);
			UIHeroInfo.ins.updateUIEXP();
		}
		this.playAudio(this.audioDie);
		if (this.hadItem)
		{
			this._gamemanager.instanceItem(base.transform.position.x);
		}
		if (this.materialCodeDrop != 0)
		{
			this._gamemanager.instanceMaterial(this.materialCodeDrop, this.materialNumDrop, base.transform.position.x);
		}
		EnemiesPooling.instance.listEnemy.Add(this);
		if (this.isBoss)
		{
			DataHolder.Instance.missionData.addDone(null, "KILL-BOSS", 1);
		}
		else
		{
			DataHolder.Instance.missionData.addDone(null, "KILL-MONS", 1);
			DataHolder.Instance.achievementData.addDone(null, "KILL-MONSTER", 1);
			DataHolder.Instance.playerData.addTotalKillMons(1);
		}
	}

	public void stopAudio()
	{
		this._audio.Stop();
	}

	public IEnumerator avoid_attack_of_hero()
	{
		yield return new WaitForSeconds(0.4f);
		if (!this.isDie)
		{
			if (base.transform.position.x > this.hero.transform.position.x)
			{
				this.posFllow = this.hero.transform.position.x + UnityEngine.Random.Range(3f, 5f);
			}
			else
			{
				this.posFllow = this.hero.transform.position.x - UnityEngine.Random.Range(3f, 5f);
			}
			this.updatePosition();
		}
		yield break;
	}

	private IEnumerator fightsback()
	{
		yield return new WaitForSeconds(0.3f);
		if (!this.isDie)
		{
			this.attack();
		}
		yield break;
	}

	private IEnumerator disableObj()
	{
		yield return new WaitForSeconds(3f);
		base.gameObject.SetActive(false);
		yield break;
	}

	public void triggerHero()
	{
		this.hero.hit(this.damage);
		if (this.hero.returnDamage)
		{
			this.hit((int)((float)this.damage * this.hero.percentReturnDamage) + this.defend);
		}
	}

	public void triggerHero(int _damage)
	{
		this.hero.hit(_damage);
		if (this.hero.returnDamage)
		{
			this.hit((int)((float)_damage * this.hero.percentReturnDamage) + this.defend);
		}
	}

	public void superSkillHero(float posX)
	{
		this.hero.superHit(posX);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!this.isDie)
		{
			if (other.gameObject.tag.Equals("heroAtt"))
			{
				this.hit(GameSave.damageAtt);
				this.hero.playAudioImpact();
			}
			else if (other.gameObject.tag.Equals("mutilSkill"))
			{
				this.hit(GameSave.damageSkill2);
				if (this.hero.transform.position.x < base.transform.position.x)
				{
					iTween.MoveTo(base.gameObject, new Vector3(base.transform.position.x + 0.5f, base.transform.position.y, 0f), 0.2f);
				}
				else
				{
					iTween.MoveTo(base.gameObject, new Vector3(base.transform.position.x - 0.5f, base.transform.position.y, 0f), 0.2f);
				}
			}
			else if (other.gameObject.tag.Equals("circle"))
			{
				this.hit(GameSave.damageSkill3);
				if (this.hero.transform.position.x < base.transform.position.x)
				{
					iTween.MoveTo(base.gameObject, new Vector3(base.transform.position.x + 0.5f, base.transform.position.y, 0f), 0.2f);
				}
				else
				{
					iTween.MoveTo(base.gameObject, new Vector3(base.transform.position.x - 0.5f, base.transform.position.y, 0f), 0.2f);
				}
			}
			else if (other.gameObject.tag.Equals("Pet"))
			{
				this.hit(GameSave.damagePet);
			}
			else if (other.gameObject.tag.Equals("tornado"))
			{
				this.hit(GameSave.damageRotation);
			}
		}
	}

	private IEnumerator showEffect(GameObject obj, float timeDelay, float timeDestroy)
	{
		yield return new WaitForSeconds(timeDelay);
		obj.SetActive(true);
		yield return new WaitForSeconds(timeDestroy);
		obj.SetActive(false);
		yield break;
	}

	private void showHitParticle()
	{
		if (!this.hitEffectParticle.activeSelf)
		{
			base.StartCoroutine(this.hitParticle());
		}
	}

	private IEnumerator hitParticle()
	{
		this.hitEffectParticle.SetActive(true);
		yield return new WaitForSeconds(1f);
		this.hitEffectParticle.SetActive(false);
		yield break;
	}

	public void playAudio(AudioClip _audioClip)
	{
		if (GameConfig.soundVolume > 0f)
		{
			if (this._audio.volume != GameConfig.soundVolume)
			{
				this._audio.volume = GameConfig.soundVolume;
			}
			this._audio.clip = _audioClip;
			this._audio.Play();
		}
	}

	public void playAudio(AudioClip _audioClip, float timeDelay)
	{
		base.StartCoroutine(this.ST_playAudio(_audioClip, timeDelay));
	}

	private IEnumerator ST_playAudio(AudioClip _audioClip, float timeDelay)
	{
		yield return new WaitForSeconds(timeDelay);
		this.playAudio(_audioClip);
		yield break;
	}

	public virtual void setVolumeSound(float _volume)
	{
		this._audio.volume = _volume;
	}

	public string code;

	public bool isBoss;

	public Hero hero;

	public GameManager _gamemanager;

	public float distanceMax;

	public float distanceMin;

	public EnemiesAnimationSpine _animations;

	[HideInInspector]
	public float posFllow;

	[HideInInspector]
	public float timeDistance;

	[HideInInspector]
	public int Hptotal;

	private int rd;

	[HideInInspector]
	public bool canGetHit;

	public GameObject attEffect;

	public float timeDelayEffect;

	public float timeDestroyEffect;

	[HideInInspector]
	public float distanceWithHero;

	private Animator _animatorHit;

	public bool addRigibodyWhenDie;

	public float posYBefore;

	public GameObject hitEffectParticle;

	public AudioSource _audio;

	public AudioClip audioAttack;

	public AudioClip audioDie;

	public Enemies.ListSkill _listSkill;

	public EnemyDefine _enemyDefine;

	[HideInInspector]
	public float increase;

	public Sprite avatar;

	public bool hadItem;

	public int materialCodeDrop;

	public int materialNumDrop;

	[HideInInspector]
	public int damageBefore;

	[Serializable]
	public class ListSkill
	{
		public bool avoidSkill;

		public bool hadFightsback;
	}
}
