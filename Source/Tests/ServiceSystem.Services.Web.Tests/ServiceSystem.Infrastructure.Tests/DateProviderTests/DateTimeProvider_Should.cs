using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceSystem.Infrastructure.DateProvider;

namespace ServiceSystem.UnitTests.ServiceSystem.Infrastructure.Tests.DateProviderTests
{
    [TestClass]
    public class DateTimeProvider_Should
    {
        [TestMethod]
        public void ReturnCurrentUtcTime()
        {
            // Arrange & Act & Assert
            Assert.IsTrue(DateTime.UtcNow - DateTimeProvider.Current.UtcNow < new TimeSpan(0, 0, 1));
        }
    }
}
