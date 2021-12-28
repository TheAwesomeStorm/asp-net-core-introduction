using System.Linq;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.App.Controller
{
    public class LivrosController : Microsoft.AspNetCore.Mvc.Controller
    {
        private static LivroRepositorioCsv _repositorioCsv;

        static LivrosController()
        {
            _repositorioCsv = new LivroRepositorioCsv();
        }

        public string Detalhes(int id)
        {
            var livro = _repositorioCsv.Todos.First(l => l.Id == id);
            return livro.Detalhes();
        }

        public IActionResult ParaLer()
        {
            ViewBag.Livros = _repositorioCsv.ParaLer.Livros;
            return View("lista");
        }

        public IActionResult Lendo()
        {
            ViewBag.Livros = _repositorioCsv.Lendo.Livros;
            return View("lista");
        }

        public IActionResult Lidos()
        {
            ViewBag.Livros = _repositorioCsv.Lidos.Livros;
            return View("lista");
        }
    }
}