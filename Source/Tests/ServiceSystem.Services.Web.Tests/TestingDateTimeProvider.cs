using System;
using ServiceSystem.Infrastructure.DateProvider;

namespace ServiceSystem.UnitTests
{
    public class TestingDateTimeProvider : DateTimeProvider
    {
        public override DateTime UtcNow
        {
            get
            {
                return new DateTime(1990, 12, 12);
            }
        }
    }
}
