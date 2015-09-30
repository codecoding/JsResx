using System.Text;
using System.Web.Mvc;
using JsResx.Example.Resources;
using JsResx.Objects;

namespace JsResx.Example.Controllers
{
    public partial class JsResxController
    {
        public ActionResult GetConstants()
        {
            return JavaScript(JsObjectSerializer.GetScript<RequestTypeCodes>("Data"));

            /* RETURN VALUE   ****************************************************************************************************
            
            var Data= { "Normal": "NOR",  "Additional": "ADD" };

            - The variable name has been defined by us.
            *********************************************************************************************************************/
        }

        public ActionResult GetConstantsAggregate()
        {
            const string mainvar = "Data";
            var builder = Utils.MainVarBuilder(mainvar);
            builder.Append(JsObjectSerializer.GetScript<RequestTypeCodes>(mainvar, "requestTypes"));
            builder.Append(JsObjectSerializer.GetScript<BasicCodes>(mainvar, "basicCodes"));
            return JavaScript(builder.ToString());

            /* RETURN VALUE   ****************************************************************************************************
            
            var Data={}; Data.requestTypes= {  "Normal": "NOR",  "Additional": "ADD" }; Data.basicCodes= {  "Large": "L",  "Medium": "M",  "Small": "S" }; 

            - The variable name has been defined by us.
            *********************************************************************************************************************/
        }

        public ActionResult GetObject()
        {
            var user = new User {Age=38,Name = "Rob"};
            var builder = new StringBuilder();
            builder.Append(JsObjectSerializer.GetScript(user, "User"));
            return JavaScript(builder.ToString());

            /* RETURN VALUE   ****************************************************************************************************
            
            var User={"Name":"Rob","Age":38};  

            - The variable name has been defined by us.
            - Note that we can also use this kind of serialization as the aggregate example
            *********************************************************************************************************************/
        }

    }
}