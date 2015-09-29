using System.Collections.Generic;

namespace JsResx.Resources
{
    public interface IJsTranslationsScriptManager : IJsResourceScriptManager
    {
        IEnumerable<KeyValuePair<string, string>> DefaultLanguageEntries { get; set; }
    }
}
