using System;
using UnityEngine;

public class SpecialSoundManager : MonoBehaviour
{
	public static SpecialSoundManager Instance
	{
		get
		{
			if (SpecialSoundManager.instance == null)
			{
				SpecialSoundManager.instance = UnityEngine.Object.FindObjectOfType<SpecialSoundManager>();
			}
			return SpecialSoundManager.instance;
		}
	}

	public void playAudio(string name)
	{
		float @float = PlayerPrefs.GetFloat("SOUND_VOLUME");
		if (name != null)
		{
			if (!(name == "PayGold"))
			{
				if (!(name == "PayRuby"))
				{
					if (!(name == "PopUpOpen"))
					{
						if (!(name == "ButtonClick"))
						{
							if (name == "PayPurchaser")
							{
								this.audioSource.PlayOneShot(this.buttonClick, @float);
							}
						}
						else
						{
							this.audioSource.PlayOneShot(this.buttonClick, @float);
						}
					}
					else
					{
						this.audioSource.PlayOneShot(this.popUpOpen, @float);
					}
				}
				else
				{
					this.audioSource.PlayOneShot(this.payRuby, @float);
				}
			}
			else
			{
				this.audioSource.PlayOneShot(this.payGold, @float);
			}
		}
	}

	private static SpecialSoundManager instance;

	public AudioSource audioSource;

	public AudioClip payGold;

	public AudioClip payRuby;

	public AudioClip popUpOpen;

	public AudioClip buttonClick;

	public AudioClip payPurchaser;
}
