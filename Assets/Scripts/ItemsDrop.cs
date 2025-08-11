using System;
using System.Collections;
using UnityEngine;

public class ItemsDrop : MonoBehaviour
{
	public void onShow(string nameItem, float posX)
	{
		this.nameItem = nameItem;
		this.canEat = true;
		this.imgItem.sprite = DataHolder.Instance.mainItemsDefine.getScrollByCode(nameItem).productIcon;
		this.imgColor.sprite = DataHolder.Instance.mainItemsDefine.getScrollByCode(nameItem).icon;
		base.gameObject.SetActive(true);
		base.Invoke("disable", 5f);
		base.transform.position = new Vector3(posX + UnityEngine.Random.Range(-2f, 2f), 0.1f, 0f);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (this.canEat && col.gameObject.tag.Equals("hero"))
		{
			this.canEat = false;
			this.impact.SetActive(true);
			this.eff.SetActive(false);
			this.imgColor.gameObject.SetActive(false);
			this.imgItem.gameObject.SetActive(false);
			base.StartCoroutine(this.disableObj());
			GameResult.ins.itemEat.Add(this.nameItem);
			if (GameConfig.soundVolume > 0f)
			{
				if (this._audio.volume != GameConfig.soundVolume)
				{
					this._audio.volume = GameConfig.soundVolume;
				}
				this._audio.Play();
			}
			base.CancelInvoke("disable");
		}
	}

	private IEnumerator disableObj()
	{
		yield return new WaitForSeconds(1f);
		this.impact.SetActive(false);
		this.eff.SetActive(true);
		this.canEat = false;
		this.imgColor.gameObject.SetActive(true);
		this.imgItem.gameObject.SetActive(true);
		base.gameObject.SetActive(false);
		yield break;
	}

	private void disable()
	{
		this.canEat = false;
		base.gameObject.SetActive(false);
	}

	public SpriteRenderer imgColor;

	public SpriteRenderer imgItem;

	private string nameItem;

	public bool canEat;

	public GameObject impact;

	public GameObject eff;

	public AudioSource _audio;
}
