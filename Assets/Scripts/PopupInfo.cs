using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupInfo : MonoBehaviour
{
	private void Awake()
	{
		PopupInfo.ins = this;
		this.scr = base.GetComponent<AudioSource>();
	}

	private void initBgMusic()
	{
		this.scr = base.GetComponent<AudioSource>();
		for (int i = 0; i < this._audioClip._audioclips.Length; i++)
		{
			MusicTest musicTest = UnityEngine.Object.Instantiate<MusicTest>(this.musicChild);
			musicTest.transform.SetParent(this.parrentBgMusic, false);
			musicTest.onShow(this._audioClip._audioclips[i].name, this.scr, this._audioClip._audioclips[i], false);
		}
	}

	public void onMusicChange()
	{
		GameConfig.musicVolume = this.sliderMusic.value;
		PlayerPrefs.SetFloat("MUSIC_VOLUME", GameConfig.musicVolume);
		this.bgMusic.volume = GameConfig.musicVolume;
		this.scr.volume = GameConfig.musicVolume;
	}

	public void onSoundChange()
	{
		GameConfig.soundVolume = this.sliderSound.value;
		PlayerPrefs.SetFloat("SOUND_VOLUME", GameConfig.soundVolume);
	}

	private void initBossMusic()
	{
		this.scr = base.GetComponent<AudioSource>();
		for (int i = 0; i < this._bossAudioClip._audioclips.Length; i++)
		{
			MusicTest musicTest = UnityEngine.Object.Instantiate<MusicTest>(this.musicChild);
			musicTest.transform.SetParent(this.parrentBossMusic, false);
			musicTest.onShow(this._bossAudioClip._audioclips[i].name, this.scr, this._bossAudioClip._audioclips[i], true);
		}
	}

	public void onShow()
	{
		this.lv = DataHolder.Instance.playerData.level;
		this.txtHero.text = "Hero lv: " + (DataHolder.Instance.playerData.level + 1).ToString();
		this.txtDamageBase.text = "1.Damage Base = " + DataHolder.Instance.playerDefine.getATK(this.lv);
		this.txtDamageItem.text = "2.Damage Item = " + DataHolder.Instance.playerData.getDamageBonus();
		this.skill = (float)DataHolder.Instance.playerDefine.getATK(this.lv) * ((float)DataHolder.Instance.skillData.getEffectASkill("ADD-ATK") / 100f);
		this.txtDamageSkill.text = "3.Damage Skill Upgrade = (1+2) * " + DataHolder.Instance.skillData.getEffectASkill("ADD-ATK").ToString() + "% = " + this.skill.ToString();
		this.txtDamagenormal.text = "4.Damage Normal Att =" + DataHolder.Instance.skillData.getDamageAskill("NORMAL-ATK").ToString();
		if (DataHolder.Instance.skillData.isLearedSkill("FAST-PUNCH"))
		{
			this.txtKameha.text = "5.Damage fast punch = " + DataHolder.Instance.skillData.getDamageAskill("FAST-PUNCH").ToString();
		}
		if (DataHolder.Instance.skillData.isLearedSkill("FAST-SHOOT"))
		{
			this.txtKameha.text = "5.Damage fast shoot = " + DataHolder.Instance.skillData.getDamageAskill("FAST-SHOOT").ToString();
		}
		if (DataHolder.Instance.skillData.isLearedSkill("KAMEHA"))
		{
			this.txtKameha.text = "5.Damage kameha = " + DataHolder.Instance.skillData.getDamageAskill("KAMEHA").ToString();
		}
		this.txtKinhkhi.text = "6.Damage kinhkhi = " + DataHolder.Instance.skillData.getDamageAskill("KENHKHI").ToString();
		this.example.text = "example normal att = 1+2+3+4 = " + (DataHolder.Instance.playerData.atk + DataHolder.Instance.skillData.getDamageAskill("NORMAL-ATK")).ToString();
		this.df.text = "Defense = " + DataHolder.Instance.playerData.def.ToString();
		this.hp.text = "HP = " + DataHolder.Instance.playerData.hp.ToString();
		IEnumerator enumerator = this.parrentTf.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform obj2 = (Transform)obj;
				UnityEngine.Object.Destroy(obj2);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.addE();
		if (this.list != null)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				EnemyInfo enemyInfo = UnityEngine.Object.Instantiate<EnemyInfo>(this.info);
				enemyInfo.onShow(this.list[i]._name, this.list[i].level, this.list[i].damage, this.list[i].defend, this.list[i].HP);
				enemyInfo.transform.SetParent(this.parrentTf, false);
			}
		}
		this.parrent.SetActive(true);
	}

	public void resume()
	{
		this.parrent.SetActive(false);
		Time.timeScale = 1f;
		this.bgMusic.volume = 1f;
		this._gamemanager.setVolumeCharacter(GameConfig.soundVolume);
	}

	public void btnHeroTab()
	{
		this.groupHero.SetActive(true);
		this.groupEnemies.SetActive(false);
		this.groupBgMusic.SetActive(false);
		this.groupBossMusic.SetActive(false);
		this.groupAllEnemies.SetActive(false);
		this.groupLevel.SetActive(false);
	}

	public void btnEnemiesTab()
	{
		this.groupHero.SetActive(false);
		this.groupEnemies.SetActive(true);
		this.groupBgMusic.SetActive(false);
		this.groupBossMusic.SetActive(false);
		this.groupAllEnemies.SetActive(false);
		this.groupLevel.SetActive(false);
	}

	public void btnBgMusic()
	{
		this.groupHero.SetActive(false);
		this.groupEnemies.SetActive(false);
		this.groupBgMusic.SetActive(true);
		this.groupBossMusic.SetActive(false);
		this.groupAllEnemies.SetActive(false);
		this.groupLevel.SetActive(false);
	}

	public void btnBossMusic()
	{
		this.groupHero.SetActive(false);
		this.groupEnemies.SetActive(false);
		this.groupBgMusic.SetActive(false);
		this.groupBossMusic.SetActive(true);
		this.groupAllEnemies.SetActive(false);
		this.groupLevel.SetActive(false);
	}

	public void btnAllEnemies()
	{
		this.groupHero.SetActive(false);
		this.groupEnemies.SetActive(false);
		this.groupBgMusic.SetActive(false);
		this.groupBossMusic.SetActive(false);
		this.groupLevel.SetActive(false);
		this.groupAllEnemies.SetActive(true);
	}

	public void btnLevel()
	{
		this.groupHero.SetActive(false);
		this.groupEnemies.SetActive(false);
		this.groupBgMusic.SetActive(false);
		this.groupBossMusic.SetActive(false);
		this.groupAllEnemies.SetActive(false);
		this.groupLevel.SetActive(true);
	}

	public void btnPlay()
	{
		bool flag = true;
		try
		{
			DataHolder.selectedMap = int.Parse(this.txtMap.text.ToString());
		}
		catch (FormatException ex)
		{
			Time.timeScale = 1f;
			NotificationPopup.instance.onShow("select map la so");
			flag = false;
		}
		try
		{
			DataHolder.selectedlevel = int.Parse(this.txtLevel.text.ToString());
		}
		catch (FormatException ex2)
		{
			Time.timeScale = 1f;
			NotificationPopup.instance.onShow("select map la so");
			flag = false;
		}
		if (flag)
		{
			this._gamemanager.loadScene("GamePlay");
		}
	}

	private void addE()
	{
		this.list.Clear();
		List<Enemies> enemiesPlaying = this._gamemanager.enemiesPlaying;
		for (int i = 0; i < enemiesPlaying.Count; i++)
		{
			if (!this.check(enemiesPlaying[i]))
			{
				this.list.Add(enemiesPlaying[i]);
			}
		}
	}

	private bool check(Enemies e)
	{
		if (this.list.Count > 0)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (e._name.Equals(this.list[i]._name) && e.level == this.list[i].level)
				{
					return true;
				}
			}
		}
		return false;
	}

	public void setNumSkill()
	{
		int value = int.Parse(this.txtNumSkill.text.ToString());
		PlayerPrefs.SetInt("numSkillSet", value);
	}

	public static PopupInfo ins;

	public Text txtHero;

	public Text txtDamageBase;

	public Text txtDamageItem;

	public Text txtDamageSkill;

	public Text txtDamagenormal;

	public Text txtKameha;

	public Text txtKinhkhi;

	public Text example;

	public Text df;

	public Text hp;

	private int lv;

	private float skill;

	public GameObject parrent;

	public GameObject groupHero;

	public GameObject groupEnemies;

	public GameObject groupBgMusic;

	public GameObject groupBossMusic;

	public GameObject groupAllEnemies;

	public GameObject groupLevel;

	public Transform parrentBgMusic;

	public Transform parrentBossMusic;

	public MusicTest musicChild;

	public List<Enemies> list;

	public EnemyInfo info;

	public Transform parrentTf;

	public GameManager _gamemanager;

	public AudioSource bgMusic;

	public Slider sliderMusic;

	public Slider sliderSound;

	public Text txtAllEnemies;

	public Text txtMap;

	public Text txtLevel;

	public Text txtNumSkill;

	private AudioSource scr;

	public PopupInfo.BGAudioClip _audioClip;

	public PopupInfo.BossAudioClip _bossAudioClip;

	[Serializable]
	public class BGAudioClip
	{
		public AudioClip[] _audioclips;
	}

	[Serializable]
	public class BossAudioClip
	{
		public AudioClip[] _audioclips;
	}
}
