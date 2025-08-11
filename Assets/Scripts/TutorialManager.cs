using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
	private IEnumerator Start()
	{
		if (GameSave.isNextGame)
		{
			yield break;
		}
		this.setType();
		switch (this.currentType)
		{
		case TutorialManager.Type.START:
			this.currentSteps = this.starterSteps;
			break;
		case TutorialManager.Type.UPGRADE:
			this.currentSteps = this.upgradeSteps;
			break;
		case TutorialManager.Type.SKILL:
			this.currentSteps = this.skillSteps;
			break;
		case TutorialManager.Type.ACESSORIES:
			this.currentSteps = this.accessoriSteps;
			break;
		case TutorialManager.Type.SHOP_EVENT:
			this.currentSteps = this.shopSteps;
			break;
		case TutorialManager.Type.MISSION_VIDGIFT:
			this.currentSteps = this.missionVidGiftSteps;
			break;
		}
		if (DataHolder.Instance.playerData.name.Equals(string.Empty))
		{
			yield return new WaitUntil(() => !UIController.Instance.enterName.activeSelf);
		}
		if (this.currentType != TutorialManager.Type.NONE)
		{
			this.curStepID = 0;
			this.doStep();
			TutorialManager.isTutorialing = true;
		}
		else
		{
			TutorialManager.isTutorialing = false;
		}
		yield return null;
		yield break;
	}

	private void setType()
	{
		if (DataHolder.Instance.tutorialData.tutorials[0].status == 0)
		{
			this.currentType = TutorialManager.Type.START;
			return;
		}
		if (DataHolder.Instance.tutorialData.tutorials[1].status == 0)
		{
			this.currentType = TutorialManager.Type.SHOP_EVENT;
			return;
		}
		if (DataHolder.Instance.tutorialData.tutorials[2].status == 0)
		{
			this.currentType = TutorialManager.Type.UPGRADE;
			return;
		}
		if (DataHolder.Instance.tutorialData.tutorials[3].status == 0 && DataHolder.Instance.playerData.level >= 2)
		{
			this.currentType = TutorialManager.Type.SKILL;
			return;
		}
		if (DataHolder.Instance.tutorialData.tutorials[4].status == 0)
		{
			this.currentType = TutorialManager.Type.ACESSORIES;
			return;
		}
		if (DataHolder.Instance.tutorialData.tutorials[5].status == 0)
		{
			this.currentType = TutorialManager.Type.MISSION_VIDGIFT;
			return;
		}
	}

	private void doStep()
	{
		if (!this.canvasTutorial.activeSelf)
		{
			this.canvasTutorial.SetActive(true);
		}
		TutorialManager.Step step = this.currentSteps[this.curStepID];
		this.messageTxt.text = step.getContent();
		this.messageTxt.GetComponent<RectTransform>().localScale = step.getScale();
		this.messageObj.GetComponent<RectTransform>().localPosition = step.getMessagePos();
		this.messageObj.GetComponent<RectTransform>().localScale = step.getScale();
		this.finger.GetComponent<RectTransform>().localPosition = step.getFingerPos();
		step.doHightLight(new UnityAction(this.nextStep));
	}

	private void nextStep()
	{
		TutorialManager.Step step = this.currentSteps[this.curStepID];
		step.disable();
		if (this.curStepID < this.currentSteps.Length - 1)
		{
			this.curStepID++;
			this.doStep();
		}
		else
		{
			this.canvasTutorial.SetActive(false);
			if (this.currentType == TutorialManager.Type.UPGRADE)
			{
				UIController.Instance.showStarterPackAfterTut();
			}
			TutorialManager.isTutorialing = false;
			DataHolder.Instance.tutorialData.setTutorial(this.currentType, 1);
			UIController.Instance.checkRate();
		}
	}

	public void skip()
	{
		DataHolder.Instance.tutorialData.setTutorial(this.currentType, 1);
		if (this.currentType == TutorialManager.Type.UPGRADE)
		{
			UIController.Instance.showStarterPackAfterTut();
		}
		TutorialManager.Step step = this.currentSteps[this.curStepID];
		step.disable();
		this.canvasTutorial.SetActive(false);
		TutorialManager.isTutorialing = false;
		UIController.Instance.checkRate();
	}

	public TutorialManager.Step[] starterSteps;

	public TutorialManager.Step[] upgradeSteps;

	public TutorialManager.Step[] skillSteps;

	public TutorialManager.Step[] shopSteps;

	public TutorialManager.Step[] accessoriSteps;

	public TutorialManager.Step[] missionVidGiftSteps;

	public TutorialManager.Step[] currentSteps;

	[SerializeField]
	private int curStepID;

	public GameObject canvasTutorial;

	public Text messageTxt;

	public GameObject messageObj;

	public GameObject finger;

	public TutorialManager.Type currentType;

	public static bool isTutorialing;

	public enum Type
	{
		NONE,
		START,
		UPGRADE,
		SKILL,
		ACESSORIES,
		SHOP_EVENT,
		MISSION_VIDGIFT,
		EVENT
	}

	[Serializable]
	public class Step
	{
		public void doHightLight(UnityAction onBtnClick)
		{
			this.canvas = this.higtLightObj.AddComponent<Canvas>();
			this.canvas.overrideSorting = true;
			this.canvas.sortingLayerName = "Tutorial";
			this.canvas.sortingOrder = 5;
			this.graphRay = this.higtLightObj.AddComponent<GraphicRaycaster>();
			this.tutPanel.SetActive(true);
			this.onBtnClick = onBtnClick;
			this.button.onClick.AddListener(onBtnClick);
			this.cachedButtonStatus = this.button.interactable;
			if (!this.cachedButtonStatus)
			{
				this.button.interactable = true;
			}
		}

		public void disable()
		{
			UnityEngine.Object.Destroy(this.graphRay);
			UnityEngine.Object.Destroy(this.canvas);
			this.tutPanel.SetActive(false);
			this.button.onClick.RemoveListener(this.onBtnClick);
			this.button.interactable = this.cachedButtonStatus;
		}

		public string getContent()
		{
			return this.content;
		}

		public Vector2 getMessagePos()
		{
			return this.messagePos;
		}

		public Vector2 getFingerPos()
		{
			return this.fingerPos;
		}

		public Vector2 getScale()
		{
			return this.scale;
		}

		[SerializeField]
		private string content;

		[SerializeField]
		private GameObject higtLightObj;

		[SerializeField]
		private Vector2 messagePos;

		[SerializeField]
		private Vector2 fingerPos;

		[SerializeField]
		private Vector2 skilPos;

		[SerializeField]
		private Vector2 scale;

		[SerializeField]
		private Button button;

		[SerializeField]
		private GameObject tutPanel;

		[SerializeField]
		private Canvas canvas;

		[SerializeField]
		private GraphicRaycaster graphRay;

		[SerializeField]
		private UnityAction onBtnClick;

		private bool cachedButtonStatus;
	}
}
