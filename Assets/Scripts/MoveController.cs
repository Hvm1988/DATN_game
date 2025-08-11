using System;
using UnityEngine;

public class MoveController : MonoBehaviour
{
	private void Start()
	{
		if (this.isObjPardding)
		{
			base.gameObject.transform.position = new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f)).x + this.marginLeft, Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f)).y + this.marginBottom);
			MoveController.posBefore = this.circleSprite.transform.position;
		}
	}

	private void OnMouseEnter()
	{
		if (this.isButtonLeft)
		{
			this._gameManager.moveLeft();
			MoveController.isBtnLeftClick = true;
		}
		else
		{
			this._gameManager.moveRight();
			MoveController.isBtnLeftClick = false;
		}
	}

	private void OnMouseOver()
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.circleSprite.transform.position = new Vector3(vector.x, MoveController.posBefore.y, 0f);
		if (this.circleSprite.transform.localPosition.x > MoveController.btnCircleDistance)
		{
			this.circleSprite.transform.localPosition = new Vector3(MoveController.btnCircleDistance, 0f, 0f);
		}
		else if (this.circleSprite.transform.localPosition.x < -MoveController.btnCircleDistance)
		{
			this.circleSprite.transform.localPosition = new Vector3(-MoveController.btnCircleDistance, 0f, 0f);
		}
		else
		{
			this.circleSprite.transform.localPosition = new Vector3(this.circleSprite.transform.localPosition.x, 0f, 0f);
		}
	}

	private void OnMouseUp()
	{
		this._gameManager.idle();
		this.circleSprite.transform.localPosition = Vector3.zero;
	}

	public void onUp()
	{
		this.circleSprite.transform.localPosition = Vector3.zero;
	}

	public float marginBottom;

	public float marginLeft;

	private static float distanceMax = 1f;

	public bool isObjPardding;

	public GameManager _gameManager;

	public bool isButtonLeft;

	public GameObject circleSprite;

	private static Vector3 posBefore;

	public static bool isBtnLeftClick;

	private static float btnCircleDistance = 0.5f;
}
