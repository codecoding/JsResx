#JsResx

This library will help you to expose some of your .NET objects into your JavaScript code.

##Resources

You can expose any .resx file you have on your server into your JavaScript.

Besides that, you can also set the default language, bringing the same experience you have on your server to the client. If a translation is missing it will fall back to the default language.

##AppSettings

You can expose several AppSettings keys if you need to. It's based in a prefix convention so only the keys that you choose will be exposed.

##Objects

You can expose constants to your JavaScript. This is very useful if you have codes in your server that you also want to use in your client.

On the other hand, you can also serialize and expose a .NET plain object.

---

Please, check the example project for the specifics. Check the JsResxController in order to see how it works.
