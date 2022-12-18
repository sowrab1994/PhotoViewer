using NUnit.Framework;
using ProductConfig;

namespace ConfigTest
{

    [TestFixture]
    internal class ConfigTest
    {
        [TestCase("TestMethod", new object[] {"TestParam1", "TestParam2"})]
        public void TestFunctionBuilder(string methodName, object[] obj)
        {
            var res = Config.BuildJavacriptFunction(methodName, obj);
            Assert.AreEqual(methodName + "('" + obj[0] + "', '" + obj[1] + "');", res);
        }
    }
}
