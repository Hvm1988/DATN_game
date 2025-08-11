using System;
using UnityEngine;

public class Set_number_show : MonoBehaviour
{
	public void set_num(int value)
	{
		if (value > 99999)
		{
			value = 99999;
		}
		for (int i = 0; i < this.Num.Length; i++)
		{
			this.Num[i].sprite = null;
		}
		this.lengt_ = value.ToString().Length;
		this.vitri = this.lengt_;
		this.phan_du = value;
		for (int j = 0; j < this.lengt_; j++)
		{
			int num = this.phan_du / this.mod_[this.vitri - 1];
			this.Num[j].sprite = ResourceImage.ins.numberWhite[num];
			this.phan_du %= this.mod_[this.vitri - 1];
			this.vitri--;
		}
	}

	public void set_numRed(int value)
	{
		if (value > 99999)
		{
			value = 99999;
		}
		for (int i = 0; i < this.Num.Length; i++)
		{
			this.Num[i].sprite = null;
		}
		this.lengt_ = value.ToString().Length;
		this.vitri = this.lengt_;
		this.phan_du = value;
		for (int j = 0; j < this.lengt_; j++)
		{
			int num = this.phan_du / this.mod_[this.vitri - 1];
			this.Num[j].sprite = ResourceImage.ins.numberRed[num];
			this.phan_du %= this.mod_[this.vitri - 1];
			this.vitri--;
		}
	}

	public void set_numGreen(int value)
	{
		if (value > 99999)
		{
			value = 99999;
		}
		for (int i = 0; i < this.Num.Length; i++)
		{
			this.Num[i].sprite = null;
		}
		this.lengt_ = value.ToString().Length;
		this.vitri = this.lengt_;
		this.phan_du = value;
		for (int j = 0; j < this.lengt_; j++)
		{
			int num = this.phan_du / this.mod_[this.vitri - 1];
			this.Num[j].sprite = ResourceImage.ins.numberGreen[num];
			this.phan_du %= this.mod_[this.vitri - 1];
			this.vitri--;
		}
	}

	public void set_numGold(int value)
	{
		if (value > 99999)
		{
			value = 99999;
		}
		for (int i = 0; i < this.Num.Length; i++)
		{
			this.Num[i].sprite = null;
		}
		this.lengt_ = value.ToString().Length;
		this.vitri = this.lengt_;
		this.phan_du = value;
		for (int j = 0; j < this.lengt_; j++)
		{
			int num = this.phan_du / this.mod_[this.vitri - 1];
			this.Num[j].sprite = ResourceImage.ins.numberGold[num];
			this.phan_du %= this.mod_[this.vitri - 1];
			this.vitri--;
		}
	}

	public void set_numPink(int value)
	{
		if (value > 99999)
		{
			value = 99999;
		}
		for (int i = 0; i < this.Num.Length; i++)
		{
			this.Num[i].sprite = null;
		}
		this.lengt_ = value.ToString().Length;
		this.vitri = this.lengt_;
		this.phan_du = value;
		for (int j = 0; j < this.lengt_; j++)
		{
			int num = this.phan_du / this.mod_[this.vitri - 1];
			this.Num[j].sprite = ResourceImage.ins.numberPink[num];
			this.phan_du %= this.mod_[this.vitri - 1];
			this.vitri--;
		}
	}

	public SpriteRenderer[] Num;

	private readonly int[] mod_ = new int[]
	{
		1,
		10,
		100,
		1000,
		10000
	};

	private int lengt_;

	private int phan_nguyen;

	private int phan_du;

	private int vitri;
}
