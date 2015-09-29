using System.Globalization;
using System.Web.Mvc;
using JsResx.Example.Resources;
using JsResx.Resources;

namespace JsResx.Example.Controllers
{
    public partial class JsResxController
    {
        public ActionResult GetResources()
        {
            var lm = new LocalizationManager();
            var rm = ClientMessages.ResourceManager;

            //using the thread culture to get the resx
            var data = lm.GetAll(rm); 
            var tm = new JsTranslationsScriptManager
            {
                DefaultLanguageEntries = lm.GetAll(rm, new CultureInfo("en")), //if set, in case any translation is missing will look for it in English.
                VariableName = "MyTranslations" //Translations is the default variable name.
            };

            //generate the script
            var script = tm.GetScript(data);
            return JavaScript(script);

            /* RETURN VALUE   ****************************************************************************************************

           var MyTranslations = {  'Accept': 'Aceptar',  'Action': 'Acción',  'Add': 'Adicional',  'Yes': 'Si',  '_Date': 'Fecha',  'OnlyEnglish': 'OnlyEnglish' };

           - The default variable name is Translations but we have customized it.

           *********************************************************************************************************************/
        }
    }
}