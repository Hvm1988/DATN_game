using System;
using UnityEngine;

[CreateAssetMenu(fileName = "VideoGiftData", menuName = "Game Data/Video Gift")]
public class VideoGiftData : DataModel
{
	public override void initFirstTime()
	{
		this.status = new int[]
		{
			0,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		};
	}

	public override void loadFromFireBase()
	{
		throw new NotImplementedException();
	}

	public bool canReward(int id)
	{
		return this.status[id] == 0;
	}

	public void reward(int id)
	{
		this.status[id] = 1;
		if (id < this.status.Length - 1)
		{
			this.status[id + 1] = 0;
		}
		if (VidGiftNotifier.Instance != null)
		{
			VidGiftNotifier.Instance.setUI();
		}
		this.save();
	}

	public void unlockNext(int id)
	{
		if (id < this.status.Length - 1)
		{
			this.status[id + 1] = 0;
		}
		if (VidGiftNotifier.Instance != null)
		{
			VidGiftNotifier.Instance.setUI();
		}
		this.save();
	}

	public bool haveAReward()
	{
		int[] array = this.status;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == 0)
			{
				return true;
			}
		}
		return false;
	}

	public int[] status;
}
