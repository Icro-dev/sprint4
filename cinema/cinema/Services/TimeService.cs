namespace cinema.Services;

public class TimeService : ITimeService
{
    public string GetNameForDayOfWeek(int weekday)
    {
        switch (weekday)
        {
            case 0: return "Zondag";
            case 1: return "Maandag";
            case 2: return "Dinsdag";
            case 3: return "Woensdag";
            case 4: return "Donderdag";
            case 5: return "Vrijdag";
            case 6: return "Zaterdag";
            default: return "NODAY";
        }
    }
}