using System;

public class CustomInt
{
	public static string toString(int value)
	{
		if (value >= 1000000)
		{
			float num = (float)value / 1000000f;
			if (value % 1000000 == 0)
			{
				return num + "m";
			}
			return num.ToString("0") + "m";
		}
		else
		{
			if (value < 10000)
			{
				return value + string.Empty;
			}
			float num2 = (float)value / 1000f;
			if (value % 1000 == 0)
			{
				return num2 + "k";
			}
			return num2.ToString("0") + "k";
		}
	}
}
