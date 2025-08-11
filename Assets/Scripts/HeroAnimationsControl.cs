using System;
using Spine;
using Spine.Unity;
using UnityEngine;

public class HeroAnimationsControl : MonoBehaviour
{
	private void Start()
	{
		this.state = base.GetComponent<SkeletonAnimation>();
		SkeletonAnimation component = base.GetComponent<SkeletonAnimation>();
		if (component == null)
		{
			return;
		}
		component.state.Event += this.HandleEvent;
	}

	public void playAnimation(string isName, bool isLoop)
	{
		this.state.state.SetAnimation(0, isName, isLoop);
	}

	private void HandleEvent(TrackEntry entry, Spine.Event e)
	{
		if (e.Data.Name.Equals("done"))
		{
			Hero.animationDone = true;
			Hero.heroCanAttack = true;
			if (!Hero.btnMoveClick)
			{
				this.playAnimation(this.idle, true);
			}
			else if (this.hero.arrow == -1)
			{
				this.hero.moveLeft();
			}
			else if (this.hero.arrow == 1)
			{
				this.hero.moveRight();
			}
			if (this.hero.is_SSJ_up)
			{
				this.hero.ssjEffect.SetActive(true);
			}
			if (Hero.jumpLoop)
			{
				this.hero.jump();
			}
			if (Hero.is_loopCombo)
			{
				this.hero.attack();
			}
			if (this.hero.skill_playing)
			{
				this.hero.skill_playing = false;
			}
		}
		else if (e.Data.Name.Equals("start"))
		{
			Hero.animationDone = true;
			Hero.jumping = false;
			this.hero.canMove = true;
			Hero.heroCanAttack = true;
			this.hero._audioGame.run.Stop();
		}
		else if (e.Data.Name.Equals("box_enable"))
		{
			this.boxFake.SetActive(true);
		}
		else if (e.Data.Name.Equals("box_disable"))
		{
			this.boxFake.SetActive(false);
			this.hero.setRotationHero();
		}
		else if (e.Data.Name.Equals("rotation_start"))
		{
			this.boxRotation.SetActive(true);
		}
		else if (e.Data.Name.Equals("rotation_end"))
		{
			Hero.is_longRotation = false;
			this.boxRotation.SetActive(false);
		}
		else if (e.Data.Name.Equals("box_land"))
		{
			this.boxLand.SetActive(true);
			this._gameManager.playCamAnimation(0f, "cameraVibrate2");
		}
		else if (e.Data.Name.Equals("can_move"))
		{
			this.hero.canMove = true;
		}
		else if (e.Data.Name.Equals("cant_move"))
		{
			this.hero.canMove = false;
		}
	}

	public void upgrade(string lv)
	{
		this.state.initialSkinName = "lv" + lv;
	}

	private SkeletonAnimation state;

	[SpineAnimation("", "", true, false)]
	public string idle;

	[SpineAnimation("", "", true, false)]
	public string move;

	[SpineAnimation("", "", true, false)]
	public string punch1;

	[SpineAnimation("", "", true, false)]
	public string punch2;

	[SpineAnimation("", "", true, false)]
	public string punch3;

	[SpineAnimation("", "", true, false)]
	public string punchCombo;

	[SpineAnimation("", "", true, false)]
	public string skill1;

	[SpineAnimation("", "", true, false)]
	public string skill2;

	[SpineAnimation("", "", true, false)]
	public string skill3;

	[SpineAnimation("", "", true, false)]
	public string skill4;

	[SpineAnimation("", "", true, false)]
	public string ssj;

	[SpineAnimation("", "", true, false)]
	public string jump;

	[SpineAnimation("", "", true, false)]
	public string hit;

	[SpineAnimation("", "", true, false)]
	public string start;

	[SpineAnimation("", "", true, false)]
	public string sortRotation;

	[SpineAnimation("", "", true, false)]
	public string longRotation;

	[SpineAnimation("", "", true, false)]
	public string dead;

	private const string animationDone = "done";

	private const string idlePlaying = "start";

	private const string enableBox = "box_enable";

	private const string disableBox = "box_disable";

	private const string instance = "instance";

	private const string comboLoop = "is_loop";

	private const string rotationStart = "rotation_start";

	private const string rotationEnd = "rotation_end";

	private const string strBoxLand = "box_land";

	private const string strCanMove = "can_move";

	private const string strCantMove = "cant_move";

	public GameObject boxFake;

	public GameObject boxLand;

	public GameObject boxRotation;

	public GameManager _gameManager;

	public Hero hero;

	public MoveController moveControl;
}
