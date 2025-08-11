using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChestCountDown : MonoBehaviour
{
	private void Start()
	{
		this.checkTime();
	}

	public void checkTime()
	{
		if (DataHolder.Instance.inventory.getAllAttrByCode("SILVER-KEY") == 0 && DataHolder.Instance.inventory.freeKey[0] == 0)
		{
			int num = DatePassHelper.getSecPassed("COUNTDOWN-KEY1");
			if (num == -1)
			{
				DatePassHelper.saveNowToPref("COUNTDOWN-KEY1", DatePassHelper.DateFormat.ddMMyyyyhhmmss);
				num = 0;
			}
			if (num > this.countDownSec[0])
			{
				DataHolder.Instance.inventory.addFreeKey(0, 1);
				this.countDowns[0].text = string.Empty;
			}
			else if (!this.counting[0])
			{
				base.StartCoroutine(this.countDownCor1(this.countDowns[0], this.countDownSec[0] - num, delegate
				{
				}));
			}
		}
		if (DataHolder.Instance.inventory.getAllAttrByCode("GOLDEN-KEY") == 0)
		{
			int num2 = DatePassHelper.getSecPassed("COUNTDOWN-KEY2");
			if (num2 == -1)
			{
				DatePassHelper.saveNowToPref("COUNTDOWN-KEY2", DatePassHelper.DateFormat.ddMMyyyyhhmmss);
				num2 = 0;
			}
			if (num2 > this.countDownSec[1])
			{
				DataHolder.Instance.inventory.addFreeKey(1, 1);
				this.countDowns[1].text = string.Empty;
			}
			else if (!this.counting[1])
			{
				base.StartCoroutine(this.countDownCor2(this.countDowns[1], this.countDownSec[1] - num2, delegate
				{
				}));
			}
		}
		if (DataHolder.Instance.inventory.getAllAttrByCode("DIAMOND-KEY") == 0)
		{
			int num3 = DatePassHelper.getSecPassed("COUNTDOWN-KEY3");
			if (num3 == -1)
			{
				DatePassHelper.saveNowToPref("COUNTDOWN-KEY3", DatePassHelper.DateFormat.ddMMyyyyhhmmss);
				num3 = 0;
			}
			if (num3 > this.countDownSec[2])
			{
				DataHolder.Instance.inventory.addFreeKey(2, 1);
				this.countDowns[2].text = string.Empty;
			}
			else if (!this.counting[2])
			{
				base.StartCoroutine(this.countDownCor3(this.countDowns[2], this.countDownSec[2] - num3, delegate
				{
				}));
			}
		}
	}

	private IEnumerator countDownCor1(Text label, int start, Action onFinish)
	{
		this.counting[0] = true;
		int _start = start;
		for (;;)
		{
			label.text = "Free after: " + DatePassHelper.splitSecondToString(_start);
			if (_start == 0)
			{
				break;
			}
			yield return new WaitForSeconds(1f);
			_start--;
		}
		if (onFinish != null)
		{
			onFinish();
		}
		DataHolder.Instance.inventory.addFreeKey(0, 1);
		this.counting[0] = false;
		this.chestManager.setUI();
		TreasureNotifier.Instance.refresh();
		AccessoriesNotifier.Instance.refresh();
		yield break;
		yield break;
	}

	private IEnumerator countDownCor2(Text label, int start, Action onFinish)
	{
		this.counting[1] = true;
		int _start = start;
		for (;;)
		{
			label.text = "Free after: " + DatePassHelper.splitSecondToString(_start);
			if (_start == 0)
			{
				break;
			}
			yield return new WaitForSeconds(1f);
			_start--;
		}
		if (onFinish != null)
		{
			onFinish();
		}
		DataHolder.Instance.inventory.addFreeKey(1, 1);
		this.counting[1] = false;
		this.chestManager.setUI();
		TreasureNotifier.Instance.refresh();
		AccessoriesNotifier.Instance.refresh();
		yield break;
		yield break;
	}

	private IEnumerator countDownCor3(Text label, int start, Action onFinish)
	{
		this.counting[2] = true;
		int _start = start;
		for (;;)
		{
			label.text = "Free after: " + DatePassHelper.splitSecondToString(_start);
			if (_start == 0)
			{
				break;
			}
			yield return new WaitForSeconds(1f);
			_start--;
		}
		if (onFinish != null)
		{
			onFinish();
		}
		DataHolder.Instance.inventory.addFreeKey(2, 1);
		this.counting[2] = false;
		this.chestManager.setUI();
		TreasureNotifier.Instance.refresh();
		AccessoriesNotifier.Instance.refresh();
		yield break;
		yield break;
	}

	public Text[] countDowns;

	public ChestManager chestManager;

	public bool[] counting = new bool[3];

	private int[] countDownSec = new int[]
	{
		600,
		28800,
		86400
	};
}
