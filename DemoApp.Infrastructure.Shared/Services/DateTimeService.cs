using DemoApp.Application.Interfaces;

using System;

namespace DemoApp.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}