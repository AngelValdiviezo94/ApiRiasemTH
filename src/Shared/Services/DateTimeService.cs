using EnrolApp.Application.Common.Interfaces;


namespace EnrolApp.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
