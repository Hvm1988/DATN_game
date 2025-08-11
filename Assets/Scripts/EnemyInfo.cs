using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour
{
	public void onShow(string name, int level, int att, int def, int hp)
	{
		this.txtName.text = name + " :lv " + (level + 1).ToString();
		this.txtAtt.text = "Att = " + att.ToString();
		this.txtDef.text = "Def = " + def.ToString();
		this.txtHP.text = "HP = " + hp.ToString();
	}

	public Text txtName;

	public Text txtAtt;

	public Text txtDef;

	public Text txtHP;
}
