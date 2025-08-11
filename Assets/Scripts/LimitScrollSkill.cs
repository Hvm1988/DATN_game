using System;
using UnityEngine;

public class LimitScrollSkill : MonoBehaviour
{
	private void Update()
	{
		this.rectTrans.localPosition = new Vector2(Mathf.Clamp(this.rectTrans.localPosition.x, -190f, 173f), 0f);
	}

	public RectTransform rectTrans;
}
