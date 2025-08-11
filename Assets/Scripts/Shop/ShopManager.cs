using System;
using System.Diagnostics;
using UnityEngine;

namespace Shop
{
	public class ShopManager : MonoBehaviour
	{
		public static event ShopManager.OnBuySuccessIAP onBuySuccessIAP;

		private void Awake()
		{
		}

		public Purchaser purchaser;

		public static ShopManager Instance;

		public delegate void OnBuySuccessIAP();
	}
}
