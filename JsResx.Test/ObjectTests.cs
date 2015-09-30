using JsResx.Example.Resources;
using JsResx.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsResx.Test
{
    [TestClass]
    public class ObjectTests
    {

        [TestMethod]
        public void SerializerGetsInstantiated()
        {
            Assert.IsTrue(JsObjectSerializer.Serializer !=null);
        }

        [TestMethod]
        public void SerializeObjectWithNoSecondaryVar()
        {
            var user = new User { Age = 38, Name = "Rob" };
            var result = JsObjectSerializer.GetScript(user, "User");
            Assert.IsTrue(result.StartsWith(@"var User"));
            Assert.IsTrue(result.Contains("{\"Name\":\"Rob\",\"Age\":38};"));
        }

        [TestMethod]
        public void SerializeObjectWithSecondaryVar()
        {
            var user = new User { Age = 38, Name = "Rob" };
            var result = JsObjectSerializer.GetScript(user, "Data", "User");
            Assert.IsTrue(result.StartsWith(@"Data.User={"));
            Assert.IsTrue(result.Contains("{\"Name\":\"Rob\",\"Age\":38};"));
        }

        [TestMethod]
        public void ConstantsGetSerialized()
        {
            var result =  JsObjectSerializer.GetScript<RequestTypeCodes>("Data");
            Assert.IsTrue(result.StartsWith(@"var Data="));
            Assert.IsTrue(result.Contains(@"{ ""Normal"":""NOR"", ""Additional"":""ADD"" };"));
        }

        [TestMethod]
        public void ConstantsGetSerializedWhenAggregated()
        {
            const string mainvar = "Data";
            var builder = Utils.MainVarBuilder(mainvar);
            builder.Append(JsObjectSerializer.GetScript<RequestTypeCodes>(mainvar, "requestTypes"));
            builder.Append(JsObjectSerializer.GetScript<BasicCodes>(mainvar, "basicCodes"));
            var result = builder.ToString();
            Assert.IsTrue(result.StartsWith(@"var Data={};"));
            Assert.IsTrue(result.Contains(@"Data.requestTypes="));
            Assert.IsTrue(result.Contains(@"{ ""Normal"":""NOR"", ""Additional"":""ADD"" };"));
            Assert.IsTrue(result.Contains(@"Data.basicCodes="));
            Assert.IsTrue(result.Contains(@"{ ""Large"":""L"", ""Medium"":""M"", ""Small"":""S"" };"));
        }

        [TestMethod]
        public void FailingTest()
        {
            Assert.IsTrue(false);
        }
    }
}
