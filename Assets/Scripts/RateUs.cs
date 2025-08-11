using System;
using UnityEngine;

public class RateUs : MonoBehaviour
{
	private void Awake()
	{
		RateUs.ins = this;
		this._anim = base.GetComponent<Animator>();
	}

	public void onShow()
	{
		if (!DataHolder.Instance.playerData.clickedRate)
		{
			this._anim.Play("rateOpen");
		}
	}

	public void onShowEveryWhere()
	{
		this._anim.Play("rateOpen");
	}

	public void btnRate()
	{
		DataHolder.Instance.playerData.clickedRate = true;
		DataHolder.Instance.playerData.save();
		Application.OpenURL("market://details?id=com.");
	}

	public void btbnLater()
	{
		this._anim.Play("rateClose");
	}

	public static RateUs ins;

	private Animator _anim;
}
