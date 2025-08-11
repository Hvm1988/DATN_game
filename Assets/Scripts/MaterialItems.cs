using System;
using UnityEngine;

public class MaterialItems : MonoBehaviour
{
	private void Awake()
	{
		this._audio = base.GetComponent<AudioSource>();
		this.box = base.GetComponent<CircleCollider2D>();
	}

	public void OnShow(int code, int num, float posX)
	{
		this.code = code;
		this.res = DataHolder.Instance.mainItemsDefine.getResByCode(GetItemById.getInstance()[code]);
		this.rd = num;
		base.transform.position = new Vector3(posX + UnityEngine.Random.Range(-2f, 2f), -0.3f, 0f);
		this.spr.sprite = this.res.icon;
		this.canEat = true;
		this.effect.SetActive(true);
		this.effectEat.SetActive(false);
		this.box.enabled = true;
		base.Invoke("disable", 5f);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("hero") && this.canEat)
		{
			this.canEat = false;
			this.box.enabled = false;
			this.effect.SetActive(false);
			this.superEffect.SetActive(false);
			this.effectEat.SetActive(true);
			this.spr.sprite = null;
			this.setNumber.set_numGreen(this.rd);
			this.setNumber.gameObject.SetActive(true);
			GameSave.itemsEat[this.code - 1] += this.rd;
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

	private void disable()
	{
		if (this.canEat)
		{
			this.canEat = false;
			this.effect.SetActive(false);
			this.superEffect.SetActive(false);
			this.spr.sprite = null;
		}
	}

	[HideInInspector]
	public int code;

	public SpriteRenderer spr;

	[HideInInspector]
	public bool canEat;

	public GameObject effect;

	public GameObject effectEat;

	public GameObject superEffect;

	public Set_number_show setNumber;

	private int rd;

	private AudioSource _audio;

	private ResourceItem res;

	private CircleCollider2D box;
}
