using System;
using UnityEngine;

public class Android19 : Enemies
{
	public override void attack()
	{
		this.canGetHit = false;
		this.rdAction = UnityEngine.Random.Range(0, 2);
		if (this.rdAction == 0)
		{
			this.att1();
		}
		else
		{
			this.att2();
		}
	}

	public override void hit(int _damage)
	{
		base.hit(_damage);
		base.playAudio(this.audioImpact);
	}

	private void att1()
	{
		this._animations.playAnimation(this._animations.attack, false);
		base.playAudio(this.audioAttack);
	}

	private void att2()
	{
		this._animations.playAnimation(this._animations.skill, false);
		base.playAudio(this.audioSkill);
	}

	private int rdAction;

	public AudioClip audioSkill;

	public AudioClip audioImpact;
}
