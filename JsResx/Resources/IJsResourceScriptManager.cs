using System.Collections.Generic;

namespace JsResx.Resources
{
    public interface IJsResourceScriptManager
    {
        string GetScript(IEnumerable<KeyValuePair<string, string>> dictionary, JsNamespaceCollection namespaces = null );
        bool VarIsMainVar { get; set; }
    }
}
