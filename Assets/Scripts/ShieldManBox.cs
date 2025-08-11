using System;
using UnityEngine;

public class ShieldManBox : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("hero"))
		{
			this.parrent.triggerHero(this.parrent.damage / 3);
			this.parrent.superSkillHero(base.transform.position.x);
		}
	}

	public Enemies parrent;
}
