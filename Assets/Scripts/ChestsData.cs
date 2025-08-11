using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Game Data/Chest Data")]
public class ChestsData : ScriptableObject
{
	public Chest[] chests;
}
