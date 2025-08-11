using System;
using UnityEngine;

public class AnimationEventObj : MonoBehaviour
{
	private void Start()
	{
		this._animator = base.GetComponent<Animator>();
	}

	public void disableAnimator()
	{
		this._animator.enabled = false;
	}

	private Animator _animator;
}
