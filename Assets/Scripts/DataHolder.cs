using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Event;
using LitJson;
using Shop;
using UnityEngine;

public class DataHolder : NetworkAble
{
	public static DataHolder Instance
	{
		get
		{
			if (DataHolder.instance == null)
			{
				DataHolder.instance = UnityEngine.Object.FindObjectOfType<DataHolder>();
			}
			return DataHolder.instance;
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.initPref();
	}

	private void Start()
	{
		UnityEngine.Debug.Log("Start");
		this.init();
		if (!this.initedFireBase)
		{
			this.initFireBase();
		}
	}

	private void OnEnable()
	{
		UnityEngine.Debug.Log("OnEnable");
	}

	public void initFireBase()
	{
	}

	public void init()
	{
		ItemFactory.init(this.mainItemsDefine);

	}

	public void initPref()
	{
		UnityEngine.Debug.Log("===============initPref");
		this.playerData.loadFromPref();
		this.inventory.loadFromPref();
		this.skillData.loadFromPref();
		this.videoGiftData.loadFromPref();
		this.missionData.loadFromPref();
		this.tutorialData.loadFromPref();
		this.achievementData.loadFromPref();
		this.shopDefine.loadFromPref();
		this.dailyGiftDefine.loadFromPref();
		this.vipGiftDefine.loadFromPref();
		this.grownGiftDefine.loadFromPref();
		this.playerDefine.loadFromPref();
		this.enemiesDefine.loadFromPref();
		for (int i = 0; i < this.levelDatas.Count; i++)
		{
			this.levelDatas[i].loadFromPref();
			this.addLoadedFireBase(" pref");
		}
		DatePassHelper.processIfNewDay("VIDEOGIFT", DatePassHelper.DateFormat.ddMMyyyy, delegate
		{
			this.videoGiftData.initFirstTime();
			this.videoGiftData.save();
		}, null, true);
		DatePassHelper.processIfNewDay("MISSIONDAILY", DatePassHelper.DateFormat.ddMMyyyy, delegate
		{
			this.missionData.initFirstTime();
			this.missionData.save();
		}, null, true);
		DatePassHelper.processIfNewDay("STACKGIFT", DatePassHelper.DateFormat.ddMMyyyy, delegate
		{
			this.playerData.refreshIapStackGift();
		}, null, true);
		DatePassHelper.processIfNewDay("DAILYGIFT", DatePassHelper.DateFormat.ddMMyyyy, delegate
		{
			this.playerData.unlockNextGiftDay();
		}, null, true);
		this.playerData.reCalStat();
		DataHolder.loadedPref = true;
	}

	public void newInitFromJson(JsonData jsonData)
	{
		UnityEngine.Debug.Log("newInitFromJson");
		JsonUtility.FromJsonOverwrite(jsonData["shop"].ToJson(), this.shopDefine);
		this.shopDefine.save();
		JsonUtility.FromJsonOverwrite(jsonData["daily-gift"].ToJson(), this.dailyGiftDefine);
		this.dailyGiftDefine.save();
		JsonUtility.FromJsonOverwrite(jsonData["vip-gift"].ToJson(), this.vipGiftDefine);
		this.vipGiftDefine.save();
		JsonUtility.FromJsonOverwrite(jsonData["grown-gift"].ToJson(), this.grownGiftDefine);
		this.grownGiftDefine.save();
		JsonUtility.FromJsonOverwrite(jsonData["player-define"].ToJson(), this.playerDefine);
		this.playerDefine.save();
		JsonUtility.FromJsonOverwrite(jsonData["enemies-define"].ToJson(), this.enemiesDefine);
		this.enemiesDefine.save();
		string json = jsonData["level-define"].ToJson();
		for (int i = 0; i < this.levelDatas.Count; i++)
		{
			JsonUtility.FromJsonOverwrite(JsonMapper.ToObject(json)[this.levelDatas[i].code].ToJson(), this.levelDatas[i]);
			this.levelDatas[i].save();
		}
		UnityEngine.Debug.Log("end");
	}




	public void timeOutFireBase()
	{
		UnityEngine.Debug.Log("No network");
		this.shopDefine.loadFromPref();
		this.dailyGiftDefine.loadFromPref();
		this.vipGiftDefine.loadFromPref();
		this.grownGiftDefine.loadFromPref();
		this.playerDefine.loadFromPref();
		this.enemiesDefine.loadFromPref();
		for (int i = 0; i < this.levelDatas.Count; i++)
		{
			this.levelDatas[i].loadFromPref();
			this.addLoadedFireBase(" pref");
		}
	}

	public void addLoadedFireBase(string status = "")
	{
		DataHolder.fireBaseLoaded++;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.playerData.addExp(10000);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			PlayerPrefs.DeleteAll();
		}
	}

	public void pushScore(int time, string level_id)
	{
		//UnityEngine.Debug.Log(string.Concat(new object[]
		//{
		//	"pushScore",
		//	time,
		//	"  ",
		//	level_id
		//}));
		//Dictionary<string, string> datas = new Dictionary<string, string>
		//{
		//	{
		//		"name",
		//		this.playerData.name
		//	},
		//	{
		//		"level_id",
		//		level_id
		//	},
		//	{
		//		"time",
		//		time + string.Empty
		//	}
		//};
		//string url = "http://";
		//base.StartCoroutine(base.callAPI(url, datas, delegate(string result)
		//{
		//	UnityEngine.Debug.Log(result);
		//}, delegate(string res)
		//{
		//	UnityEngine.Debug.Log("fail");
		//}));
	}

	private static DataHolder instance;

	public TestData testData;

	public Inventory inventory;

	public PlayerData playerData;

	public NItemDefine nItemDefine;

	public MainItemsDefine mainItemsDefine;

	public MissionDefine missionDefine;

	public MissionData missionData;

	public AchievementDefine achievementDefine;

	public AchievementData achievementData;

	public SkillData skillData;

	public SkillDefine skillDefine;

	public VideoGiftData videoGiftData;

	public PlayerDefine playerDefine;

	public static bool inited;

	public static int selectedMap;

	public static int selectedlevel;

	public static int difficult;

	public static bool watchedVideoAddStat;

	public ShopDefine shopDefine;

	public ItemDefine shopItemDefine;

	public DailyGiftDefine dailyGiftDefine;

	public VipGiftDefine vipGiftDefine;

	public GrownGiftDefine grownGiftDefine;

	public EnemiesDefine enemiesDefine;

	public TutorialData tutorialData;

	public static int totalFireBaseLoad = 6;

	public static int fireBaseLoaded;

	public LoadingHelper loadingHelper;

	public List<LevelData> levelDatas;

	public static bool loadedPref;

	public bool initedFireBase;

	public bool willShow3Ruby;

	public static bool showUpgradeItem;

	public static bool showUpgradeSkill;
}
