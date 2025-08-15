using UnityEngine;

public static class DemoSeed
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Seed()
    {
        if (!DemoMode.On) return;
        if (PlayerPrefs.GetInt("demo_seeded", 0) == 1) return;

        var pd = DataHolder.Instance.playerData;
        pd.level = 10;
        pd.gold = 99999;
        pd.ruby = 9999;
        pd.energy = 100;
        pd.reCalStat();

        PlayerPrefs.SetInt("demo_seeded", 1);
        PlayerPrefs.Save();
    }
}
