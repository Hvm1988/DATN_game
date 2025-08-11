using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public void OnShow(Vector3 pos)
	{
		this.cantGetCoin = true;
		this.rigid.constraints = RigidbodyConstraints2D.None;
		base.transform.position = pos;
		this.box.enabled = true;
		this._animator.enabled = true;
		this._animator.Play("coinAnimation", 0, 0f);
		this._sprite.enabled = true;
		this.eff.SetActive(false);
		this.getCoin = UnityEngine.Random.Range(GameSave.minGetCoin, GameSave.maxGetCoin);
		this.getCoin += this.getCoin * GameConfig.addGoldDropBase / 100;
		this.rigid.AddForce(new Vector2((float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(300, 500)));
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (this.cantGetCoin && other.gameObject.tag.Equals("hero"))
		{
			this.disableObj();
			this.cantGetCoin = false;
			this.eff.SetActive(true);
			this.txtCoin.set_numGold(this.getCoin);
			GameSave.getCoin += this.getCoin;
			this.txtCoin.gameObject.SetActive(true);
			if (GameConfig.soundVolume > 0f)
			{
				if (this._audio.volume != GameConfig.soundVolume)
				{
					this._audio.volume = GameConfig.soundVolume;
				}
				this._audio.Play();
			}
		}
	}

	public void disableObj()
	{
		this.rigid.constraints = RigidbodyConstraints2D.FreezeAll;
		base.transform.eulerAngles = Vector3.zero;
		this._animator.enabled = false;
		this._sprite.enabled = false;
		this.box.enabled = false;
	}

	public Set_number_show txtCoin;

	public Rigidbody2D rigid;

	public SpriteRenderer _sprite;

	public Animator _animator;

	public AudioSource _audio;

	public CircleCollider2D box;

	private int rd;

	public int getCoin;

	public bool cantGetCoin;

	public GameObject eff;
}
