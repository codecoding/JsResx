#JsResx [![Build status](https://ci.appveyor.com/api/projects/status/6r8dt1529l8dpr7u?svg=true)](https://ci.appveyor.com/project/codecoding/jsresx) <a href="http://jsresx.scm.azurewebsites.net"><img src="http://azuredeploy.net/deploybutton.png" width="100"   /></a>



Bring your Resources and .NET objects to your JavaScript code.

##Resources

You can expose any .resx file you have on your server into your JavaScript.

Besides that, you can also set the default language, bringing the same experience you have on your server to the client. If a translation is missing it will fall back to the default language.

Example:

``` csharp

public ActionResult GetResources()
{
    var lm = new LocalizationManager();
    var rm = ClientMessages.ResourceManager;

    //using the thread culture to get the resx
    var data = lm.GetAll(rm); 
    var tm = new JsTranslationsScriptManager
    {
        //if set, in case any translation is missing will look for it in English.
        DefaultLanguageEntries = lm.GetAll(rm, new CultureInfo("en")), 
        VariableName = "MyTranslations" 
    };

    //generate the script
    var script = tm.GetScript(data);
    return JavaScript(script);
}

/* RETURN VALUE   ************************************************************************************

var MyTranslations = {  'Accept': 'Aceptar',  'Action': 'Accion',  'Add': 'Adicional',  'Yes': 'Si',
'_Date': 'Fecha',  'OnlyEnglish': 'OnlyEnglish' };

- The default variable name is Translations but we have customized it.

*****************************************************************************************************/

```

##AppSettings

You can expose several **AppSettings keys** if you need to. It's based in a prefix convention so only the keys that you choose will be exposed. **Note the jsConf_ prefix**

``` xml

<!-- Max contact details allowed -->
<add key="jsConf_maxContactItems" value="50" />
<!-- Max addresses allowed -->
<add key="jsConf_maxAddressItems" value="50" />

```

``` csharp

public JavaScriptResult GetConfigurationCustom()
{
    //1. we set the prefix of the AppSettings that we're going to export.
    const string JS_PREFIX = "jsConf_";
    //2. we use our utility method in order to retrieve the key/value pairs.
    var data = Utils.FilterAppSettings(JS_PREFIX);
    //3. we instantiate our JsConfigurationScriptManager
    var jsm = new JsConfigurationScriptManager { Prefix = JS_PREFIX, VariableName = "MyVar", ExposeRelativePath = true};
    //4. we create the script
    var script = jsm.GetScript(data);
    //5. we expose the script
    return JavaScript(script);
}

/* RETURN VALUE   ****************************************************************************

var Myvar = {  'maxContactItems': 50,  'maxAddressItems': 50,  'relPath':'/' };

- The variable name has been defined by us.
- If you forget to inform the prefix when instantiating the JsConfigurationScriptManager, 
  the prefix will appear
    in the property names.
- relPath exposes the relative path of the app.

*********************************************************************************************/

```

##Objects

You can expose **constants** to your JavaScript. This is very useful if you have codes in your server that you also want to use in your client.

``` csharp

//structure that we're going to expose
public struct RequestTypeCodes
{
    public const string Normal = "NOR";
    public const string Additional = "ADD";
}

//our exposing action method
public ActionResult GetConstants()
{ 
    return JavaScript(JsObjectSerializer.GetScript<RequestTypeCodes>("Data")); 
} 

/* RETURN VALUE   ***************************************************
            
var Data= { "Normal": "NOR",  "Additional": "ADD" };

- The variable name has been defined by us.
********************************************************************/
```
    
On the other hand, you can also serialize and expose **any .NET plain object**.

``` csharp

public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public ActionResult GetObject()
{
    var user = new User {Age=38,Name = "Rob"};
    var builder = new StringBuilder();
    builder.Append(JsObjectSerializer.GetScript(user, "User"));
    return JavaScript(builder.ToString());
}

/* RETURN VALUE   ****************************************************************
    
var User={"Name":"Rob","Age":38};  

- The variable name has been defined by us.
- Note that we can also use this kind of serialization as the aggregate example
**********************************************************************************/

```

Or even expose groups of Constants and Objects at the same time:

``` csharp
//structs with constants
public struct RequestTypeCodes
{
    public const string Normal = "NOR";
    public const string Additional = "ADD";
}

public struct BasicCodes
{
    public const string Large = "L";
    public const string Medium = "M";
    public const string Small = "S";
}

//action method
public ActionResult GetConstantsAggregate()
{
    const string mainvar = "Data";
    var builder = Utils.MainVarBuilder(mainvar);
    builder.Append(JsObjectSerializer.GetScript<RequestTypeCodes>(mainvar, "requestTypes"));
    builder.Append(JsObjectSerializer.GetScript<BasicCodes>(mainvar, "basicCodes"));
    return JavaScript(builder.ToString());
}

/* RETURN VALUE   ***********************************************************
    
    var Data={}; 
    Data.requestTypes= {  "Normal": "NOR",  "Additional": "ADD" }; 
    Data.basicCodes= {  "Large": "L",  "Medium": "M",  "Small": "S" }; 

    - The variable name has been defined by us.
    *************************************************************************/

```

## Do you want to see it working?

Please, check the [example project](http://jsresx.scm.azurewebsites.net) out for the specifics. Check the JsResxController out in order to see how it works. 
