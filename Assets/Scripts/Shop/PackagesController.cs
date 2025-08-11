using System;
using System.Collections;
using UnityEngine;

namespace Shop
{
	public class PackagesController : MonoBehaviour
	{
		private void Start()
		{
			if (this.curID == this.posx.Length - 1)
			{
				this.nextBtn.SetActive(false);
			}
			else
			{
				this.nextBtn.SetActive(true);
			}
			if (this.curID == 0)
			{
				this.backBtn.SetActive(false);
			}
			else
			{
				this.backBtn.SetActive(true);
			}
		}

		public void next()
		{
			SoundManager.Instance.playAudio("ButtonClick");
			if (this.curID < this.posx.Length - 1)
			{
				this.curID++;
				base.StartCoroutine(this.movecor());
			}
		}

		public void back()
		{
			SoundManager.Instance.playAudio("ButtonClick");
			if (this.curID > 0)
			{
				this.curID--;
				base.StartCoroutine(this.movecor());
			}
		}

		private IEnumerator movecor()
		{
			while (this.holder.localPosition.x != this.posx[this.curID])
			{
				this.holder.localPosition = Vector3.MoveTowards(this.holder.localPosition, new Vector3(this.posx[this.curID], this.holder.localPosition.y, 0f), 50f);
				yield return new WaitForSeconds(Time.deltaTime);
			}
			if (this.curID == this.posx.Length - 1)
			{
				this.nextBtn.SetActive(false);
			}
			else
			{
				this.nextBtn.SetActive(true);
			}
			if (this.curID == 0)
			{
				this.backBtn.SetActive(false);
			}
			else
			{
				this.backBtn.SetActive(true);
			}
			yield break;
		}

		public float[] posx = new float[]
		{
			1336f,
			882f,
			428f,
			-26f,
			-480f,
			-934f
		};

		public int curID;

		public RectTransform holder;

		public GameObject nextBtn;

		public GameObject backBtn;
	}
}
