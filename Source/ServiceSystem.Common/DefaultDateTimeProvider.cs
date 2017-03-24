using System;

namespace MvcTemplate.Common
{
    public class DefaultDateTimeProvider : DateTimeProvider
    {
        public override DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}
