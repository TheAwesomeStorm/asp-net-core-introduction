using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using static Alura.ListaLeitura.App.HTML.HtmlLoader;

namespace Alura.ListaLeitura.App.Delegates
{
    public static class RouteDelegates
    {
        private static LivroRepositorioCsv _repositorioCsv;

        static RouteDelegates()
        {
            _repositorioCsv = new LivroRepositorioCsv();
        }

        public static Task ProcessarFormulario(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = context.Request.Form["titulo"].First(),
                Autor = context.Request.Form["autor"].First(),
            };
            _repositorioCsv.Incluir(livro);
            return context.Response.WriteAsync("Livro adicionado a partir do formulário");
        }

        public static Task ExibeFormulario(HttpContext context)
        {
            var html = CarregarArquivoHtml("formulario");
            return context.Response.WriteAsync(html);
        }

        public static Task ExibeDetalhes(HttpContext context)
        {
            var id = Convert.ToInt32(context.GetRouteValue("id"));
            var livro = _repositorioCsv.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
        }

        public static Task NovoLivroParaLer(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = Convert.ToString(context.GetRouteValue("nome")),
                Autor = Convert.ToString(context.GetRouteValue("autor"))
            };
            _repositorioCsv.Incluir(livro);
            return context.Response.WriteAsync("Novo livro adicionado com sucesso");
        }

        public static Task LivrosParaLer(HttpContext context)
        {
            var html = CarregarArquivoHtml("para-ler");

            foreach (var livro in _repositorioCsv.ParaLer.Livros)
            {
                html = html
                    .Replace("#NOVO-ITEM#", $"<li>{livro.Titulo} - {livro.Autor}</li>#NOVO-ITEM#");
            }

            html = html.Replace("#NOVO-ITEM#", "");
            
            return context.Response.WriteAsync(html);
        }

        public static Task LivrosLendo(HttpContext context)
        {
            return context.Response.WriteAsync(_repositorioCsv.Lendo.ToString());
        }

        public static Task LivrosLidos(HttpContext context)
        {
            return context.Response.WriteAsync(_repositorioCsv.Lidos.ToString());
        }
    }
}