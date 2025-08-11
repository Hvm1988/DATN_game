using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class Purchaser : MonoBehaviour, IStoreListener
{
	private void Start()
	{
		if (Purchaser.m_StoreController == null)
		{
			this.InitializePurchasing();
		}
	}

	public void InitializePurchasing()
	{
		if (this.IsInitialized())
		{
			return;
		}
		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), new IPurchasingModule[0]);
		configurationBuilder.AddProduct(Purchaser.kProductIDConsumable1, ProductType.Consumable);
		configurationBuilder.AddProduct(Purchaser.kProductIDConsumable2, ProductType.Consumable);
		configurationBuilder.AddProduct(Purchaser.kProductIDConsumable3, ProductType.Consumable);
		configurationBuilder.AddProduct(Purchaser.kProductIDConsumable4, ProductType.Consumable);
		configurationBuilder.AddProduct(Purchaser.kProductIDConsumable5, ProductType.Consumable);
		configurationBuilder.AddProduct(Purchaser.kProductIDConsumable6, ProductType.Consumable);
		configurationBuilder.AddProduct(Purchaser.kProductIDConsumable7, ProductType.Consumable);
		configurationBuilder.AddProduct(Purchaser.kProductIDConsumable8, ProductType.Consumable);
		configurationBuilder.AddProduct(Purchaser.kProductIDConsumable9, ProductType.Consumable);
		configurationBuilder.AddProduct(Purchaser.kProductIDConsumable10, ProductType.Consumable);
		configurationBuilder.AddProduct(Purchaser.kProductIDConsumable11, ProductType.Consumable);
		configurationBuilder.AddProduct(Purchaser.kProductIDConsumable12, ProductType.Consumable);
		UnityPurchasing.Initialize(this, configurationBuilder);
	}

	private bool IsInitialized()
	{
		return Purchaser.m_StoreController != null && Purchaser.m_StoreExtensionProvider != null;
	}

	public void BuyConsumable1()
	{
		this.BuyProductID(Purchaser.kProductIDConsumable1);
		UnityEngine.Debug.Log("BuyConsumable1");
	}

	public void BuyConsumable2()
	{
		this.BuyProductID(Purchaser.kProductIDConsumable2);
	}

	public void BuyConsumable3()
	{
		this.BuyProductID(Purchaser.kProductIDConsumable3);
	}

	public void BuyConsumable4()
	{
		this.BuyProductID(Purchaser.kProductIDConsumable4);
	}

	public void BuyConsumable5()
	{
		this.BuyProductID(Purchaser.kProductIDConsumable5);
	}

	public void BuyConsumable6()
	{
		this.BuyProductID(Purchaser.kProductIDConsumable6);
	}

	public void BuyConsumable7()
	{
		this.BuyProductID(Purchaser.kProductIDConsumable7);
	}

	public void BuyConsumable8()
	{
		this.BuyProductID(Purchaser.kProductIDConsumable8);
	}

	public void BuyConsumable9()
	{
		this.BuyProductID(Purchaser.kProductIDConsumable9);
	}

	public void BuyConsumable10()
	{
		this.BuyProductID(Purchaser.kProductIDConsumable10);
	}

	public void BuyConsumable11()
	{
		this.BuyProductID(Purchaser.kProductIDConsumable11);
	}

	public void BuyConsumable12()
	{
		this.BuyProductID(Purchaser.kProductIDConsumable12);
	}

	public void BuyProductID(string productId)
	{
		if (this.IsInitialized())
		{
			Product product = Purchaser.m_StoreController.products.WithID(productId);
			if (product != null && product.availableToPurchase)
			{
				UnityEngine.Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				Purchaser.m_StoreController.InitiatePurchase(product);
			}
			else
			{
				UnityEngine.Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		else
		{
			UnityEngine.Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}

	public void RestorePurchases()
	{
		if (!this.IsInitialized())
		{
			UnityEngine.Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
		{
			UnityEngine.Debug.Log("RestorePurchases started ...");
			IAppleExtensions extension = Purchaser.m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			extension.RestoreTransactions(delegate (bool result)
			{
				UnityEngine.Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		else
		{
			UnityEngine.Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		Purchaser.m_StoreController = controller;
		Purchaser.m_StoreExtensionProvider = extensions;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		if (string.Equals(args.purchasedProduct.definition.id, Purchaser.kProductIDConsumable1, StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (this.onSuccessPurchaser != null)
			{
				this.onSuccessPurchaser();
			}
		}
		else if (string.Equals(args.purchasedProduct.definition.id, Purchaser.kProductIDConsumable2, StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (this.onSuccessPurchaser != null)
			{
				this.onSuccessPurchaser();
			}
		}
		else if (string.Equals(args.purchasedProduct.definition.id, Purchaser.kProductIDConsumable3, StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (this.onSuccessPurchaser != null)
			{
				this.onSuccessPurchaser();
			}
		}
		else if (string.Equals(args.purchasedProduct.definition.id, Purchaser.kProductIDConsumable4, StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (this.onSuccessPurchaser != null)
			{
				this.onSuccessPurchaser();
			}
		}
		else if (string.Equals(args.purchasedProduct.definition.id, Purchaser.kProductIDConsumable5, StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (this.onSuccessPurchaser != null)
			{
				this.onSuccessPurchaser();
			}
		}
		else if (string.Equals(args.purchasedProduct.definition.id, Purchaser.kProductIDConsumable6, StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (this.onSuccessPurchaser != null)
			{
				this.onSuccessPurchaser();
			}
		}
		else if (string.Equals(args.purchasedProduct.definition.id, Purchaser.kProductIDConsumable7, StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (this.onSuccessPurchaser != null)
			{
				this.onSuccessPurchaser();
			}
		}
		else if (string.Equals(args.purchasedProduct.definition.id, Purchaser.kProductIDConsumable8, StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (this.onSuccessPurchaser != null)
			{
				this.onSuccessPurchaser();
			}
		}
		else if (string.Equals(args.purchasedProduct.definition.id, Purchaser.kProductIDConsumable9, StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (this.onSuccessPurchaser != null)
			{
				this.onSuccessPurchaser();
			}
		}
		else if (string.Equals(args.purchasedProduct.definition.id, Purchaser.kProductIDConsumable10, StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (this.onSuccessPurchaser != null)
			{
				this.onSuccessPurchaser();
			}
		}
		else if (string.Equals(args.purchasedProduct.definition.id, Purchaser.kProductIDConsumable11, StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (this.onSuccessPurchaser != null)
			{
				this.onSuccessPurchaser();
			}
		}
		else if (string.Equals(args.purchasedProduct.definition.id, Purchaser.kProductIDConsumable12, StringComparison.Ordinal))
		{
			UnityEngine.Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			if (this.onSuccessPurchaser != null)
			{
				this.onSuccessPurchaser();
			}
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
	}

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        
    }

    private static IStoreController m_StoreController;

	private static IExtensionProvider m_StoreExtensionProvider;

	public static string kProductIDConsumable1 = "dragonfight2_0.99";

	public static string kProductIDConsumable2 = "dragonfight2_1.99";

	public static string kProductIDConsumable3 = "dragonfight2_2.99";

	public static string kProductIDConsumable4 = "dragonfight2_3.99";

	public static string kProductIDConsumable5 = "dragonfight2_5.99";

	public static string kProductIDConsumable6 = "dragonfight2_11.99";

	public static string kProductIDConsumable7 = "dragonfight2_12.99";

	public static string kProductIDConsumable8 = "dragonfight2_25.99";

	public static string kProductIDConsumable9 = "dragonfight2_51.99";

	public static string kProductIDConsumable10 = "dragonfight2_4.99";

	public static string kProductIDConsumable11 = "dragonfight2_9.99";

	public static string kProductIDConsumable12 = "dragonfight2_19.99";

	public Action onSuccessPurchaser;
}
