using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
	private void Start()
	{
		if (this.code.Equals("NORMAL-ATK"))
		{
			this.onClick();
		}
	}

	public void init(SkillTreeManager stm)
	{
		this.STM = stm;
		this.skillSave = DataHolder.Instance.skillData.getASkill(this.code);
		if (this.skillSave.code.Equals("NORMAL-ATK"))
		{
			this.skillSave.unlockNextSkill();
		}
		this.setUI();
	}

	public void setUI()
	{
		if (!this.skillSave.canUpgrade())
		{
			this.icon.color = new Color(0.3f, 0.3f, 0.3f, 1f);
		}
		else
		{
			this.icon.color = Color.white;
			if (this.skillSave.level > 0)
			{
				for (int i = 0; i < this.line.Length; i++)
				{
					SkillSlotUI.LineSkillUI lineSkillUI = this.line[i];
					string nextSkill = lineSkillUI.nextSkill;
					SkillSave askill = DataHolder.Instance.skillData.getASkill(nextSkill);
					if (askill.level > 0)
					{
						lineSkillUI.show();
					}
				}
			}
		}
	}

	public SkillSlotUI.LineSkillUI getLine(string code)
	{
		foreach (SkillSlotUI.LineSkillUI lineSkillUI in this.line)
		{
			if (lineSkillUI.nextSkill.Equals(code))
			{
				return lineSkillUI;
			}
		}
		return null;
	}

	public void onClick()
	{
		this.STM.onClick(this.skillSave);
	}

	public Image icon;

	public SkillSlotUI.LineSkillUI[] line;

	public bool isLock;

	public string code;

	public SkillSave skillSave;

	public SkillTreeManager STM;

	[Serializable]
	public class LineSkillUI
	{
		public void hide()
		{
			this.light.SetActive(false);
		}

		public void show()
		{
			this.light.SetActive(true);
		}

		public string nextSkill;

		public GameObject dark;

		public GameObject light;
	}
}
