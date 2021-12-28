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
    public static class CadastroController
    {
        private static LivroRepositorioCsv _repositorioCsv;

        static CadastroController()
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
    }
}