using LLS.Identity.Domain.Interfaces;

namespace LLS.Identity.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now() => DateTime.Now;
}