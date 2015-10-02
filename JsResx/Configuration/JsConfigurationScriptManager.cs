using System.Web;
using JsResx.Resources;
using Microsoft.VisualBasic;

namespace JsResx.Configuration
{
    public class JsConfigurationScriptManager : JsResourceScriptBase, IJsConfigurationScriptManager
    {
        public string Prefix { get; set; } = "jsConf_";
        public bool ExposeRelativePath { get; set; } = false;
        public override string VariableName { get; set; } = "ServerConfigurations";

        /// <summary>
        /// Creates the item.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="entry">The entry.</param>
        protected override void CreateItem(System.Text.StringBuilder builder, System.Collections.Generic.KeyValuePair<string, string> entry)
        {
            var format = @"""{0}"":""{1}"",";
            if ((Information.IsNumeric(entry.Value)))
            {
                format = @"""{0}"":{1},";
            }
            builder.AppendFormat(format, entry.Key.Replace(Prefix, ""), SanitizeString(entry.Value));
        }

        /// <summary>
        /// Adds items after create items.
        /// </summary>
        /// <param name="builder">The builder.</param>

        protected override void AddItemsAfterCreateItems(System.Text.StringBuilder builder)
        {
            if (!ExposeRelativePath) return;
            var relPath = VirtualPathUtility.ToAbsolute("~");
            relPath = relPath == "/" ? relPath : relPath + "/";
            builder.AppendFormat(" 'relPath':'{0}', ", relPath);
            base.AddItemsAfterCreateItems(builder);

        }

    }
}
