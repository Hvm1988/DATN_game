using System;

public interface IPurchase { void Buy(string productId, Action ok, Action<string> fail); }
public interface IAds { void ShowReward(Action ok, Action fail); void ShowInterstitial(); }

public class MockPurchase : IPurchase
{
    public void Buy(string productId, Action ok, Action<string> fail) { ok?.Invoke(); }
}
public class MockAds : IAds
{
    public void ShowReward(Action ok, Action fail) { ok?.Invoke(); }
    public void ShowInterstitial() { }
}

public static class Services
{
    public static IPurchase IAP;
    public static IAds Ads;

    [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Boot()
    {
        if (DemoMode.On)
        {
            IAP = new MockPurchase();
            Ads = new MockAds();
        }
        else
        {
            // TODO: gắn IAP/Ads thật nếu có
            IAP = new MockPurchase();
            Ads = new MockAds();
        }
    }
}
