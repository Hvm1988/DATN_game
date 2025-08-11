using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private void Awake()
	{
		if (GameManager.Instance == null)
		{
			GameManager.Instance = this;
		}

		GameConfig.soundVolume = PlayerPrefs.GetFloat("SOUND_VOLUME");
		GameConfig.musicVolume = PlayerPrefs.GetFloat("MUSIC_VOLUME");
		this.bgAudio.volume = GameConfig.musicVolume;
		this.bgAudio.Play();
		if (PlayerPrefs.GetInt("theFirstGamePlay") == 0)
		{
			this.tutorialDone = false;
			this.tutorialGroup.SetActive(true);
		}
		else
		{
			this.tutorialDone = true;
			this.tutorialGroup.SetActive(false);
		}
		this.isTestGame = false;
	}

	private void Start()
	{
		GameSave.gamePasue = true;
		GameSave.endGame = false;
		GameSave.getCoin = 0;
		GameSave.getExp = 0;
		GameSave.getRuby = 0;
		GameManager.numGame = 0;
		this.isHeroDyingOut = false;
		this.setVolume = false;
		this.hadBoss = false;
		GameSave.isNextGame = false;
		GameSave.resetItemEat();
		base.StartCoroutine(this.animationStart());
		this.listEnemies = DataHolder.Instance.enemiesDefine;
		this.readLevelDataPooling();
		this.percentDropMaterial = DataHolder.Instance.skillData.getEffectASkill("ADD-MAT");
		GameConfig.addGoldDropBase = DataHolder.Instance.skillData.getEffectASkill("ADD-GOLD");
		GameConfig.addExpDropBase = DataHolder.Instance.skillData.getEffectASkill("ADD-EXP");
		this.levelHeroBefore = DataHolder.Instance.playerData.level;
		if (GameConfig.addGoldDropBase > 0)
		{
			BonusIconControl.ins.coin.SetActive(true);
		}
		if (GameConfig.addExpDropBase > 0)
		{
			BonusIconControl.ins.exp.SetActive(true);
		}
		if (this.percentDropMaterial > 0)
		{
			BonusIconControl.ins.mar.SetActive(true);
		}
	}

	private IEnumerator animationStart()
	{
		if (DataHolder.Instance.skillData.isLearedSkill("TORNADO"))
		{
			this.btnTimeCountDowns[0].gameObject.GetComponent<Image>().sprite = this.spr_button[4];
			this.btnTimeCountDowns[0].isActive = true;
		}
		else
		{
			this.btnTimeCountDowns[0].isActive = false;
		}
		if (DataHolder.Instance.skillData.isLearedSkill("FAST-PUNCH"))
		{
			this.btnTimeCountDowns[1].gameObject.GetComponent<Image>().sprite = this.spr_button[0];
			this.btnTimeCountDowns[1].isActive = true;
		}
		else
		{
			this.btnTimeCountDowns[1].isActive = false;
		}
		if (this.hero.numSKillActive != 0)
		{
			this.btnTimeCountDowns[2].gameObject.GetComponent<Image>().sprite = this.spr_button[this.hero.numSKillActive];
			this.btnTimeCountDowns[2].isActive = true;
		}
		else
		{
			this.btnTimeCountDowns[2].isActive = false;
		}
		if (DataHolder.Instance.skillData.isLearedSkill("SUMMON"))
		{
			this.btnTimeCountDowns[3].gameObject.GetComponent<Image>().sprite = this.spr_button[5];
			this.btnTimeCountDowns[3].isActive = true;
		}
		else
		{
			this.btnTimeCountDowns[3].isActive = false;
		}
		if (!this.tutorialDone)
		{
			this.btnTimeCountDowns[0].gameObject.GetComponent<Image>().sprite = this.spr_button[4];
			this.btnTimeCountDowns[0].isActive = true;
			this.btnTimeCountDowns[1].gameObject.GetComponent<Image>().sprite = this.spr_button[0];
			this.btnTimeCountDowns[1].isActive = true;
			this.hero.numSKillActive = 1;
			this.btnTimeCountDowns[2].gameObject.GetComponent<Image>().sprite = this.spr_button[this.hero.numSKillActive];
			this.btnTimeCountDowns[2].isActive = true;
			GameSave.damageSkill2 = GameSave.damageAtt * 2;
			GameSave.damageSkill3 = GameSave.damageAtt * 3;
		}
		yield return new WaitForSeconds(1f);
		this.loadFake.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		this.hero.heroStart();
		yield return new WaitForSeconds(0.5f);
		if (!this.tutorialDone)
		{
			this.hand.SetActive(false);
			this.mapTurnObj.SetActive(false);
		}
		else
		{
			this.hand.SetActive(true);
		}
		GameSave.gamePasue = false;
		if (this.tutorialDone)
		{
			Timer.ins.startTimeCountDown();
		}
		yield break;
	}

	public void startGame()
	{
		GameManager.numGame++;
		this.hand.SetActive(false);
		base.StartCoroutine(this.activeEnemy(0f));
		this.boxGameStart.SetActive(false);
		this.doorLeft.SetActive(true);
		this.doorRight.SetActive(true);
		if (GameManager.numGame == 1)
		{
			this.playAudio(this.ui_start);
			this.pointAnimator.Play("point1");
			this.doorLeft.transform.position = new Vector3(-5.5f, 0f, 0f);
			this.doorRight.transform.position = new Vector3(22.5f, 0f, 0f);
			this.cam.zoneLeft = 2f;
			this.num_1s = this.enemiesTurn1.Count / 2;
			this.num_2s = (int)((float)(this.enemiesTurn1.Count - this.num_1s) * 0.35f);
			this.num_die = this.num_2s;
			this.num_dieAll = this.enemiesTurn1.Count - this.num_1s - this.num_2s - this.num_die;
		}
		else if (GameManager.numGame == 2)
		{
			this.playAudio(this.ui_start);
			if (!this.isHeroDyingOut)
			{
				this.bgAudio.clip = this._audioBgTurn2;
				this.bgAudio.Play();
			}
			this.cam.zoneLeft = 30f;
			this.doorLeft.transform.position = new Vector3(22.5f, 0f, 0f);
			this.doorRight.transform.position = new Vector3(50.5f, 0f, 0f);
			this.pointAnimator.Play("point2");
			this.num_1s = this.enemiesTurn2.Count / 2;
			this.num_2s = (int)((float)(this.enemiesTurn2.Count - this.num_1s) * 0.35f);
			this.num_die = this.num_2s;
			this.num_dieAll = this.enemiesTurn2.Count - this.num_1s - this.num_2s - this.num_die;
		}
		else if (GameManager.numGame == 3)
		{
			this.playAudio(this.ui_start);
			if (!this.isHeroDyingOut)
			{
				this.bgAudio.clip = this._audioBgTurn3;
				this.bgAudio.Play();
			}
			this.isAutoFire = true;
			this.cam.zoneLeft = 58f;
			this.doorLeft.transform.position = new Vector3(50.5f, 0f, 0f);
			this.doorRight.transform.position = new Vector3(78.5f, 0f, 0f);
			this.pointAnimator.Play("point3");
			this.num_1s = this.enemiesTurn3.Count / 2;
			this.num_2s = (int)((float)(this.enemiesTurn3.Count - this.num_1s) * 0.35f);
			this.num_die = this.num_2s;
			this.num_dieAll = this.enemiesTurn3.Count - this.num_1s - this.num_2s - this.num_die;
		}
	}

	private IEnumerator activeEnemy(float _time)
	{
		if (!GameSave.endGame)
		{
			yield return new WaitForSeconds(_time);
			this.rd = UnityEngine.Random.Range(0, 2);
			if (GameManager.numGame == 1)
			{
				int index = UnityEngine.Random.Range(0, this.enemiesTurn1.Count);
				if (this.enemiesTurn1.Count > 0)
				{
					if (this.enemiesPlaying.Count < GameConfig.maxEnemiesPlaying)
					{
						if (this.rd == 0)
						{
							this.enemiesTurn1[index].transform.position = new Vector3(this.doorLeft.transform.position.x, this.enemiesTurn1[index].posYBefore, 0f);
						}
						else
						{
							this.enemiesTurn1[index].transform.position = new Vector3(this.doorRight.transform.position.x, this.enemiesTurn1[index].posYBefore, 0f);
						}
						this.enemiesTurn1[index].gameObject.SetActive(true);
						this.enemiesTurn1[index].resetObjStart();
						this.enemiesPlaying.Add(this.enemiesTurn1[index]);
						this.enemiesTurn1.Remove(this.enemiesTurn1[index]);
						if (this.num_1s > 0)
						{
							this.num_1s--;
						}
						else
						{
							this.num_2s--;
						}
					}
					if (this.num_1s > 0)
					{
						base.StartCoroutine(this.activeEnemy(1f));
					}
					else if (this.num_2s > 0)
					{
						base.StartCoroutine(this.activeEnemy(3f));
					}
				}
			}
			else if (GameManager.numGame == 2)
			{
				int index2 = UnityEngine.Random.Range(0, this.enemiesTurn1.Count);
				if (this.enemiesTurn2.Count > 0)
				{
					if (this.enemiesPlaying.Count < GameConfig.maxEnemiesPlaying)
					{
						if (this.rd == 0)
						{
							this.enemiesTurn2[index2].transform.position = new Vector3(this.doorLeft.transform.position.x, this.enemiesTurn2[index2].posYBefore, 0f);
						}
						else
						{
							this.enemiesTurn2[index2].transform.position = new Vector3(this.doorRight.transform.position.x, this.enemiesTurn2[index2].posYBefore, 0f);
						}
						this.enemiesTurn2[index2].gameObject.SetActive(true);
						this.enemiesTurn2[index2].resetObjStart();
						this.enemiesPlaying.Add(this.enemiesTurn2[index2]);
						this.enemiesTurn2.Remove(this.enemiesTurn2[index2]);
						if (this.num_1s > 0)
						{
							this.num_1s--;
						}
						else
						{
							this.num_2s--;
						}
					}
					if (this.num_1s > 0)
					{
						base.StartCoroutine(this.activeEnemy(1f));
					}
					else if (this.num_2s > 0)
					{
						base.StartCoroutine(this.activeEnemy(3f));
					}
				}
			}
			else if (GameManager.numGame == 3)
			{
				int index3 = UnityEngine.Random.Range(0, this.enemiesTurn1.Count);
				if (this.enemiesTurn3.Count > 0)
				{
					if (this.enemiesPlaying.Count < GameConfig.maxEnemiesPlaying)
					{
						if (this.rd == 0)
						{
							this.enemiesTurn3[index3].transform.position = new Vector3(this.doorLeft.transform.position.x, this.enemiesTurn3[index3].posYBefore, 0f);
						}
						else
						{
							this.enemiesTurn3[index3].transform.position = new Vector3(this.doorRight.transform.position.x, this.enemiesTurn3[index3].posYBefore, 0f);
						}
						this.enemiesTurn3[index3].gameObject.SetActive(true);
						this.enemiesTurn3[index3].resetObjStart();
						this.enemiesPlaying.Add(this.enemiesTurn3[index3]);
						this.enemiesTurn3.Remove(this.enemiesTurn3[index3]);
						if (this.num_1s > 0)
						{
							this.num_1s--;
						}
						else
						{
							this.num_2s--;
						}
					}
					if (this.num_1s > 0)
					{
						base.StartCoroutine(this.activeEnemy(1f));
					}
					else if (this.num_2s > 0)
					{
						base.StartCoroutine(this.activeEnemy(3f));
					}
				}
			}
		}
		yield break;
	}

	public void checkGame(Enemies obj)
	{
		this.enemiesPlaying.Remove(obj);
		if (this.enemiesTurn1.Count == 0 && this.enemiesPlaying.Count == 0 && GameManager.numGame == 1)
		{
			if (this.tutorialDone)
			{
				this.hand.SetActive(true);
				this.doorLeft.SetActive(false);
				this.doorRight.SetActive(false);
				this.boxGameStart.SetActive(true);
				this.boxGameStart.transform.position = new Vector3(33.5f, 0f, 0f);
				this.cam.zoneRight = 42.5f;
			}
			else
			{
				PlayerPrefs.SetInt("theFirstGamePlay", 1);
				Timer.ins.endTimeCountDown();
				base.StartCoroutine(this.delayTutorialDone());
			}
		}
		else if (this.enemiesTurn2.Count == 0 && this.enemiesPlaying.Count == 0 && GameManager.numGame == 2)
		{
			this.hand.SetActive(true);
			this.doorLeft.SetActive(false);
			this.doorRight.SetActive(false);
			this.boxGameStart.SetActive(true);
			this.boxGameStart.transform.position = new Vector3(61.5f, 0f, 0f);
			this.cam.zoneRight = 70.5f;
			if (this.enemiesTurn3.Count == 0 && this.listBoss.Count == 0)
			{
				this.gameVictory();
			}
		}
		if (this.enemiesTurn3.Count == 0 && this.enemiesPlaying.Count == 0 && GameManager.numGame == 3 && this.listBoss.Count == 0)
		{
			this.gameVictory();
		}
		if (this.tutorialDone)
		{
			this.rd = UnityEngine.Random.Range(0, 100);
			if (this.rd < GameConfig.percentRubyDropBase)
			{
				for (int i = 0; i < this._rubies.Count; i++)
				{
					if (!this._rubies[i].canEat)
					{
						this._rubies[i].OnReady(obj.transform.position.x);
						break;
					}
				}
			}
		}
		if (GameManager.numGame == 1)
		{
			if (this.enemiesPlaying.Count > 0)
			{
				if (this.num_die > 0 && this.enemiesPlaying.Count < GameConfig.maxEnemiesPlaying)
				{
					this.rd = UnityEngine.Random.Range(0, 2);
					int index = UnityEngine.Random.Range(0, this.enemiesTurn1.Count);
					if (this.rd == 0)
					{
						this.enemiesTurn1[index].transform.position = new Vector3(this.doorLeft.transform.position.x, this.enemiesTurn1[index].posYBefore, 0f);
					}
					else
					{
						this.enemiesTurn1[index].transform.position = new Vector3(this.doorRight.transform.position.x, this.enemiesTurn1[index].posYBefore, 0f);
					}
					this.enemiesTurn1[index].gameObject.SetActive(true);
					this.enemiesTurn1[index].resetObjStart();
					this.enemiesPlaying.Add(this.enemiesTurn1[index]);
					this.enemiesTurn1.Remove(this.enemiesTurn1[index]);
					this.num_die--;
				}
			}
			else if (this.num_dieAll > 0)
			{
				for (int j = 0; j < this.num_dieAll; j++)
				{
					this.rd = UnityEngine.Random.Range(0, 2);
					int index2 = UnityEngine.Random.Range(0, this.enemiesTurn1.Count);
					if (this.rd == 0)
					{
						this.enemiesTurn1[index2].transform.position = new Vector3(this.doorLeft.transform.position.x, this.enemiesTurn1[index2].posYBefore, 0f);
					}
					else
					{
						this.enemiesTurn1[index2].transform.position = new Vector3(this.doorRight.transform.position.x, this.enemiesTurn1[index2].posYBefore, 0f);
					}
					this.enemiesTurn1[index2].gameObject.SetActive(true);
					this.enemiesTurn1[index2].resetObjStart();
					this.enemiesPlaying.Add(this.enemiesTurn1[index2]);
					this.enemiesTurn1.Remove(this.enemiesTurn1[index2]);
				}
				this.num_dieAll = 0;
			}
		}
		else if (GameManager.numGame == 2)
		{
			if (this.enemiesPlaying.Count > 0)
			{
				if (this.num_die > 0 && this.enemiesPlaying.Count < GameConfig.maxEnemiesPlaying)
				{
					this.rd = UnityEngine.Random.Range(0, 2);
					int index3 = UnityEngine.Random.Range(0, this.enemiesTurn2.Count);
					if (this.rd == 0)
					{
						this.enemiesTurn2[index3].transform.position = new Vector3(this.doorLeft.transform.position.x, this.enemiesTurn2[index3].posYBefore, 0f);
					}
					else
					{
						this.enemiesTurn2[index3].transform.position = new Vector3(this.doorRight.transform.position.x, this.enemiesTurn2[index3].posYBefore, 0f);
					}
					this.enemiesTurn2[index3].gameObject.SetActive(true);
					this.enemiesTurn2[index3].resetObjStart();
					this.enemiesPlaying.Add(this.enemiesTurn2[index3]);
					this.enemiesTurn2.Remove(this.enemiesTurn2[index3]);
					this.num_die--;
				}
			}
			else if (this.num_dieAll > 0)
			{
				for (int k = 0; k < this.num_dieAll; k++)
				{
					this.rd = UnityEngine.Random.Range(0, 2);
					int index4 = UnityEngine.Random.Range(0, this.enemiesTurn2.Count);
					if (this.rd == 0)
					{
						this.enemiesTurn2[index4].transform.position = new Vector3(this.doorLeft.transform.position.x, this.enemiesTurn2[index4].posYBefore, 0f);
					}
					else
					{
						this.enemiesTurn2[index4].transform.position = new Vector3(this.doorRight.transform.position.x, this.enemiesTurn2[index4].posYBefore, 0f);
					}
					this.enemiesTurn2[index4].gameObject.SetActive(true);
					this.enemiesTurn2[index4].resetObjStart();
					this.enemiesPlaying.Add(this.enemiesTurn2[index4]);
					this.enemiesTurn2.Remove(this.enemiesTurn2[index4]);
				}
				this.num_dieAll = 0;
			}
		}
		else if (GameManager.numGame == 3)
		{
			if (this.enemiesPlaying.Count > 0)
			{
				if (this.num_die > 0 && this.enemiesPlaying.Count < GameConfig.maxEnemiesPlaying)
				{
					this.rd = UnityEngine.Random.Range(0, 2);
					int index5 = UnityEngine.Random.Range(0, this.enemiesTurn3.Count);
					if (this.rd == 0)
					{
						this.enemiesTurn3[index5].transform.position = new Vector3(this.doorLeft.transform.position.x, this.enemiesTurn3[index5].posYBefore, 0f);
					}
					else
					{
						this.enemiesTurn3[index5].transform.position = new Vector3(this.doorRight.transform.position.x, this.enemiesTurn3[index5].posYBefore, 0f);
					}
					this.enemiesTurn3[index5].gameObject.SetActive(true);
					this.enemiesTurn3[index5].resetObjStart();
					this.enemiesPlaying.Add(this.enemiesTurn3[index5]);
					this.enemiesTurn3.Remove(this.enemiesTurn3[index5]);
					this.num_die--;
				}
			}
			else if (this.num_dieAll > 0)
			{
				for (int l = 0; l < this.num_dieAll; l++)
				{
					this.rd = UnityEngine.Random.Range(0, 2);
					int index6 = UnityEngine.Random.Range(0, this.enemiesTurn3.Count);
					if (this.rd == 0)
					{
						this.enemiesTurn3[index6].transform.position = new Vector3(this.doorLeft.transform.position.x, this.enemiesTurn3[index6].posYBefore, 0f);
					}
					else
					{
						this.enemiesTurn3[index6].transform.position = new Vector3(this.doorRight.transform.position.x, this.enemiesTurn3[index6].posYBefore, 0f);
					}
					this.enemiesTurn3[index6].gameObject.SetActive(true);
					this.enemiesTurn3[index6].resetObjStart();
					this.enemiesPlaying.Add(this.enemiesTurn3[index6]);
					this.enemiesTurn3.Remove(this.enemiesTurn3[index6]);
				}
				this.num_dieAll = 0;
			}
			else if (this.hadBoss && this.enemiesTurn3.Count == 0 && this.listBoss.Count > 0)
			{
				base.StartCoroutine(this.warningCotrol());
				if (!this.isHeroDyingOut)
				{
					this.bgAudio.clip = this._audioBossComing;
					this.bgAudio.Play();
				}
				for (int m = 0; m < this.listBoss.Count; m++)
				{
					this.rd = UnityEngine.Random.Range(0, 2);
					int index7 = UnityEngine.Random.Range(0, this.listBoss.Count);
					if (this.rd == 0)
					{
						this.listBoss[index7].transform.position = new Vector3(this.doorLeft.transform.position.x, this.listBoss[index7].posYBefore, 0f);
					}
					else
					{
						this.listBoss[index7].transform.position = new Vector3(this.doorRight.transform.position.x, this.listBoss[index7].posYBefore, 0f);
					}
					this.listBoss[index7].gameObject.SetActive(true);
					this.listBoss[index7].resetObjStart();
					this.enemiesPlaying.Add(this.listBoss[index7]);
					this.listBoss.Remove(this.listBoss[index7]);
				}
			}
		}
	}

	public void instanceItem(float posX)
	{
		if (this.tutorialDone)
		{
			for (int i = 0; i < this._itemsDrop.Count; i++)
			{
				if (!this._itemsDrop[i].canEat)
				{
					this._itemsDrop[i].onShow(this._itemCode[UnityEngine.Random.Range(0, this._itemCode.Length)], posX);
					break;
				}
			}
		}
	}

	public void instanceMaterial(int codeMaterial, int num, float posX)
	{
		if (this.tutorialDone)
		{
			for (int i = 0; i < this._materials.Count; i++)
			{
				if (!this._materials[i].canEat)
				{
					this._materials[i].OnShow(codeMaterial, num, posX);
					break;
				}
			}
		}
	}

	private IEnumerator delayTutorialDone()
	{
		Time.timeScale = 0.5f;
		yield return new WaitForSeconds(2f);
		Time.timeScale = 1f;
		NotificationPopup.instance.onShow("You do very well! We will start the real fight now.", delegate()
		{
			this.loadScene("GamePlay");
		});
		yield break;
	}

	private IEnumerator warningCotrol()
	{
		this.warningBoss.SetActive(true);
		yield return new WaitForSeconds(3f);
		this.warningBoss.SetActive(false);
		yield break;
	}

	public void gameVictory()
	{
		Timer.ins.endTimeCountDown();
		Time.timeScale = 0.5f;
		GameResult.ins.show(true, 3f);
		this.pointAnimator.Play("point4");
		this.bgAudio.Stop();
		base.StartCoroutine(this.delayVictory());
	}

	private IEnumerator delayVictory()
	{
		yield return new WaitForSeconds(3f);
		GameSave.endGame = true;
		this.hero.win();
		yield break;
	}

	public void gameLose()
	{
		GameSave.endGame = true;
		Timer.ins.endTimeCountDown();
		Time.timeScale = 0.3f;
		GameResult.ins.show(false, 2f);
		this.bgAudio.Stop();
		for (int i = 0; i < this.enemiesPlaying.Count; i++)
		{
			this.enemiesPlaying[i].stopAudio();
		}
	}

	private void readLevelDataPooling()
	{
		GameSave.levelName = DataHolder.selectedMap.ToString() + "-" + DataHolder.selectedlevel.ToString();
		string path = "GameData/LevelData/" + GameSave.levelName;
		if (this.isTestGame)
		{
			path = "GameData/LevelData/1-1";
			GameSave.levelName = "1-1";
			GameSave.gamePasue = false;
		}
		LevelData levelData = Resources.Load(path, typeof(LevelData)) as LevelData;
		PopupInfo.ins.txtAllEnemies.text = "select map : " + DataHolder.selectedMap.ToString() + "\n";
		Text txtAllEnemies = PopupInfo.ins.txtAllEnemies;
		txtAllEnemies.text = txtAllEnemies.text + "select level : " + DataHolder.selectedlevel.ToString() + "\n";
		Text txtAllEnemies2 = PopupInfo.ins.txtAllEnemies;
		txtAllEnemies2.text += JsonUtility.ToJson(levelData).ToString();
		EnemiesPooling.instance.resetItemDrop();
		string[] turn = levelData.turn1;
		for (int i = 0; i < turn.Length; i += 3)
		{
			for (int j = 0; j < int.Parse(turn[i + 1]); j++)
			{
				if (EnemiesPooling.instance.getEnemy(turn[i]) != null)
				{
					Enemies enemy = EnemiesPooling.instance.getEnemy(turn[i]);
					EnemiesPooling.instance.removeObj(enemy);
					enemy.hero = this.hero;
					enemy._gamemanager = this;
					enemy.isBoss = false;
					this.enemiesTurn1.Add(enemy);
					enemy._enemyDefine = this.listEnemies.getEnemies(turn[i].ToString());
					enemy.level = ((int.Parse(turn[i + 2]) < 1) ? 0 : (int.Parse(turn[i + 2]) - 1));
					enemy.gameObject.SetActive(false);
				}
				else
				{
					Enemies original = Resources.Load(turn[i], typeof(Enemies)) as Enemies;
					Enemies enemies = UnityEngine.Object.Instantiate<Enemies>(original);
					enemies.hero = this.hero;
					enemies._gamemanager = this;
					enemies.isBoss = false;
					this.enemiesTurn1.Add(enemies);
					enemies._enemyDefine = this.listEnemies.getEnemies(turn[i].ToString());
					enemies.level = ((int.Parse(turn[i + 2]) < 1) ? 0 : (int.Parse(turn[i + 2]) - 1));
					enemies.gameObject.SetActive(false);
				}
			}
		}
		string[] turn2 = levelData.turn2;
		for (int k = 0; k < turn2.Length; k += 3)
		{
			for (int l = 0; l < int.Parse(turn2[k + 1]); l++)
			{
				if (EnemiesPooling.instance.getEnemy(turn2[k]) != null)
				{
					Enemies enemy2 = EnemiesPooling.instance.getEnemy(turn2[k]);
					EnemiesPooling.instance.removeObj(enemy2);
					enemy2.hero = this.hero;
					enemy2._gamemanager = this;
					enemy2.isBoss = false;
					this.enemiesTurn2.Add(enemy2);
					enemy2._enemyDefine = this.listEnemies.getEnemies(turn2[k].ToString());
					enemy2.level = ((int.Parse(turn2[k + 2]) < 1) ? 0 : (int.Parse(turn2[k + 2]) - 1));
					enemy2.gameObject.SetActive(false);
				}
				else
				{
					Enemies original2 = Resources.Load(turn2[k], typeof(Enemies)) as Enemies;
					Enemies enemies2 = UnityEngine.Object.Instantiate<Enemies>(original2);
					enemies2.hero = this.hero;
					enemies2._gamemanager = this;
					enemies2.isBoss = false;
					this.enemiesTurn2.Add(enemies2);
					enemies2._enemyDefine = this.listEnemies.getEnemies(turn2[k].ToString());
					enemies2.level = ((int.Parse(turn2[k + 2]) < 1) ? 0 : (int.Parse(turn2[k + 2]) - 1));
					enemies2.gameObject.SetActive(false);
				}
			}
		}
		string[] turn3 = levelData.turn3;
		for (int m = 0; m < turn3.Length; m += 3)
		{
			for (int n = 0; n < int.Parse(turn3[m + 1]); n++)
			{
				if (EnemiesPooling.instance.getEnemy(turn3[m]) != null)
				{
					Enemies enemy3 = EnemiesPooling.instance.getEnemy(turn3[m]);
					EnemiesPooling.instance.removeObj(enemy3);
					enemy3.hero = this.hero;
					enemy3._gamemanager = this;
					enemy3.isBoss = false;
					this.enemiesTurn3.Add(enemy3);
					enemy3._enemyDefine = this.listEnemies.getEnemies(turn3[m].ToString());
					enemy3.level = ((int.Parse(turn3[m + 2]) < 1) ? 0 : (int.Parse(turn3[m + 2]) - 1));
					enemy3.gameObject.SetActive(false);
				}
				else
				{
					Enemies original3 = Resources.Load(turn3[m], typeof(Enemies)) as Enemies;
					Enemies enemies3 = UnityEngine.Object.Instantiate<Enemies>(original3);
					enemies3.hero = this.hero;
					enemies3._gamemanager = this;
					enemies3.isBoss = false;
					this.enemiesTurn3.Add(enemies3);
					enemies3._enemyDefine = this.listEnemies.getEnemies(turn3[m].ToString());
					enemies3.level = ((int.Parse(turn3[m + 2]) < 1) ? 0 : (int.Parse(turn3[m + 2]) - 1));
					enemies3.gameObject.SetActive(false);
				}
			}
		}
		this._itemCode = new string[levelData.scroll.Length];
		for (int num = 0; num < this._itemCode.Length; num++)
		{
			this._itemCode[num] = GetScrollItemById.getInstance()[levelData.scroll[num]];
		}
		if (levelData.boss.Length <= 1 || levelData.boss == null)
		{
			this.hadBoss = false;
		}
		else
		{
			this.hadBoss = true;
			string[] boss = levelData.boss;
			for (int num2 = 0; num2 < boss.Length; num2 += 3)
			{
				for (int num3 = 0; num3 < int.Parse(boss[num2 + 1]); num3++)
				{
					if (EnemiesPooling.instance.getEnemy(boss[num2]) != null)
					{
						Enemies enemy4 = EnemiesPooling.instance.getEnemy(boss[num2]);
						EnemiesPooling.instance.removeObj(enemy4);
						enemy4.hero = this.hero;
						enemy4._gamemanager = this;
						enemy4.isBoss = true;
						this.listBoss.Add(enemy4);
						enemy4._enemyDefine = this.listEnemies.getEnemies(boss[num2].ToString());
						enemy4.level = ((int.Parse(boss[num2 + 2]) < 1) ? 0 : (int.Parse(boss[num2 + 2]) - 1));
						enemy4.gameObject.SetActive(false);
					}
					else
					{
						Enemies original4 = Resources.Load(boss[num2], typeof(Enemies)) as Enemies;
						Enemies enemies4 = UnityEngine.Object.Instantiate<Enemies>(original4);
						enemies4.hero = this.hero;
						enemies4._gamemanager = this;
						enemies4.isBoss = true;
						this.listBoss.Add(enemies4);
						enemies4._enemyDefine = this.listEnemies.getEnemies(boss[num2].ToString());
						enemies4.level = ((int.Parse(boss[num2 + 2]) < 1) ? 0 : (int.Parse(boss[num2 + 2]) - 1));
						enemies4.gameObject.SetActive(false);
					}
				}
			}
		}
		this.rd = UnityEngine.Random.Range(0, 3);
		if (this.rd == 0)
		{
			this.listEnemiesSave = this.enemiesTurn1;
		}
		else if (this.rd == 1)
		{
			this.listEnemiesSave = this.enemiesTurn2;
		}
		else
		{
			this.listEnemiesSave = this.enemiesTurn3;
		}
		this.rd = UnityEngine.Random.Range(0, this.listEnemiesSave.Count);
		this.listEnemiesSave[this.rd].hadItem = true;
		List<int> list = new List<int>();
		for (int num4 = 0; num4 < this.enemiesTurn1.Count; num4++)
		{
			list.Add(num4);
		}
		for (int num5 = 0; num5 < levelData.materialTurn1.Length; num5 += 2)
		{
			this.rd = UnityEngine.Random.Range(0, list.Count);
			this.enemiesTurn1[list[this.rd]].materialCodeDrop = levelData.materialTurn1[num5];
			this.enemiesTurn1[list[this.rd]].materialNumDrop = levelData.materialTurn1[num5 + 1];
			if (list.Count > 1)
			{
				list.RemoveAt(this.rd);
			}
		}
		list.Clear();
		for (int num6 = 0; num6 < this.enemiesTurn2.Count; num6++)
		{
			list.Add(num6);
		}
		for (int num7 = 0; num7 < levelData.materialTurn2.Length; num7 += 2)
		{
			this.rd = UnityEngine.Random.Range(0, list.Count);
			this.enemiesTurn2[list[this.rd]].materialCodeDrop = levelData.materialTurn2[num7];
			this.enemiesTurn2[list[this.rd]].materialNumDrop = levelData.materialTurn2[num7 + 1];
			if (list.Count > 1)
			{
				list.RemoveAt(this.rd);
			}
		}
		list.Clear();
		for (int num8 = 0; num8 < this.enemiesTurn3.Count; num8++)
		{
			list.Add(num8);
		}
		for (int num9 = 0; num9 < levelData.materialTurn3.Length; num9 += 2)
		{
			this.rd = UnityEngine.Random.Range(0, list.Count);
			this.enemiesTurn3[list[this.rd]].materialCodeDrop = levelData.materialTurn3[num9];
			this.enemiesTurn3[list[this.rd]].materialNumDrop = levelData.materialTurn3[num9 + 1];
			if (list.Count > 1)
			{
				list.RemoveAt(this.rd);
			}
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.moveLeft();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.moveRight();
		}
		else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
		{
			this.idle();
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			this.jump();
		}
		if (Input.GetKeyDown(KeyCode.V))
		{
			this.attackDown();
		}
		if (Input.GetKeyUp(KeyCode.V))
		{
			this.attackUp();
		}
	}

	public void attackDown()
	{
		if (!GameSave.gamePasue && !GameSave.endGame)
		{
			Hero.is_loopCombo = true;
			this.hero.attack();
		}
	}

	public void attackUp()
	{
		Hero.is_loopCombo = false;
	}

	public void jump()
	{
		if (!GameSave.gamePasue && !GameSave.endGame)
		{
			this.hero.jump();
			Hero.jumpLoop = true;
		}
	}

	public void jumpUp()
	{
		Hero.jumpLoop = false;
	}

	public void skillSingle()
	{
		if (this.btnTimeCountDowns[0].isActive && !GameSave.gamePasue && !GameSave.endGame && this.btnTimeCountDowns[0].ratio == 0f && this.hero.checkAction())
		{
			this.hero.skill_rotation();
			this.btnTimeCountDowns[0].timeCount = 0f;
		}
	}

	public void skillUpgrade()
	{
		if (this.btnTimeCountDowns[1].isActive && !GameSave.gamePasue && !GameSave.endGame && this.btnTimeCountDowns[1].ratio == 0f && this.hero.checkAction())
		{
			this.hero.skill_2();
			this.btnTimeCountDowns[1].timeCount = 0f;
			this.playCamAnimation(0.6f, "cameraVibrate2");
		}
	}

	public void skillDragonBall()
	{
		if (this.btnTimeCountDowns[2].isActive && !GameSave.gamePasue && !GameSave.endGame && this.btnTimeCountDowns[2].ratio == 0f && this.hero.checkAction())
		{
			this.hero.skill_3();
			this.btnTimeCountDowns[2].timeCount = 0f;
			if (this.hero.numSKillActive == 1)
			{
				this.playCamAnimation(0.9f, "cameraVibrate3");
			}
			else if (this.hero.numSKillActive == 2)
			{
				this.playCamAnimation(0.7f, "cameraVibrate3");
			}
			else if (this.hero.numSKillActive == 3)
			{
				this.playCamAnimation(1.2f, "cameraVibrate4");
			}
		}
	}

	public void callPetSupport()
	{
		if (this.btnTimeCountDowns[3].isActive && !GameSave.gamePasue && !GameSave.endGame && this.btnTimeCountDowns[3].ratio == 0f)
		{
			this.btnTimeCountDowns[3].timeCount = 0f;
			this._petAnim.transform.parent.transform.localPosition = new Vector3(this.hero.transform.position.x - 10f, -1f, 0f);
			this._petAnim.transform.localEulerAngles = Vector3.zero;
			this._petAnim.transform.parent.gameObject.SetActive(true);
			this._petAnim.playAnimationAttack();
		}
	}

	public void moveLeft()
	{
		this.joytickArrow = -1;
		if (this.joytickArrow != this.hero.arrow && !GameSave.gamePasue && !GameSave.endGame)
		{
			this.hero.moveLeft();
			this.hero.arrow = this.joytickArrow;
		}
	}

	public void moveRight()
	{
		this.joytickArrow = 1;
		if (this.joytickArrow != this.hero.arrow && !GameSave.gamePasue && !GameSave.endGame)
		{
			this.hero.moveRight();
			this.hero.arrow = this.joytickArrow;
		}
	}

	public void idle()
	{
		if (!GameSave.gamePasue && !GameSave.endGame)
		{
			this.hero.idle();
			this.joytickArrow = 0;
		}
	}

	public void ssj()
	{
		if (!GameSave.gamePasue && !GameSave.endGame && this.btnTimeCountDowns[4].ratio == 0f && this.hero.checkAction())
		{
			this.hero.SSJ();
			this.btnTimeCountDowns[4].timeCount = 0f;
		}
	}

	public void btnControlDrag()
	{
		Vector3 vector = Camera.main.ViewportToScreenPoint(Input.mousePosition);
		this.btnControl.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(vector.x, vector.y, 0f) * 0.021875f;
	}

	private IEnumerator camAni(float timedelay)
	{
		yield return new WaitForSeconds(timedelay);
		this._animatorCamera.Play("cameraVibrate1");
		yield break;
	}

	private IEnumerator camAni(float timedelay, string _name)
	{
		yield return new WaitForSeconds(timedelay);
		this._animatorCamera.Play(_name);
		yield break;
	}

	public void playCamAnimation(float _timeDelay, string _name)
	{
		base.StartCoroutine(this.camAni(_timeDelay, _name));
	}

	private IEnumerator autoFireRepeat()
	{
		if (!GameSave.endGame)
		{
			yield return new WaitForSeconds(3f);
			this.ringTager.SetActive(true);
			iTween.MoveTo(this.ringTager, iTween.Hash(new object[]
			{
				"position",
				new Vector3(this.hero.transform.position.x, 0f, 0f),
				"time",
				0.5f,
				"easetype",
				iTween.EaseType.linear
			}));
			if (this.autoFire.target == null)
			{
				this.autoFire.target = this.hero;
			}
			this.autoFire.OnEnableObj();
			base.StartCoroutine(this.autoFireRepeat());
		}
		yield break;
	}

	private void playAudio(AudioClip _audioClip)
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

	private void playAudioClick()
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

	public void openPopupGamePause()
	{
		this.popupGamePause.SetActive(true);
		this.setVolumeCharacter(0f);
	}

	public void setVolumeCharacter(float _volume)
	{
		this.hero.setVolumeSound(_volume);
		for (int i = 0; i < this.enemiesPlaying.Count; i++)
		{
			this.enemiesPlaying[i].setVolumeSound(_volume);
		}
	}

	public void loadSceneHome()
	{
		this.loadScene("MainMenu");
	}

	public void loadScene(string sceneName)
	{
		for (int i = 0; i < this.enemiesPlaying.Count; i++)
		{
			EnemiesPooling.instance.listEnemy.Add(this.enemiesPlaying[i]);
			this.enemiesPlaying[i].gameObject.SetActive(false);
		}
		for (int j = 0; j < this.enemiesTurn1.Count; j++)
		{
			EnemiesPooling.instance.listEnemy.Add(this.enemiesTurn1[j]);
			this.enemiesTurn1[j].gameObject.SetActive(false);
		}
		for (int k = 0; k < this.enemiesTurn2.Count; k++)
		{
			EnemiesPooling.instance.listEnemy.Add(this.enemiesTurn2[k]);
			this.enemiesTurn2[k].gameObject.SetActive(false);
		}
		for (int l = 0; l < this.enemiesTurn3.Count; l++)
		{
			EnemiesPooling.instance.listEnemy.Add(this.enemiesTurn3[l]);
			this.enemiesTurn3[l].gameObject.SetActive(false);
		}
		EnemiesPooling.instance.disableAll();
		this.playAudioClick();
		base.StartCoroutine(this.loadSceneAsyn(sceneName));
	}

	private IEnumerator loadSceneAsyn(string sceneHome)
	{
		this.loadFake.SetActive(true);
		AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(sceneHome);
		Time.timeScale = 1f;
		if (this.tutorialDone)
		{
			DataHolder.Instance.willShow3Ruby = true;
			this.tipLoading.SetActive(true);
		}
		while (!sceneLoad.isDone)
		{
			this.loadingBar.fillAmount = sceneLoad.progress;
			this.loadingStatus.text = "Loading " + (int)(this.loadingBar.fillAmount * 100f) + "%";
			if (sceneLoad.progress >= 0.9f)
			{
				this.loadingBar.fillAmount = 1f;
				this.loadingStatus.text = "Loading " + (int)(this.loadingBar.fillAmount * 100f) + "%";
			}
			yield return null;
		}
		yield break;
	}

	public void continuePlay()
	{
		GameResult.ins.popup.SetActive(false);
		this.hero.isDie = false;
		GameSave.endGame = false;
		Hero.heroCanAttack = true;
		this.hero.dieEffectPar.SetActive(false);
		this.hero.idle();
		for (int i = 0; i < this.enemiesPlaying.Count; i++)
		{
			this.enemiesPlaying[i].resetObj();
		}
		this.hero.HP = this.hero.HpBefore / 2;
		this.hero.updateUIHero();
		Timer.ins.startTimeCountDown(120);
		if ((this.enemiesTurn1.Count > 0 || this.enemiesTurn2.Count > 0 || this.enemiesTurn3.Count > 0) && (this.num_1s > 0 || this.num_2s > 0))
		{
			base.StartCoroutine(this.activeEnemy(1f));
		}
		this.delayAudioBg();
	}

	private void delayAudioBg()
	{
		this.setVolumeCharacter(GameConfig.soundVolume);
		this.soundBgReset();
	}

	public void watchVideo()
	{
		AdsManager.AdNumbers = 2;
		this.playAudioClick();
		if (this.countWatchVideo_playAgain < GameConfig.numPlayAgain_video)
		{

			AdsManager.adsManager.showRewardVideo2();

		}
		
	}

	private IEnumerator waitDoneAdsCor()
	{
		yield return new WaitUntil(() => this.isDondeVideo);
		this.isDondeVideo = false;
		this.watchVideoSuccess();
		yield break;
	}

	public void watchVideoSuccess()
	{
		this.continuePlay();
		this.countWatchVideo_playAgain++;
	}

	public void soundBgHeroDyingOutControl()
	{
		this.isHeroDyingOut = true;
		this.bgAudio.clip = this._audioBgHeroDyingOut;
		this.bgAudio.Play();
	}

	public void soundBgReset()
	{
		this.isHeroDyingOut = false;
		if (GameManager.numGame == 1)
		{
			this.bgAudio.clip = this._audioBgTurn1;
		}
		else if (GameManager.numGame == 2)
		{
			this.bgAudio.clip = this._audioBgTurn2;
		}
		else
		{
			this.bgAudio.clip = this._audioBgTurn3;
		}
		this.bgAudio.Play();
	}

	private void watchVideoFail()
	{
		NotificationPopup.instance.onShow(TextContent.videoNotReady);
	}

	public void continueRuby()
	{
		this.playAudioClick();
		if (this.countRuby_playAgain < GameConfig.numPlayAgain_ruby)
		{
			if (DataHolder.Instance.playerData.ruby >= this.rubies_value[this.countWatchVideo_playAgain])
			{
				DataHolder.Instance.playerData.addRuby(-this.rubies_value[this.countWatchVideo_playAgain]);
				this.continuePlay();
				this.countRuby_playAgain++;
			}
			else
			{
				NotificationPopup.instance.onShow(TextContent.rubyNotEnough, this.rubies_value[this.countRuby_playAgain].ToString(), this.spr_ruby, delegate()
				{
					this.openShop();
				});
			}
		}
	}

	private void openShop()
	{
		this.shop.SetActive(true);
	}

	public static GameManager Instance;

	public Hero hero;

	public ButtonTimeCountDown[] btnTimeCountDowns;

	public GameObject btnControl;

	public Animator _animatorCamera;

	public HeroAnimationsControl heroAnimation;

	public GameObject doorLeft;

	public GameObject doorRight;

	public GameObject hand;

	public List<Enemies> enemiesTurn1;

	public List<Enemies> enemiesTurn2;

	public List<Enemies> enemiesTurn3;

	public List<Enemies> enemiesPlaying;

	public List<Enemies> enemiesRead;

	public List<Enemies> listEnemiesSave;

	public List<Enemies> listBoss;

	public List<int> enemiesNum;

	private int rd;

	public int levelHeroBefore;

	public float timeDelayCam;

	public static int numGame;

	public CameraFollow cam;

	public GameObject boxGameStart;

	public List<MaterialItems> _materials;

	public List<ItemsDrop> _itemsDrop;

	public List<RubyEat> _rubies;

	private RubyEat rubySave;

	private string[] _itemCode;

	public EffectGame autoFire;

	private bool isAutoFire;

	public GameObject ringTager;

	private int percentDropMaterial;

	public Sprite[] spr_button;

	public Animator pointAnimator;

	public AudioSource _audio;

	public AudioClip ui_start;

	public GameObject popupGamePause;

	public AudioSource bgAudio;

	public GameObject warningBoss;

	public bool hadBoss;

	public GameObject BossInfoObj;

	public GameObject mapTurnObj;

	private EnemiesDefine listEnemies;

	public Image loadingBar;

	public Text loadingStatus;

	public GameObject loadFake;

	public EnemiesDefine dataEnemies;

	public AudioSource _audioClick;

	public Sprite spr_ruby;

	public GameObject shop;

	public int countWatchVideo_playAgain;

	public int countRuby_playAgain;

	[HideInInspector]
	public int[] rubies_value = new int[]
	{
		30,
		60,
		150
	};

	private int joytickArrow;

	public AudioClip _audioBossComing;

	public AudioClip _audioBgTurn1;

	public AudioClip _audioBgTurn2;

	public AudioClip _audioBgTurn3;

	public AudioClip _audioBgHeroDyingOut;

	public bool isHeroDyingOut;

	public bool isTestGame;

	public bool tutorialDone;

	public GameObject tutorialGroup;

	public PetAnimation _petAnim;

	public LevelData lvdt;

	public GameObject tipLoading;

	private bool setVolume;

	public int num_1s;

	public int num_2s;

	public int num_die;

	public int num_dieAll;

	private bool isDondeVideo;
}
