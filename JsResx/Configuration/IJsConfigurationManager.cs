using JsResx.Resources;

namespace JsResx.Configuration
{
    public interface IJsConfigurationScriptManager : IJsResourceScriptManager
    {
        string Prefix { get; set; }

        string VariableName { get; set; }
    }
}