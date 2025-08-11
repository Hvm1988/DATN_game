using System;

public class GiftStack : GiftSlot
{
	public new void onClick()
	{
		base.onClick();
		DataHolder.Instance.playerData.setRewardIAPStackGift(this.id, 1);
		this.iapSG.setUI();
	}

	public IAPStackGift iapSG;

	public int id;
}
