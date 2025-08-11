using System;
using UnityEngine;

namespace Shop
{
	public class ItemInShopSlot : MonoBehaviour
	{
		private void Start()
		{
			this.init();
		}

		public void init()
		{
			this.items = DataHolder.Instance.shopDefine.items;
			for (int i = 0; i < this.items.Length; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabs);
				gameObject.transform.SetParent(this.holder, false);
				gameObject.transform.localScale = Vector2.one;
				try
				{
					gameObject.GetComponent<SlotItemInShop>().init(this.items[i], DataHolder.Instance.shopItemDefine);
				}
				catch (Exception ex)
				{
				}
			}
		}

		public GameObject prefabs;

		public Transform holder;

		public ShopDefine.SingleItem[] items;
	}
}
