using System;

namespace ServiceSystem.Infrastructure.DateProvider
{
    public class DefaultDateTimeProvider : DateTimeProvider
    {
        public override DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}
