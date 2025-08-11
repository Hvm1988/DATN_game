using System;
using System.Collections;
using Spine;
using Spine.Unity;
using UnityEngine;

public class PetAnimation : MonoBehaviour
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
		GameSave.damagePet = DataHolder.Instance.skillData.getDamageAskill("SUMMON");
	}

	public void playAnimationAttack()
	{
		this.skeletonAnimation.state.SetAnimation(0, this.fly, false);
	}

	private void OnEnable()
	{
		base.StartCoroutine(this.playAudio(this._audioPet, 0f));
	}

	private void HandleEvent(TrackEntry entry, Spine.Event e)
	{
		if (e.Data.Name.Equals("done"))
		{
			this.effect.SetActive(true);
			this.boxFake.SetActive(true);
			this.skeletonAnimation.state.SetAnimation(0, this.attack, true);
			this.left = new Vector3(this._gameManager.doorLeft.transform.position.x - 5f, 1.08f, 0f);
			this.right = new Vector3(this._gameManager.doorRight.transform.position.x + 5f, 1.08f, 0f);
			base.StartCoroutine(this.control());
		}
	}

	private IEnumerator control()
	{
		base.StartCoroutine(this.playAudio(this._audioLaser, 0.3f));
		base.transform.parent.position = this.right;
		base.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		iTween.MoveTo(base.transform.parent.gameObject, iTween.Hash(new object[]
		{
			"position",
			this.left,
			"time",
			5,
			"easetype",
			iTween.EaseType.linear
		}));
		this.bem1.SetActive(true);
		this.bem.SetActive(false);
		yield return new WaitForSeconds(5.1f);
		base.transform.localEulerAngles = Vector3.zero;
		this.bem1.SetActive(false);
		this.bem.SetActive(true);
		iTween.MoveTo(base.transform.parent.gameObject, iTween.Hash(new object[]
		{
			"position",
			this.right,
			"time",
			5,
			"easetype",
			iTween.EaseType.linear
		}));
		yield return new WaitForSeconds(5.1f);
		this.effect.SetActive(false);
		this.boxFake.SetActive(false);
		iTween.Stop();
		this.bem1.SetActive(false);
		this.bem.SetActive(false);
		this._audioPet.Stop();
		this._audioLaser.Stop();
		base.transform.parent.gameObject.SetActive(false);
		yield break;
	}

	private IEnumerator playAudio(AudioSource _audio, float timeDelay)
	{
		yield return new WaitForSeconds(timeDelay);
		if (GameConfig.soundVolume > 0f)
		{
			if (_audio.volume != GameConfig.soundVolume)
			{
				_audio.volume = GameConfig.soundVolume;
			}
			_audio.Play();
			UnityEngine.Debug.Log("===========================");
		}
		yield break;
	}

	[SpineAnimation("", "", true, false)]
	public string attack;

	[SpineAnimation("", "", true, false)]
	public string fly;

	private const string animationDone = "done";

	private SkeletonAnimation skeletonAnimation;

	public GameObject effect;

	public GameObject boxFake;

	public GameManager _gameManager;

	private Vector3 left;

	private Vector3 right;

	public GameObject bem;

	public GameObject bem1;

	public AudioSource _audioPet;

	public AudioSource _audioLaser;
}
