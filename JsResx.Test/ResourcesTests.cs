using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading;
using JsResx.Example.Resources;
using JsResx.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsResx.Test
{
    [TestClass]
    public class ResourcesTests
    {
        private IEnumerable<KeyValuePair<string, string>> _data;
        private LocalizationManager _lm;
        private ResourceManager _rm;

        [TestInitialize]
        public void Init()
        {
            _lm = new LocalizationManager();
            _rm = ClientMessages.ResourceManager;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ES");
            _data = _lm.GetAll(_rm);
        }

        [TestMethod]
        public void GetScriptReturnsEmptyStringIfDictionaryIsNull()
        {
            var tm = new JsTranslationsScriptManager
            {
                DefaultLanguageEntries = _lm.GetAll(_rm, new CultureInfo("en")),
                VariableName = "MyTranslations"
            };

            var script = tm.GetScript(null);
            Assert.IsTrue(script == string.Empty);
        }

        [TestMethod]
        public void GetResourcesReturnsOk()
        {
            var tm = new JsTranslationsScriptManager
            {
                DefaultLanguageEntries = _lm.GetAll(_rm, new CultureInfo("en")),
                VariableName = "MyTranslations" 
            };

            var script = tm.GetScript(_data);
            const string COMPARE = @"var MyTranslations = {""Accept"":""Aceptar"",""Action"":""Acción"",""Add"":""Adicional"",""Yes"":""Si"",""_Date"":""Fecha"",""OnlyEnglish"":""OnlyEnglish""};";
            Assert.IsTrue(script == COMPARE);
        }

        [TestMethod]
        public void GetResourcesUsesTranslationsAsDefaultVar()
        {
            var tm = new JsTranslationsScriptManager
            {
                DefaultLanguageEntries = _lm.GetAll(_rm, new CultureInfo("en"))
            };
            var script = tm.GetScript(_data);
            const string COMPARE = @"var Translations = {""Accept"":""Aceptar"",""Action"":""Acción"",""Add"":""Adicional"",""Yes"":""Si"",""_Date"":""Fecha"",""OnlyEnglish"":""OnlyEnglish""};";
            Assert.IsTrue(script == COMPARE);
        }

        [TestMethod]
        public void GetResourcesWithoutDefaultCultureDoesntReturnsDefaultValues()
        {
            var tm = new JsTranslationsScriptManager
            {
                DefaultLanguageEntries = _lm.GetAll(_rm)
            };

            //generate the script
            var script = tm.GetScript(_data);
            const string COMPARE = @"var Translations = {""Accept"":""Aceptar"",""Action"":""Acción"",""Add"":""Adicional"",""Yes"":""Si"",""_Date"":""Fecha""};";
            Assert.IsTrue(script == COMPARE);
        }
    }
}
