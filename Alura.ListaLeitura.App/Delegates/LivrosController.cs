﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Alura.ListaLeitura.App.Repositorio;
using static Alura.ListaLeitura.App.HTML.HtmlLoader;

namespace Alura.ListaLeitura.App.Delegates
{
    public static class LivrosController
    {
        private static LivroRepositorioCsv _repositorioCsv;

        static LivrosController()
        {
            _repositorioCsv = new LivroRepositorioCsv();
        }

        public static Task LivrosDetalhes(HttpContext context)
        {
            var id = Convert.ToInt32(context.GetRouteValue("id"));
            var livro = _repositorioCsv.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
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