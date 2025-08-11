using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroInfo : MonoBehaviour
{
	private void Awake()
	{
		UIHeroInfo.ins = this;
		this.comboMax = 0;
	}

	public void updateUIHP(int HP)
	{
		this.HPPercent = (float)HP / (float)this.HP;
		this.txtHP.text = ((int)(this.HPPercent * 100f)).ToString() + "%";
		this.imgHP.fillAmount = this.HPPercent;
	}

	public void updateUIEXP()
	{
		this.EXPPercent = (float)DataHolder.Instance.playerData.exp / (float)DataHolder.Instance.playerData.getNextLevelExp();
		this.txtEXP.text = ((int)(this.EXPPercent * 100f)).ToString() + "%";
		this.imgEXP.fillAmount = this.EXPPercent;
		this.txtLevel.text = (DataHolder.Instance.playerData.level + 1).ToString();
	}

	public void comboCount()
	{
		this.count++;
		this.txtCombo.text = this.count.ToString() + " Hit!";
		this._comboCount.Play("count", 0, 0f);
		if (this.count > this.comboMax)
		{
			this.comboMax = this.count;
		}
	}

	public void comborestart()
	{
		this.count = 0;
	}

	public static UIHeroInfo ins;

	public int HP;

	public Image imgHP;

	public Image imgEXP;

	public Text txtHP;

	public Text txtEXP;

	private float HPPercent;

	private float EXPPercent;

	public int expNextLevel;

	public int expCurrentLevel;

	public Text txtLevel;

	public Animator _comboCount;

	public Text txtCombo;

	private int count;

	public int comboMax;
}
