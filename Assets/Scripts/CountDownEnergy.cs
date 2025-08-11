using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDownEnergy : MonoBehaviour
{
	private void Start()
	{
		this.checkEnergy();
	}

	private void checkEnergy()
	{
		if (DataHolder.Instance.playerData.energy < 100)
		{
			this.countDownText.enabled = true;
			int num = DatePassHelper.getSecPassed("ENERGY-RESTORE");
			if (num == -1)
			{
				num = 0;
				DatePassHelper.saveNowToPref("ENERGY-RESTORE", DatePassHelper.DateFormat.ddMMyyyyhhmmss);
			}
			if (num >= 300)
			{
				DatePassHelper.saveNowToPref("ENERGY-RESTORE", DatePassHelper.DateFormat.ddMMyyyyhhmmss);
				DataHolder.Instance.playerData.addEnergy(10);
				this.checkEnergy();
			}
			else
			{
				base.StartCoroutine(this.countDownCor(this.countDownText, 300 - num, new Action(this.checkEnergy)));
			}
		}
		else
		{
			this.countDownText.enabled = false;
		}
	}

	private IEnumerator countDownCor(Text label, int start, Action onFinish)
	{
		int _start = start;
		for (;;)
		{
			label.text = DatePassHelper.splitSecondToString(_start);
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
		yield break;
		yield break;
	}

	public Text countDownText;
}
