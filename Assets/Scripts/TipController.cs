using System;
using UnityEngine;
using UnityEngine.UI;

public class TipController : MonoBehaviour
{
	private void Start()
	{
		this.contentTxt.text = "Tip: " + TipController.tipTemplates[UnityEngine.Random.Range(0, TipController.tipTemplates.Length)];
	}

	private static string[] tipTemplates = new string[]
	{
		"Collect all requrie material to carft powerful item.",
		"Upgrade your eqiupment to increa your power!",
		"You can buy everything with very good price in black market.",
		"Black market will refresh each 60 minutes.",
		"You can get daily gift in event menu every new day.",
		"Chose your best skill way to build your character.",
		"<color=red>Legendary</color> > <color=yellow>Imortal</color> > <color=magenta>Mythical</color> > <color=blue>Rare</color> > <color=green>UnCommon</color> > <color=white>Common</color> item.",
		"If you have passed a level. You can use skip to quick pass this level again",
		"Stamina is availabe to buy in shop with ruby",
		"Complete all daily mission to get free ruby",
		"Get more gifts from video reward",
		"Material and scroll will drop during you clear a level",
		"Open treasure to take a chance to win many value item",
		"If you continue play a level when your material inventory is full. The item pick up when kill monster when do not go in your inventory.",
		"Hold or hit reapeatedly the attack button to active extra combo",
		"You should kill the ranger monster first.",
		"You can hit the attack button after jump to destroy fly monster."
	};

	public Text contentTxt;
}
