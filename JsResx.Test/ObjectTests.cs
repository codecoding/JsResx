using System.Text;
using JsResx.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsResx.Test
{
    [TestClass]
    public class ObjectTests
    {
        class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        [TestMethod]
        public void SerializerGetsInstantiated()
        {
            Assert.IsTrue(JsObjectSerializer.Serializer !=null);
        }

        [TestMethod]
        public void SerializeObjectWithNoSecondaryVar()
        {
            var user = new User { Age = 38, Name = "Rob" };
            var builder = new StringBuilder();
            builder.Append(JsObjectSerializer.GetScript(user, "User"));
            var result = builder.ToString();
            Assert.IsTrue(result.StartsWith(@"var User"));
            Assert.IsTrue(result.Contains("{\"Name\":\"Rob\",\"Age\":38};"));
        }

        [TestMethod]
        public void SerializeObjectWithSecondaryVar()
        {
            const string mainvar = "Data";
            var user = new User { Age = 38, Name = "Rob" };
            var builder = new StringBuilder();
            builder.Append(JsObjectSerializer.GetScript(user, mainvar, "User"));
            var result = builder.ToString();
            Assert.IsTrue(result.StartsWith(@"Data.User={"));
            Assert.IsTrue(result.Contains("{\"Name\":\"Rob\",\"Age\":38};"));
        }
    }
}
