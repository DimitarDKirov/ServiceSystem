using System;

namespace ServiceSystem.Infrastructure.DateProvider
{
    public abstract class DateTimeProvider
    {
        private static DateTimeProvider current;

        static DateTimeProvider()
        {
            DateTimeProvider.current =
            new DefaultDateTimeProvider();
        }

        public static DateTimeProvider Current
        {
            get { return DateTimeProvider.current; }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                DateTimeProvider.current = value;
            }
        }

        public abstract DateTime UtcNow { get; }

        public static void ResetToDefault()
        {
            DateTimeProvider.current = new DefaultDateTimeProvider();
        }
    }
}
