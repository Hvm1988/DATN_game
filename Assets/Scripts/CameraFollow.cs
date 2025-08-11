using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	private void Awake()
	{
		CameraFollow.ins = this;
		this.isMove = true;
	}

	private void Update()
	{
		if (this.isMove)
		{
			this.posBefore = base.transform.position;
			this.desiredPosition = this.target.position + this.offset;
			this.smoothedPosition = Vector3.Lerp(base.transform.position, this.desiredPosition, this.smoothSpeed);
			if (base.transform.position.x <= this.zoneRight && base.transform.position.x >= this.zoneLeft)
			{
				base.transform.position = this.smoothedPosition;
			}
			if (base.transform.position.x > this.zoneRight)
			{
				base.transform.position = new Vector3(this.zoneRight, this.smoothedPosition.y, -10f);
			}
			if (base.transform.position.x < this.zoneLeft)
			{
				base.transform.position = new Vector3(this.zoneLeft, this.smoothedPosition.y, -10f);
			}
			this.camMovementAmount = new Vector3(this.posBefore.x - base.transform.position.x, 0f, 0f);
			this.layerBG2.transform.Translate(-this.camMovementAmount.x * 0.5f, 0f, 0f);
		}
	}

	public Transform target;

	private float smoothSpeed = 0.08f;

	private Vector3 offset = new Vector3(0f, 2f, -10f);

	public static CameraFollow ins;

	private Vector3 desiredPosition;

	private Vector3 smoothedPosition;

	public float zoneLeft;

	public float zoneRight = 20f;

	public GameObject layerBG2;

	private Vector3 posBefore;

	private Vector3 camMovementAmount;

	public bool isMove;
}
