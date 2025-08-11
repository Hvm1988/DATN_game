using System;
using UnityEngine;
using UnityEngine.UI;

public class NotifierSelecmap : MonoBehaviour
{
	public void init(string content)
	{
		this.content_txt.text = content;
	}

	public Text content_txt;
}
