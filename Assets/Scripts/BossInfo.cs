using System;
using UnityEngine;
using UnityEngine.UI;

public class BossInfo : MonoBehaviour
{
	private void Awake()
	{
		BossInfo.instance = this;
	}

	public void onSetup(string nameBoss, int HP, Sprite avatar)
	{
		this.HP = HP;
		this.txtName.text = nameBoss;
		this.avatar.sprite = avatar;
	}

	public void updateUI(int HP)
	{
		this.percent = (float)HP / (float)this.HP;
		this.HP_fill.fillAmount = this.percent;
	}

	public Text txtName;

	public Image HP_fill;

	public Image avatar;

	public static BossInfo instance;

	private int HP;

	private float percent;
}
