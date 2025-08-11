using System;
using UnityEngine;
using UnityEngine.UI;

public class EnterYourName : MonoBehaviour
{
	private void Start()
	{
		this.randomName = string.Concat(new object[]
		{
			"Guest",
			UnityEngine.Random.Range(0, 9),
			UnityEngine.Random.Range(0, 9),
			UnityEngine.Random.Range(0, 9),
			UnityEngine.Random.Range(0, 9),
			UnityEngine.Random.Range(0, 9)
		});
		this.inputField.text = this.randomName;
	}

	public void onOK()
	{
		if (this.inputField.text.Equals(string.Empty))
		{
			DataHolder.Instance.playerData.setName(this.randomName);
		}
		else
		{
			DataHolder.Instance.playerData.setName(this.inputField.text);
		}
	}

	public Text placeHolderTxt;

	public InputField inputField;

	private string randomName;
}
