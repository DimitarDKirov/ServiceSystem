using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceSystem.Infrastructure.PublicCodeProvider;

namespace ServiceSystem.UnitTests.ServiceSystem.Infrastructure.Tests.PublicCodeProviderTests
{
    [TestClass]
    public class Encode_Should
    {
        [TestMethod]
        public void GenerateCorrectCodeWhenIdAndNameAreLongEnough()
        {
            //Arrange
            IPublicCodeProvider codec = new PublicCodeProvider();
            var id = 1234;
            var name = "AbCd";

            //Act
            var codedResult = codec.Encode(id, name);

            //Assert
            Assert.AreEqual("1234AbC", codedResult);
        }

        [TestMethod]
        public void GenerateCodeWithRandomSymbols_WhenNameIsShort()
        {
            //Arramge
            IPublicCodeProvider codec = new PublicCodeProvider();
            var id = 1234;
            var name = "a";

            //Act
            var codedResult = codec.Encode(id, name);

            //Assert
            Assert.AreEqual(7, codedResult.Length);
            Assert.AreEqual("1234a", codedResult.Substring(0, 5));
        }
    }
}
