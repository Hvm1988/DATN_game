using System;
using UnityEngine;

public class BoxTriggerEvent : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("hero"))
		{
			this.parrent.triggerHero();
			if (this.is_Skill)
			{
				this.parrent.superSkillHero(base.transform.position.x);
			}
		}
	}

	public Enemies parrent;

	public bool is_Skill;
}
