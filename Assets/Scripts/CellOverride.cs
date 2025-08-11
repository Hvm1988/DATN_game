using System;
using System.Collections;
using UnityEngine;

public class CellOverride : Enemies
{
	public override void attack()
	{
		base.attack();
		base.StartCoroutine(this.delay());
	}

	private IEnumerator delay()
	{
		yield return new WaitForSeconds(0.5f);
		this.objSkill.SetActive(true);
		yield break;
	}

	public GameObject objSkill;
}
