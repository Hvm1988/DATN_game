using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PopupController : MonoBehaviour
{
    private void Awake()
    {
		if (animator == null)
		{
			animator = GetComponent<Animator>();
			animator.enabled = false;
		}
    }

    private void OnEnable()
	{
		if (this.onEnable != null)
		{
			this.onEnable.Invoke();
			StartCoroutine(CREnable());
		}
	}

	private void OnDisable()
	{
		if (this.onDisable != null)
		{
			this.onDisable.Invoke();
		}
	}

	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
		this.father = base.transform.parent.gameObject;
	}

	public void disable()
	{
		//transform.localScale = Vector3.zero;
		//this.animator.Play("Hide");
		StartCoroutine(CRDisable());
	}

	public void disablePanel()
	{
		this.father.SetActive(false);
	}


    IEnumerator CREnable()
    {
		yield return null;
        float t = 0;
        float timeCount = 0.25f;
        while (t < timeCount)
        {
            t += Time.deltaTime;
            float factor = t / timeCount;
			transform.localScale = Vector3.Lerp(Vector3.one * 0.3f, Vector3.one, factor);
            yield return null;
        }
    }

    IEnumerator CRDisable()
	{
		float t = 0;
		float timeCount = 0.2f;
		while (t < timeCount)
		{
			t += Time.deltaTime;
			float factor = t / timeCount;
			transform.localScale=Vector3.Lerp(Vector3.one, Vector3.one * 0.3f, factor);
			yield return null;
		}
		disablePanel();

    }


	public Animator animator;

	public GameObject father;

	public UnityEvent onEnable;

	public UnityEvent onDisable;
}
