using System;
using UnityEngine;

public class MainItemManagerLibary : MonoBehaviour
{
	private void OnEnable()
	{
		this.onClick(0);
	}

	public void onClick(int id)
	{
		this.selecteds[this.curID].SetActive(false);
		this.curID = id;
		this.selecteds[this.curID].SetActive(true);
		this.mainItemInfoInLib.setUI(DataHolder.Instance.mainItemsDefine.getMainByCode(this.itemCodes[this.curID]));
	}

	public GameObject[] selecteds;

	public int curID;

	public string[] itemCodes;

	public MainItemInfoInLibary mainItemInfoInLib;
}
