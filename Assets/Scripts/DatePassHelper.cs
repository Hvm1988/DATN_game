using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatePassHelper : MonoBehaviour
{
	private void Awake()
	{
		DatePassHelper.ins = this;
	}

	public static string getNowString(DatePassHelper.DateFormat format)
	{
		return DateTime.Now.ToString(DatePassHelper.DateFormatString[format]);
	}

	public static string getDateStringFromPref(string code)
	{
		return PlayerPrefs.GetString(code);
	}

	public static void saveNowToPref(string code, DatePassHelper.DateFormat format)
	{
		PlayerPrefs.SetString(code, DatePassHelper.getNowString(format));
	}

	public static void saveNowToPref(string code, DatePassHelper.DateFormat format, int adjustSec)
	{
		PlayerPrefs.SetString(code, DateTime.Now.AddSeconds(-(double)adjustSec).ToString(DatePassHelper.DateFormatString[format]));
	}

	public static bool comparePrefWithNow(string code, DatePassHelper.DateFormat format)
	{
		return DatePassHelper.getDateStringFromPref(code).Equals(DatePassHelper.getNowString(format));
	}

	public static void processIfNewDay(string code, DatePassHelper.DateFormat format, Action onTrue = null, Action onFalse = null, bool overWriteDate = true)
	{
		if (DatePassHelper.getDateStringFromPref(code).Equals(string.Empty))
		{
			if (onTrue != null)
			{
				onTrue();
			}
			if (overWriteDate)
			{
				DatePassHelper.saveNowToPref(code, format);
			}
			return;
		}
		if (DatePassHelper.getDateStringFromPref(code).Equals(DatePassHelper.getNowString(format)))
		{
			if (onFalse != null)
			{
				onFalse();
			}
			return;
		}
		if (onTrue != null)
		{
			onTrue();
		}
		if (overWriteDate)
		{
			DatePassHelper.saveNowToPref(code, format);
		}
	}

	public static DateTime getDateTimeFromPref(string code, DatePassHelper.DateFormat format)
	{
		try
		{
			string format2 = DatePassHelper.DateFormatString[format];
			return DateTime.ParseExact(DatePassHelper.getDateStringFromPref(code), format2, null);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError(ex.Message);
		}
		return DateTime.Now;
	}

	public static int getDayPassed(string code)
	{
		if (DatePassHelper.getDateStringFromPref(code).Equals(string.Empty))
		{
			DatePassHelper.saveNowToPref(code, DatePassHelper.DateFormat.ddMMyyyy);
			return 0;
		}
		DateTime dateTimeFromPref = DatePassHelper.getDateTimeFromPref(code, DatePassHelper.DateFormat.ddMMyyyy);
		DateTime now = DateTime.Now;
		return (int)(now - dateTimeFromPref).TotalDays;
	}

	public static int getHourPassed(string code)
	{
		if (DatePassHelper.getDateStringFromPref(code).Equals(string.Empty))
		{
			DatePassHelper.saveNowToPref(code, DatePassHelper.DateFormat.ddMMyyyyhhmmss);
			return 0;
		}
		DateTime dateTimeFromPref = DatePassHelper.getDateTimeFromPref(code, DatePassHelper.DateFormat.ddMMyyyyhhmmss);
		DateTime now = DateTime.Now;
		return (int)(now - dateTimeFromPref).TotalHours;
	}

	public static int getMinutePassed(string code)
	{
		if (DatePassHelper.getDateStringFromPref(code).Equals(string.Empty))
		{
			DatePassHelper.saveNowToPref(code, DatePassHelper.DateFormat.ddMMyyyyhhmmss);
			return 0;
		}
		DateTime dateTimeFromPref = DatePassHelper.getDateTimeFromPref(code, DatePassHelper.DateFormat.ddMMyyyyhhmmss);
		DateTime now = DateTime.Now;
		return (int)(now - dateTimeFromPref).TotalMinutes;
	}

	public static int getSecPassed(string code)
	{
		if (DatePassHelper.getDateStringFromPref(code).Equals(string.Empty))
		{
			return -1;
		}
		DateTime dateTimeFromPref = DatePassHelper.getDateTimeFromPref(code, DatePassHelper.DateFormat.ddMMyyyyhhmmss);
		DateTime now = DateTime.Now;
		return (int)(now - dateTimeFromPref).TotalSeconds;
	}

	public static void startCountDownSec(int start, int end = 0, Action<int> onUpdate = null, Action onFnish = null)
	{
		DatePassHelper datePassHelper = UnityEngine.Object.FindObjectOfType<DatePassHelper>();
		if (datePassHelper == null)
		{
			GameObject gameObject = new GameObject("DatePassHelper");
			gameObject.AddComponent<DatePassHelper>();
			datePassHelper = gameObject.GetComponent<DatePassHelper>();
		}
		datePassHelper.callCountDownSec(start, end, onUpdate, onFnish);
	}

	public void callCountDownSec(int start, int end = 0, Action<int> onUpdate = null, Action onFinish = null)
	{
		base.StartCoroutine(this.countDownSec(start, end, onUpdate, onFinish));
	}

	public IEnumerator countDownSec(int start, int end = 0, Action<int> onUpdate = null, Action onFinish = null)
	{
		int startTime = start;
		while (startTime != end)
		{
			yield return new WaitForSeconds(1f);
			startTime--;
			if (onUpdate != null)
			{
				onUpdate(startTime);
			}
		}
		if (onFinish != null)
		{
			onFinish();
		}
		yield break;
	}

	public static string splitSecondToString(int value)
	{
		int num = value / 60 / 60;
		int num2 = (value - num * 60 * 60) / 60;
		int num3 = value - num * 60 * 60 - num2 * 60;
		string text = num + string.Empty;
		string text2 = num2 + string.Empty;
		string text3 = num3 + string.Empty;
		if (num < 10)
		{
			text = "0" + text;
		}
		if (num2 < 10)
		{
			text2 = "0" + text2;
		}
		if (num3 < 10)
		{
			text3 = "0" + text3;
		}
		if (num > 0)
		{
			return string.Concat(new string[]
			{
				text,
				":",
				text2,
				":",
				text3
			});
		}
		return text2 + ":" + text3;
	}

	public static DatePassHelper ins;

	public static Dictionary<DatePassHelper.DateFormat, string> DateFormatString = new Dictionary<DatePassHelper.DateFormat, string>
	{
		{
			DatePassHelper.DateFormat.ddMMyyyy,
			"MM/dd/yyyy"
		},
		{
			DatePassHelper.DateFormat.ddMMyyyyhhmmss,
			"MM/dd/yyyy HH:mm:ss"
		},
		{
			DatePassHelper.DateFormat.hhmmss,
			"HH:mm:ss"
		}
	};

	public enum DateFormat
	{
		ddMMyyyy,
		ddMMyyyyhhmmss,
		hhmmss
	}
}
