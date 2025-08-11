using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
	private void Start()
	{
		GameResult.ins = this;
		this.canMove = true;
		this.canGetGift = true;
	}

	public void dragLeft()
	{
		if (this.canMove)
		{
			iTween.MoveTo(this.scrollParrent.gameObject, new Vector3(this.scrollParrent.transform.position.x + 1.3f, this.scrollParrent.transform.position.y, 0f), 0.1f);
			this.canMove = false;
			base.Invoke("stopMove", 0.1f);
		}
	}

	public void dragRight()
	{
		if (this.canMove)
		{
			iTween.MoveTo(this.scrollParrent.gameObject, new Vector3(this.scrollParrent.transform.position.x - 1.3f, this.scrollParrent.transform.position.y, 0f), 0.1f);
			this.canMove = false;
			base.Invoke("stopMove", 0.1f);
		}
	}

	public void btnGift()
	{
		this.playAudioClick();
		if (this.can_click && this.canGetGift)
		{
			this.giftGroup.SetActive(true);
		}
	}

	public void btnNext()
	{
		this.playAudioClick();
		if (this.can_click)
		{
			this._audioBG.Stop();
			if (DataHolder.selectedlevel < 20)
			{
				DataHolder.selectedlevel++;
			}
			else
			{
				DataHolder.selectedMap++;
				DataHolder.selectedlevel = 1;
			}
			GameSave.isNextGame = true;
			this._gameManager.loadScene("MainMenu");
		}
	}

	public void btnRateUs()
	{
		this.playAudioClick();
		Application.OpenURL("market://details?id=com.aw.dragon.fight.shadow.battle.legend");
	}

	public void btnLoadSceneHome()
	{
		this.playAudioClick();
		if (this.can_click)
		{
			this._audioBG.Stop();
			this._gameManager.loadScene("MainMenu");
		}
	}

	private void stopMove()
	{
		this.canMove = true;
	}

	public void show(bool isWin, float timeDelay)
	{
		base.StartCoroutine(this.showStartCoroutine(isWin, timeDelay));
	}

	private IEnumerator showStartCoroutine(bool isWin, float timeDelay)
	{
		yield return new WaitForSeconds(timeDelay);
		Time.timeScale = 1f;
		this.show(isWin);
		yield break;
	}

	public void show(bool isWin)
	{
		if (Timer.ins != null)
		{
			this.timeRemaining = Timer.ins.timeCount;
		}
		else
		{
			this.timeRemaining = HighScore.getInstance().getRecord(DataHolder.selectedMap + "-" + DataHolder.selectedlevel, DataHolder.difficult).time;
		}
		if (this._gameManager != null)
		{
			this._gameManager.setVolumeCharacter(0f);
		}
		this._audio.volume = PlayerPrefs.GetFloat("MUSIC_VOLUME");
		this.popup.SetActive(true);
		this.canGetGift = true;
		DataHolder.Instance.playerData.addGold(GameSave.getCoin);
		DataHolder.Instance.playerData.addRuby(GameSave.getRuby);
		for (int i = 0; i < GameSave.itemsEat.Length; i++)
		{
			if (GameSave.itemsEat[i] > 0)
			{
				MaterialItemsEat materialItemsEat = UnityEngine.Object.Instantiate<MaterialItemsEat>(this.child);
				materialItemsEat.transform.SetParent(this.scrollParrent, false);
				materialItemsEat.show(i, GameSave.itemsEat[i]);
				DataHolder.Instance.inventory.pickUpAItem(ItemFactory.makeAResItem(i, GameSave.itemsEat[i]));
			}
		}
		for (int j = 0; j < this.itemEat.Count; j++)
		{
			ItemEat itemEat = UnityEngine.Object.Instantiate<ItemEat>(this.childItem);
			itemEat.transform.SetParent(this.scrollParrent, false);
			itemEat.onShow(this.itemEat[j]);
			DataHolder.Instance.inventory.pickUpAItem(ItemFactory.makeAScrollItem(this.itemEat[j]));
		}
		if (isWin)
		{
			this.setColorButtonGift();
			this._audio.clip = this.audio_victory;
			this._audio.Play();
			this.groupWin.SetActive(true);
			this.groupLose.SetActive(false);
			if (this.timeRemaining > 60)
			{
				this.starAnimator.Play("threeStar");
				this.numStar = 3;
			}
			else if (this.timeRemaining <= 60 && this.timeRemaining > 30)
			{
				this.starAnimator.Play("twoStar");
				this.numStar = 2;
			}
			else if (this.timeRemaining <= 30)
			{
				this.starAnimator.Play("oneStar");
				this.numStar = 1;
			}
			DataHolder.Instance.missionData.addDone(null, "PASS-LEVEL", 1);
			DataHolder.Instance.achievementData.addDone(null, "PASS-LEVEL", 1);
			DataHolder.Instance.playerData.addTotalPassLevel(1);
			if (DataHolder.Instance.tutorialData.hasTutorial())
			{
				this.btnNextWin.SetActive(false);
				this.btnRateWin.SetActive(true);
			}
			else
			{
				this.btnNextWin.SetActive(true);
				this.btnRateWin.SetActive(false);
			}
			DataHolder.Instance.pushScore(this.timeRemaining, GameSave.levelName + "-" + DataHolder.difficult.ToString());
		}
		else
		{
			this._audio.clip = this.audio_defeat;
			this._audio.Play();
			this.groupWin.SetActive(false);
			this.groupLose.SetActive(true);
			this.numStar = 0;
			this.checklevelUp();
			if (this._gameManager.countWatchVideo_playAgain >= GameConfig.numPlayAgain_video)
			{
				this.btnWatchVideo.color = Color.gray;
			}
			else
			{
				this.btnWatchVideo.color = Color.white;
			}
			if (this._gameManager.countRuby_playAgain >= GameConfig.numPlayAgain_ruby)
			{
				this.btnRuby.color = Color.gray;
			}
			else
			{
				this.btnRuby.color = Color.white;
				this.txtButtonRuby.text = "-" + this._gameManager.rubies_value[this._gameManager.countRuby_playAgain].ToString();
			}
		}
		if (this.numStar == 0)
		{
			this.goldBonus = 0;
		}
		else if (this.numStar == 1)
		{
			this.goldBonus = 50;
		}
		else if (this.numStar == 2)
		{
			this.goldBonus = 150;
		}
		else if (this.numStar == 3)
		{
			this.goldBonus = 500;
		}
		if (HighScore.getInstance().getRecord(GameSave.levelName, DataHolder.difficult) != null)
		{
			this.timeRecord = HighScore.getInstance().getRecord(GameSave.levelName, DataHolder.difficult).time;
		}
		else
		{
			this.timeRecord = 0;
		}
		if (isWin && (this.timeRecord < this.timeRemaining || this.timeRecord == 0))
		{
			HighScore.getInstance().setRecord(GameSave.levelName, this.timeRemaining, this.numStar, DataHolder.difficult);
			this.timeRecord = this.timeRemaining;
		}
		this.txtTime.text = this.numToTime(this.timeRemaining);
		this.txtGold.text = "+" + GameSave.getCoin.ToString();
		this.txtExp.text = "+" + GameSave.getExp.ToString();
		this.txtRuby.text = "+" + GameSave.getRuby.ToString();
		this.txtBonus.text = "+" + this.goldBonus.ToString();
		this.txtRecord.text = this.numToTime(this.timeRecord);
		this.txtLevel.text = (DataHolder.Instance.playerData.level + 1).ToString();
		this.levelSlider.fillAmount = (float)DataHolder.Instance.playerData.exp / (float)DataHolder.Instance.playerData.getNextLevelExp();
		if (UIHeroInfo.ins != null)
		{
			DataHolder.Instance.playerData.setBestCombo(UIHeroInfo.ins.comboMax);
		}
		if (!DataHolder.Instance.tutorialData.hasTutorial())
		{
			this.groupUpgrade.gameObject.SetActive(true);
			this.stateUpgrade = true;
		}
		else
		{
			this.groupUpgrade.gameObject.SetActive(false);
		}
	}

	public void btnUpgrade()
	{
		this.stateUpgrade = !this.stateUpgrade;
		this.playAudioClick();
		if (this.stateUpgrade)
		{
			this.groupUpgrade.Play("openUpgrade");
		}
		else
		{
			this.groupUpgrade.Play("closeUpgrade");
		}
	}

	public void btnSkillTree()
	{
		this.playAudioClick();
		DataHolder.showUpgradeSkill = true;
		this._gameManager.loadScene("MainMenu");
	}

	public void btnItemUpgrade()
	{
		this.playAudioClick();
		DataHolder.showUpgradeItem = true;
		this._gameManager.loadScene("MainMenu");
	}

	public void setColorButtonGift()
	{
		if (this.canGetGift)
		{
			this.img_btnGift.color = Color.white;
		}
		else
		{
			this.img_btnGift.color = Color.gray;
		}
	}

	public void checklevelUp()
	{
		if (this._gameManager != null)
		{
			if (this._gameManager.levelHeroBefore < DataHolder.Instance.playerData.level)
			{
				LevelUpPopup.instance.onShow(this._gameManager.levelHeroBefore, DataHolder.Instance.playerData.level);
				this._gameManager.levelHeroBefore = DataHolder.Instance.playerData.level;
			}
		}
		else if (LevelUpPopup.instance.cacheOldLevel < DataHolder.Instance.playerData.level)
		{
			LevelUpPopup.instance.onShow(LevelUpPopup.instance.cacheOldLevel, DataHolder.Instance.playerData.level);
		}
	}

	public void showTutorial()
	{
		this.countTutorial++;
		this.panelTutorial.SetActive(true);
		if (this.countTutorial == 1)
		{
			this.txtTutorial.text = "If the time left is more 1 minutes , you will get 3 stars.";
		}
		else if (this.countTutorial == 2)
		{
			this.txtTutorial.text = "If the time left is more 30 seconds, you will get 2 star,";
		}
		else if (this.countTutorial == 3)
		{
			this.txtTutorial.text = "and  1 star if le time left is less 30 seconds.";
		}
		else if (this.countTutorial == 4)
		{
			this.panelTutorial.SetActive(false);
			PlayerPrefs.SetInt("gameResultTutorial", 1);
		}
	}

	private string numToTime(int num)
	{
		return (num / 60).ToString() + ": " + (num % 60).ToString();
	}

	public void playAudioClick()
	{
		if (GameConfig.soundVolume > 0f)
		{
			if (this._audioClick.volume != GameConfig.soundVolume)
			{
				this._audioClick.volume = GameConfig.soundVolume;
			}
			this._audioClick.Play();
		}
	}

	public static GameResult ins;

	public Text txtTime;

	public Text txtRecord;

	public Text txtGold;

	public Text txtRuby;

	public Text txtExp;

	public Text txtBonus;

	public Text txtLevel;

	public Image levelSlider;

	public GameObject groupLose;

	public GameObject groupWin;

	private int timeRemaining;

	public Animator starAnimator;

	private int numStar;

	private int goldBonus;

	public GameObject popup;

	public Transform scrollParrent;

	public MaterialItemsEat child;

	public ItemEat childItem;

	private bool canMove;

	public AudioSource _audio;

	public AudioClip audio_victory;

	public AudioClip audio_defeat;

	private int timeRecord;

	public GameObject giftGroup;

	public AudioSource _audioClick;

	public Image btnWatchVideo;

	public Image btnRuby;

	public Image img_btnGift;

	public GameManager _gameManager;

	public bool can_click;

	public Text txtButtonRuby;

	public bool canGetGift;

	public AudioSource _audioBG;

	public List<string> itemEat;

	public Canvas canvasBtnHome;

	public GameObject panelTutorial;

	public Text txtTutorial;

	private int countTutorial;

	public GameObject btnNextWin;

	public GameObject btnRateWin;

	public Animator groupUpgrade;

	private bool stateUpgrade;
}
