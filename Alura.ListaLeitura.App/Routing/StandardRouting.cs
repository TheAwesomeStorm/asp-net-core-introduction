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
            var className = Convert.ToString(context.GetRouteValue("classe"));
            var methodName = Convert.ToString(context.GetRouteValue("metodo"));

            var nameWithNamespace = $"Alura.ListaLeitura.App.Controller.{className}Controller";
            
            var method = Type.GetType(nameWithNamespace).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            var requestDelegate = (RequestDelegate)Delegate.CreateDelegate(typeof(RequestDelegate), method);

            return requestDelegate.Invoke(context);
        }
    }
}