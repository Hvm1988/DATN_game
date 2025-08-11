using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGamePlay : MonoBehaviour
{
	private void Start()
	{
		this.tutorial();
		this.handGamePlay.SetActive(false);
	}

	private void tutorial()
	{
		switch (this.count)
		{
		case 0:
			this._animator.Play("tutorialGP");
			this.txtContent.text = "Firstly  I will guide you how to fight agaiast.";
			break;
		case 1:
			this._animator.Play("ttgp1");
			this.txtContent.text = "You can use it to move, just drag left or right.";
			this.joystick.sortingOrder = 10;
			break;
		case 2:
			this._animator.Play("ttgp2");
			this.txtContent.text = "The attack button to hit the enemy.";
			this.joystick.sortingOrder = 0;
			this.btnAtt.sortingOrder = 10;
			break;
		case 3:
			this._animator.Play("ttgp3");
			this.txtContent.text = "The jump button to attack the enemy on air.";
			this.btnAtt.sortingOrder = 0;
			this.btnJump.sortingOrder = 10;
			break;
		case 4:
			this._animator.Play("ttgp4");
			this.txtContent.text = "These skills buttons with powerful attacks.";
			this.btnJump.sortingOrder = 0;
			this.btnSkill1.sortingOrder = 10;
			this.btnSkill2.sortingOrder = 10;
			this.btnSkill3.sortingOrder = 10;
			break;
		case 5:
			this._animator.Play("ttgp5");
			this.txtContent.text = "You can summon the dragon to support.";
			this.btnSkill1.sortingOrder = 0;
			this.btnSkill2.sortingOrder = 0;
			this.btnSkill3.sortingOrder = 0;
			this.btnDragon.sortingOrder = 10;
			break;
		case 6:
			this._animator.Play("ttgp6");
			this.txtContent.text = "The special skill, It make you increase: 30% attack, 30% defense within 15 minutes.";
			this.btnDragon.sortingOrder = 0;
			this.SSJ.sortingOrder = 10;
			break;
		case 7:
			this._animator.Play("ttgp7");
			this.txtContent.text = "Let's go ahead to defeat all enemy.";
			this.SSJ.sortingOrder = 0;
			break;
		case 8:
			this.skip();
			break;
		}
	}

	public void touchToContinue()
	{
		this.count++;
		this.tutorial();
	}

	public void skip()
	{
		NotificationPopup.instance.onShow("Are you ready?", delegate()
		{
			this.ready();
		});
	}

	private void ready()
	{
		base.gameObject.SetActive(false);
		this.handGamePlay.SetActive(true);
		Timer.ins.startTimeCountDown();
	}

	public Text txtContent;

	public Animator _animator;

	public Canvas joystick;

	public Canvas btnAtt;

	public Canvas btnJump;

	public Canvas btnSkill1;

	public Canvas btnSkill2;

	public Canvas btnSkill3;

	public Canvas btnDragon;

	public Canvas SSJ;

	public int count;

	public GameObject handGamePlay;
}
