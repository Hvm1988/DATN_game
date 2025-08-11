using System;

public class VidGiftNotifier : Notifier
{
	private void Awake()
	{
		VidGiftNotifier.Instance = this;
	}

	public override void setUI()
	{
		this.counter = 0;
		if (DataHolder.Instance.videoGiftData.haveAReward())
		{
			this.counter = 1;
		}
		this.redNote.SetActive(this.counter > 0);
	}

	public static VidGiftNotifier Instance;
}
