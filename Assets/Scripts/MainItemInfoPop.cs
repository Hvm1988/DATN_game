using UnityEngine;
using UnityEngine.UI;

public class MainItemInfoPop : ItemInfoPop
{
    public Text[] opNumbers;
    public Text[] opDes;
    public Button equippBtn;
    public Text equippText;

    private ItemInven item;

    public override void init(ItemInven item)
    {
        this.item = item;
        setUI(null);
    }

    private void OnEnable() { InventoryManager.onRefresh += setUI; }
    private void OnDisable() { InventoryManager.onRefresh -= setUI; }

    public override void setUI(NItem _ = null)
    {
        if (item == null) return;

        var def = DataHolder.Instance.mainItemsDefine.getMainByCode(item.code);
        base.setUI(def);

        int lvl = (item is MainItemInven mii) ? mii.level : 0;
        for (int i = 0; i < opDes.Length; i++)
        {
            if (def != null && i < def.optionItem.Count)
            {
                opDes[i].enabled = true;
                opDes[i].text = def.optionItem[i].getOpDesRichText(lvl);
            }
            else opDes[i].enabled = false;
        }

        var type = def.getItemType();
        bool equipped = DataHolder.Instance.playerData.isEquipped(type, item.key); // dùng KEY
        equippText.text = equipped ? "UnEquip" : "Equip";
        if (equippBtn) equippBtn.interactable = true;
    }

    public void equipOnclick()
    {
        if (item == null) return;
        var def = DataHolder.Instance.mainItemsDefine.getMainByCode(item.code);
        var type = def.getItemType();

        bool equipped = DataHolder.Instance.playerData.isEquipped(type, item.key); // dùng KEY
        if (equipped) DataHolder.Instance.playerData.UnEquippItem(type);
        else DataHolder.Instance.playerData.equippItem(type, item.key);

        InventoryManager.Instance.refresh();
        UpgradeNotifier.Instance.refresh();
        setUI(null);
    }

    public override void sell()
    {
        gameObject.SetActive(false);
        InventoryManager.Instance.showSellPop(this.item);
    }
}
