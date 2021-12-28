using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Alura.ListaLeitura.App.Routing
{
    public class StandardRouting
    {
        public static Task DefaultHandler(HttpContext context)
        {
            var className = Convert.ToString(context.GetRouteValue("class"));
            var methodName = Convert.ToString(context.GetRouteValue("method"));

            var nameWithNamespace = $"Alura.ListaLeitura.App.Controller.{className}Controller";

            var tipo = Type.GetType(nameWithNamespace);
            var method = tipo.GetMethods().Where(m => m.Name == methodName).First();
            var requestDelegate = (RequestDelegate)Delegate.CreateDelegate(typeof(RequestDelegate), method);

            return requestDelegate.Invoke(context);
        }
    }
}