using System;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{

    public void RefreshStatsUI() => onChangePlayerStat();   // <- thêm dòng này

    private void Awake()
	{
		this.text = base.GetComponent<Text>();
		this.slider = base.GetComponent<Image>();
	}

	private void OnEnable()
	{
		CounterFather.onChangeGold += this.onChangeGold;
		CounterFather.onChangeRuby += this.onChangeRuby;
		CounterFather.onChangeEnergy += this.onChangeEnergy;
		CounterFather.onChangeSkip += this.onChangeSkip;
		CounterFather.onChangePlayerStat += this.onChangePlayerStat;
		CounterFather.onChangePlayerName += this.onChangePlayerName;
	}

	private void OnDisable()
	{
		CounterFather.onChangeGold -= this.onChangeGold;
		CounterFather.onChangeRuby -= this.onChangeRuby;
		CounterFather.onChangeEnergy -= this.onChangeEnergy;
		CounterFather.onChangeSkip -= this.onChangeSkip;
		CounterFather.onChangePlayerStat -= this.onChangePlayerStat;
		CounterFather.onChangePlayerName -= this.onChangePlayerName;
	}

	private void Start()
	{
		if (base.name.Equals("Gold"))
		{
			this.text.text = DataHolder.Instance.playerData.getGoldString() + string.Empty;
		}
		if (base.name.Equals("Ruby"))
		{
			this.text.text = DataHolder.Instance.playerData.getRubyString() + string.Empty;
		}
		this.onChangeSkip();
		this.onChangePlayerStat();
		this.onChangeEnergy();
		this.onChangePlayerName();
	}

	private void onChangePlayerStat()
	{
		PlayerData playerData = DataHolder.Instance.playerData;
		if (base.name.Equals("hp"))
		{
			this.text.text = playerData.hp + string.Empty;
		}
		if (base.name.Equals("atk"))
		{
			this.text.text = playerData.atk + string.Empty;
		}
		if (base.name.Equals("def"))
		{
			this.text.text = playerData.def + string.Empty;
		}
		if (base.name.Equals("nameinbg"))
		{
			this.text.text = string.Concat(new object[]
			{
				"Lv:",
				playerData.level + 1,
				" ",
				playerData.name
			});
		}
		if (base.name.Equals("ExpSlider"))
		{
			this.slider.fillAmount = DataHolder.Instance.playerData.getExpFloat();
		}
		if (base.name.Equals("Exp"))
		{
			this.text.text = DataHolder.Instance.playerData.getExpPercent() + "%";
		}
	}

	private void onChangeGold()
	{
		if (base.name.Equals("Gold"))
		{
			this.text.text = DataHolder.Instance.playerData.getGoldString() + string.Empty;
		}
	}

	private void onChangeRuby()
	{
		if (base.name.Equals("Ruby"))
		{
			this.text.text = DataHolder.Instance.playerData.getRubyString() + string.Empty;
		}
	}

	private void onChangeEnergy()
	{
		if (base.name.Equals("Energy"))
		{
			this.text.text = DataHolder.Instance.playerData.energy + string.Empty;
		}
	}

	private void onChangeSkip()
	{
		if (base.name.Equals("Skip"))
		{
			this.text.text = DataHolder.Instance.playerData.skip + string.Empty;
		}
	}

	private void onChangePlayerName()
	{
		if (base.name.Equals("nameinbg"))
		{
			this.text.text = string.Concat(new object[]
			{
				"Lv:",
				DataHolder.Instance.playerData.level + 1,
				" ",
				DataHolder.Instance.playerData.name
			});
		}
	}

	private Text text;

	private Image slider;
}
