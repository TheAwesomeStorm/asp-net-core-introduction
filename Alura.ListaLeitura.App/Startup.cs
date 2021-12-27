using System.Collections.Generic;
using System.Threading.Tasks;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        private readonly LivroRepositorioCSV _repositorioCsv;

        public Startup()
        {
            _repositorioCsv = new LivroRepositorioCSV();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(Roteamento);
        }

        private Task Roteamento(HttpContext context)
        {
            var caminhosAtendidos = new Dictionary<string, RequestDelegate>
            {
                { "/livros/ler", LivrosParaLer },
                { "/livros/lendo", LivrosLendo },
                { "/livros/lidos", LivrosLidos }
            };
            
            if (caminhosAtendidos.ContainsKey(context.Request.Path))
            {
                var metodo = caminhosAtendidos[context.Request.Path];
                return metodo.Invoke(context);
            }

            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("Caminho inexistente");
        }

        private Task LivrosParaLer(HttpContext context)
        {
            return context.Response.WriteAsync(_repositorioCsv.ParaLer.ToString());
        }

        private Task LivrosLendo(HttpContext context)
        {
            return context.Response.WriteAsync(_repositorioCsv.Lendo.ToString());
        }

        private Task LivrosLidos(HttpContext context)
        {
            return context.Response.WriteAsync(_repositorioCsv.Lidos.ToString());
        }
    }
}