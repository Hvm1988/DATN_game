using System;
using UnityEngine;
using UnityEngine.UI;

public class VideoGift : MonoBehaviour
{
	private void OnEnable()
	{
		this.setUI();
	}

	private void setUI()
	{
		for (int i = 0; i < DataHolder.Instance.videoGiftData.status.Length; i++)
		{
			if (DataHolder.Instance.videoGiftData.status[i] != 1)
			{
				this.rewardBtns[i].SetActive(true);
				this.rewardBtns[i].GetComponent<Button>().interactable = DataHolder.Instance.videoGiftData.canReward(i);
				this.rewarded[i].SetActive(false);
			}
			else
			{
				this.rewardBtns[i].SetActive(false);
				this.rewarded[i].SetActive(true);
			}
		}
	}

	public void reward(int id)
	{
		AdsManager.AdNumbers = 1;
		SoundManager.Instance.playAudio("ButtonClick");
		this.curId = id;
		AdsController.Instance.showRewardVideo(delegate
		{
			try
			{
				UnityEngine.Debug.Log("=============reward");
				this.gifts[this.curId].reward();
				DataHolder.Instance.videoGiftData.reward(this.curId);
				this.setUI();
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log(ex.Message);
			}
		}, delegate
		{
			this.vidNotReady.SetActive(true);
		});
	}

	//public UnityAds unityAds;

	public GameObject[] rewardBtns;

	public GameObject[] rewarded;

	public Text[] btnText;

	public GiftSlot.Gift[] gifts;

	public GameObject vidNotReady;

	private int curId;
}
