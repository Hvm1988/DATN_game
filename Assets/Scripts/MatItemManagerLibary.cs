using System;
using UnityEngine;

public class MatItemManagerLibary : MonoBehaviour
{
	private void OnEnable()
	{
		this.onClick(0);
	}

	public void onClick(int id)
	{
		this.selecteds[this.curID].SetActive(false);
		this.curID = id;
		this.selecteds[id].SetActive(true);
		NItem ui = DataHolder.Instance.mainItemsDefine.resourceItem[id];
		this.matItemInfo.setUI(ui);
	}

	public GameObject[] selecteds;

	public MatItemInfoInLibary matItemInfo;

	public int curID;
}
