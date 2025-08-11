using System;
using Spine;
using Spine.Unity;
using UnityEngine;

public class EnemiesAnimationSpine : MonoBehaviour
{
	private void Awake()
	{
		this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
		if (this.skeletonAnimation == null)
		{
			return;
		}
	}

	private void Start()
	{
		this.skeletonAnimation.state.Event += this.HandleEvent;
	}

	public void playAnimation(string isName, bool isLoop)
	{
		this.skeletonAnimation.state.SetAnimation(0, isName, isLoop);
	}

	private void HandleEvent(TrackEntry entry, Spine.Event e)
	{
		if (e.Data.Name.Equals("done"))
		{
			this.playAnimation(this.idle, true);
			this.parrent.canGetHit = true;
		}
		else if (e.Data.Name.Equals("box_enable"))
		{
			this.boxFake.SetActive(true);
		}
		else if (e.Data.Name.Equals("box_disable"))
		{
			this.boxFake.SetActive(false);
		}
		else if (e.Data.Name.Equals("control"))
		{
			this.parrent.control();
		}
		else if (e.Data.Name.Equals("skill_enable"))
		{
			this.boxFakeSkill.SetActive(true);
		}
		else if (e.Data.Name.Equals("skill_disable"))
		{
			this.boxFakeSkill.SetActive(false);
		}
	}

	[SpineAnimation("", "", true, false)]
	public string idle;

	[SpineAnimation("", "", true, false)]
	public string move;

	[SpineAnimation("", "", true, false)]
	public string attack;

	[SpineAnimation("", "", true, false)]
	public string die;

	[SpineAnimation("", "", true, false)]
	public string skill;

	[SpineAnimation("", "", true, false)]
	public string skill2;

	[SpineAnimation("", "", true, false)]
	public string skill3;

	[SpineAnimation("", "", true, false)]
	public string hit;

	private const string animationDone = "done";

	private const string boxEnable = "box_enable";

	private const string boxDisable = "box_disable";

	private const string start = "start";

	private const string control = "control";

	private const string skillEnable = "skill_enable";

	private const string skillDisable = "skill_disable";

	public GameObject boxFake;

	public GameObject boxFakeSkill;

	public Enemies parrent;

	private SkeletonAnimation skeletonAnimation;
}
