using System;
using UnityEngine;

public class BonusIconControl : MonoBehaviour
{
	private void Awake()
	{
		BonusIconControl.ins = this;
	}

	private void Start()
	{
		if (DataHolder.watchedVideoAddStat)
		{
			this.att.SetActive(true);
			this.def.SetActive(true);
			this.hp.SetActive(true);
		}
	}

	public static BonusIconControl ins;

	public GameObject exp;

	public GameObject speedDown;

	public GameObject hp;

	public GameObject def;

	public GameObject att;

	public GameObject speedUp;

	public GameObject coin;

	public GameObject timeUp;

	public GameObject toxic;

	public GameObject mar;
}
