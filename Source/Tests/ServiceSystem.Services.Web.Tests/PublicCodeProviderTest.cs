namespace MvcTemplate.Services.Web.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ServiceSystem.Services.Web;

    [TestClass]
    public class PublicCodeProviderTest
    {
        [TestMethod]
        public void PublicCodeCodingTest()
        {
            IPublicCodeProvider codec = new PublicCodeProvider();
            var id = 1234;
            var name = "AbCd";
            var codedResult = codec.Encode(id, name);
            Assert.AreEqual("1234AbC", codedResult);
        }

        [TestMethod]
        public void PublicCodeDecodingShouldWork()
        {
            IPublicCodeProvider codec = new PublicCodeProvider();
            var id = 1234;
            var name = "AbCd";
            var codedResult = codec.Encode(id, name);
            var decodedResult = codec.Decode(codedResult);
            Assert.AreEqual(id, decodedResult);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PublicCoderShouldThrowIfMoreThan3LettersUsed()
        {
            IPublicCodeProvider codec = new PublicCodeProvider();
            var wrongInput = "123dcfv";
            var result = codec.Decode(wrongInput);
        }

        [TestMethod]
        public void PublicCoderShouldCodeWithShortInput()
        {
            IPublicCodeProvider codec = new PublicCodeProvider();
            var id = 1234;
            var name = "a";
            var codedResult = codec.Encode(id, name);
            Assert.AreEqual(7, codedResult.Length);
        }
    }
}
