using System;
using System.Collections;
using UnityEngine;

public class Hero : Person
{
	private void Start()
	{
		this.hitAnimator = base.GetComponent<Animator>();
		this._audio = base.GetComponent<AudioSource>();
		this.readHeroData();
		Hero.heroCanAttack = true;
		this.arrow = 0;
		Hero.btnMoveClick = false;
		this.canMove = true;
	}

	private void Update()
	{
		if (this.isMove && !GameSave.endGame && this.canMove && Hero.animationDone)
		{
			base.transform.Translate(Vector3.right * this.speed * (float)this.arrow * Time.deltaTime);
			if (base.transform.position.x > CameraFollow.ins.zoneRight + 6f)
			{
				base.transform.position = new Vector3(CameraFollow.ins.zoneRight + 6f, base.transform.position.y, 0f);
			}
			else if (base.transform.position.x < CameraFollow.ins.zoneLeft - 6f)
			{
				base.transform.position = new Vector3(CameraFollow.ins.zoneLeft - 6f, base.transform.position.y, 0f);
			}
		}
		this.timeOutAttack += Time.deltaTime;
	}

	public override void hit(int _damage)
	{
		if (!GameSave.gamePasue && !GameSave.endGame)
		{
			this.rdOneHit = UnityEngine.Random.Range(0, 100);
			if (this.rdOneHit < GameSave.heroGetOneHit)
			{
				this.damageHit = (((int)(1.5f * (float)_damage) - this.defend <= 1) ? 1 : ((int)(1.5f * (float)_damage) - this.defend));
				this.oneHitEffect.showEffectOneHit(this.damageHit);
			}
			else
			{
				this.damageHit = ((_damage - this.defend <= 1) ? 1 : (_damage - this.defend));
				this.hitEffect.showEffect(this.damageHit);
			}
			this.HP -= this.damageHit;
			this.bgHitEffect.SetActive(true);
			if (this.HP <= 0)
			{
				this.die();
			}
			else
			{
				UIHeroInfo.ins.updateUIHP(this.HP);
				UIHeroInfo.ins.comborestart();
				this.hitAnimator.Play("heroHit");
				if (!this._audioSourceHitObj.isPlaying)
				{
					this.rdOneHit = UnityEngine.Random.Range(0, 3);
					if (this.rdOneHit == 0)
					{
						this.playAudio(this._audioSourceHitObj, this._audioGame.hit1);
					}
					else if (this.rdOneHit == 1)
					{
						this.playAudio(this._audioSourceHitObj, this._audioGame.hit2);
					}
					else
					{
						this.playAudio(this._audioSourceHitObj, this._audioGame.hit3);
					}
				}
				if (this.HP < this.HpDyingOut)
				{
					this._gameManager.soundBgHeroDyingOutControl();
				}
			}
		}
	}

	public void superHit(float posX)
	{
		if (!GameSave.gamePasue && !GameSave.endGame && !this.isDie && !Hero.is_longRotation && !Hero.jumping)
		{
			CameraFollow.ins.isMove = false;
			if (posX < base.transform.position.x)
			{
				if (base.transform.position.x - CameraFollow.ins.transform.position.x < 8f)
				{
					iTween.MoveTo(base.gameObject, new Vector3(base.transform.position.x + 0.5f, base.transform.position.y, 0f), 0.2f);
				}
			}
			else if (CameraFollow.ins.transform.position.x - base.transform.position.x < 8f)
			{
				iTween.MoveTo(base.gameObject, new Vector3(base.transform.position.x - 0.5f, base.transform.position.y, 0f), 0.2f);
			}
		}
	}

	public override void die()
	{
		this.isDie = true;
		UIHeroInfo.ins.updateUIHP(0);
		this.dieEffectPar.SetActive(true);
		this.animations.playAnimation(this.animations.dead, false);
		this._gameManager.gameLose();
		this.playAudio(this._audioSourceHitObj, this._audioGame.die);
		base.StartCoroutine(this.stopAudio());
		if (this.is_SSJ_up)
		{
			this.ssjEffect.SetActive(false);
			this.ssjEffect2.SetActive(false);
		}
		for (int i = 0; i < this.allBoxAttack.Length; i++)
		{
			this.allBoxAttack[i].gameObject.SetActive(false);
		}
	}

	private IEnumerator stopAudio()
	{
		this._audio.Stop();
		this._audioSoundImpact.Stop();
		this._audioGame.run.Stop();
		yield return new WaitForSeconds(3f);
		this._audioSourceHitObj.Stop();
		yield break;
	}

	public void setVolumeSound(float _volume)
	{
		this._audio.volume = _volume;
		this._audioSoundImpact.volume = _volume;
		this._audioSourceHitObj.volume = _volume;
		this._audioGame.run.volume = _volume;
	}

	public void heroStart()
	{
		this.animations.playAnimation(this.animations.start, false);
		this.playAudio(this._audioGame.start);
	}

	public void readHeroData()
	{
		this.HP = DataHolder.Instance.playerData.hp;
		this.HpBefore = this.HP;
		this.EXP = DataHolder.Instance.playerData.exp;
		this.level = DataHolder.Instance.playerData.level;
		this.numSKillActive = 0;
		this.damage = DataHolder.Instance.playerData.atk + DataHolder.Instance.skillData.getDamageAskill("NORMAL-ATK");
		GameSave.damageAtt = this.damage;
		GameSave.damageRotation = DataHolder.Instance.playerData.atk + DataHolder.Instance.skillData.getDamageAskill("TORNADO");
		if (DataHolder.Instance.skillData.isLearedSkill("FAST-PUNCH"))
		{
			GameSave.damageSkill2 = DataHolder.Instance.playerData.atk + DataHolder.Instance.skillData.getDamageAskill("FAST-PUNCH");
		}
		if (DataHolder.Instance.skillData.isLearedSkill("FAST-SHOOT"))
		{
			GameSave.damageSkill3 = DataHolder.Instance.playerData.atk + DataHolder.Instance.skillData.getDamageAskill("FAST-SHOOT");
			this.numSKillActive = 1;
		}
		if (DataHolder.Instance.skillData.isLearedSkill("KAMEHA"))
		{
			GameSave.damageSkill3 = DataHolder.Instance.playerData.atk + DataHolder.Instance.skillData.getDamageAskill("KAMEHA");
			this.numSKillActive = 2;
		}
		if (DataHolder.Instance.skillData.isLearedSkill("KENHKHI"))
		{
			GameSave.damageSkill3 = DataHolder.Instance.playerData.atk + DataHolder.Instance.skillData.getDamageAskill("KENHKHI");
			this.numSKillActive = 3;
		}
		if (DataHolder.Instance.skillData.isLearedSkill("RETURN-DAMAGE"))
		{
			this.returnDamage = true;
			this.percentReturnDamage = (float)DataHolder.Instance.skillData.getEffectASkill("RETURN-DAMAGE") / 100f;
		}
		else
		{
			this.returnDamage = false;
		}
		if (DataHolder.Instance.skillData.isLearedSkill("RESTORE-HP"))
		{
			this.numHpRestore = DataHolder.Instance.skillData.getEffectASkill("RESTORE-HP");
			base.StartCoroutine(this.restoreHp());
			BonusIconControl.ins.hp.SetActive(true);
		}
		this.defend = DataHolder.Instance.playerData.def;
		this.defenseBefore = this.defend;
		this.speed = (float)(10 + 10 * DataHolder.Instance.skillData.getEffectASkill("ADD-SPEED") / 100);
		this.dameAttackNormal = GameSave.damageAtt;
		this.dameSkill = GameSave.damageSkill2;
		this.dameSkillKenhKhi = GameSave.damageSkill3;
		if (DataHolder.watchedVideoAddStat)
		{
			this.HP += (int)((float)this.HP * 0.1f);
			this.HpBefore = this.HP;
			GameSave.damageAtt += (int)((float)GameSave.damageAtt * 0.1f);
			this.damage = GameSave.damageAtt;
			GameSave.damageSkill2 += (int)((float)GameSave.damageSkill2 * 0.1f);
			GameSave.damageSkill3 += (int)((float)GameSave.damageSkill3 * 0.1f);
			this.defend += (int)((float)this.defend * 0.1f);
			this.defenseBefore = this.defend;
		}
		if (PlayerPrefs.GetInt("theFirstGamePlay") == 0)
		{
			this.HP = 999999;
			this.HpBefore = this.HP;
			this.defend = 99999;
		}
		this.HpDyingOut = (int)((float)this.HP * 0.15f);
		UIHeroInfo.ins.HP = this.HP;
		UIHeroInfo.ins.updateUIEXP();
	}

	public void updateUIHero()
	{
		UIHeroInfo.ins.updateUIHP(this.HP);
		UIHeroInfo.ins.updateUIEXP();
	}

	public void addExp(int value)
	{
		DataHolder.Instance.playerData.addExp(value);
		if (DataHolder.Instance.playerData.level != this.level)
		{
		}
	}

	private IEnumerator restoreHp()
	{
		yield return new WaitForSeconds(5f);
		if (!this.isDie && !GameSave.gamePasue && !GameSave.endGame && this._gameManager.tutorialDone && this.HP < this.HpBefore)
		{
			this.HP += this.numHpRestore;
			if (this.HP > this.HpBefore)
			{
				this.HP = this.HpBefore;
			}
			UIHeroInfo.ins.updateUIHP(this.HP);
		}
		base.StartCoroutine(this.restoreHp());
		yield break;
	}

	public bool checkAction()
	{
		return !GameSave.gamePasue && Hero.heroCanAttack && !this.isDie;
	}

	public void attack()
	{
		if (this.checkAction())
		{
			Hero.heroCanAttack = false;
			if (this.timeOutAttack >= GameConfig.timeOutAttack)
			{
				this.countCombo = 0;
			}
			else
			{
				this.countCombo++;
				if (this.countCombo > 4)
				{
					this.countCombo = 0;
				}
			}
			if (this.countCombo == 0 || this.countCombo == 2)
			{
				this.animations.playAnimation(this.animations.punch1, false);
				this.playAudio(this._audioGame.attack);
			}
			else if (this.countCombo == 1)
			{
				this.animations.playAnimation(this.animations.punch2, false);
				this.playAudio(this._audioGame.attack);
			}
			else if (this.countCombo == 3)
			{
				this.animations.playAnimation(this.animations.punch3, false);
				this.playAudio(this._audioGame.attack);
			}
			else if (this.countCombo == 4)
			{
				this.animations.playAnimation(this.animations.punchCombo, false);
				this.playAudio(this._audioGame.combo, 0.3f);
			}
			this.timeOutAttack = 0f;
			Hero.animationDone = false;
			if (this.animations.transform.localEulerAngles.y == 0f)
			{
				if (base.transform.position.x - CameraFollow.ins.transform.position.x < 8f)
				{
					iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
					{
						"position",
						new Vector3(base.transform.position.x + 0.3f, base.transform.position.y, 0f),
						"time",
						0.2f,
						"delay",
						0.3f
					}));
				}
			}
			else if (CameraFollow.ins.transform.position.x - base.transform.position.x < 8f)
			{
				iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
				{
					"position",
					new Vector3(base.transform.position.x - 0.3f, base.transform.position.y, 0f),
					"time",
					0.2f,
					"delay",
					0.3f
				}));
			}
		}
		else if (Hero.jumping)
		{
			Hero.heroCanAttack = false;
			Hero.jumping = false;
			this.canMove = true;
			this.animations.playAnimation(this.animations.sortRotation, false);
			this.playAudio(this._audioGame.rotationSort);
			this.ssjEffect.SetActive(false);
			this.ssjEffect2.SetActive(false);
		}
	}

	public void jump()
	{
		if (this.checkAction())
		{
			Hero.heroCanAttack = false;
			this.rdAction = UnityEngine.Random.Range(0, 2);
			this.animations.playAnimation(this.animations.jump, false);
			Hero.jumping = true;
			this.playAudio(this._audioGame.jump);
		}
	}

	public void moveLeft()
	{
		CameraFollow.ins.isMove = true;
		this.arrow = -1;
		Hero.btnMoveClick = true;
		this.isMove = true;
		if (this.checkAction())
		{
			this.animations.gameObject.transform.localEulerAngles = this.vectorMoveRotion;
			this.animations.playAnimation(this.animations.move, true);
			if (GameConfig.soundVolume > 0f)
			{
				if (this._audioGame.run.volume != GameConfig.soundVolume)
				{
					this._audioGame.run.volume = GameConfig.soundVolume;
				}
				this._audioGame.run.Play();
			}
		}
		else if (Hero.is_longRotation || Hero.jumping || Hero.is_loopCombo)
		{
			this.animations.gameObject.transform.localEulerAngles = this.vectorMoveRotion;
		}
	}

	public void setRotationHero()
	{
		if (this.arrow == -1)
		{
			this.animations.gameObject.transform.localEulerAngles = this.vectorMoveRotion;
		}
		else if (this.arrow == 1)
		{
			this.animations.gameObject.transform.localEulerAngles = Vector3.zero;
		}
	}

	public void moveRight()
	{
		CameraFollow.ins.isMove = true;
		this.arrow = 1;
		Hero.btnMoveClick = true;
		this.isMove = true;
		if (this.checkAction())
		{
			this.animations.gameObject.transform.localEulerAngles = Vector3.zero;
			this.animations.playAnimation(this.animations.move, true);
			if (GameConfig.soundVolume > 0f)
			{
				if (this._audioGame.run.volume != GameConfig.soundVolume)
				{
					this._audioGame.run.volume = GameConfig.soundVolume;
				}
				this._audioGame.run.Play();
			}
		}
		else if (Hero.is_longRotation || Hero.jumping || Hero.is_loopCombo)
		{
			this.animations.gameObject.transform.localEulerAngles = Vector3.zero;
		}
	}

	public void idle()
	{
		Hero.btnMoveClick = false;
		this.isMove = false;
		this.arrow = 0;
		if (this.checkAction())
		{
			this.animations.playAnimation(this.animations.idle, true);
		}
	}

	public void skill_rotation()
	{
		if (this.checkAction())
		{
			this.animations.playAnimation(this.animations.longRotation, true);
			Hero.heroCanAttack = false;
			Hero.is_longRotation = true;
			this.playAudio(this._audioGame.rotationLong, 0.1f, 3.6f);
			this.ssjEffect.SetActive(false);
			this.ssjEffect2.SetActive(false);
		}
	}

	public void skill_2()
	{
		if (this.checkAction())
		{
			this.animations.playAnimation(this.animations.skill1, true);
			this.fakeMutilAtt.SetActive(true);
			this.playAudio(this._audioGame.fastAtt, 0.1f);
			Hero.animationDone = false;
			Hero.heroCanAttack = false;
			this.skill_playing = true;
		}
	}

	public void skill_3()
	{
		if (this.checkAction())
		{
			if (this.numSKillActive == 1)
			{
				this.animations.playAnimation(this.animations.skill3, true);
				this.fakeSkill3.SetActive(true);
				this.playAudio(this._audioGame.fastPunch, 0.2f, 1.3f);
			}
			else if (this.numSKillActive == 2)
			{
				this.animations.playAnimation(this.animations.skill2, true);
				this.fakeKameha.SetActive(true);
				this.playAudio(this._audioGame.kameha);
			}
			else if (this.numSKillActive == 3)
			{
				this.animations.playAnimation(this.animations.skill4, true);
				this.fakeCircle.SetActive(true);
				this.playAudio(this._audioGame.kenhkhi);
			}
			Hero.animationDone = false;
			Hero.heroCanAttack = false;
			this.skill_playing = true;
		}
	}

	public void SSJ()
	{
		if (this.checkAction())
		{
			this.animations.playAnimation(this.animations.ssj, false);
			Hero.animationDone = false;
			Hero.heroCanAttack = false;
			base.StartCoroutine(this.showEffectSSJ());
			GameSave.damageAtt += (int)((float)GameSave.damageAtt * 0.3f);
			GameSave.damageSkill2 += (int)((float)GameSave.damageSkill2 * 0.3f);
			GameSave.damageSkill3 += (int)((float)GameSave.damageSkill3 * 0.3f);
			this.defend += this.defend / 2;
			this.is_SSJ_up = true;
			this.skill_playing = true;
			BonusIconControl.ins.att.SetActive(true);
			BonusIconControl.ins.def.SetActive(true);
			this.playAudio(this._audioGame.ssj, 0.1f);
		}
	}

	public void win()
	{
		if (this.is_SSJ_up)
		{
			this.ssjEffect.SetActive(false);
			this.ssjEffect2.SetActive(false);
		}
	}

	private IEnumerator showEffectSSJ()
	{
		yield return new WaitForSeconds(0.7f);
		this.ssjEffect.SetActive(true);
		this.ssjEffect2.SetActive(true);
		yield return new WaitForSeconds(15f);
		this.ssjEffect.SetActive(false);
		this.ssjEffect2.SetActive(false);
		GameSave.damageAtt = this.dameAttackNormal;
		GameSave.damageSkill2 = this.dameSkill;
		GameSave.damageSkill3 = this.dameSkillKenhKhi;
		this.defend = this.defenseBefore;
		this.is_SSJ_up = false;
		if (!DataHolder.watchedVideoAddStat)
		{
			BonusIconControl.ins.att.SetActive(false);
			BonusIconControl.ins.def.SetActive(false);
		}
		yield break;
	}

	private IEnumerator STplayAudio(AudioClip _audioClip, float timeDelay, float timeLength)
	{
		yield return new WaitForSeconds(timeDelay);
		this._audio.clip = _audioClip;
		this._audio.loop = true;
		this._audio.Play();
		yield return new WaitForSeconds(timeLength);
		this._audio.Stop();
		yield break;
	}

	private IEnumerator STplayAudio(AudioClip _audioClip, float timeDelay)
	{
		yield return new WaitForSeconds(timeDelay);
		if (this._audio.volume != GameConfig.soundVolume)
		{
			this._audio.volume = GameConfig.soundVolume;
		}
		this._audio.clip = _audioClip;
		this._audio.loop = false;
		this._audio.Play();
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
			this._audio.loop = false;
			this._audio.Play();
		}
	}

	public void playAudio(AudioClip _audioClip, float _pitch, bool empty)
	{
		if (GameConfig.soundVolume > 0f)
		{
			if (this._audio.volume != GameConfig.soundVolume)
			{
				this._audio.volume = GameConfig.soundVolume;
			}
			this._audio.clip = _audioClip;
			this._audio.loop = false;
			this._audio.Play();
		}
	}

	public void playAudio(AudioClip _audioClip, float timeDelay)
	{
		if (GameConfig.soundVolume > 0f)
		{
			base.StartCoroutine(this.STplayAudio(_audioClip, timeDelay));
		}
	}

	public void playAudio(AudioClip _audioClip, float timeDelay, float timeLength)
	{
		if (GameConfig.soundVolume > 0f)
		{
			base.StartCoroutine(this.STplayAudio(_audioClip, timeDelay, timeLength));
		}
	}

	public void animationHitDone()
	{
		this._animHitDone = true;
	}

	public void playAudio(AudioSource _audioSource, AudioClip _audioClip)
	{
		if (GameConfig.soundVolume > 0f)
		{
			if (_audioSource.volume != GameConfig.soundVolume)
			{
				_audioSource.volume = GameConfig.soundVolume;
			}
			_audioSource.clip = _audioClip;
			_audioSource.Play();
		}
	}

	public void playAudioImpact()
	{
		if (!this._audioSoundImpact.isPlaying && GameConfig.soundVolume > 0f)
		{
			if (this._audioSoundImpact.volume != GameConfig.soundVolume)
			{
				this._audioSoundImpact.volume = GameConfig.soundVolume;
			}
			this._audioSoundImpact.Play();
		}
	}

	private const int distanceWithCam = 6;

	public HeroAnimationsControl animations;

	public GameManager _gameManager;

	public static bool heroCanAttack;

	private int rdAction;

	public bool isMove;

	public static bool animationDone = true;

	public static bool btnMoveClick;

	public GameObject fakeCircle;

	public GameObject fakeKameha;

	public GameObject fakeMutilAtt;

	public GameObject fakeSkill3;

	public int arrow;

	public static bool is_loopCombo;

	public static bool jumping;

	public static bool is_longRotation;

	private Animator hitAnimator;

	public GameObject ssjEffect;

	public GameObject ssjEffect2;

	public int numSKillActive;

	private AudioSource _audio;

	public AudioSource _audioSourceHitObj;

	public Hero.AudioGame _audioGame;

	public static bool jumpLoop;

	private bool _animHitDone;

	[HideInInspector]
	public bool is_SSJ_up;

	[HideInInspector]
	public bool skill_playing;

	private int dameAttackNormal;

	private int dameSkill;

	private int dameSkillKenhKhi;

	[HideInInspector]
	public int HpBefore;

	public int countCombo;

	public GameObject dieEffectPar;

	public GameObject bgHitEffect;

	public AudioSource _audioSoundImpact;

	private int defenseBefore;

	public bool canMove;

	private int HpDyingOut;

	public float timeOutAttack;

	public Transform[] allBoxAttack;

	[HideInInspector]
	public bool returnDamage;

	[HideInInspector]
	public float percentReturnDamage;

	[HideInInspector]
	public int numHpRestore;

	[Serializable]
	public class AudioGame
	{
		public AudioSource run;

		public AudioClip fastPunch;

		public AudioClip fastAtt;

		public AudioClip start;

		public AudioClip kameha;

		public AudioClip kenhkhi;

		public AudioClip attack;

		public AudioClip jump;

		public AudioClip rotationSort;

		public AudioClip rotationLong;

		public AudioClip combo;

		public AudioClip hit1;

		public AudioClip hit2;

		public AudioClip hit3;

		public AudioClip die;

		public AudioClip ssj;
	}
}
