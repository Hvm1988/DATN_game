using System;
using UnityEngine;

public class AnimationsControl : MonoBehaviour
{
	private void Start()
	{
		if (this.getComponentAnimator)
		{
			this._animator = base.gameObject.GetComponent<Animator>();
		}
	}

	public void disableObj()
	{
		base.gameObject.SetActive(false);
	}

	public void disableAnimator()
	{
		this._animator.enabled = false;
	}

	public bool getComponentAnimator;

	private Animator _animator;
}
