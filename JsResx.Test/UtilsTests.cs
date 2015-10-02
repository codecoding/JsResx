using System.Collections;
using System.Collections.Generic;
using JsResx.Example.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsResx.Test
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void GetConstantsReturnsListConstants()
        {
            var c = Utils.GetConstants(typeof(RequestTypeCodes));
            Assert.IsTrue(c.Count == 2);
            Assert.IsTrue(c[0].Name=="Normal");
            Assert.IsTrue(c[1].Name=="Additional");
            Assert.IsTrue(c[0].GetValue(c[0]).ToString()=="NOR");
            Assert.IsTrue(c[1].GetValue(c[1]).ToString()=="ADD");
        }

        [TestMethod]
        public void MainBuilderVarReturnsScript()
        {
            var res = Utils.MainVarBuilder("Data").ToString();
            Assert.IsTrue(res==@"var Data={};");
        }

        [TestMethod]
        public void FilterAppSettingsReturnsEntries()
        {
            var res = Utils.FilterAppSettings("jsConf_") as IList;
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Count == 1);
        }

        [TestMethod]
        public void FilterAppSettingsReturnsCorrectEntries()
        {
            var res = (IList)Utils.FilterAppSettings("jsConf_");
            var r0 = (KeyValuePair<string, string>)res[0];
            Assert.IsTrue(r0.Key == "jsConf_test1");
            Assert.IsTrue(r0.Value == "1");
        }

        [TestMethod]
        public void GetStringReturnsString()
        {
            var res = Utils.GetString("test2");
            Assert.IsFalse(string.IsNullOrEmpty(res));
            Assert.IsTrue(res == "2");
        }

        [TestMethod]
        public void GetStringReturnsDefaultValueIfNotPresent()
        {
            var res = Utils.GetString("test3","default");
            Assert.IsFalse(string.IsNullOrEmpty(res));
            Assert.IsTrue(res == "default");
        }
    }
}
