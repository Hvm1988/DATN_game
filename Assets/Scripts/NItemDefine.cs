using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game Data/Item Define")]
public class NItemDefine : ScriptableObject
{
	public NItemDefine.CostToUpgrade[] costToUpgrade;

	public int[] goldCostToCraft;

	public int[] rubyCostToCraft;

	[Serializable]
	public class CostToUpgrade
	{
		public int[] gold;

		public int[] ruby;

		public int[] percent;
	}
}
