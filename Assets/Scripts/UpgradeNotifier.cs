using System;

public class UpgradeNotifier : Notifier
{
	private void Awake()
	{
		UpgradeNotifier.Instance = this;
	}

	public override void setUI()
	{
		this.counter = 0;
		foreach (MainItemType type in this.types)
		{
			MainItemInven itemByType = DataHolder.Instance.playerData.getItemByType(type);
			if (itemByType != null)
			{
				if (itemByType.canUpgradeWithGold() || itemByType.canUpgradeWithRuby())
				{
					this.counter++;
				}
			}
		}
		this.redNote.SetActive(this.counter > 0);
	}

	public static UpgradeNotifier Instance;

	private MainItemType[] types = new MainItemType[]
	{
		MainItemType.ARMOR,
		MainItemType.GLOVER,
		MainItemType.SHOE,
		MainItemType.PANTS
	};
}
