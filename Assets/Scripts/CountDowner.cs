using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CountDowner : MonoBehaviour
{
	private IEnumerator countDownCor(int start)
	{
		int _start = start;
		for (;;)
		{
			this.labelTime.text = DatePassHelper.splitSecondToString(_start);
			if (_start == 0)
			{
				break;
			}
			yield return new WaitForSeconds(1f);
			_start--;
		}
		if (this.onFinish != null)
		{
			this.onFinish.Invoke();
		}
		yield break;
		yield break;
	}

	public bool counting;

	public Text labelTime;

	public int coolDown;

	public UnityEvent onFinish;
}
