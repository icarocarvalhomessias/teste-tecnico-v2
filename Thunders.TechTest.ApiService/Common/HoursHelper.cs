namespace Thunders.TechTest.ApiService.Common;

public static class HoursHelper
{
    public static string FormatHour(int hour)
    {
        if (hour == 0)
            return "12AM";
        else if (hour < 12)
            return $"{hour}AM";
        else if (hour == 12)
            return "12PM";
        else
            return $"{hour - 12}PM";
    }
}
