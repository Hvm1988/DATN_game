using System;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPopup : PopupsBase
{
	private void Awake()
	{
		NotificationPopup.instance = this;
	}

	public override void OnEnable()
	{
	}

	public override void onClose()
	{
		base.onClose();
	}

	public void onShow(string content, string value, Sprite img)
	{
		this.txtContent.text = content;
		this.txtValue.text = value;
		this.img.gameObject.SetActive(true);
		this.img.sprite = img;
		this.img.SetNativeSize();
		this.parrent.SetActive(true);
		this.btnOk.onClick.RemoveAllListeners();
		this.btnOk.onClick.AddListener(delegate()
		{
			this.onClose();
		});
	}

	public void onShow(string content, string value, Sprite img, Action action)
	{
		this.txtContent.text = content;
		this.txtValue.text = value;
		this.img.gameObject.SetActive(true);
		this.img.sprite = img;
		this.img.SetNativeSize();
		this.parrent.SetActive(true);
		this.btnOk.onClick.RemoveAllListeners();
		this.btnOk.onClick.AddListener(delegate()
		{
			action();
		});
		this.btnOk.onClick.AddListener(delegate()
		{
			this.onClose();
		});
	}

	public void onShow(string content)
	{
		this.txtContent.text = content;
		this.txtValue.text = string.Empty;
		this.img.gameObject.SetActive(false);
		this.parrent.SetActive(true);
		this.btnOk.onClick.RemoveAllListeners();
		this.btnOk.onClick.AddListener(delegate()
		{
			this.onClose();
		});
	}

	public void onShow(string content, Action action)
	{
		this.txtContent.text = content;
		this.txtValue.text = string.Empty;
		this.img.gameObject.SetActive(false);
		this.parrent.SetActive(true);
		this.btnOk.onClick.RemoveAllListeners();
		this.btnOk.onClick.AddListener(delegate()
		{
			this.onClose();
		});
		this.btnOk.onClick.AddListener(delegate()
		{
			action();
		});
	}

	public static NotificationPopup instance;

	public Text txtContent;

	public Text txtValue;

	public Image img;

	public Button btnOk;
}
