using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;

namespace Alura.ListaLeitura.App
{
    public class StartUp
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(Roteamento);
        }

        public Task Roteamento(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var caminhosAtendidos = new Dictionary<string, string>
            {
                { "/livros/paraler", repo.ParaLer.ToString() },
                { "/livros/lendo", repo.Lendo.ToString() },
                { "/livros/lidos", repo.Lidos.ToString() }
            };
            
            if (caminhosAtendidos.ContainsKey(context.Request.Path))
            {
                return context.Response.WriteAsync(caminhosAtendidos[context.Request.Path]);
            }
            
            return context.Response.WriteAsync("Caminho inexistente");
        }
        
        public Task LivrosParaLer(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(repo.ParaLer.ToString());
        }
    }
}