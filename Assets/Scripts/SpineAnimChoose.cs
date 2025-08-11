using System;
using Spine;
using Spine.Unity;
using UnityEngine;

public class SpineAnimChoose : MonoBehaviour
{
	private void Start()
	{
		this.skeletonAnimation.state.Event += this.HandleEvent;
	}

	public void playAnimation(bool isLoop)
	{
		this.skeletonAnimation.state.SetAnimation(0, this.animName, isLoop);
	}

	private void HandleEvent(TrackEntry entry, Spine.Event e)
	{
		if (e.Data.Name.Equals("effect_done"))
		{
			base.gameObject.SetActive(false);
		}
	}

	[SpineAnimation("", "", true, false)]
	public string animName;

	public SkeletonAnimation skeletonAnimation;
}
