using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLibary : MonoBehaviour
{
	private void Start()
	{
		this.onClickSkill("NORMAL-ATK");
	}

	public void onClickSkill(string code)
	{
		Skill skill = DataHolder.Instance.skillDefine.getSkill(code);
		this.nameSKill.text = skill.name;
		this.desSkill.text = skill.des;
	}

	private IEnumerator moveWheel(string code)
	{
		for (;;)
		{
			this.wheel.eulerAngles = Vector3.MoveTowards(this.wheel.eulerAngles, new Vector3(0f, 0f, (float)this.wheelAngel[code]), 10f);
			if (this.wheel.eulerAngles.z == (float)this.wheelAngel[code])
			{
				break;
			}
			yield return new WaitForSeconds(Time.deltaTime);
		}
		yield break;
	}

	public RectTransform wheel;

	public Text nameSKill;

	public Text desSkill;

	private Dictionary<string, int> wheelAngel = new Dictionary<string, int>
	{
		{
			"KAMEHA",
			0
		},
		{
			"FAST-SHOOT",
			-60
		},
		{
			"FAST-PUNCH",
			-120
		},
		{
			"SUMMON",
			-180
		},
		{
			"TORNADO",
			-240
		},
		{
			"KENHKHI",
			-300
		}
	};
}
