using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceSystem.Infrastructure.DateProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSystem.UnitTests.ServiceSystem.Infrastructure.Tests.DateProviderTests
{
    [TestClass]
    public class DateTimeProvider_Should
    {
        [TestMethod]
        public void ReturnCurrentUtcTime()
        {
            //Arrange & Act & Assert
            Assert.AreEqual(DateTime.UtcNow, DateTimeProvider.Current.UtcNow);
        }
    }
}
