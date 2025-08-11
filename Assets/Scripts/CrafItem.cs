using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class CrafItem : MonoBehaviour
{
	public static CrafItem Instance
	{
		get
		{
			if (CrafItem.instance == null)
			{
				CrafItem.instance = UnityEngine.Object.FindObjectOfType<CrafItem>();
			}
			return CrafItem.instance;
		}
	}

	public static event CrafItem.OnRefresh onRefresh;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
		}
	}

	private void shootLightingOrb()
	{
		for (int i = 0; i < 3; i++)
		{
			if (!this.slotRequries[i].isHide)
			{
				this.lighTinngEffects[i].gameObject.SetActive(true);
				base.StartCoroutine(this.moveCurveCor(this.targetShoot.transform.position, this.lighTinngEffects[i], i));
			}
		}
	}

	private IEnumerator moveCurveCor(Vector3 pos, Transform lighting, int i)
	{
		lighting.localPosition = Vector3.zero;
		AnimationCurve curveX = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, lighting.position.x),
			new Keyframe(0.4f, pos.x)
		});
		AnimationCurve curveY = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, lighting.position.y),
			new Keyframe(0.5f, pos.y)
		});
		curveY.AddKey(0.25f, lighting.position.y + 1f - (float)i);
		float startTime = Time.time;
		float speed = UnityEngine.Random.Range(0.8f, 0.9f);
		float distCovered = 0f;
		while (distCovered < 0.5f)
		{
			distCovered = (Time.time - startTime) * speed;
			lighting.position = new Vector3(curveX.Evaluate(distCovered), curveY.Evaluate(distCovered), lighting.position.z);
			yield return new WaitForFixedUpdate();
		}
		this.lighTinngEffects[i].gameObject.SetActive(false);
		yield break;
	}

	public void onClickToAScroll(ScrollItemInven _item)
	{
		if (_item == null)
		{
			this.scrollIcon.sprite = this.scrollLevels[0];
			this.itemPreview.enabled = false;
			this.productName.text = string.Empty;
			this.goldCost_txt.transform.parent.gameObject.SetActive(false);
			this.rubyCost_txt.transform.parent.gameObject.SetActive(false);
			for (int i = 0; i < this.slotRequries.Length; i++)
			{
				this.slotRequries[i].hide();
			}
			for (int j = 0; j < this.productOptions.Length; j++)
			{
				this.productOptions[j].enabled = false;
			}
			return;
		}
		this.goldCost_txt.transform.parent.gameObject.SetActive(true);
		this.rubyCost_txt.transform.parent.gameObject.SetActive(true);
		this.itemPreview.enabled = true;
		this.item = DataHolder.Instance.mainItemsDefine.getScrollByCode(_item.code);
		this.goldCost_txt.text = DataHolder.Instance.nItemDefine.goldCostToCraft[(int)this.item.color] + string.Empty;
		this.rubyCost_txt.text = DataHolder.Instance.nItemDefine.rubyCostToCraft[(int)this.item.color] + string.Empty;
		this.currentScroll = _item;
		this.scrollIcon.sprite = this.scrollLevels[(int)this.item.color];
		MainItem mainByCode = DataHolder.Instance.mainItemsDefine.getMainByCode(this.item.codeProduct);
		this.productName.text = mainByCode.name;
		this.itemPreview.sprite = mainByCode.icon;
		this.itemReward.sprite = mainByCode.icon;
		List<ScrollItem.ItemCostResource> costRes = this.item.getCostRes();
		for (int k = 0; k < this.slotRequries.Length; k++)
		{
			if (k < costRes.Count)
			{
				this.slotRequries[k].show(costRes[k].code, costRes[k].cost);
			}
			else
			{
				this.slotRequries[k].hide();
			}
		}
		for (int l = 0; l < this.productOptions.Length; l++)
		{
			if (l < mainByCode.optionItem.Count)
			{
				this.productOptions[l].enabled = true;
				this.productOptions[l].text = mainByCode.optionItem[l].getOpDesRichText(0);
			}
			else
			{
				this.productOptions[l].enabled = false;
			}
		}
	}

	public void craft(int type)
	{
		if (this.blockCraft)
		{
			return;
		}
		if (TutorialManager.isTutorialing)
		{
			return;
		}
		if (DataHolder.Instance.inventory.getFreeSlotMain() == 0)
		{
			UIController.Instance.outSlotItem.init(OutOfSlotItem.TypeOut.CRAFT, null);
			return;
		}
		try
		{
			if (type == 0)
			{
				DataHolder.Instance.playerData.addGold(-DataHolder.Instance.nItemDefine.goldCostToCraft[(int)this.item.color]);
				SoundManager.Instance.playAudio("PayGold");
			}
			else
			{
				DataHolder.Instance.playerData.addRuby(-DataHolder.Instance.nItemDefine.rubyCostToCraft[(int)this.item.color]);
				SoundManager.Instance.playAudio("PayRuby");
			}
			this.currentScroll.craft();
			DataHolder.Instance.missionData.addDone(null, "CRAFT", 1);
			DataHolder.Instance.inventory.clearResList();
			this.blockCraft = true;
			base.StartCoroutine(this.craftEffectCor());
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			SoundManager.Instance.playAudio("ButtonClick");
			if (ex.Message.Equals("NOT_ENOUGHT_RES"))
			{
				this.notEnoughtMaterial.SetActive(true);
			}
			if (ex.Message.Equals("NOT_ENOUGHT_GOLD"))
			{
				this.notEnoughtGold.SetActive(true);
			}
			if (ex.Message.Equals("NOT_ENOUGHT_RUBY"))
			{
				this.notEnoughtRuby.SetActive(true);
			}
		}
	}

	private IEnumerator craftEffectCor()
	{
		this.shootLightingOrb();
		yield return new WaitForSeconds(0.5f);
		SoundManager.Instance.playAudio("Unlock");
		base.GetComponent<Animator>().Play("Craft");
		yield break;
	}

	public void openGetItem()
	{
		this.getItem.SetActive(true);
	}

	public void refresh()
	{
		this.blockCraft = false;
		base.GetComponent<Animator>().Play("Idle");
		if (CrafItem.onRefresh != null)
		{
			CrafItem.onRefresh();
		}
	}

	private static CrafItem instance;

	public Image scrollIcon;

	public Sprite[] scrollLevels;

	public CrafItem.SlotRequire[] slotRequries;

	public Text productName;

	public Text[] productOptions;

	public ScrollItemInven currentScroll;

	public Image itemPreview;

	public Image itemReward;

	public Transform[] lighTinngEffects;

	public Transform targetShoot;

	public GameObject lightingOrb;

	public GameObject getItem;

	private bool blockCraft;

	public GameObject notEnoughtMaterial;

	public GameObject notEnoughtGold;

	public GameObject notEnoughtRuby;

	public GameObject outSlotMain;

	public Text goldCost_txt;

	public Text rubyCost_txt;

	private ScrollItem item;

	public delegate void OnRefresh();

	[Serializable]
	public class SlotRequire
	{
		public void hide()
		{
			this.icon.enabled = false;
			this.name.enabled = false;
			this.number.enabled = false;
			this.isHide = true;
		}

		public void show(string resCode, int numberReq)
		{
			this.icon.enabled = true;
			this.name.enabled = true;
			this.number.enabled = true;
			this.isHide = false;
			ResourceItem resByCode = DataHolder.Instance.mainItemsDefine.getResByCode(resCode);
			this.icon.sprite = resByCode.icon;
			this.name.text = resByCode.name;
			int allResByCode = DataHolder.Instance.inventory.getAllResByCode(resCode);
			if (allResByCode >= numberReq)
			{
				this.number.text = string.Concat(new object[]
				{
					"<color=#28FF00FF>",
					allResByCode,
					"</color>/",
					numberReq
				});
			}
			else
			{
				this.number.text = string.Concat(new object[]
				{
					"<color=red>",
					allResByCode,
					"</color>/",
					numberReq
				});
			}
		}

		public Image icon;

		public Text name;

		public Text number;

		public bool isHide;
	}
}
