using System;
using UnityEngine;

public class UpgradeUIController : MonoBehaviour
{
	public static UpgradeUIController Instance
	{
		get
		{
			if (UpgradeUIController.instance == null)
			{
				UpgradeUIController.instance = UnityEngine.Object.FindObjectOfType<UpgradeUIController>();
			}
			return UpgradeUIController.instance;
		}
	}

	public void SetUI()
	{
		for (int i = 0; i < this.subItemInUpgrades.Length; i++)
		{
			this.subItemInUpgrades[i].setUI();
		}
	}

	public SubItemInUpgrade[] subItemInUpgrades;

	private static UpgradeUIController instance;
}
