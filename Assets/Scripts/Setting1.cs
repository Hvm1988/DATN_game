using System;

public static class Setting1
{
    public static event Action onChangeVolume;

    public static void RaiseChangeVolume()
    {
        onChangeVolume?.Invoke(); // chỉ Setting được phép Invoke
    }
}
