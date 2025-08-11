using System;
using UnityEngine;

public class ScoreEffect : MonoBehaviour
{
	public void showEffect(int num)
	{
		this.txts[0].set_num(num);
		this.txts[0].gameObject.SetActive(true);
		this.txtSave = this.txts[0];
		for (int i = 0; i < this.txts.Length - 1; i++)
		{
			this.txts[i] = this.txts[i + 1];
		}
		this.txts[this.txts.Length - 1] = this.txtSave;
	}

	public void showEffectOneHit(int num)
	{
		this.txts[0].set_numRed(num);
		this.txts[0].gameObject.SetActive(true);
		this.txtSave = this.txts[0];
		for (int i = 0; i < this.txts.Length - 1; i++)
		{
			this.txts[i] = this.txts[i + 1];
		}
		this.txts[this.txts.Length - 1] = this.txtSave;
	}

	public Set_number_show[] txts;

	private Set_number_show txtSave;
}
