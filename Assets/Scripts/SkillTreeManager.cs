using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
	private void OnEnable()
	{
		this.restoreBtn.gameObject.SetActive(false);
		this.init();
	}

	public void init()
	{
		foreach (SkillSave skillSave in DataHolder.Instance.skillData.skillSaves)
		{
			skillSave.calCondition();
		}
		DataHolder.Instance.skillData.save();
		foreach (SkillSlotUI skillSlotUI in this.skillSlotUIs)
		{
			skillSlotUI.init(this);
		}
	}

	public void setUI()
	{
		foreach (SkillSlotUI skillSlotUI in this.skillSlotUIs)
		{
			skillSlotUI.setUI();
		}
		this.onClick(this.curSkill);
	}

	public void onClick(SkillSave skill)
	{
		this.curSkill = skill;
		this.sk = DataHolder.Instance.skillDefine.getSkill(skill.code);
		this.iconPreview.sprite = this.sk.icon;
		this.name.text = this.sk.name;
		this.level.text = skill.getLevelString();
		this.des.text = this.sk.des + "\n" + this.sk.getDetailSkill(skill.level);
		this.goldCost.text = this.sk.getGoldNextLevel(this.curSkill.level) + string.Empty;
		this.rubyCost.text = this.sk.getRubyNextLevel(this.curSkill.level) + string.Empty;
		if (this.sk.GetType() == typeof(SkillActive))
		{
			this.des.text = this.des.text + "\n" + ((SkillActive)this.sk).getCoolDown(skill.level);
		}
		if (!this.curSkill.canUpgrade())
		{
			this.upGold.gameObject.SetActive(false);
			this.upRuby.gameObject.SetActive(false);
			this.lvlReq.enabled = true;
			if (this.curSkill.conditions[0] == 0)
			{
				this.lvlReq.text = "You must unlock previous skill!";
			}
			else if (this.curSkill.conditions[1] == 0)
			{
				this.lvlReq.text = "Reach to level " + (this.sk.levelReq + 1) + " to learn!";
			}
			else if (this.curSkill.conditions[2] == 0)
			{
				this.lvlReq.text = "You chosen another skill line!";
			}
		}
		else
		{
			this.upGold.gameObject.SetActive(skill.level < 10);
			this.upRuby.gameObject.SetActive(skill.level < 10);
			this.lvlReq.enabled = false;
		}
		this.setSkillNode(skill.level);
	}

	public void callUpgardeSkill(int type)
	{
		this.typeUpgrade = type;
		if (this.sk.inGroupSkills.Length > 0 && this.curSkill.level == 0)
		{
			this.waringSkillLine.SetActive(true);
		}
		else
		{
			this.upgradeSkill();
		}
	}

	public void upgradeSkill()
	{
		try
		{
			this.curSkill.upgrade(this.typeUpgrade);
			this.setUI();
		}
		catch (Exception ex)
		{
			if (ex.Message.Equals("NOT_ENOUGHT_GOLD"))
			{
				this.notEnoughtGold.SetActive(true);
			}
			if (ex.Message.Equals("NOT_ENOUGHT_RUBY"))
			{
				this.notEnoughtRuby.SetActive(true);
			}
		}
	}

	public void restore()
	{
	}

	public void setSkillNode(int level)
	{
		for (int i = 0; i < this.skillLevelNode.Length; i++)
		{
			if (i <= level - 1)
			{
				this.skillLevelNode[i].SetActive(true);
			}
			else
			{
				this.skillLevelNode[i].SetActive(false);
			}
		}
	}

	public SkillSlotUI[] skillSlotUIs;

	public Image iconPreview;

	public new Text name;

	public Text level;

	public Text des;

	public Text detail;

	public Text coolDown;

	public Button upGold;

	public Text goldCost;

	public Button upRuby;

	public Text rubyCost;

	public SkillSave curSkill;

	public Text lvlReq;

	public Button restoreBtn;

	public GameObject[] skillLevelNode;

	public GameObject notEnoughtRuby;

	public GameObject notEnoughtGold;

	private int typeUpgrade;

	public GameObject waringSkillLine;

	private Skill sk;
}
