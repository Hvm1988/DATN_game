using System;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDefine", menuName = "Game Data/Player Define")]
public class PlayerDefine : DataModel
{
    public bool expIsCumulative = true; // true: cột EXP là TỔNG tích lũy tới cấp đó. false: EXP để lên từ L -> L+1

    // Cột trong mỗi dòng playerStats: "ATK,HP,DEF,EXP"
    public const int COL_ATK = 0;
    public const int COL_HP = 1;
    public const int COL_DEF = 2;
    public const int COL_EXP = 3;

    public int MaxLevel => playerStats != null ? playerStats.Length : 0;

    public override void initFirstTime() { }
    public override void loadFromFireBase() { }

    // Base stat tăng theo cấp (hàm mũ, an toàn với cấp âm)
    public int getATK(int curlevel)
    {
        double f = increDamagePerLevel / 100.0 + 1.0;
        return (int)(baseDamage * Math.Pow(f, Math.Max(0, curlevel)));
    }

    public int getHP(int curlevel)
    {
        double f = increHpPerLevel / 100.0 + 1.0;
        return (int)(baseHp * Math.Pow(f, Math.Max(0, curlevel)));
    }

    public int getDEF(int curlevel)
    {
        double f = increDefPerLevel / 100.0 + 1.0;
        return (int)(baseDef * Math.Pow(f, Math.Max(0, curlevel)));
    }

    // EXP theo bảng
    public int getEXP(int curlevel) => getValueStat(curlevel, COL_EXP);

    // Lấy giá trị từ bảng playerStats theo (level,type) an toàn, không out-of-range
    public int getValueStat(int level, int type)
    {
        if (playerStats == null || playerStats.Length == 0) return 0;

        int lvlIdx = Mathf.Clamp(level - 1, 0, playerStats.Length - 1); // level 1..N -> index 0..N-1
        string row = playerStats[lvlIdx] ?? string.Empty;

        string[] parts = row.Split(new[] { ',' }, StringSplitOptions.None);
        if (parts == null || parts.Length == 0) return 0;

        int colIdx = Mathf.Clamp(type, 0, parts.Length - 1);

        if (int.TryParse(parts[colIdx].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int val))
            return val;

        return 0;
    }
    public int GetExpToNext_1Based(int level)
    {
        int max = MaxLevel;
        if (max <= 0 || level >= max) return 0;              // đã max

        if (expIsCumulative)
        {
            int curTotal = getEXP(Mathf.Clamp(level, 1, max));   // tổng tới L
            int nextTotal = getEXP(Mathf.Clamp(level + 1, 1, max));   // tổng tới L+1
            return Mathf.Max(1, nextTotal - curTotal);
        }
        else
        {
            // Bảng lưu EXP cần cho từng cấp: hàng L là cost để lên L+1
            return Mathf.Max(1, getEXP(Mathf.Clamp(level, 1, max)));
        }
    }
    // Dữ liệu
    [Tooltip("Mỗi phần tử là một cấp. Chuỗi dạng: ATK,HP,DEF,EXP")]
    public string[] playerStats;

    public int baseDef;
    public int baseDamage;
    public int baseHp;

    public double increDefPerLevel;
    public double increDamagePerLevel;
    public double increHpPerLevel;

    public float[] iapGiftPoint;
}
