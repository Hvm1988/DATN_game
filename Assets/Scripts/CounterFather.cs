using System;
using System.Diagnostics;
using UnityEngine;

public class CounterFather : MonoBehaviour
{
	public static event CounterFather.OnChangeGold onChangeGold;

	public static event CounterFather.OnChangeRuby onChangeRuby;

	public static event CounterFather.OnChangeEnergy onChangeEnergy;


	public static event CounterFather.OnChangeSkip onChangeSkip;


	public static event CounterFather.OnChangePlayerStat onChangePlayerStat;


	public static event CounterFather.OnChangePlayerStat onChangePlayerName;

	public static CounterFather Instance
	{
		get
		{
			if (CounterFather.instance == null)
			{
				CounterFather.instance = UnityEngine.Object.FindObjectOfType<CounterFather>();
			}
			return CounterFather.instance;
		}
	}

	public void changeGold()
	{
		if (CounterFather.onChangeGold != null)
		{
			CounterFather.onChangeGold();
		}
	}

	public void changeRuby()
	{
		if (CounterFather.onChangeRuby != null)
		{
			CounterFather.onChangeRuby();
		}
	}

	public void changeName()
	{
		if (CounterFather.onChangePlayerName != null)
		{
			CounterFather.onChangePlayerName();
		}
	}

	public void changeEnergy()
	{
		if (CounterFather.onChangeEnergy != null)
		{
			CounterFather.onChangeEnergy();
		}
	}

	public void changeSkip()
	{
		if (CounterFather.onChangeSkip != null)
		{
			CounterFather.onChangeSkip();
		}
	}

	public void changePlayerStat()
	{
		if (CounterFather.onChangePlayerStat != null)
		{
			CounterFather.onChangePlayerStat();
		}
	}

	private static CounterFather instance;

	public delegate void OnChangeGold();

	public delegate void OnChangeRuby();

	public delegate void OnChangeEnergy();

	public delegate void OnChangeSkip();

	public delegate void OnChangePlayerStat();

	public delegate void OnChangePlayerName();
}
