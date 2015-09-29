using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using JsResx.Configuration;

namespace JsResx.Example.Controllers
{
    public partial class JsResxController
    {

        [OutputCache(Location = OutputCacheLocation.ServerAndClient, Duration = 31536000, Order = 0, VaryByParam = "v,l")]
        public JavaScriptResult GetConfiguration()
        {
            //1. we set the prefix of the AppSettings that we're going to export.
            var jsPrefix = Utils.GetString("JsConfPrefix");
            //2. we use our utility method in order to retrieve the key/value pairs.
            var data = Utils.FilterAppSettings(jsPrefix);
            //3. we instantiate our JsConfigurationScriptManager (typically we'll use DI and maybe use it through the controller)
            var jsm = new JsConfigurationScriptManager();
            //4. we create the script
            var script = jsm.GetScript(data);
            //5. we expose the script
            return JavaScript(script);

            /* RETURN VALUE   ****************************************************************************************************
            
            var ServerConfigurations = {  'maxContactItems': 50,  'maxAddressItems': 50 };

            - The default variable name is ServerConfigurations.

            *********************************************************************************************************************/
        }

        [OutputCache(Location = OutputCacheLocation.ServerAndClient, Duration = 31536000, Order = 0, VaryByParam = "v,l")]
        public JavaScriptResult GetConfigurationCustom()
        {
            //1. we set the prefix of the AppSettings that we're going to export.
            const string JS_PREFIX = "jsConf_";
            //2. we use our utility method in order to retrieve the key/value pairs.
            var data = Utils.FilterAppSettings(JS_PREFIX);
            //3. we instantiate our JsConfigurationScriptManager (typically we'll use DI and maybe use it through the controller)
            var jsm = new JsConfigurationScriptManager { Prefix = JS_PREFIX, VariableName = "MyVar", ExposeRelativePath = true};
            //4. we create the script
            var script = jsm.GetScript(data);
            //5. we expose the script
            return JavaScript(script);

            /* RETURN VALUE   ****************************************************************************************************
            
            var Myvar = {  'maxContactItems': 50,  'maxAddressItems': 50,  'relPath':'/' };

            - The variable name has been defined by us.
            - If you forget to inform the prefix when instantiating the JsConfigurationScriptManager, the prefix will appear
              in the property names.
            - relPath exposes the relative path of the app.

            *********************************************************************************************************************/
        }

        [OutputCache(Location = OutputCacheLocation.ServerAndClient, Duration = 31536000, Order = 0, VaryByParam = "v,l")]
        public JavaScriptResult GetConfigurationAggregate()
        {
            var builder = Utils.MainVarBuilder("Data"); //main variable of the script.
            AddDataCodeFromConfig(builder, "CODE_Visitor_", "Data.VisitorCodes");
            AddDataCodeFromConfig(builder, "CODE_Order_", "Data.OrderCodes");
            AddDataCodeFromConfig(builder, "CODE_Base_", "Data.BaseCodes");
            return JavaScript(builder.ToString());

            /* RETURN VALUE   ****************************************************************************************************
            
            var Data={}; Data.VisitorCodes = {  '1': 120001,  '2': 120002,  '3': 120003 }; Data.OrderCodes = {  '1': 113001,  '2': 113002,  '3': 113003 }; Data.BaseCodes = {  '1': 108001,  '2': 108002,  '3': 108003 };

            - The variable name has been defined by us and set as a secondary variable (VarIsMainVar = false).
            *********************************************************************************************************************/
        }

        private static void AddDataCodeFromConfig(StringBuilder builder, string prefix, string varName)
        {
            var jsm = new JsConfigurationScriptManager {
                Prefix = prefix,
                VariableName = varName,
                VarIsMainVar = false
            };
            var codes = Utils.FilterAppSettings(jsm.Prefix);
            builder.Append(jsm.GetScript(codes));
        }
    }
}