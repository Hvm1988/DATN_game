using System;
using UnityEngine;
using UnityEngine.UI;

public class SpecialGift : PopupsBase
{
	private void Awake()
	{
		if (SpecialGift.spe == null)
		{
			SpecialGift.spe = this;
		}
	}
	public override void OnEnable()
	{
		base.OnEnable();
		this.numReload = 2;
		this.reload();
		this.updateBtnReload();
	}

	private void updateBtnReload()
	{
		this.txtReload.text = this.numReload.ToString() + "/2";
	}

	private void reload()
	{
		this.rdGold = GameSave.getCoin;
		this.rdRuby = GameSave.getRuby;
		this.txtGold.text = this.rdGold.ToString();
		this.txtRuby.text = this.rdRuby.ToString();
	}

	public void btnReload()
	{
		this.numReload--;
		if (this.numReload >= 0)
		{
			this.reload();
			this.updateBtnReload();
		}
		base.audioClick();
	}

	public void btnWatchVideo()
	{
		AdsManager.AdNumbers = 3;
		AdsManager.adsManager.showRewardVideo2();
		base.audioClick();
	}

	public void watchVideoSuccess()
	{
		NotificationPopup.instance.onShow(TextContent.getRewardSuccess);
		DataHolder.Instance.playerData.addGold(this.rdGold);
		DataHolder.Instance.playerData.addRuby(this.rdRuby);
		base.onClose();
		this._gameResult.canGetGift = false;
		this._gameResult.setColorButtonGift();
	}

	private void watchVideoFail()
	{
		NotificationPopup.instance.onShow(TextContent.videoNotReady);
	}
	public static SpecialGift spe;

	public Text txtReload;

	public Text txtGold;

	public Text txtRuby;

	public Text txtPower;

	private int numReload;

	private int rdGold;

	private int rdRuby;

	private int rdPower;

	public Sprite spr_gold;

	public Sprite spr_ruby;

	public Sprite spr_power;

	private int rd;

	public GameResult _gameResult;
}