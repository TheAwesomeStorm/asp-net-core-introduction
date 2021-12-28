using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        private LivroRepositorioCsv _repositorioCsv;

        public Startup()
        {
            _repositorioCsv = new LivroRepositorioCsv();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("livros/paraler", LivrosParaLer);
            builder.MapRoute("livros/lendo", LivrosLendo);
            builder.MapRoute("livros/lidos", LivrosLidos);
            builder.MapRoute("cadastro/livro/{nome}/{autor}", NovoLivroParaLer);
            builder.MapRoute("livros/detalhes/{id:int}", ExibeDetalhes);
            builder.MapRoute("cadastro/novolivro", ExibeFormulario);
            builder.MapRoute("cadastro/incluir", ProcessarFormulario);

            var routes = builder.Build();
            
            // app.Run(Roteamento);
            app.UseRouter(routes);
        }

        private Task ProcessarFormulario(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = context.Request.Form["titulo"].First(),
                Autor = context.Request.Form["autor"].First(),
            };
            _repositorioCsv.Incluir(livro);
            return context.Response.WriteAsync("Livro adicionado a partir do formulário");
        }

        private Task ExibeFormulario(HttpContext context)
        {
            var html = CarregarArquivoHTML("formulario");
            return context.Response.WriteAsync(html);
        }

        private string CarregarArquivoHTML(string fileName)
        {
            var filePath = $"HTML/{fileName}.html";
            using (var arquivo = File.OpenText(filePath))
            {
                return arquivo.ReadToEnd();
            }
        }

        private Task ExibeDetalhes(HttpContext context)
        {
            var id = Convert.ToInt32(context.GetRouteValue("id"));
            var livro = _repositorioCsv.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
        }

        private Task NovoLivroParaLer(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = Convert.ToString(context.GetRouteValue("nome")),
                Autor = Convert.ToString(context.GetRouteValue("autor"))
            };
            _repositorioCsv.Incluir(livro);
            return context.Response.WriteAsync("Novo livro adicionado com sucesso");
        }

        private Task LivrosParaLer(HttpContext context)
        {
            var html = CarregarArquivoHTML("para-ler");

            foreach (var livro in _repositorioCsv.ParaLer.Livros)
            {
                html = html
                    .Replace("#NOVO-ITEM#", $"<li>{livro.Titulo} - {livro.Autor}</li>#NOVO-ITEM#");
            }

            html = html.Replace("#NOVO-ITEM#", "");
            
            return context.Response.WriteAsync(html);
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