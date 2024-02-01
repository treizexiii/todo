namespace WebApp.Tools;

public static class DateTimeExtension
{
    public static string ToFrenchDate(this DateTime dateTimeOffset)
    {
        return dateTimeOffset.ToString("dd/MM/yyyy");
    }
}