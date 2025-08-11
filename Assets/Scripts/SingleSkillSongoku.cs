using System;
using UnityEngine;

public class SingleSkillSongoku : MonoBehaviour
{
	private void Awake()
	{
		this.parrent = base.transform.parent.gameObject;
		this.posBefore = base.transform.localPosition;
	}

	private void OnEnable()
	{
		base.transform.parent = null;
		if (this.hero.transform.localEulerAngles.y == 180f)
		{
			base.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		}
		else
		{
			base.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		}
		base.Invoke("disableObj", 2f);
	}

	private void Update()
	{
		base.transform.Translate(Vector3.right * 20f * Time.deltaTime);
	}

	private void disableObj()
	{
		base.gameObject.SetActive(false);
		base.transform.SetParent(this.parrent.transform);
		base.transform.localPosition = this.posBefore;
	}

	private int arrow;

	public GameObject hero;

	private GameObject parrent;

	private Vector3 posBefore;
}
