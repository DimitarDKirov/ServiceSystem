using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceSystem.Infrastructure.PublicCodeProvider;

namespace ServiceSystem.UnitTests.ServiceSystem.Infrastructure.Tests.PublicCodeProviderTests
{
    [TestClass]
    public class Decode_Should
    {
        [TestMethod]
        public void ReturnOriginalValuesAfterEncodeAndDecode()
        {
            //Arrange
            IPublicCodeProvider codec = new PublicCodeProvider();
            var id = 1234;
            var name = "AbCd";

            //Act
            var codedResult = codec.Encode(id, name);
            var decodedResult = codec.Decode(codedResult);

            //Assert
            Assert.AreEqual(id, decodedResult);
        }

        [TestMethod]
        public void Throw_WhenInputStringCOntainMoreThanThreeLetters()
        {
            //Arrange
            IPublicCodeProvider codec = new PublicCodeProvider();
            var wrongInput = "123dcfv";

            //Act & Assert
            Assert.ThrowsException<FormatException>(() => codec.Decode(wrongInput));
        }
    }
}
