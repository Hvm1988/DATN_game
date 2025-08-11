using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPopup : MonoBehaviour
{
	private void Awake()
	{
		LevelUpPopup.instance = this;
	}

	public void onShow(int levelBefore, int levelCurrent)
	{
		this.bef_level.text = (levelBefore + 1).ToString();
		this.cur_level.text = (levelCurrent + 1).ToString();
		this.bef_att.text = DataHolder.Instance.playerDefine.getATK(levelBefore).ToString();
		this.cur_att.text = DataHolder.Instance.playerDefine.getATK(levelCurrent).ToString();
		this.bef_def.text = DataHolder.Instance.playerDefine.getDEF(levelBefore).ToString();
		this.cur_def.text = DataHolder.Instance.playerDefine.getDEF(levelCurrent).ToString();
		this.bef_hp.text = DataHolder.Instance.playerDefine.getHP(levelBefore).ToString();
		this.cur_hp.text = DataHolder.Instance.playerDefine.getHP(levelCurrent).ToString();
		this.rd_coin = 500 + 50 * levelCurrent;
		this.rd_ruby = 25 + 5 * levelCurrent;
		this.coin_bonus.text = this.rd_coin.ToString();
		this.ruby_bonus.text = this.rd_ruby.ToString();
		DataHolder.Instance.playerData.addGold(this.rd_coin);
		DataHolder.Instance.playerData.addRuby(this.rd_ruby);
		this.group.SetActive(true);
		if (GameConfig.musicVolume > 0f)
		{
			this._audio.volume = GameConfig.musicVolume;
			this._audio.Play();
		}
	}

	public void onClose()
	{
		this._animator.Play("close");
		base.StartCoroutine(this.delay());
	}

	private IEnumerator delay()
	{
		yield return new WaitForSeconds(0.3f);
		this.group.SetActive(false);
		yield break;
	}

	public static LevelUpPopup instance;

	public Animator _animator;

	public GameObject group;

	public Text bef_level;

	public Text cur_level;

	public Text bef_att;

	public Text cur_att;

	public Text bef_def;

	public Text cur_def;

	public Text bef_hp;

	public Text cur_hp;

	public Text ruby_bonus;

	public Text coin_bonus;

	private int rd_coin;

	private int rd_ruby;

	public AudioSource _audio;

	public int cacheOldLevel;
}
