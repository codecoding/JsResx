using System.Linq;
using System.Text;
using JsResx.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsResx.Test
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void GetConfigurationReturnsOk()
        {
            var data = Utils.FilterAppSettings("jsConf_");
            var jsm = new JsConfigurationScriptManager();
            var script = jsm.GetScript(data);
            Assert.IsTrue(script.StartsWith("var ServerConfigurations = "));
            Assert.IsTrue(script.Contains(@"{""test1"":1};"));
        }

        [TestMethod]
        public void GetConfigurationReturnsStringsProperly()
        {
            var data = Utils.FilterAppSettings("conf_");
            var jsm = new JsConfigurationScriptManager {Prefix = "conf_"};
            var script = jsm.GetScript(data);
            Assert.IsTrue(script.StartsWith("var ServerConfigurations = "));
            Assert.IsTrue(script.Contains(@"{""test3"":""hello""};"));
        }

        [TestMethod]
        public void GetConfigurationFailsIfnotPrefixPresent()
        {
            var data = Utils.FilterAppSettings("conf_");
            var jsm = new JsConfigurationScriptManager();
            var script = jsm.GetScript(data);
            Assert.IsFalse(script.Contains(@"""test3"""));
        }

        [TestMethod]
        public void GetConfigurationUsesjsConf_PrefixAsDefault()
        {
            var data = Utils.FilterAppSettings("jsConf_");
            var jsm = new JsConfigurationScriptManager();
            var script = jsm.GetScript(data);
            Assert.IsTrue(script.Contains(@"{""test1"":1};"));
        }

        [TestMethod]
        public void GetConfigurationReturnsOnlyEntriesBeginningWithkEY()
        {
            var data = Utils.FilterAppSettings("conf_");
            Assert.IsTrue(data.Count()==1);
        }

        [TestMethod]
        public void GetConfigurationCustomReturnsCustonNames()
        {
            const string JS_PREFIX = "jsConf_";
            var data = Utils.FilterAppSettings(JS_PREFIX);
            var jsm = new JsConfigurationScriptManager { Prefix = JS_PREFIX, VariableName = "MyVar"};
            var script = jsm.GetScript(data);
            Assert.IsTrue(script.StartsWith("var MyVar = "));
            Assert.IsTrue(script.Contains(@"{""test1"":1};"));
        }

        [TestMethod]
        public void GetConfigurationAggregate()
        {
            var builder = Utils.MainVarBuilder("Data"); //main variable of the script.
            AddDataCodeFromConfig(builder, "CODE_Visitor_", "Data.VisitorCodes");
            AddDataCodeFromConfig(builder, "CODE_Order_", "Data.OrderCodes");
            AddDataCodeFromConfig(builder, "CODE_Base_", "Data.BaseCodes");
            var res = builder.ToString();
            var compare =
                @"var Data={};Data.VisitorCodes = {""1"":120001,""2"":120002,""3"":120003};Data.OrderCodes = {""1"":113001,""2"":113002,""3"":113003};Data.BaseCodes = {""1"":108001,""2"":108002,""3"":108003};";
            Assert.IsTrue(res == compare );
        }

        private static void AddDataCodeFromConfig(StringBuilder builder, string prefix, string varName)
        {
            var jsm = new JsConfigurationScriptManager
            {
                Prefix = prefix,
                VariableName = varName,
                VarIsMainVar = false
            };
            var codes = Utils.FilterAppSettings(jsm.Prefix);
            builder.Append(jsm.GetScript(codes));
            Assert.IsTrue(true);
        }
    }
}
